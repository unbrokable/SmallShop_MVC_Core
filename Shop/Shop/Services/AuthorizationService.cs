using Shop.Data;
using Shop.Data.Entities;
using Shop.Interfaces;
using System;
using System.Linq;

namespace Shop.Services
{
    public class AuthorizationService:IAuthorizationServiceShop
    {
        ApplicationContext db = new ApplicationContext();

        public bool Login(string email, string password)
        {
            var user = db.Users.FirstOrDefault(i => i.Email.CompareTo(email) == 0 && i.Password.CompareTo(password) == 0);
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
                db.Users.Add(new User
                {
                    Email = email,
                    Password = password,
                    Login = login
                });
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }
        
        public bool CheckUniqueEmail(string email)
        {
            return !db.Users.Any(i => i.Email.CompareTo(email) == 0);
        }
    }
}
