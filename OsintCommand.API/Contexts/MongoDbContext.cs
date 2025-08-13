using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OsintCommand.API.Entities;
using OsintCommand.API.Settings;

namespace OsintCommand.API.Contexts
{
    public class MongoDbContext
    {
        public IMongoDatabase _database { get; }
        public IMongoClient _client { get; }

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            _client = new MongoClient(settings.Value.ConnectionString);
            _database = _client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string? collectionName = null)
        {
            string name;
            if (!string.IsNullOrEmpty(collectionName))
            {
                name = collectionName;
            }
            else
            {
                switch (typeof(T).Name)
                {
                    case nameof(ScriptFakeAccount):
                        name = "script_fakeaccount";
                        break;
                    case nameof(GroupFakeAccount):
                        name = "group_fakeaccount";
                        break;
                    case nameof(FriendFakeAccount):
                        name = "friend_fakeaccount";
                        break;

                    default:
                        name = typeof(T).Name.ToLower();
                        break;
                }
            }

            return _database.GetCollection<T>(name);
        }

        public Task<IClientSessionHandle> StartSessionAsync(
            ClientSessionOptions? options = null, CancellationToken ct = default)
            => _client.StartSessionAsync(options, ct);
    }
}
