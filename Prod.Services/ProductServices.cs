namespace Prod.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Transactions;
    using AutoMapper;
    using Prod.Entities;
    using Prod.DAL;
    using Prod.DAL.UnitOfWork;

    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Prod.Entities.ProductEntity GetProductById(int productId)
        {
            var product = _unitOfWork.ProductRepository.GetByID(productId);
            if (product != null)
            {
                Mapper.CreateMap<Product, ProductEntity>();
                var productModel = Mapper.Map<Product, ProductEntity>(product);
                return productModel;
            }
            return null;
        }

        public IEnumerable<Prod.Entities.ProductEntity> GetAllProducts()
        {
            var products = _unitOfWork.ProductRepository.GetAll().ToList();
            if (products.Any())
            {
                Mapper.CreateMap<Product, ProductEntity>();
                var productsModel = Mapper.Map<List<Product>, List<ProductEntity>>(products);
                return productsModel;
            }
            return null;
        }

        public int CreateProduct(Prod.Entities.ProductEntity productEntity)
        {
            using (var scope = new TransactionScope())
            {
                var product = new Product
                {
                    Name = productEntity.Name,
                    ProductNumber = productEntity.ProductNumber,
                    Color = productEntity.Color,
                    StandardCost = productEntity.StandardCost,
                    ListPrice = productEntity.ListPrice,
                    Size = productEntity.Size,
                    Weight = productEntity.Weight,
                    ProductCategoryID = productEntity.ProductCategoryID,
                    ProductModelID = productEntity.ProductModelID,
                    SellStartDate = productEntity.SellStartDate,
                    SellEndDate = productEntity.SellEndDate,
                    DiscontinuedDate = productEntity.DiscontinuedDate,
                    ThumbNailPhoto = productEntity.ThumbNailPhoto,
                    ThumbnailPhotoFileName = productEntity.ThumbnailPhotoFileName,
                    rowguid = productEntity.rowguid,
                    ModifiedDate = productEntity.ModifiedDate
                };
                _unitOfWork.ProductRepository.Insert(product);
                _unitOfWork.Save();
                scope.Complete();
                return product.ProductID;
            }
        }

        public bool UpdateProduct(int productId, Prod.Entities.ProductEntity productEntity)
        {
            var success = false;
            if (productEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.ProductRepository.GetByID(productId);
                    if (product != null)
                    {
                        product.Name = productEntity.Name;
                        product.ProductNumber = productEntity.ProductNumber;
                        product.Color = productEntity.Color;
                        product.StandardCost = productEntity.StandardCost;
                        product.ListPrice = productEntity.ListPrice;
                        product.Size = productEntity.Size;
                        product.Weight = productEntity.Weight;
                        product.ProductCategoryID = productEntity.ProductCategoryID;
                        product.ProductModelID = productEntity.ProductModelID;
                        product.SellStartDate = productEntity.SellStartDate;
                        product.SellEndDate = productEntity.SellEndDate;
                        product.DiscontinuedDate = productEntity.DiscontinuedDate;
                        product.ThumbNailPhoto = productEntity.ThumbNailPhoto;
                        product.ThumbnailPhotoFileName = productEntity.ThumbnailPhotoFileName;
                        product.rowguid = productEntity.rowguid;
                        product.ModifiedDate = productEntity.ModifiedDate;
                        _unitOfWork.ProductRepository.Update(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteProduct(int productId)
        {
            var success = false;
            if (productId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.ProductRepository.GetByID(productId);
                    if (product != null)
                    {

                        _unitOfWork.ProductRepository.Delete(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
