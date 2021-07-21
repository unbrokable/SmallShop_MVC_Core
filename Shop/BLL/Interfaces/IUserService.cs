using Shop.Models;
using Shop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        bool ChangeData(string email, string password, string login, string currentUser);

        UserModel GetUser(string email);
        PurchaseMenuViewModel GetPurchases(string email, int page, int amountOfElementOnPage);
        bool CheckPassword(int id, string password);
        public bool CheckUniqueEmail(string email, int id);
       
    }
}
