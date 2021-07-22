using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<T> FindAsync<T>(Func<T,bool> predicate) where T : class
        {
            return Task.FromResult( bd.Set<T>().FirstOrDefault(predicate));
        }

        public Task<IEnumerable<T>> GetAsync<T>(Func<T, bool> predicate) where T : class
        {
            return Task.FromResult( bd.Set<T>().Where(predicate).AsEnumerable());
        }

        public Task<IEnumerable<T>> GetPageAsync<T>(int skip, int page, Func<T, bool> predicate = null) where T : class
        {
            if ( predicate == null)
            {
                return Task.FromResult( bd.Set<T>().Skip(skip).Take(page).AsEnumerable());
            }
            return Task.FromResult(bd.Set<T>().Where( predicate).Skip(skip).Take(page).AsEnumerable());
        }

        public Task<bool> IsExistAsync<T>(Func<T, bool> predicate) where T : class
        {
            return  Task.FromResult(bd.Set<T>().Any(predicate));
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
