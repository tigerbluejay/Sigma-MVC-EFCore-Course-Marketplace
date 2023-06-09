using Sigma.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Sigma.DataAccess.Repository.Interfaces;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace SigmaWeb.ViewComponents
{
	// this page is the code behind file of the view component
	public class ShoppingCartViewComponent : ViewComponent
	{
		private readonly IUnitOfWork _unitOfWork;

		public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claim != null) // this means the user is logged in
			{
				if (HttpContext.Session.GetInt32(SD.SessionCart) != null) // if there's a session
				{
					return View(HttpContext.Session.GetInt32(SD.SessionCart)); 
					// pass the session value to the view
				} else // we go to db to retrieve the count
				{
					HttpContext.Session.SetInt32(SD.SessionCart,
						_unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value)
						.ToList().Count()); // retrieving the number of ShoppingCart objects of our user
					return View(HttpContext.Session.GetInt32(SD.SessionCart));
				} 
			} else // the user has not signed in
			{
				HttpContext.Session.Clear();
				return View(0);
			}
		}
	}
}
