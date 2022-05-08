using Microsoft.AspNetCore.Mvc;
using System.IO;
using TShirt.DataAccess;
using TShirt.Models;

namespace TShirt.Controllers
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
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);

        }

        /// <summary>
        /// GET - Return Create View
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST - Create a new category
        /// </summary>
        /// <param name="obj">A Category object</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot match the name of the category");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                //Enable one time notification after a specific action - here to ensure it is successful
                TempData["Success"] = "Category successfully created";
                // if controller where somewhere else, a second parametre allows to specify this in RedirectToAction
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        /// <summary>
        /// GET - Open view to later edit category
        /// </summary>
        /// <param name="id">Category Id which will be edited</param>
        /// <returns></returns>
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryFromSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromFirst);
        }

        /// <summary>
        /// POST - Edit Category data
        /// </summary>
        /// <param name="obj">A related Category object</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot match the name of the category");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category successfully edited";
                // if controller were somewhere else, a second parametre to "RedirectToAction" allows to specify the exact one
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        /// <summary>
        /// GET - Go the Delete View
        /// </summary>
        /// <param name="id">Unique Id of product to be deleted</param>
        /// <returns></returns>
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        /// <summary>
        /// POST - Delete Related category
        /// </summary>
        /// <param name="id">related Id of category deleted</param>
        /// <returns></returns>  
        //!! "ActionName" attribute allows to name a method the same as previous, with similar arguments, it will be differienciated thanks to POST and GET
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category successfully deleted";
            // if controller were somewhere else, a second parametre to "RedirectToAction" allows to specify the exact one
            return RedirectToAction("Index");
        }
    }

}
