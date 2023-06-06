using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Sigma.DataAccess.Repository.Interfaces;
using Sigma.Models.ViewModels;
using System.Security.Claims;

namespace SigmaWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public int OrderTotal { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
//        public IActionResult Index()
        //{
            // shoppingCartVM = new ShoppingCartVM()
          //  {
                //  ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
                //  includeProperties: "Product"),
                //  OrderHeader = new()
          //  };
        // return View(shoppingCartVM);
        //}
    }
}
