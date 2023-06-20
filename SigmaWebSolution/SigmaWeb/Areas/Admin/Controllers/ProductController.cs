using Sigma.DataAccess;
using Sigma.DataAccess.Repository.Interfaces;
using Sigma.Models;
using Sigma.Models.ViewModels;
using Sigma.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Microsoft.Extensions.Hosting;
using Sigma.DataAccess.Repository;

namespace SigmaWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
		}

		public IActionResult Index()
		{
			return View();
		}

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new() { Product = new() };

            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
            }
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
			{
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    // if image is not empty we want to upload the image
                    // we need to give it a name based on GUID, because if two people
                    // decide to give the file the same name, then it is a mess
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    // delete existing file
                    if (productVM.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, 
                            productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    productVM.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                _unitOfWork.Save(); // here it goes to the db and saves changes
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            // if it is not valid we return the Create View with the object passed on
            return View(productVM);
        }
		


        #region APICalls
        [HttpGet]
		public IActionResult GetAll()
		{
			var productList = _unitOfWork.Product.GetAll();
			return Json(new { data = productList });
		}

		public IActionResult Delete(int? id)
		{
			var productToDelete = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
			if (productToDelete == null)
			{
				return Json(new { success = false, message = "Error while deleting" });
			}

			var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToDelete.ImageUrl.TrimStart('\\'));
			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}

			_unitOfWork.Product.Remove(productToDelete);
			_unitOfWork.Save();

            return View(nameof(Index));

		}

		#endregion
	}
}
