using FoodStoreApp.Data;
using FoodStoreApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodStoreApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categorylist = _db.Category;
            return View(categorylist);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category cat)
        {
            _db.Category.Add(cat);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category cat)
        {
            if(ModelState.IsValid)
            {
                _db.Category.Update(cat);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cat);
        }

        public IActionResult Delete(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var cat = _db.Category.Find(id);
            if (cat == null)
            {
                return NotFound();
            }
            _db.Category.Remove(cat);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
