using SmartInventory.BLL.Interfaces;
using SmartInventory.BLL.Mapping;
using SmartInventory.BLL.Model;
using SmartInventory.Contrct.Request;
using SmartInventory.DAL.Interfaces;
using SmartInventory.Model;
using System;
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
}
