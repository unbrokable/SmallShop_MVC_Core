using DAL.Interfaces;
using Shop.Data;
using Shop.Data.Entities;
using Shop.Interfaces;
using System;
using System.Linq;

namespace Shop.Services
{
    public class AuthorizationService:IAuthorizationServiceShop
    {
        private readonly IRepository repository;

        public AuthorizationService( IRepository repository)
        {
            this.repository = repository;
        } 

        public bool Login(string email, string password)
        {
            var user = repository.Find<User>(i => i.Email.CompareTo(email) == 0 && i.Password.CompareTo(password) == 0);
            if (user == null)
            {
                return false;
            }
            return true;
        }
        public bool Registrate(string email, string password, string login)
        {
            try
            {
                repository.Add(new User
                {
                    Email = email,
                    Password = password,
                    Login = login
                });
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }
        
        public bool CheckUniqueEmail(string email)
        {
            return !repository.IsExist<User>(i => i.Email.CompareTo(email) == 0);
        }
    }
}
