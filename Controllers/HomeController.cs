using Microsoft.AspNetCore.Mvc;
using Shop_HW.Data;
using Shop_HW.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Shop_HW.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        //Home Index - show latest products
        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreateDate)
                .Take(8)
                .AsEnumerable();

            return View(products);
        }

        //Home Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
