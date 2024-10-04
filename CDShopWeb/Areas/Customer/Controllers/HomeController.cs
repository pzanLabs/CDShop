using Microsoft.AspNetCore.Authorization;
using CDShop.DataAccess.Repository.IRepository;
using CDShop.Models;
using CDShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CDShopWeb.Areas.Customer.Controllers;
[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
         _unitOfWork= unitOfWork;
         _logger = logger;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> productsFromDB = _unitOfWork.Product.GetAll(includeProperties:"Genre,Package");

        return View(productsFromDB);
    }

    public IActionResult Details(int productId)
    {

        ShoppingCart cartObj = new()
        {
            Count = 1,
            ProductId = productId,
            Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Genre,Package")
        };

        return View(cartObj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult Details(ShoppingCart cartObj)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        cartObj.ApplicationUserId = claim.Value;

        ShoppingCart cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.ApplicationUserId == claim.Value && u.ProductId == cartObj.ProductId);

        if(cart == null)
        {
            _unitOfWork.ShoppingCart.Add(cartObj);
        }
        else
        {
            _unitOfWork.ShoppingCart.IncrementCount(cart, cartObj.Count);
        }
        
        _unitOfWork.Save();

        return RedirectToAction("Index");
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
