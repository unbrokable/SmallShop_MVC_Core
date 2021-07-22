using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository
    {
        public Task AddAsync<T>(params T[] data) where T: class;
        public Task RemoveAsync<T>(T data) where T: class;
        public Task UpdateAsync<T>(T data) where T:class;
        public Task<T> FindAsync<T>(Func<T, bool> predicate) where T : class;
        public Task<IEnumerable<T>> GetAsync<T>(Func<T, bool> predicate) where T : class;
        public Task<IEnumerable<T>> GetPageAsync<T>( int amount, int page, Func<T, bool> predicate) where T : class;
        public Task<bool> IsExistAsync<T>(Func<T, bool> predicate) where T : class; 
    }
}
