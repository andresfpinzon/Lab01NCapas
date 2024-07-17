using DAL.Datos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EFRepository : IRepository
    {
        //Llamada a la clase de contexto
        ApplicationDbContext _context;

        //Constructor
        public EFRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        //Dispose
        private bool _disposedValue;

        public async Task<TEntity> CreateAsync<TEntity>(TEntity toCreate) where TEntity : class
        {
            TEntity Result = default(TEntity);
            try
            {
                await _context.Set<TEntity>().AddAsync(toCreate);
                await _context.SaveChangesAsync();
            }
            catch (DbException) 
            { 
                throw;
            }
            return Result;
        }

        public async Task<bool> DeleteAsync<TEntity>(TEntity toDelete) where TEntity : class
        {
            bool Result = false;
            try
            {
                _context.Entry<TEntity>(toDelete).State = EntityState.Deleted;
                Result = await _context.SaveChangesAsync() > 0;
            }
            catch (DbException)
            {

                throw;
            }
            return Result;
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public Task<List<TEntity>> FilterAsync<TEntity>(Expression<Func<TEntity, bool>> Criteria) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> RetrieveAsync<TEntity>(Expression<Func<TEntity, bool>> Criteria) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync<TEntity>(TEntity toUpdate) where TEntity : class
        {
            bool Result = false;
            try
            {
                _context.Entry<TEntity>(toUpdate).State = EntityState.Modified;
                Result = await _context.SaveChangesAsync() > 0;
            }
            catch (DbException)
            {

                throw;
            }
            return Result;
        }
    }
}
