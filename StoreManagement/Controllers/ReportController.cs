using Microsoft.AspNetCore.Mvc;
using StoreManagementApp.Data;
using StoreManagementApp.Models;
using System.Linq;

namespace StoreManagementApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly StoreContext _context;

        public ReportController(StoreContext context)
        {
            _context = context;
        }

        // GET: /Report/Index
        public IActionResult Index()
        {
            // Group transactions by date, then calculate sales and revenue
            var reports = _context.Transactions
                .GroupBy(t => t.Date.Date)
                .Select(g => new Report
                {
                    Date = g.Key.ToString("yyyy-MM-dd"),
                    TotalSales = g.Where(t => !t.IsPurchase).Sum(t => t.Quantity),
                    TotalRevenue = g.Where(t => !t.IsPurchase).Sum(t => t.Quantity * t.Product.Price)
                })
                .ToList();

            return View(reports);
        }
    }
}
