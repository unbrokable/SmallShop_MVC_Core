using Shop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Interfaces
{
    public interface IPurchaseService
    {
        public Task<bool> BuyAsync(string email, List<ProductViewModel> products);
        public Task<ProductViewModel> GetProductByIdAsync(int id);
    }
}
