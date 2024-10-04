using CDShop.DataAccess.Repository.IRepository;
using CDShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace CDShopWeb.Areas.Admin.Controllers;
[Area(nameof(Admin))]

public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

     public CompanyController(IUnitOfWork unitOfWork)
     {
        _unitOfWork = unitOfWork;
     }

     public IActionResult Index()
     {
        return View();
     }

     public IActionResult Upsert(int? id)
     {
        Company company = new();

        if( id == 0 || id == null)
        {
            return View(company);
        }
        else
        {
            company = _unitOfWork.Company.GetFirstOrDefault(x => x.Id == id);
            return View(company);
        }
     }

     [HttpPost]
     [ValidateAntiForgeryToken]

     public IActionResult Upsert(Company company)
     {
        if(ModelState.IsValid)
        {
            if(company.Id== 0)
            {
                _unitOfWork.Company.Add(company);
            }
            else
            {
                _unitOfWork.Company.Update(company);
            }
             _unitOfWork.Save();
        }
         return RedirectToAction(nameof(Index));
     }

    #region API CALLS

    public IActionResult GetAll()
    {
        var companyList = _unitOfWork.Company.GetAll();
        return Json(new { data = companyList });
    }
    [HttpDelete]

    public IActionResult Delete(int? id)
    {
        var obj = _unitOfWork.Company.GetFirstOrDefault(x => x.Id == id);
        if (id == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }
        _unitOfWork.Company.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Deleted" });
    }

    #endregion

}

