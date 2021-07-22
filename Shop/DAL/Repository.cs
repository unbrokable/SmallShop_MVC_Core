using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Repository : IRepository
    {
        public ApplicationContext bd;
        public Repository(ApplicationContext bd)
        {
            this.bd = bd;
        }
        public async Task AddAsync<T>(params T[] data) where T : class
        {
            bd.Set<T>().AddRange(data);
            await bd.SaveChangesAsync();
        }

        public async Task<T> FindAsync<T>(Expression<Func<T,bool>> predicate) where T : class
        {
            return await  bd.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await bd.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetPageAsync<T>(int skip, int page, Expression<Func<T, bool>> predicate = null) where T : class
        {
            if ( predicate == null)
            {
                return await bd.Set<T>().Skip(skip).Take(page).ToListAsync();
            }
            return await bd.Set<T>().Where( predicate).Skip(skip).Take(page).ToListAsync();
        }

        public async Task<bool> IsExistAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return  await bd.Set<T>().AnyAsync(predicate);
        }

        public async Task RemoveAsync<T>(T data) where T : class
        {
            bd.Set<T>().Remove(data);
            await bd.SaveChangesAsync();
        }

        public async Task UpdateAsync<T>(T data) where T : class
        {
            bd.Set<T>().Update(data);
            await bd.SaveChangesAsync();
        }
        
    }
}
