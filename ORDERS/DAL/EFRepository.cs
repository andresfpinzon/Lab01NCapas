using DAL.Datos;
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

        public Task<bool> DeleteAsync<TEntity>(TEntity toDelete) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> FilterAsync<TEntity>(Expression<Func<TEntity, bool>> Criteria) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> RetrieveAsync<TEntity>(Expression<Func<TEntity, bool>> Criteria) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync<TEntity>(TEntity toUpdate) where TEntity : class
        {
            throw new NotImplementedException();
        }
    }
}
