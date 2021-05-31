using Shop.Models;
using Shop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Interfaces
{
    public interface IUserService
    {
        bool ChangeData(int id, string email, string password, string login);
        UserModel GetUser(string email);
        PurchaseMenuViewModel GetPurchases(string email, int page, int amountOfElementOnPage);
        bool CheckPassword(int id, string password);
        public bool CheckUniqueEmail(string email);
       
    }
}
