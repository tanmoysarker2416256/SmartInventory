using SmartInventory.Contrct.Request;
using SmartInventory.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.BLL.Mapping; 

public static class ContractMapping
{
    public static Product MapToProduct(this CreateProductRequest request)
    {
        return new Product
        {
            Name = request.Name,

            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            CreateTime = DateTime.Now,
            CreatedBy = 1
        };
    }
}
