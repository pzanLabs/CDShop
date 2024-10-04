using CDShop.DataAccess.Repository.IRepository;
using CDShop.Models;
using CDShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace CDShopWeb.Areas.Admin.Controllers;
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
        return View();
    }
    public IActionResult Upsert(int? id)
    {
        ProductVM productVM = new()
        {
            Product = new(),
            GenreList = _unitOfWork.Genre.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
            PackageList = _unitOfWork.Package.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
        };
        if (id == 0 || id == null)
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

    public IActionResult Upsert(ProductVM obj)
    {
        if (ModelState.IsValid)
        {
            if (obj.Product.Id == 0)
            {
                _unitOfWork.Product.Add(obj.Product);
            }
            else
            {
                _unitOfWork.Product.Update(obj.Product);
            }
        }
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }



    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        var productList = _unitOfWork.Product.GetAll(includeProperties: "Genre,Package");
        return Json(new { data = productList });
    }
    [HttpDelete]
    public IActionResult Delete(int? id)
    {

        var obj = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
        if (id == null)
            return Json(new { success = false, message = "Error while deleting" });

        _unitOfWork.Product.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Deleted" });
    }

    #endregion
}
    

