using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IRepository
    {
        public void Add<T>(params T[] data) where T: class;
        public void Remove<T>(T data) where T: class;
        public void Update<T>(T data) where T:class;
        public T Find<T>(Func<T, bool> predicate) where T : class;
        public IEnumerable<T> Get<T>(Func<T, bool> predicate) where T : class;
        public IEnumerable<T> GetPage<T>( int amount, int page, Func<T, bool> predicate) where T : class;
        public bool IsExist<T>(Func<T, bool> predicate) where T : class; 
    }
}
