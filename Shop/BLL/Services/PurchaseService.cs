using DAL.Interfaces;
using Shop.Data;
using Shop.Data.Entities;
using Shop.Interfaces;
using Shop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Services
{
    public class PurchaseService: IPurchaseService
    {
        private readonly IRepository repository;
        public PurchaseService(IRepository repository)
        {
            this.repository = repository;
        }

        public bool Buy(string email, List<ProductViewModel> products)
        {
            var user = repository.Find<User>(i => i.Email.CompareTo(email) == 0);

            var purchase = new Purchase()
            {
                User = user,
                Date = DateTime.Now

            };
            repository.Add<Purchase>(purchase);
            var product = repository.Get<Product>(i => products.Select(j => j.Id).Contains(i.Id)).ToList();
            List<CompositionPurchase> compositionPurchases = new List<CompositionPurchase>(product.Count);
            for(int i =0; i < product.Count; i++)
            {
                compositionPurchases.Add(new CompositionPurchase()
                {
                    Purchase = purchase,
                    Product = product[i],
                    Amount = products.FirstOrDefault(j => j.Id == product[i].Id).Amount
                   
                });    
            }
            try
            {
                repository.Add<CompositionPurchase>(compositionPurchases.ToArray());
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public ProductViewModel GetProductById(int id)
        {
            var product = repository.Find<Product>(i => i.Id == id);

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

