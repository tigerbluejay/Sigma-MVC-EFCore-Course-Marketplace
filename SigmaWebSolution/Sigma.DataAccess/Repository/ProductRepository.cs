using Sigma.DataAccess.Repository.Interfaces;
using Sigma.Models;
using Sigma.DataAccess.Data;
using Sigma.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            // we use this emplementation to demonstrate the way in which we can
            // update just the properties we want. Although in this example all
            // properties are updated. Plus, it's useful to handle the ImageUrl
            // scenario explained below.
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.Price = obj.Price;
                objFromDb.Author = obj.Author;
                // here we need to check because if the obj has no image url and we update,
                // the existing image from previously will be gone.
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }

        }
    }
}
