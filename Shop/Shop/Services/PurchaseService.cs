using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Data;
using Shop.Data.Entities;
using Shop.Interfaces;
using Shop.Models;
using Shop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Services
{
    public class PurchaseService: IPurchaseService
    {
        ApplicationContext db = new ApplicationContext();

        public bool Buy(string email, List<ProductViewModel> products)
        {
            var user = db.Users.FirstOrDefault(i => i.Email.CompareTo(email) == 0);

            var purchase = new Purchase()
            {
                User = user,
                Date = DateTime.Now

            };
            db.Purchases.Add(purchase);
            var product = db.Products.Where(i => products.Select(j => j.Id).Contains(i.Id)).ToList();
            for(int i =0; i < product.Count; i++)
            {
                db.CompositionPurchases.Add(new CompositionPurchase()
                {
                    Purchase = purchase,
                    Product = product[i],
                    Amount = products.FirstOrDefault(j => j.Id == product[i].Id).Amount
                   
                });    
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public ProductViewModel GetProductById(int id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == id);

            return new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Type = product.Type,
                Manufacture = product.Manufacture,
                Amount = 1
            };   
        }
    }
}

