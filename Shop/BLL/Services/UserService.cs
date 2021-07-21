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

namespace Shop.Services
{
    public class UserService:IUserService
    {
        private readonly IRepository repository;
        public UserService(IRepository repository)
        {
            this.repository = repository;
        }
        public bool ChangeData( string email, string password, string login , string currentUser)
        {
            try
            {
                var user = repository.Find<User>(i => i.Email.CompareTo(currentUser) == 0);

                if(CheckUniqueEmail(email, user.Id) || user.Email.CompareTo(currentUser) != 0)
                {
                    return false;
                }

                user.Email = email ?? user.Email;
                user.Password = password ?? user.Password;
                user.Login = login ?? user.Login;

                repository.Update<User>(user);
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public UserModel GetUser(string email)
        {
            var user = repository.Find<User>(i => i.Email.CompareTo(email) == 0);

            return new UserModel()
            {
                Id = user.Id,
               Email = user.Email,
               Login = user.Login,
            };
                
        }

        public PurchaseMenuViewModel GetPurchases(string email,int page = 1, int amountOfElementOnPage = 3)
        {
            var purchaseViewModels = new List<PurchaseViewModel>();
            User user =  repository.Find<User>(i => i.Email.CompareTo(email) == 0);
            var purchases = repository.GetPage<Purchase> ((page - 1) * amountOfElementOnPage,amountOfElementOnPage ,i => i.UserId == user.Id);
            var count = purchases?.Count() ?? 0;

            foreach (var purchase in purchases)
            {
                var prod = repository.Get<CompositionPurchase>(i => i.PurchaseId == purchase.Id)
                        .Join(repository.Get<Product>(i => true),
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

        public bool CheckPassword(int id, string password)
        {
            return  repository.IsExist<User>(i => i.Password.CompareTo(password) == 0 && i.Id == id);
        }

        public bool CheckUniqueEmail(string email, int id)
        {
            return repository.IsExist<User>(i => i.Email.CompareTo(email) == 0 && id != i.Id);
        }
    }
}
