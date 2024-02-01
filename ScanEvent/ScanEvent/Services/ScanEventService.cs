using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ScanEvent.Models;
using ScanEvent.ResponseModels;

namespace ScanEvent.Services
{
    public class ScanEventService
    {
        private const string ApiEndpoint = "http://localhost/v1/scans/scanevents";

        private readonly Dictionary<int, ScanEventModel> storedScanEvents;
        private readonly ILogger<ScanEventService> logger;

        public ScanEventService(ILogger<ScanEventService> logger)
        {
            this.logger = logger;
            storedScanEvents = [];
        }

        public async Task ProcessScanEvents(int fromEventId, int limit)
        {
            var scanEvents = await FetchScanEvents(fromEventId, limit);

            foreach (var scanEvent in scanEvents)
            {
                StoreScanEvent(scanEvent);
            }
        }

        private async Task<List<ScanEventModel>> FetchScanEvents(int fromEventId, int limit)
        {
            using var httpClient = new HttpClient();
            var apiUrl = $"{ApiEndpoint}?FromEventId={fromEventId}&Limit={limit}";

            try
            {
                var response = await httpClient.GetStringAsync(apiUrl);
                var scanEventApiResponse = JsonSerializer.Deserialize<ScanEventsResponseModel>(response);
                return scanEventApiResponse?.ScanEvents ?? [];
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching scan events from API: {ex.Message}");
                throw;
            }
        }

        public ScanEventModel GetLastEventForParcel(int parcelId)
        {
            if (storedScanEvents.TryGetValue(parcelId, out var lastScanEvent))
            {
                return lastScanEvent;
            }

            return null;
        }

        private void StoreScanEvent(ScanEventModel scanEvent)
        {
            if (storedScanEvents.ContainsKey(scanEvent.ParcelId))
            {
                var storedEvent = storedScanEvents[scanEvent.ParcelId] = scanEvent;

                if (scanEvent.CreatedDateTimeUtc > storedEvent.CreatedDateTimeUtc)
                {
                    storedScanEvents[scanEvent.ParcelId] = scanEvent;
                }
            }
            else
            {
                storedScanEvents.Add(scanEvent.ParcelId, scanEvent);
            }
        }
    }
}
