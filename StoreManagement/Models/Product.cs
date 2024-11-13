using System.Collections.Generic;

namespace StoreManagementApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }  // Add this property if needed
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // Mark Transactions as not required for creation
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
