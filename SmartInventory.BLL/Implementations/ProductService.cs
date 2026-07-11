using SmartInventory.BLL.Helpers;
using SmartInventory.BLL.Interfaces;
using SmartInventory.BLL.Mapping;
using SmartInventory.BLL.Model;
using SmartInventory.Contrct.Request;
using SmartInventory.Contrct.Response;
using SmartInventory.DAL.Interfaces;
using SmartInventory.Model;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.BLL.Implementations;

public class ProductService : IProductService

{
    private readonly IProductUnitOfWork _productUnitOfWork;

    public ProductService(IProductUnitOfWork productUnitOfWork)
    {
        _productUnitOfWork = productUnitOfWork;
    }

 

    public async Task<Result<int>> AddAsync(CreateProductRequest product)
    {
        if (product == null)
        {
            return Result<int>.FailedResult("product can't be null");
        }

        var existingProduct = await _productUnitOfWork.ProductRepository.GetAsync(
            x => x.Id, x => x.Name == product.Name, null, null, false
            );
        if (existingProduct.Any())
        {
            return Result<int>.FailedResult("A product with same name already exist");
        }

        try
        {
          

            var newProduct = product.MapToProduct();


            await _productUnitOfWork.ProductRepository.AddAsync(newProduct);
            var saved =  await _productUnitOfWork.SaveChanegsAsync();

            if(!saved)
            {
                return Result<int>.FailedResult("failed to save the product ");
            }
            return Result<int>.SuccessResult(newProduct.Id);
        }
        catch (Exception)
        {
            return Result<int>.FailedResult("A error occour while adding the product ");
        }

        
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var product = await _productUnitOfWork.ProductRepository.GetByIdAsync(id);

        if (product == null)
        {
            return Result<bool>.FailedResult("product not found ");
        }

        await _productUnitOfWork.ProductRepository.DeleteAsync(product);
        var saved = await _productUnitOfWork.SaveChanegsAsync();

        if (!saved)
        {
            return Result<bool>.FailedResult("failed to delet the product ");
        }
        return Result<bool>.SuccessResult(true);
    }


    public async Task< Result<IList<Product>>> GetAllAsync()
    {
         var products = await _productUnitOfWork.ProductRepository.GetAsync(x=>x , null ,
             x=>x.OrderByDescending(x=>x.Id), null , true);

        return Result<IList<Product>>.SuccessResult(products);
    }

    public async Task<Result<Product>> GetByIdAsync(int id)
    {
        var product = await _productUnitOfWork.ProductRepository.GetByIdAsync(id);
        
        if(product == null)
        {
            return Result<Product>.FailedResult($"product with {id} is not found ");
        }
        return Result<Product>.SuccessResult(product);
    }


    public async Task<Result<int>> UpdateAsync(Product product)
    {
        if (product == null)
        {
            return Result<int>.FailedResult("product can't be null");
        }

        var existproduct = await _productUnitOfWork.ProductRepository.GetByIdAsync(product.Id);
        if (existproduct == null)
        {
            return Result<int>.FailedResult($"product with {product.Id} is not found");
        }

       
            
            await _productUnitOfWork.ProductRepository.UpdateAsync(product);
           var saved =  await _productUnitOfWork.SaveChanegsAsync();

        if (!saved)
        {
            return Result<int>.FailedResult("failed to update the product ");
        }
        return Result<int>.SuccessResult(existproduct .Id);

    }


    public async Task<DataTableResponse<Product>>GetDataTableAsync(DataTableRequest request)
    {
        try
        {
            //builder search predicate 

            var searchPredicate = DataTableHelper.BuilderSearchPredicate<Product>(
                request,
                SearchValue =>
                {
                    var lowerSearch = SearchValue.ToLower();
                    return p =>
                    p.Name.ToLower().Contains(lowerSearch) ||
                    p.Description.ToLower().Contains(lowerSearch) ||
                    p.Price.ToString().Contains(SearchValue);
                }
                );

            //build order by expression 

            Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null;

            if(request.Order != null && request.Order.Any() && request.Column != null)
            {
                var order = request.Order.First();
                var columnIndex = order.Column;
                var isAscending = order.Dir.ToLower() == "asc";

                if(columnIndex>=0 && columnIndex < request.Column.Count)
                {
                    var column = request.Column[columnIndex];
                    var columnKey = column.Data.ToLower();

                    orderBy = columnKey switch
                    {
                        "name" => isAscending
                         ? q => q.OrderBy(p => p.Name)
                         : q => q.OrderByDescending(p => p.Name),
                        "description" => isAscending
                         ? q => q.OrderBy(p => p.Description)
                         : q => q.OrderByDescending(p => p.Description),
                        "price" => isAscending
                        ? q => q.OrderBy(p => p.Price)
                        : q => q.OrderByDescending(p => p.Price),
                        "stockquantity" => isAscending
                         ? q => q.OrderBy(p => p.StockQuantity)
                         : q => q.OrderByDescending(p => p.StockQuantity),
                        _ => null
                    };
                }
            }

            
            // default ordering if no order specificed
           
            orderBy ??= q => q.OrderByDescending(p => p.Id);

            //calculate pagination
            var (pageIndex, pageSize) = DataTableHelper.CalculatePagination(request);

            //get data from repository 
           var (items , total , totalFilter ) = await _productUnitOfWork.ProductRepository.GetAsync(
                p => p,
                searchPredicate,
                orderBy,
                null,
                pageIndex,
                pageSize,
                true
                );
            return new DataTableResponse<Product>
            {
                 Draw =request.Draw,
                 RecordsTotal = total,
                 RecordsFiltered = totalFilter,
                 Data = items.ToList()
            };

        }
        catch(Exception) 
        {
            throw; 
        }

    }

}
