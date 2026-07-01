using SmartInventory.DAL.Context;
using SmartInventory.DAL.Core;
using SmartInventory.DAL.Interfaces;
using SmartInventory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.DAL.Implementation;

public class ProductRepository : Repository<Product, int, SmartInventoryDbContext>, IProductRepository
{

    public ProductRepository(SmartInventoryDbContext dbcontext ) : base( dbcontext )
    {
        
    }

    public int CountProduct()
    {
        return _dbset.Count();
    }

 
}
