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

        //Home Index - show products with pagination
        public IActionResult Index(int page = 1, int pageSize = 12)
        {
            var total = _context.Products.Count();
            var totalPages = (int)Math.Ceiling(total / (double)pageSize);

            var products = _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreateDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsEnumerable();

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;

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
