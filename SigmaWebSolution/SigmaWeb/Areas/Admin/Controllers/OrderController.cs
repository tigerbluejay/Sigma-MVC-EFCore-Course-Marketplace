using Sigma.DataAccess.Repository.Interfaces;
using Sigma.Models;
using Sigma.Models.ViewModels;
using Sigma.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using System.ComponentModel;
using System.Security.Claims;

namespace SigmaWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class OrderController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		
		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		
		public IActionResult Index()
		{
			return View();
		}
	}
}
