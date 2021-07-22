using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models.ViewModels
{
    public class PurchaseMenuViewModel
    {
            public IEnumerable<PurchaseViewModel> Purchases { get; set; }
            public PageViewModel PageViewModel { get; set; }
    }
}
