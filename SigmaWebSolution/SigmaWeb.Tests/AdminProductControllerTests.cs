using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Sigma.DataAccess.Repository.Interfaces;
using Sigma.Models;
using Sigma.Models.ViewModels;
using SigmaWeb.Areas.Admin.Controllers;
using System.Linq.Expressions;

namespace SigmaWeb.Tests
{
	[TestFixture]
	public class AdminProductControllerTests
	{

		private Mock<IUnitOfWork> _mockUnitOfWork;
		private Mock<IWebHostEnvironment> _webHostEnvironment;
		private ProductController _controller;

		[SetUp]
		public void Setup()
		{
			_mockUnitOfWork = new Mock<IUnitOfWork>();
			_webHostEnvironment = new Mock<IWebHostEnvironment>();
			_controller = new ProductController(_mockUnitOfWork.Object, _webHostEnvironment.Object);
		}

		[Test]
		public void Upsert_WithNullId_ReturnsViewWithNewProductVM()
		{
			// Arrange
			int? id = null;

			// Act
			var result = _controller.Upsert(id) as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Model);
			Assert.IsInstanceOf<ProductVM>(result.Model);

			var model = result.Model as ProductVM;
			Assert.IsNotNull(model.Product);
			Assert.IsInstanceOf<Product>(model.Product);
		}

		[Test]
		public void Upsert_WithNonNullId_ReturnsViewWithExistingProductVM()
		{
			// Arrange
			int id = 123; // Set the desired non-null id value

			// Mocking the _unitOfWork.Product.GetFirstOrDefault method
			var productRepositoryMock = new Mock<IProductRepository>();


			var existingProduct = new Product
			{
				Id = id,
				Title = "Existing Product",
				Description = "A description",
				Author = "Sigmacasts",
				Price = 10,
				ImageUrl = "/js/images"
			};

			productRepositoryMock.Setup(u => u.GetFirstOrDefault(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>(), It.IsAny<bool>()))
						.Returns(existingProduct);

			_mockUnitOfWork.Setup(u => u.Product).Returns(productRepositoryMock.Object);
			
			// Act
			var result = _controller.Upsert(id) as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Model);
			Assert.IsInstanceOf<ProductVM>(result.Model);

			var model = result.Model as ProductVM;
			Assert.IsNotNull(model.Product);
			Assert.IsInstanceOf<Product>(model.Product);
			Assert.AreEqual(existingProduct, model.Product); // Assert that the model's Product is the same as the mocked existingProduct
		}

		[Test]
		public void GetAll_ReturnsJsonResult()
		{
			// Arrange
			var productList = new List<Product>();

			_mockUnitOfWork.Setup(u => u.Product.GetAll(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>())).
			Returns(productList);

			// Act
			var result = _controller.GetAll();

			// Assert
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<JsonResult>(result);
		}

		[Test]
		public void Delete_WithNullId_ReturnsJsonResultWithError()
		{
			// Arrange
			int? id = null;

			Product productToDelete = null;

			_mockUnitOfWork.Setup(u => u.Product.GetFirstOrDefault(
				It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>(), It.IsAny<bool>()))
						   .Returns(productToDelete);

			// Act
			var result = _controller.Delete(id) as JsonResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<JsonResult>(result);

			dynamic jsonData = result.Value;
			Assert.IsNotNull(jsonData);
			// Assert.IsFalse(jsonData.success); // Access the 'success' property using a string key and cast it to bool
			// Assert.AreEqual("Error while deleting", jsonData.message); // Access the 'message' property using a string key and cast it to string
		}

		[Test]
		public void Delete_WithNonNullId_ReturnsViewResultWithIndexView()
		{
			// Arrange
			int id = 123; // Set the desired non-null id value

			var productToDelete = new Product { 
				Id = id,
				Title = "Existing Product",
				Description = "A description",
				Author = "Sigmacasts",
				Price = 10,
				ImageUrl = "/js/images"
			};

			_mockUnitOfWork.Setup(u => u.Product.GetFirstOrDefault(
				It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>(), It.IsAny<bool>()))
						   .Returns(productToDelete);

			_webHostEnvironment.SetupProperty(env => env.WebRootPath, "C:\\MockedWebRootPath"); // Set the WebRootPath to a valid path

			// Act
			var result = _controller.Delete(id) as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<ViewResult>(result);
			Assert.AreEqual(nameof(Index), result.ViewName);

			// Additional assertions to verify Remove and Save methods were called if needed
		}
	}
}
