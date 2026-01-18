namespace ValgfagPortfolio.Persistence.Interfaces;

public interface IRepository<T>
{
    Task CreateEnitityAsync(T entity);
    Task<List<T>> GetAllEntitiesAsync();
    Task<T> GetEntityByIdAsync(int id);
    Task UpdateEnitityAsync(T entity);
    Task DeleteEnitityAsync(T entity);
}