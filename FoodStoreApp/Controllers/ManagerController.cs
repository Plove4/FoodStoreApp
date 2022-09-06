using FoodStoreApp.Models;
using FoodStoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Dependency;

namespace FoodStoreApp.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ManagerController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Manager> managerList = _db.Manager;
            return View(managerList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Manager meg)
        {
            _db.Manager.Add(meg);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            return View();
        }

        public IActionResult Delete(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var meg = _db.Manager.Find(id);
            if (meg == null)
            {
                return NotFound();
            }
            _db.Manager.Remove(meg);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
