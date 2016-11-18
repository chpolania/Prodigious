namespace Prod.Services
{
    using System.Collections.Generic;
    using Prod.Entities;

    public interface IProductServices
    {
        ProductEntity GetProductById(int productId);
        IEnumerable<ProductEntity> GetAllProducts();
        int CreateProduct(ProductEntity productEntity);
        bool UpdateProduct(int productId,ProductEntity productEntity);
        bool DeleteProduct(int productId);
    }
}
