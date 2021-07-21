using Shop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel( int?type , string name)
        {
            SelectedType = type;
            SelectedName = name;
        }
        public int? SelectedType { get; private set; }   
        public string SelectedName { get; private set; }
    }
}
