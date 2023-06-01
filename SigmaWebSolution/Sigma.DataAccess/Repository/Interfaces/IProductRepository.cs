using Sigma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.DataAccess.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
    }
}
