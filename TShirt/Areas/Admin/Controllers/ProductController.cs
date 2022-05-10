using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using TShirt.DataAccess;
using TShirt.DataAccess.Repository.IRepository;
using TShirt.Models;
using TShirt.Models.ViewModels;

namespace TShirt.Controllers
{
    // Optional with .Net Core 6, but easier to read
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        // for saving pictures locally
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        // Get - Update Insert
        public IActionResult Upsert(int? id)
        {
            // That way the View will be tightly binded and no need to ViewBag/Data
            // If later we need to add 10 more properties, we can do it from ProductView Model, to keep the controller light
            ProductViewModel productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                DesignTypeList = _unitOfWork.DesignType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                //create product
                // --- ViewData Vs. ViewBag ---
                // ViewBag -- can get any name, here .CategoryList and linked to related value - Wrapper for ViewData
                /*ViewBag.CategoryList = CategoryList;  //create ViewBag to pass data to the controller*/
                // ViewData -- Different syntax, need the related type in View
                /*ViewData["DesignTypeList"] = DesignTypeList;*/
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
                //update the product
            }

        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if(file != null)
                {
                    // In production we don't want to risk a user can upload two files with the same name, so renamed on upload
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if(obj.Product.ImageUrl != null)
                    {
                        var oldImagepath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagepath))
                        {
                            System.IO.File.Delete(oldImagepath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                
                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                }
                _unitOfWork.Save();
                TempData["Success"] = "Product created successfully";
                return RedirectToAction("Index");
                // if controller were somewhere else, a second parametre to "RedirectToAction" allows to specify the exact one
                }
            return View(obj);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var designTypeFromDb = _unitOfWork.DesignType.GetFirstOrDefault(x => x.Id == id);

            if (designTypeFromDb == null)
            {
                return NotFound();
            }

            return View(designTypeFromDb);
        }

        
        //! "ActionName" attribute allows to name a method the same as previous, with similar arguments, it will be differienciated thanks to POST and GET
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            //var obj = _db.Categories.Find(id);
            var obj = _unitOfWork.DesignType.GetFirstOrDefault(x => x.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.DesignType.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Design successfully deleted";
            // if controller were somewhere else, a second parametre to "RedirectToAction" allows to specify the exact one
            return RedirectToAction("Index");
        }
    
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            // Properties are case sensitive
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category,DesignType");
                return Json(new { data = productList });
        }
        #endregion
   
    }
}
