using System;
using System.Collections.Generic;
using StoreManagementApp.Models;


namespace StoreManagementApp.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
