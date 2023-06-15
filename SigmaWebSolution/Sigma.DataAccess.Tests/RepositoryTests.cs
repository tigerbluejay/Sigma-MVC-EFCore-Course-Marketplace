using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Sigma.DataAccess.Data;
using Sigma.DataAccess.Repository;
using Sigma.Models;
using System.Collections;

namespace Sigma.DataAccess.Tests
{
	[TestFixture]
	public class RepositoryTests
	{
		private DbContextOptions<ApplicationDbContext> options;
		private Product product_One;
		private Product product_Two;

		public RepositoryTests()
		{
			product_One = new Product()
			{
				Id = 1,
				Title = "HTML Course",
				Description = "A course on HTML",
				Author = "Sigmacasts",
				Price = 10,
				ImageUrl = "/js/images/"
			};
			product_Two = new Product()
			{
				Id = 2,
				Title = "CSS Course",
				Description = "A course on CSS",
				Author = "Sigmacasts",
				Price = 20,
				ImageUrl = "/js/images/"
			};

		}

		[SetUp]
		public void SetUp()
		{
			// Arrange - We create an in-memory database
			options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: "tempSigma").Options;
		}

		[Test]
		[Order(1)]
		public void ProductPersistedToDb()
		{
			using (var unitOfWork = CreateUnitOfWork())
			{
				var productRepository = unitOfWork.Product;

				// Act - We add entry to the database
				productRepository.Add(product_One);
				unitOfWork.Save();
			}

			using (var unitOfWork = CreateUnitOfWork())
			{
				var productRepository = unitOfWork.Product;

				// Assert - We make sure everything was added successfully
				var productFromDb = productRepository.GetFirstOrDefault(u => u.Id == 1);
				Assert.AreEqual(product_One.Id, productFromDb.Id);
				Assert.AreEqual(product_One.Title, productFromDb.Title);
				Assert.AreEqual(product_One.Description, productFromDb.Description);
				Assert.AreEqual(product_One.Author, productFromDb.Author);
				Assert.AreEqual(product_One.Price, productFromDb.Price);
				Assert.AreEqual(product_One.ImageUrl, productFromDb.ImageUrl);
			}
		}

		private UnitOfWork CreateUnitOfWork()
		{
			var dbContext = new ApplicationDbContext(options);
			return new UnitOfWork(dbContext);
		}

		[Test]
		[Order(2)]
		public void AllProductsPersistedToDb() // GetAll()
		{
			// Arrange - We add All Bookings
			var expected = new List<Product> { product_One, product_Two };

			using (var unitOfWork = CreateUnitOfWork())
			{
				var context = new ApplicationDbContext(options);
				context.Database.EnsureDeleted(); // we must delete the records from the previous
												  // test else there will be problems when running Book(studyRoomBooking_One) below.
				var productRepository = unitOfWork.Product;
				productRepository.Add(product_One);
				productRepository.Add(product_Two);
				unitOfWork.Save();

			}
				// Act - Get All Bookings
			List<Product> result;
			using (var unitOfWork = CreateUnitOfWork())
			{ 
				var productRepository = unitOfWork.Product;
				result = productRepository.GetAll().ToList();
			}
			// Assert - We made sure everything was added successfully.
			// We use CollectionAssert to use BookingCompare
			// Booking Compare compares objects, so it compares the expected list
			// with the two bookings, with the result list using the GetAll() method
			CollectionAssert.AreEqual(expected, result, new ProductCompare());
			
		}

		// this is a class
		private class ProductCompare : IComparer
		{
			public int Compare(object x, object y)
			{
				var product1 = (Product)x;
				var product2 = (Product)y;

				if (product1.Id != product2.Id)
				{
					return 1;
				}
				else
				{
					return 0;
				}
			}
		}
	}
}