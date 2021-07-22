using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Interfaces
{
    public interface IAuthorizationServiceShop
    {
        Task<bool> LoginAsync(string email, string password);
        Task<bool> RegistrateAsync(string email, string password, string login);
    }
}
