using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Sigma.DataAccess.Repository.Interfaces;
using Sigma.Models;
using Sigma.Utilities;
using SigmaWeb.Areas.Customer.Controllers;
using System.Linq.Expressions;
using System.Security.Claims;

namespace SigmaWeb.Tests
{

	[TestFixture]
	public class CustomerHomeControllerTests
	{
		private Mock<ILogger<HomeController>> _logger;
		private Mock<IUnitOfWork> _unitOfWork;
		private HomeController _homeController;

		[SetUp]
		public void SetUp()
		{

		}
		[Test]
		public void IndexMethodCallsGetAll()
		{
			// mock objects
			var mockLogger = new Mock<ILogger<HomeController>>();
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			var mockProductRepository = new Mock<IProductRepository>();
			// instance of controller with objects
			var controller = new HomeController(mockLogger.Object, mockUnitOfWork.Object);
			// product list
			var productList = new List<Product>
			{
				new Product {
					Id = 1, Title = "HTML Course",
					Description = "An HTML course", Price= 10,
					Author="Sigmacasts", ImageUrl = "/js/images"
				},
				new Product {
					Id = 1, Title = "CSS Course",
					Description = "A CSS course", Price= 20,
					Author="Sigmacasts", ImageUrl = "/js/images"
				}
			};

			// Setup the mock to return the productList when GetAll is called
			mockProductRepository
				.Setup(repo => repo.GetAll(
					It.IsAny<Expression<Func<Product, bool>>>(), // Optional argument 1
					It.IsAny<string>() // Optional argument 2
				))
				.Callback<Expression<Func<Product, bool>>, string>((filter, includeProperties) =>
				{
					// Perform custom logic if needed
				})
				.Returns(productList);

			// Setup the mock unit of work to return the mock product repository
			mockUnitOfWork.Setup(uow => uow.Product).Returns(mockProductRepository.Object);

			// Act
			var result = controller.Index();

			// Assert
			Assert.IsInstanceOf<ViewResult>(result);
			Assert.IsNotNull(result);
			var viewResult = result as ViewResult;
			Assert.AreEqual(productList, viewResult.Model);
			mockProductRepository.Verify(repo => repo.GetAll(
				It.IsAny<Expression<Func<Product, bool>>>(), // Optional argument 1
				It.IsAny<string>() // Optional argument 2
			), Times.Once);
		}


		[Test]
		public void DetailsMethodCallsAView()
		{
			var mockLogger = new Mock<ILogger<HomeController>>();
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			var mockProductRepository = new Mock<IProductRepository>();
			var controller = new HomeController(mockLogger.Object, mockUnitOfWork.Object);
			var product = new Product
			{
				Id = 1,
				Title = "Sample Product",
				Description = "A sample product",
				Price = 10,
				Author = "Sample Author",
				ImageUrl = "/images/sample.jpg"
			};

			// Setup the mock unit of work to return the mock product repository
			mockUnitOfWork.Setup(uow => uow.Product).Returns(mockProductRepository.Object);

			// Act
			var result = controller.Details(product.Id);

			// Assert - Here we are not checking anything related to the shopping cart
			Assert.IsInstanceOf<ViewResult>(result);
			var viewResult = result as ViewResult;
			Assert.IsNotNull(viewResult);

		}

	}

}

	
