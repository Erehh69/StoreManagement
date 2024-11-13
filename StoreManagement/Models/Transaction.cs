using StoreManagementApp.Models;

namespace StoreManagementApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public bool IsPurchase { get; set; }
        public DateTime Date { get; set; }
    }
}
