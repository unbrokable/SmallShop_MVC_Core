using DAL.Interfaces;
using Shop.Data;
using Shop.Data.Entities;
using Shop.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Services
{
    public class AuthorizationService:IAuthorizationServiceShop
    {
        private readonly IRepository repository;

        public AuthorizationService( IRepository repository)
        {
            this.repository = repository;
        } 

        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await repository.FindAsync<User>(i => i.Email.CompareTo(email) == 0 && i.Password.CompareTo(password) == 0);
            if (user == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> RegistrateAsync(string email, string password, string login)
        {
            try
            {
                await repository.AddAsync(new User
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
        
        public async Task<bool> CheckUniqueEmailAsync(string email)
        {
            return  ! (await repository.IsExistAsync<User>(i => i.Email.CompareTo(email) == 0));
        }
    }
}
