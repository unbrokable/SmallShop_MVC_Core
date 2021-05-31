using Shop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Interfaces
{
    public interface IPurchaseService
    {
        public bool Buy(string email, List<ProductViewModel> products);
        public ProductViewModel GetProductById(int id);
    }
}
