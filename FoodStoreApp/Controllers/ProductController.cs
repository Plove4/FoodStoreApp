using FoodStoreApp.Data;
using FoodStoreApp.Models;
using FoodStoreApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodStoreApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productlist = _db.Product;
            foreach (var product in productlist)
            {
                product.Category = _db.Category.FirstOrDefault(u => u.ID == product.Categoryid);
            };

            return View(productlist);
        }

        public IActionResult Upsert(int? id)
        {
            ProductViewModel viewModel = new ProductViewModel()
            {
                Product = new(),
                CategorySelectList = _db.Category.Select(item => new SelectListItem
                {
                    Text = item.Title,
                    Value = item.ID.ToString()
                })
            };


            if (id == null)
            {
                return View(viewModel);
            }
            else
            {
                // Обновляем выбранную позицию товара
                viewModel.Product = _db.Product.Find(id);
                if (viewModel.Product == null)
                    return NotFound();

                return View(viewModel);
            }

        }
    }
}
