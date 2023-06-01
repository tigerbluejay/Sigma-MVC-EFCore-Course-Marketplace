using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        /*
        IProductRepository Product { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IOrderHeaderRepository OrderHeader { get; }
        */
        void Save();
    }
}
