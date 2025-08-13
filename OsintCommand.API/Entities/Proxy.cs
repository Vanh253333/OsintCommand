using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OsintCommand.API.Entities
{
    public enum ProxyType { Http = 0, Socks5 = 1 }
    public class Proxy
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("type")]
        public ProxyType Type { get; set; }

        [BsonElement("host")]
        public string Host { get; set; }

        [BsonElement("port")]
        public int Port { get; set; }

        [BsonElement("username")]
        public string? Username { get; set; }

        [BsonElement("password")]
        public string? Password { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("proxyGroupId")]
        public string? ProxyGroupId { get; set; }

        [BsonElement("maxUse")]
        public int MaxUse { get; set; } = 1; // Số lần sử dụng tối đa của proxy này

        [BsonElement("inUseCount")]
        public int InUseCount { get; set; } // số account đang dùng
    }

    public class ProxyGroup
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}
