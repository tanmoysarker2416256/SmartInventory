using SmartInventory.DAL.Context;
using SmartInventory.DAL.Core;
using SmartInventory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.DAL.Interfaces; 

public interface IProductRepository : IRepository<Product , int , SmartInventoryDbContext >
{
    int CountProduct();

}
 