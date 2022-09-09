using Market.Domain.Entity;
using Market.Domain.Interfaces;
using Market.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Service.Interfaces
{
    public interface IProductService
    {
        Task<BaseResponse<IEnumerable<Product>>> GetProducts();

        Task<BaseResponse<IEnumerable<ProductType>>> GetTypes();

        Task<BaseResponse<Product>> GetProduct(int id);

        Task<BaseResponse<bool>> DeleteProduct(int id);
        
        Task<BaseResponse<Product>> GetProductByName(string name);

        Task<BaseResponse<ProductViewModel>> CreateProduct(ProductViewModel model);

        Task<BaseResponse<Product>> Edit(int id, ProductViewModel model);
    }
}
