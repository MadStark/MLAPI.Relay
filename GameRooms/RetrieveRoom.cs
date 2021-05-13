using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos.Table;

namespace GameRooms
{
    public static class RetrieveRoom
    {
        [FunctionName("RetrieveRoom")]
        public static async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{gameId}/get-room-info/{roomId}")] HttpRequest req,
            string gameId, string roomId,
            [Table("rooms", Connection = "RoomsStorage")] CloudTable roomsTable,
            ILogger log)
        {
            TableResult result = await roomsTable.ExecuteAsync(TableOperation.Retrieve<RoomEntity>("room", roomId));

            RoomEntity room = result.Result as RoomEntity;
            if (room == null)
            {
                return new BadRequestObjectResult($"Room {roomId} not found.");
            }

            return new OkObjectResult(new RetrieveResult {
                endpoint = room.Endpoint,
                port = room.Port,
                relay = room.Relay
            });
        }

        private struct RetrieveResult
        {
            public string endpoint;

            public int port;

            public string relay;
        }
    }
}
