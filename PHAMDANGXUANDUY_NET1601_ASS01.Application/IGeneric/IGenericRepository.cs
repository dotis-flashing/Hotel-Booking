
namespace PHAMDANGXUANDUY_NET1601_ASS01.Application.IGeneric
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
        Task<T> GetById(int id);

    }
}
