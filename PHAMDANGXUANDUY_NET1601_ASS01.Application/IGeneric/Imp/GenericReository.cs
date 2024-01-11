using Microsoft.EntityFrameworkCore;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;

namespace PHAMDANGXUANDUY_NET1601_ASS01.Application.IGeneric.Imp
{
    public class GenericReository<T> : IGenericRepository<T> where T : class
    {
        protected readonly FUMiniHotelManagementContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericReository(FUMiniHotelManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            var crud = await _dbSet.AddAsync(entity);
            return crud.Entity;
        }

        public async Task<T> Delete(T entity)
        {
            var crud = _dbSet.Remove(entity);
            return crud.Entity;
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var check = await _dbSet.FindAsync(id);
            if (check == null)
            {
                throw new Exception("Khong tim thay");
            }
            return check;
        }

        public async Task<T> Update(T entity)
        {
            var crud = _dbSet.Update(entity);
            return crud.Entity;
        }

        public void UpdateT(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
