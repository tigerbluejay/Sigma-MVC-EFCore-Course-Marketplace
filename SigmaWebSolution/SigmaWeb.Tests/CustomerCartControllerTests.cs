using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Sigma.DataAccess.Repository.Interfaces;
using Sigma.Models;
using Sigma.Utilities;
using SigmaWeb.Areas.Customer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static SigmaWeb.Areas.Customer.Controllers.CartController;

namespace SigmaWeb.Tests
{
	[TestFixture]
	public class CustomerCartControllerTests
	{

		private Mock<IUnitOfWork> _mockUnitOfWork;
		private Mock<EmailSender> _mockemailSender;
		private Mock<IShoppingCartRepository> _mockShoppingCartRepository;
		private Mock<HttpContext> _mockHttpContext;
		private Mock<ISession> _mockSession;
		private CartController _controller;
		


		[SetUp]
		public void Setup()
		{
			_mockUnitOfWork = new Mock<IUnitOfWork>();
			_mockemailSender = new Mock<EmailSender>();
			_mockShoppingCartRepository = new Mock<IShoppingCartRepository>();
			_mockUnitOfWork.Setup(uow => uow.ShoppingCart).Returns(_mockShoppingCartRepository.Object);
			_mockHttpContext = new Mock<HttpContext>();
			_mockSession = new Mock<ISession>();
			_controller = new CartController(_mockUnitOfWork.Object, _mockemailSender.Object);
		}

		[Test]
		public void Plus_WhenCartExists_ReturnsRedirectToActionResult()
		{
			// Arrange
			int cartId = 1;
			var cart = new ShoppingCart { Id = cartId };

			_mockShoppingCartRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<ShoppingCart, bool>>>(),
				It.IsAny<string>(), It.IsAny<bool>()))
			.Returns(cart);

			// Act
			var result = _controller.Plus(cartId);

			// Assert
			Assert.IsInstanceOf<RedirectToActionResult>(result);
			Assert.AreEqual(nameof(HomeController.Index), (result as RedirectToActionResult)?.ActionName);

			_mockShoppingCartRepository.Verify(repo => repo.IncrementCount(cart, 1), Times.Once);
			_mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
		}

		[Test]
		public void Minus_WhenCartExistsWithCountGreaterThan1_DecreasesCartCount()
		{
			// Arrange
			int cartId = 1;
			var cart = new ShoppingCart { Id = cartId, Count = 2 }; // cart with count greater than 1

			_mockShoppingCartRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<ShoppingCart, bool>>>(),
				It.IsAny<string>(), It.IsAny<bool>()))
				.Returns(cart);

			// Act
			var result = _controller.Minus(cartId);

			// Assert
			Assert.IsInstanceOf<RedirectToActionResult>(result);
			Assert.AreEqual(nameof(HomeController.Index), (result as RedirectToActionResult)?.ActionName);

			_mockShoppingCartRepository.Verify(repo => repo.DecrementCount(cart, 1), Times.Once);
			_mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
		}

		[Test]
		public void Minus_WhenCartExistsWithCountEquals1_RemovesCartAndDecreasesCount()
		{
			// Arrange
			int cartId = 1;
			var cart = new ShoppingCart { Id = cartId, Count = 1 }; // cart with count equal to 1

			_mockShoppingCartRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<ShoppingCart, bool>>>()
				, It.IsAny<string>(), It.IsAny<bool>()))
				.Returns(cart);
			_mockShoppingCartRepository.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<ShoppingCart, bool>>>()
				, It.IsAny<string>()))
				.Returns(new List<ShoppingCart> { cart }); // return the same cart in GetAll

			_mockHttpContext.SetupGet(context => context.Session).Returns(_mockSession.Object);
			_controller.ControllerContext.HttpContext = _mockHttpContext.Object;


			var mockSessionWrapper = new Mock<ISessionWrapper>();
			mockSessionWrapper.Setup(session => session.Set("SessionCart", It.IsAny<int>()))
				.Callback<string, int>((key, value) => _mockSession.Object.SetInt32(key, value));
			_controller.sessionWrapper = mockSessionWrapper.Object;

			// Act
			var result = _controller.Minus(cartId);

			// Assert
			Assert.IsInstanceOf<RedirectToActionResult>(result);
			Assert.AreEqual(nameof(HomeController.Index), (result as RedirectToActionResult)?.ActionName);

			_mockShoppingCartRepository.Verify(repo => repo.Remove(cart), Times.Once);
			_mockShoppingCartRepository.Verify(repo => repo.GetAll(It.IsAny<Expression<Func<ShoppingCart, bool>>>(), It.IsAny<string>()), Times.Once);
			_mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
		}

		[Test]
		public void Remove_WhenCartExists_RemovesCartAndUpdatesCount()
		{
			// Arrange
			int cartId = 1;
			var cart = new ShoppingCart { Id = cartId, ApplicationUserId = "testUser" };
			var carts = new List<ShoppingCart> { cart };

			_mockShoppingCartRepository.Setup(repo => repo.GetFirstOrDefault(It.IsAny<Expression<Func<ShoppingCart, bool>>>(),
				It.IsAny<string>(), It.IsAny<bool>()))
				.Returns(cart);
			_mockShoppingCartRepository.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<ShoppingCart, bool>>>(),
				It.IsAny<string>()))
				.Returns(carts);
			_mockHttpContext.SetupGet(context => context.Session).Returns(_mockSession.Object);
			_controller.ControllerContext.HttpContext = _mockHttpContext.Object;

			int expectedCount = carts.Count - 1;
			
			var mockSessionWrapper = new Mock<ISessionWrapper>();
			mockSessionWrapper.Setup(session => session.Set("SessionCart", It.IsAny<int>()))
				.Callback<string, int>((key, value) => _mockSession.Object.SetInt32(key, value));
			_controller.sessionWrapper = mockSessionWrapper.Object;


			// Act
			var result = _controller.Remove(cartId);

			// Assert
			Assert.IsInstanceOf<RedirectToActionResult>(result);
			Assert.AreEqual(nameof(HomeController.Index), (result as RedirectToActionResult)?.ActionName);

			_mockShoppingCartRepository.Verify(repo => repo.Remove(cart), Times.Once);
			_mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
			_mockShoppingCartRepository.Verify(repo => repo.GetAll(It.IsAny<Expression<Func<ShoppingCart, bool>>>(),
				It.IsAny<string>()), Times.Once);
		}
	}

}
