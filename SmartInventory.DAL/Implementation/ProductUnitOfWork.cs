using Microsoft.EntityFrameworkCore;
using SmartInventory.DAL.Context;
using SmartInventory.DAL.Core;
using SmartInventory.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.DAL.Implementation;

public class ProductUnitOfWork : UnitOfWork, IProductUnitOfWork
{
    public ProductUnitOfWork(SmartInventoryDbContext context , IProductRepository productRepository) : base(context)
    {
          ProductRepository = productRepository;
    }

    public IProductRepository ProductRepository {  get;  }
}
