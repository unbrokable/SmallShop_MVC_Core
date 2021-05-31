using Shop.Data.Entities;
using Shop.Data;
using System.Linq;
using System;
using Shop.Models;
using System.Collections.Generic;
using Shop.Models.ViewModels;
using Shop.Interfaces;

namespace Shop.Services
{
    public class UserService:IUserService
    {
        ApplicationContext db = new ApplicationContext();
        public bool ChangeData(int id, string email, string password, string login)
        {
            try
            {
                var user = db.Users.First(i => i.Id == id);

                user.Email = email ?? user.Email;
                user.Password = password ?? user.Password;
                user.Login = login ?? user.Login;

                db.Users.Update(user);
                db.SaveChanges();
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public UserModel GetUser(string email)
        {
            var user = db.Users
                .FirstOrDefault(i => i.Email.CompareTo(email) == 0);
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
            User user = db.Users.First(i => i.Email.CompareTo(email) == 0);
            IQueryable<Purchase> purchases = db.Purchases.Where(i => i.UserId == user.Id);
            var count = purchases.Count();
            purchases = purchases
                .OrderBy(i=> i.Date)
                .Skip((page - 1) * amountOfElementOnPage)
                .Take(amountOfElementOnPage);
            foreach (var purchase in purchases)
            {
                var prod = db.CompositionPurchases
                        .Where(i => i.PurchaseId == purchase.Id)
                        .Join(db.Products,
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
            return db.Users.FirstOrDefault(i => i.Password.CompareTo(password) == 0) == null ? false : true;
        }

        public bool CheckUniqueEmail(string email)
        {
            return !db.Users.Any(i => i.Email.CompareTo(email) == 0);
        }
    }
}
