using MongoDB.Bson;
using OsintCommand.API.Entities;
using System;
using System.ComponentModel;
using System.Reflection;
namespace OsintCommand.API
{
    public class Utils
    {

        public static string GetEnumDescriptionFromInt<T>(int value) where T : Enum
        {
            var type = typeof(T);
            var name = Enum.GetName(type, value);
            if (name != null)
            {
                var field = type.GetField(name);
                var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                if (attr != null)
                    return attr.Description;
                else
                    return name; // Nếu không có Description, trả về tên enum
            }
            return null; // Không tìm thấy
        }

        public static object ConvertJTokenToDotNetObject(object obj)
        {
            if (obj is Newtonsoft.Json.Linq.JValue jval)
                return jval.Value;
            if (obj is Newtonsoft.Json.Linq.JObject jobj)
                return jobj.ToObject<Dictionary<string, object>>()
                           .ToDictionary(kv => kv.Key, kv => ConvertJTokenToDotNetObject(kv.Value));
            if (obj is Newtonsoft.Json.Linq.JArray jarr)
                return jarr.Select(ConvertJTokenToDotNetObject).ToList();
            return obj;
        }

        /// <summary>
        /// // "host:port" hoặc "host:port:user:pass"
        /// </summary>
        public static Proxy ParseToEntity(string raw, ProxyType type, string? groupId = null, int maxUse = 1)
        {
            var p = raw?.Trim().Split(':') ?? Array.Empty<string>();
            if (p.Length < 2) throw new ArgumentException("Định dạng proxy sai");
            if (!int.TryParse(p[1], out var port)) throw new ArgumentException("Port không hợp lệ");

            return new Proxy
            {
                Type = type,
                Host = p[0],
                Port = port,
                Username = p.Length > 2 ? p[2] : null,
                Password = p.Length > 3 ? p[3] : null,
                ProxyGroupId = groupId,
                MaxUse = Math.Max(1, maxUse),
                InUseCount = 0
            };
        }

    }
}
