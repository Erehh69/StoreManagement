using System;
using System.Collections.Generic;
using StoreManagementApp.Models;

namespace StoreManagementApp.Models
{
    public class Report
    {
        public string Date { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");  // Initialize to prevent warning
        public int TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
    }

}
