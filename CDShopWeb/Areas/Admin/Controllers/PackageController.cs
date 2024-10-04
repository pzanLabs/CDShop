using CDShop.DataAccess.Repository.IRepository;
using CDShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CDShopWeb.Areas.Admin.Controllers;
[Area("Admin")]
    public class PackageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PackageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Package> objPackageList = _unitOfWork.Package.GetAll();
            return View(objPackageList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Package obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Package.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var packageFromDb = _unitOfWork.Package.GetFirstOrDefault(u => u.Id == id);

            if (packageFromDb == null)
            {
                return NotFound();
            }
            return View(packageFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Package obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Package.Update(obj);
                _unitOfWork.Save();
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
            var packageFromDb = _unitOfWork.Package.GetFirstOrDefault(u => u.Id == id);
            if (packageFromDb == null)
            {
                return NotFound();
            }
            return View(packageFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeletePost(int? id)
        {
            var packageFromDb = _unitOfWork.Package.GetFirstOrDefault(u => u.Id == id);
            if (packageFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Package.Remove(packageFromDb);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }

