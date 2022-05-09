using Microsoft.AspNetCore.Mvc;
using System.IO;
using TShirt.DataAccess;
using TShirt.DataAccess.Repository.IRepository;
using TShirt.Models;

namespace TShirt.Controllers
{
    // Optional with .Net Core 6, but easier to read
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objDesignTypeList = _unitOfWork.Product.GetAll();
            return View(objDesignTypeList);

        }

        
        // Get - Update Insert
        public IActionResult Upsert(int? id)
        {
            Product product = new();
            if (id == null || id == 0)
            {
                //create product
                return View(product);
            }
            else
            {
                //update the product
            }

            return View(product);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(DesignType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DesignType.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Design successfully edited";
                // if controller were somewhere else, a second parametre to "RedirectToAction" allows to specify the exact one
                return RedirectToAction("Index");
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
    }

}
