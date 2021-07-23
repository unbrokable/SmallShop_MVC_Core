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
        Task<bool> ChangeDataAsync(string email, string password, string login, string currentUser);

        Task<UserModel> GetUserAsync(string email);
        Task<PurchaseMenuViewModel> GetPurchasesAsync(string email, int page, int amountOfElementOnPage);
        Task<bool> CheckPasswordAsync(int id, string password);
        Task<bool> CheckUniqueEmailAsync(string email, int id);
       
    }
}
