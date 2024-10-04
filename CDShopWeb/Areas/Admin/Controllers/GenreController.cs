using CDShop.DataAccess.Repository.IRepository;
using CDShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CDShopWeb.Areas.Admin.Controllers;
[Area("Admin")]

    public class GenreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Genre> objGenreList = _unitOfWork.Genre.GetAll();
            return View(objGenreList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Genre obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Genre.Add(obj);
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

            var genreFromDb = _unitOfWork.Genre.GetFirstOrDefault(u => u.Id == id);

            if (genreFromDb == null)
            {
                return NotFound();
            }
            return View(genreFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Genre obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Genre.Update(obj);
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
            var genreFromDb = _unitOfWork.Genre.GetFirstOrDefault(u => u.Id == id);
            if (genreFromDb == null)
            {
                return NotFound();
            }
            return View(genreFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeletePost(int? id)
        {
            var genreFromDb = _unitOfWork.Genre.GetFirstOrDefault(u => u.Id == id);
            if (genreFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Genre.Remove(genreFromDb);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }

