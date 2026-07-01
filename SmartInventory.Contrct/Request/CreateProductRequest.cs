using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Contrct.Request;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }
    

}
