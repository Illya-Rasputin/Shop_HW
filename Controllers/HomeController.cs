using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_HW.Data;
using Shop_HW.Models;
using System.Diagnostics;

namespace Shop_HW.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //Home Index - show products with pagination
        public IActionResult Index(int page = 1, int pageSize = 12, string sortOrder = "name_asc")
        {
            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            query = sortOrder switch
            {
                "name_asc" => query.OrderBy(p => p.Name),
                "name_desc" => query.OrderByDescending(p => p.Name),
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                "rating_asc" => query.OrderBy(p => p.Rating),
                "rating_desc" => query.OrderByDescending(p => p.Rating),
                _ => query.OrderByDescending(p => p.CreateDate)
            };

            var total = query.Count();
            var totalPages = (int)Math.Ceiling(total / (double)pageSize);

            var products = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsEnumerable();

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SortOrder = sortOrder;

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
