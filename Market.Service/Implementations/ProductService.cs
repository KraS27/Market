using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Interfaces;
using Market.Domain.ViewModels;
using Market.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Market.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;       

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<BaseResponse<bool>> DeleteProduct(int id)
        {
            var baseResponse = new BaseResponse<bool>()
            {
                Data = true
            };

            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                {
                    baseResponse.Data = false;
                    baseResponse.Status = Domain.Enum.StatusCode.NotFound;
                }
                else
                {
                    baseResponse.Status = Domain.Enum.StatusCode.Ok;
                }

                return baseResponse;
            }
            catch(Exception ex)
            {
                baseResponse.Data = false;
                baseResponse.Description = $"[DeleteProduct]: {ex.Message}";
                baseResponse.Status = Domain.Enum.StatusCode.InternalServerError;

                return baseResponse;
            }
        }

        public async Task<BaseResponse<Product>> GetProductByName(string name)
        {
            var baseResponse = new BaseResponse<Product>();

            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Caption == name);

                if (product == null)
                {
                    baseResponse.Description = "Product Not Found";
                    baseResponse.Status = Domain.Enum.StatusCode.NotFound;
                }
                else
                {
                    baseResponse.Data = product;
                    baseResponse.Status = Domain.Enum.StatusCode.Ok;
                }

                return baseResponse;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[GetProduct]: {ex.Message}";
                baseResponse.Status = Domain.Enum.StatusCode.InternalServerError;

                return baseResponse;
            }
        }

        public async Task<BaseResponse<Product>> GetProduct(int id)
        {
            var baseResponse = new BaseResponse<Product>();

            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                {                    
                    baseResponse.Description = "Product Not Found";
                    baseResponse.Status = Domain.Enum.StatusCode.NotFound;
                }
                else
                {
                    baseResponse.Data = product;
                    baseResponse.Status = Domain.Enum.StatusCode.Ok;
                }
 
                return baseResponse;
            }
            catch(Exception ex)
            {       
                baseResponse.Description = $"[GetProduct]: {ex.Message}";
                baseResponse.Status = Domain.Enum.StatusCode.InternalServerError;

                return baseResponse;
            }
        }

        public async Task<BaseResponse<IEnumerable<Product>>> GetProducts()
        {
            var baseResponse = new BaseResponse<IEnumerable<Product>>();
            try
            {
                var products = await _productRepository.GetAll().ToListAsync();

                if(products.Count() == 0)
                {
                    baseResponse.Description = "Элементы не найдены";
                    baseResponse.Status = Domain.Enum.StatusCode.NotFound;
                }
                else
                {
                    baseResponse.Data = products;
                    baseResponse.Status = Domain.Enum.StatusCode.Ok;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[GetProducts]: {ex.Message}";
                baseResponse.Status = Domain.Enum.StatusCode.InternalServerError;

                return baseResponse;
            }
        }

        public async Task<BaseResponse<ProductViewModel>> CreateProduct(ProductViewModel model)
        {
            var baseResponse = new BaseResponse<ProductViewModel>();
            
            try
            {            

                byte[] imageData = null;
                
                using (var binaryReader = new BinaryReader(model.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.Image.Length);
                }
                                
                var product = new Product()
                {
                    Id = model.Id,
                    Caption = model.Caption,
                    Price = model.Price,
                    Description = model.Description,
                    ProductType = await _productRepository.GetTypes()
                                           .FirstOrDefaultAsync(x => x.Name == model.ProductType.Name),
                    Image = imageData
                };

                await _productRepository.Create(product);
            }
            catch (Exception ex)
            {                
                baseResponse.Status = Domain.Enum.StatusCode.InternalServerError;
                baseResponse.Description = $"[CreateProduct]: {ex.Message}";            
            }

            return baseResponse;
        }

        public async Task<BaseResponse<Product>> Edit(int id, ProductViewModel model)
        {
            var baseResponse = new BaseResponse<Product>();

            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                {
                    baseResponse.Description = "Product Not Found";
                    baseResponse.Status = Domain.Enum.StatusCode.NotFound;

                    return baseResponse;
                }
                else
                {
                    product.Id = model.Id;
                    product.Caption = model.Caption;
                    product.Price = model.Price;

                    await _productRepository.Update(product);

                    return baseResponse;
                }               
            }
            catch(Exception ex)
            {
                baseResponse.Status = Domain.Enum.StatusCode.InternalServerError;
                baseResponse.Description = $"[CreateProduct]: {ex.Message}";

                return baseResponse;
            }
        }

        public async Task<BaseResponse<IEnumerable<ProductType>>> GetTypes()
        {
            var baseResponse = new BaseResponse<IEnumerable<ProductType>>();
            try
            {
                var types = await _productRepository.GetTypes().ToArrayAsync();

                if (types.Count() == 0)
                {
                    baseResponse.Description = "Элементы не найдены";
                    baseResponse.Status = Domain.Enum.StatusCode.NotFound;
                }
                else
                {
                    baseResponse.Data = types;
                    baseResponse.Status = Domain.Enum.StatusCode.Ok;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[GetProducts]: {ex.Message}";
                baseResponse.Status = Domain.Enum.StatusCode.InternalServerError;

                return baseResponse;
            }
        }
    }
}
