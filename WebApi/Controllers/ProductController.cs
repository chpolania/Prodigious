namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.OData.Query;
    using AttributeRouting;
    using AttributeRouting.Web.Http;
    using Prod.Entities;
    using Prod.Services;
    using WebApi.ActionFilters;
    using WebApi.ErrorHelper;

    [AuthorizationRequired]
    [RoutePrefix("v1/Products/Product")]
    public class ProductController : ApiController
    {
        private readonly IProductServices _productServices;

        /// <summary>
        /// Constructor que inicia la instancia del servicio.
        /// </summary>
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        /// <summary>
        /// Obtener todos los productos.
        /// </summary>
        /// <returns>Retorna lista de productos.</returns>
        [Queryable]
        [GET("allproducts")]
        public HttpResponseMessage Get()
        {
            var products = _productServices.GetAllProducts().AsQueryable();
            var productEntities = products as List<ProductEntity> ?? products.ToList();
            if (productEntities.Any())
                return Request.CreateResponse(HttpStatusCode.OK, productEntities.AsQueryable());
            throw new ApiDataException(1000, "Products not found", HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Obternet solo un producto.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un producto.</returns>
        public HttpResponseMessage Get(int id)
        {
            if (id > 0)
            {
                var product = _productServices.GetProductById(id);
                if (product != null)
                    return Request.CreateResponse(HttpStatusCode.OK, product);

                throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        /// <summary>
        /// Crear un producto.
        /// </summary>
        /// <param name="productEntity"></param>
        /// <returns>Retorna id del producto.</returns>
        [POST("Create")]
        public int Post([FromBody] ProductEntity productEntity)
        {
            return _productServices.CreateProduct(productEntity);
        }

        /// <summary>
        /// Actualizar un producto.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productEntity"></param>
        /// <returns>Retorna si se pudo o no actualizar.</returns>
        [PUT("Update/productid/{id}")]
        public bool Put(int id, [FromBody] ProductEntity productEntity)
        {
            return id > 0 && _productServices.UpdateProduct(id, productEntity);
        }
    }
}
