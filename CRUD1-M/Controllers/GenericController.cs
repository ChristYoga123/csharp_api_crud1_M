using Microsoft.AspNetCore.Mvc;

namespace CRUD1_M.Controllers
{
    public interface GenericController <T, ID, DTO>
    {
        ActionResult<T> findAll();
        ActionResult<T> findById(ID id);
        ActionResult<T> create(DTO dto);
        ActionResult<T> update(ID id, DTO dto);
        ActionResult<T> delete(ID id);
    }
}
