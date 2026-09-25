using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_HW.Data;
using Shop_HW.Models;


namespace Shop_HW.Controllers
{
    public class ProductController : Controller
    {
        
        private readonly AppDbContext _context;
        
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductModel> products = _context.Products
                .Include(p => p.Category)
                .AsEnumerable();

            return View(products);
        }
        public IActionResult ViewProduct(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            var categories = _context.Categories.OrderBy(c => c.Name).ToList();
            ViewBag.Categories = categories;

            return View(product);
        }

        // POST: Product/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Models.ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
                return View(model);
            }

            var product = _context.Products.Find(model.Id);
            if (product == null)
                return NotFound();

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Amount = model.Amount;
            product.Image = model.Image;
            product.CategoryId = model.CategoryId;

            _context.SaveChanges();

            
            return RedirectToAction("Index");
        }
    }
    
}
