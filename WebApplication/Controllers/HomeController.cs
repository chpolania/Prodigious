using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http.Formatting;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:50875");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return View(GetAllProducts());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Prod.Entities.ProductEntity model)
        {
            bool stateTransaction = false;
            if (ModelState.IsValid)
            {
                stateTransaction = AddProduct(model);
                if (stateTransaction)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "La solicitud no pudo ser procesada.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Faltan algunos campos necesarios para crear el elemento.");
            }

            
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            return View(GetProduct(id));
        }

        [HttpPost]
        public ActionResult Edit(Prod.Entities.ProductEntity model)
        {
            bool stateTransaction = false;
            if (ModelState.IsValid)
            {
                stateTransaction = EditProduct(model);
                if (stateTransaction)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "La solicitud no pudo ser procesada.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Faltan algunos campos necesarios para crear el elemento.");
            }


            return View(model);
        }

        public ActionResult Details(int id)
        {
            return View(GetProduct(id));
        }

        private List<Prod.Entities.ProductEntity> GetAllProducts()
        {
            List<Prod.Entities.ProductEntity> ret = null;
            HttpClient Client = StartClient();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("v1/Products/Product/allproducts").Result;
            if (response.IsSuccessStatusCode)
            {
                ret = response.Content.ReadAsAsync<List<Prod.Entities.ProductEntity>>().Result;
            }
            return ret;
        }

        private Prod.Entities.ProductEntity GetProduct(int id)
        {
            HttpClient Client = StartClient(); ;
            Prod.Entities.ProductEntity ret = null;
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync(string.Format("v1/Products/Product/allproducts?id={0}", id)).Result;
            if (response.IsSuccessStatusCode)
            {
                ret = response.Content.ReadAsAsync<Prod.Entities.ProductEntity>().Result;
            }
            return ret;

        }

        private bool AddProduct(Prod.Entities.ProductEntity newProduct)
        {
            int ret = 0;
            HttpClient Client = StartClient();
            Uri urlInvocada = new Uri(ConfigurationManager.AppSettings["ApiUrl"] + "/v1/Products/Product/Create");
            HttpResponseMessage response = Client.PostAsync(urlInvocada.AbsoluteUri, newProduct, new JsonMediaTypeFormatter()).Result;
            if (response.IsSuccessStatusCode)
            {
                ret = response.Content.ReadAsAsync<int>().Result;
            }

            return ret > 0 ? true : false;
        }

        private bool EditProduct(Prod.Entities.ProductEntity product)
        {
            bool ret = false;
            HttpClient Client = StartClient();

            Uri urlInvocada = new Uri(ConfigurationManager.AppSettings["ApiUrl"] + "/v1/Products/Product/Update/productid/" + product.ProductID);
            HttpResponseMessage response = Client.PutAsync(urlInvocada.AbsoluteUri, product, new JsonMediaTypeFormatter()).Result;
            if (response.IsSuccessStatusCode)
            {
                ret = response.Content.ReadAsAsync<bool>().Result;
            }

            return ret;
        }

        private HttpClient StartClient()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
            Client.DefaultRequestHeaders.Add("Token", "4a6ec0ad-b643-44c4-9cc8-18cb88795880");
            return Client;
        }
    }
}