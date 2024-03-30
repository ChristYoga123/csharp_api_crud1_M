namespace CRUD1_M.Services
{
    public interface GenericServices<T, ID, DTO>
    {
        List<T> findAll();
        T findById(ID id);
        T create(DTO dto);
        T update(ID id, DTO dto);
        T delete(ID id);
    }
}
