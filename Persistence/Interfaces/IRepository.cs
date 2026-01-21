namespace ValgfagPortfolio.Persistence.Interfaces;

public interface IRepository<T>
{
    Task CreateEntityAsync(T entity);
    Task<List<T>> GetAllEntitiesAsync();
    Task<T> GetEntityByIdAsync(int id);
    Task UpdateEntityAsync(T entity);
    Task DeleteEntityAsync(T entity);
}