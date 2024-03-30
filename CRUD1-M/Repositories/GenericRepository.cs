namespace CRUD1_M.Repositories
{
    public interface GenericRepository<T, ID>
    {
        List<T> findAll();
        T findById(ID id);
        T create(T entity);
        T update(T entity);
        T delete(T entity);
    }
}
