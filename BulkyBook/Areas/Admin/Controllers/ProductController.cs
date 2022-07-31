using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> coverTypeList = unitOfWork.Product.GetAll();
            return View(coverTypeList);
        }

       

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = this.unitOfWork.Category.GetAll()
                .Select(i=>new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = this.unitOfWork.CoverType.GetAll()
                .Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),

            };

            if (id == null || id == 0)
            {
                //ViewBag.CategoryList = CategoryList;
               // ViewData["CoverTypeList"] = CoverTypeList;
                return View(productVM);
            }

           

            return View(productVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                //unitOfWork.CoverType.Update(obj);
                unitOfWork.Save();
                TempData["success"] = "CoverType updated successfully";
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

            var coverType = unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);

            if (coverType == null)
            {
                return NotFound();
            }

            return View(coverType);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);

            if (obj == null) return NotFound();

            unitOfWork.CoverType.Remove(obj);
            unitOfWork.Save();
            TempData["success"] = "CoverType deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
