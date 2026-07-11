using Microsoft.AspNetCore.Mvc;
using SmartInventory.BLL.Interfaces;
using SmartInventory.BLL.Model;
using SmartInventory.Contrct.Request;
using SmartInventory.Contrct.Response;
using SmartInventory.Model;

namespace SmartInventory.web.Controllers; 

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public IActionResult Index()

    {   
      // var products = await _productService.GetAllAsync();
        return View();
    }

    [HttpPost]
    [IgnoreAntiforgeryToken]

    public async Task<IActionResult> GetDataTables([FromForm] DataTableRequest request)
    {
        if(request == null)
        {
            return BadRequest(new DataTableResponse<Product>
            {
                Draw = 0,
                RecordsTotal = 0,
                RecordsFiltered = 0,
                Data = new List<Product>(),
                Error = "Invalid request"
            });
        }
        var response = await _productService.GetDataTableAsync(request);
        return Json(response);
    }












    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest product )

    {
        if(ModelState.IsValid==false)
        {
            return View(product);
        }

         var result= await _productService.AddAsync(product);

        if(result.Success)
        {
            TempData["SuccessMessage"] = "Product created Successfully!";
            return RedirectToAction("Index");
        }

        else
        {
            TempData["ErrorMessage"] = result.Error;
            return View(product);
        }

    }


    public async Task<IActionResult>Delete(int id)
    {
        var result = await _productService.DeleteAsync(id);
        if(result.Success)
        {
            return RedirectToAction("Index");
        }

         ModelState.AddModelError(string.Empty, result.Error ?? "an error occured");
        return BadRequest();
    }

}
