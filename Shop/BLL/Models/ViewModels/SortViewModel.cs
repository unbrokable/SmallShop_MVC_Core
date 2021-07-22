using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models.ViewModels
{   
    public enum SortState
    {
            NameAsc,   
            NameDesc,   
            PriceAsc, 
            PriceDesc,    
    }
    public class SortViewModel
    {
        public SortState NameSort { get; private set; } 
        public SortState PriceSort { get; private set; }    
        public SortState Current { get; private set; }    
        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            PriceSort = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            Current = sortOrder;
        }
    }
}
