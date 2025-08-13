namespace OsintCommand.API.Repositories
{
    public interface IRepository<T> where T : class
    {
        // Retrieve all documents from the collection
        Task<IEnumerable<T>> GetAllAsync();
        // Retrieve a single document by its ID
        Task<T?> GetByIdAsync(string id);
        // Insert a new document into the collection
        Task AddAsync(T entity);
        // Update an existing document by its ID
        Task UpdateAsync(string id, T entity);
        // Delete a document by its ID
        Task DeleteAsync(string id);
        Task DeleteManyAsync(List<string> ids);
        Task<IEnumerable<T>> GetByIdsAsync(List<string> ids);
    }
}
