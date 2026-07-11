using SmartInventory.BLL.Model;
using SmartInventory.Contrct.Request;
using SmartInventory.Contrct.Response;
using SmartInventory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.BLL.Interfaces; 

public interface IProductService

{

    Task<Result<IList<Product>>> GetAllAsync();

    Task<Result<Product>> GetByIdAsync(int id);
   Task< Result<int>> AddAsync (CreateProductRequest product);

    Task<Result<int>> UpdateAsync (Product product);

    Task<Result<bool>> DeleteAsync (int id);
    Task<DataTableResponse<Product>> GetDataTableAsync(DataTableRequest request);
}
