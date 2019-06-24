using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class Purchase
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public long Date { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public string ac { get; set; }
        public List<string> Code { get; set; }
        public string DatePurchase { get; set; }
        public string Image { get; set; }
    }
}