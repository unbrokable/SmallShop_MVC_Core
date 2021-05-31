using Shop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class PurchaseViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<ProductViewModel> Products { get; set;}
        public decimal Total
        {
            get
            {
                return Products.Sum(i => i.Price* i.Amount);
            }
        }
    }
}
