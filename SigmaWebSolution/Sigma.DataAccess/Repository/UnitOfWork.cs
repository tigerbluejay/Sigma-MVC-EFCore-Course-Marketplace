using Sigma.DataAccess.Repository.Interfaces;
using Sigma.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            
            Product = new ProductRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            
        }

        
        public IProductRepository Product { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        
        public void Save()
        {
            _db.SaveChanges();
        }
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

	    protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_db.Dispose();
			}
		}
	}
}
