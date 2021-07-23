using Shop.Data.Entities;
using Shop.Data;
using System.Linq;
using System;
using Shop.Models;
using System.Collections.Generic;
using Shop.Models.ViewModels;
using Shop.Interfaces;
using BLL.Interfaces;
using DAL.Interfaces;
using System.Threading.Tasks;

namespace Shop.Services
{
    public class UserService:IUserService
    {
        private readonly IRepository repository;
        public UserService(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<bool> ChangeDataAsync( string email, string password, string login , string currentUser)
        {
            try
            {
                var user = await repository.FindAsync<User>(i => i.Email.CompareTo(currentUser) == 0);

                if(await CheckUniqueEmailAsync(email, user.Id) || user.Email.CompareTo(currentUser) != 0)
                {
                    return false;
                }

                user.Email = email ?? user.Email;
                user.Password = password ?? user.Password;
                user.Login = login ?? user.Login;

                await repository.UpdateAsync<User>(user);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<UserModel> GetUserAsync(string email)
        {
            var user = await repository.FindAsync<User>(i => i.Email.CompareTo(email) == 0);

            return new UserModel()
            {
                Id = user.Id,
               Email = user.Email,
               Login = user.Login,
            };
                
        }

        public async Task<PurchaseMenuViewModel> GetPurchasesAsync(string email,int page = 1, int amountOfElementOnPage = 3)
        {
            var purchaseViewModels = new List<PurchaseViewModel>();
            User user = await repository.FindAsync<User>(i => i.Email.CompareTo(email) == 0);
            var purchases = await repository.GetPageAsync<Purchase> ((page - 1) * amountOfElementOnPage,amountOfElementOnPage ,i => i.UserId == user.Id);
            var count = purchases?.Count() ?? 0;

            foreach (var purchase in purchases)
            {
                var prod = (await repository.GetAsync<CompositionPurchase>(i => i.PurchaseId == purchase.Id))
                        .Join( await repository.GetAsync<Product>(i => true),
                            c => c.ProductId,
                            p => p.Id,
                            (c, p) => new Models.ViewModels.ProductViewModel
                            {
                                Id = p.Id,
                                Amount = c.Amount,
                                Name = p.Name,
                                Manufacture = p.Manufacture,
                                Price = p.Price,
                                Type = p.Type
                            }
                        );
                purchaseViewModels.Add(new PurchaseViewModel()
                {
                    Id = purchase.Id,
                    Date = purchase.Date,
                    Products = prod.ToList()
                }); 
            }
           
            return new PurchaseMenuViewModel()
            {
                Purchases = purchaseViewModels,
                PageViewModel = new PageViewModel(count, page, amountOfElementOnPage)
            }; 
        }

        public async Task<bool> CheckPasswordAsync(int id, string password)
        {
            return await repository.IsExistAsync<User>(i => i.Password.CompareTo(password) == 0 && i.Id == id);
        }

        public async Task<bool> CheckUniqueEmailAsync(string email, int id)
        {
            return await repository.IsExistAsync<User>(i => i.Email.CompareTo(email) == 0 && id != i.Id);
        }
    }
}
