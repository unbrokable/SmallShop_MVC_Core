using Microsoft.AspNetCore.Mvc.Rendering;
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
            // устанавливаем начальный элемент, который позволит выбрать всех
            //  var values = Enum.GetValues(typeof(TypeProduct));          

            //  companies.Insert(0, new Company { Name = "Все", Id = 0 });
            //  SelectedType = new SelectList(companies, "Id", "Name", company);
            SelectedType = type;
            SelectedName = name;
        }
        public SelectList Types { get; private set; } 
        public int? SelectedType { get; private set; }   
        public string SelectedName { get; private set; }
    }
}
