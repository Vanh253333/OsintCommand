using MongoDB.Bson;
using MongoDB.Driver;
using OsintCommand.API.Contexts;

namespace OsintCommand.API.Repositories
{
    public class Repository<T>: IRepository<T> where T : class
    {
        protected readonly IMongoCollection<T> _collection;   
        public Repository(MongoDbContext context) 
        {
            _collection = context.GetCollection<T>();
        }

        // Insert a new document into the collection
        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            var objectId = new ObjectId(id.ToString());
            // Delete a document by its id
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", objectId));
        }

        public async Task DeleteManyAsync(List<string> ids)
        {
            var objectIds = ids.Select(id => new ObjectId(id)).ToList();
            var filter = Builders<T>.Filter.In("_id", objectIds);
            await _collection.DeleteManyAsync(filter);
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // Find all documents and convert them to a list
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            // Convert the id to an ObjectId before querying
            var objectId = new ObjectId(id);
            // Find a document by its id
            return await _collection.Find(Builders<T>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetByIdsAsync(List<string> ids)
        {
            var objectIds = ids.Select(id => new ObjectId(id)).ToList();
            var filter = Builders<T>.Filter.In("_id", objectIds);
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task UpdateAsync(string id, T entity)
        {
            var objectId = new ObjectId(id);
            await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", objectId), entity);
        }


    }
}
