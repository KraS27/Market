using Market.Domain.ViewModels;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;        

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await productService.DeleteProduct(id);

            if(response.Status == Domain.Enum.StatusCode.Ok)
            {
                return RedirectToAction("GetAllProducts");
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var response = await productService.GetTypes();

            if(response.Status == Domain.Enum.StatusCode.Ok)
            {
                return View(new ProductViewModel
                {
                    ProductTypes = response.Data
                });
            }
            return RedirectToAction("Error");           
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                await productService.CreateProduct(model);
            }

            return RedirectToAction("GetAllProducts");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {           
            var response = await productService.GetProducts();
            
            if (response.Status == Domain.Enum.StatusCode.Ok)
            {
                return View(response.Data.ToList());
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            var response = await productService.GetProduct(id);

            if (response.Status == Domain.Enum.StatusCode.Ok)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var response = await productService.GetProductByName(name);

            if (response.Status == Domain.Enum.StatusCode.Ok)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }
    }
}
