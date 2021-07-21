using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Entities
{
    public enum TypeProduct
    {
        Phone=1, Computer, Mouse, Screen
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public decimal Price { get; set; }
        public TypeProduct Type { get; set; }
        public string Manufacture { get; set; }

        public List<CompositionPurchase> Purchases { get; set; }
    }
}
