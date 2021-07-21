using DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using Shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class Repository : IRepository
    {
        public ApplicationContext bd;

        public Repository(ApplicationContext bd)
        {
            this.bd = bd;
        }
        public void Add<T>(params T[] data) where T : class
        {
            bd.Set<T>().AddRange(data);
            bd.SaveChanges();
        }

        public T Find<T>(Func<T,bool> predicate) where T : class
        {
            return bd.Set<T>().FirstOrDefault(predicate);
        }

        public IEnumerable<T> Get<T>(Func<T, bool> predicate) where T : class
        {
            return bd.Set<T>().Where(predicate).ToList();
        }

        public IEnumerable<T> GetPage<T>(int skip, int page, Func<T, bool> predicate = null) where T : class
        {
            if ( predicate == null)
            {
                return bd.Set<T>().Skip(skip).Take(page).ToList();
            }
            return bd.Set<T>().Where( predicate).Skip(skip).Take(page).ToList();
        }

        public bool IsExist<T>(Func<T, bool> predicate) where T : class
        {
            return bd.Set<T>().Any(predicate);
        }

        public void Remove<T>(T data) where T : class
        {
            bd.Set<T>().Remove(data);
            bd.SaveChanges();
        }

        public void Update<T>(T data) where T : class
        {
            bd.Set<T>().Update(data);
            bd.SaveChanges();
        }
        
    }
}
