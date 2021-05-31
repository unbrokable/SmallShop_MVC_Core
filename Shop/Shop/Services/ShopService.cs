using Shop.Data;
using Shop.Data.Entities;
using Shop.Interfaces;
using Shop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Services
{
    public class ShopService:IShopService
    {
        ApplicationContext bd = new ApplicationContext();

        public ProductMenuViewModel LoadProducts(int? type, string name, int page = 1, SortState sort = SortState.PriceAsc, int amountOfElementOnPage = 3)
        {
            IQueryable<Product> products = bd.Products;
            if (type != null)
            {
                TypeProduct typeProduct = (TypeProduct)Enum.ToObject(typeof(TypeProduct), type);
                products = products.Where(i => (int)i.Type == type);
            }

            if (!String.IsNullOrEmpty(name))
            {
                products = products.Where(i => i.Name.Contains(name));
            }

            products = sort switch
            {
                SortState.NameDesc => products.OrderByDescending(i => i.Name),
                SortState.PriceAsc => products.OrderBy(i => i.Price),
                SortState.PriceDesc => products.OrderByDescending(i => i.Price),
                _ => products.OrderBy(i => i.Name),
            };
            var count = products.Count();
            var items = products.Skip((page - 1) * amountOfElementOnPage).Take(amountOfElementOnPage).ToList();

            ProductMenuViewModel viewModel = new ProductMenuViewModel()
            {
                PageViewModel = new PageViewModel(count, page, amountOfElementOnPage),
                SortViewModel = new SortViewModel(sort),
                FilterViewModel = new FilterViewModel(type, name),
                Products = items
            };
            return viewModel;
        }
    }
}
