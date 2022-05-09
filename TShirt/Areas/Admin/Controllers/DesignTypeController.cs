using Microsoft.AspNetCore.Mvc;
using System.IO;
using TShirt.DataAccess;
using TShirt.DataAccess.Repository.IRepository;
using TShirt.Models;

namespace TShirt.Controllers
{
    // Optional with .Net Core 6, but easier to read
    [Area("Admin")]
    public class DesignTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DesignTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<DesignType> objDesignTypeList = _unitOfWork.DesignType.GetAll();
            return View(objDesignTypeList);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DesignType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DesignType.Add(obj);
                _unitOfWork.Save();
                //Enable one time notification after a specific action - here to ensure it is successful
                TempData["Success"] = "Design successfully created";
                // if controller where somewhere else, a second parametre allows to specify this in RedirectToAction
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var designTypeFromFirst = _unitOfWork.DesignType.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (designTypeFromFirst == null)
            {
                return NotFound();
            }

            return View(designTypeFromFirst);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DesignType obj)
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
