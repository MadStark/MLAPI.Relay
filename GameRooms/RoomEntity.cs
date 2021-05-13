using Microsoft.Azure.Cosmos.Table;

namespace GameRooms
{
    public class RoomEntity : TableEntity
    {
        [IgnoreProperty]
        public string Key {
            get => RowKey;
            set => RowKey = value;
        }

        public string Endpoint { get; set; }

        public int Port { get; set; }

        public string Relay { get; set; }


        public RoomEntity()
        {
            PartitionKey = "room";
        }

        public RoomEntity(string roomKey, string endpoint, int port, string relay) : this()
        {
            Key = roomKey;
            Endpoint = endpoint;
            Port = port;
            Relay = relay;
        }
    }
}
