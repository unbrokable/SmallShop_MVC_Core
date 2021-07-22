using Shop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Interfaces
{
    public interface IShopService
    {
        Task<ProductMenuViewModel> LoadProductsAsync(int? type, string name, int page, SortState sort, int amountOfElementOnPage);
    }
}
