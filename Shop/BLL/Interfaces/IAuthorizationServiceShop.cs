using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Interfaces
{
    public interface IAuthorizationServiceShop
    {
        bool Login(string email, string password);
        bool Registrate(string email, string password, string login);
    }
}
