using FoodStoreApp.Data;
using FoodStoreApp.Models;
using FoodStoreApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FoodStoreApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
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
                Product = new Product(),
                CategorySelectList = _db.Category.Select(item => new SelectListItem
                {
                    Text = item.Title,
                    Value = item.ID.ToString()
                }),
                ManagerSelectList = _db.Manager.Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                }),
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if(viewModel.Product.Id == 0)
                {
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extansion = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extansion), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    viewModel.Product.Image = fileName + extansion;
                    _db.Product.Add(viewModel.Product);
                }
                else
                {
                    var objFromDb = _db.Product.AsNoTracking().FirstOrDefault(u => u.Id == viewModel.Product.Id);

                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Image);

                        if(System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension),FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        viewModel.Product.Image = fileName + extension;
                    }
                    else
                    {
                        viewModel.Product.Image = objFromDb.Image;
                    }
                    _db.Product.Update(viewModel.Product);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            viewModel.CategorySelectList = _db.Category.Select(i => new SelectListItem
            {
                Text = i.Title,
                Value = i.ID.ToString()
            });
            viewModel.ManagerSelectList = _db.Manager.Select(i => new SelectListItem
            {
                Text=i.Name,
                Value = i.Id.ToString()
            });

            return View(viewModel);
        }

        //Get-Delete
        public IActionResult Delete(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var pr = _db.Product.Find(id);
            if (pr == null)
            {
                return NotFound();
            }
            _db.Product.Remove(pr);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
