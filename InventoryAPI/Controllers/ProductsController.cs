using InventoryAPI.Models;
using InventoryAPI.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace InventoryAPI.Controllers
{
    public class ProductsController : ApiController
    {
        public string apiVersion = Resources.API_INFO.API_VERSION;
        private ProductServices productService = new ProductServices();

        [HttpGet]
        [Route("inventory/products")]
        public IHttpActionResult GetProducts()
        {
            var productList = productService.GetProduct();

            HttpContext.Current.Response.AppendHeader("API-Version", apiVersion);
            HttpContext.Current.Response.AppendHeader("HTTP-Method", "GET");
            HttpContext.Current.Response.AppendHeader("Response-Type", "JSON");
            HttpContext.Current.Response.AppendHeader("Response-Object-Length", productList.Count.ToString() + " Items");
            HttpContext.Current.Response.AppendHeader("List-URL", Request.RequestUri.AbsoluteUri);

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(productList, serializerSettings);
        }

        [HttpGet]
        [Route("inventory/products/{order}/{orderby}")]
        public IHttpActionResult GetProductOrder(string order, string orderBy)
        {
            var productList = productService.GetProductOrder(order.Trim(), orderBy.Trim());

            HttpContext.Current.Response.AppendHeader("API-Version", apiVersion);
            HttpContext.Current.Response.AppendHeader("HTTP-Method", "GET");
            HttpContext.Current.Response.AppendHeader("Response-Type", "JSON");
            HttpContext.Current.Response.AppendHeader("Response-Object-Length", productList.Count.ToString() + " Items");
            HttpContext.Current.Response.AppendHeader("List-URL", Request.RequestUri.AbsoluteUri);

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(productList, serializerSettings);
        }

        [HttpGet]
        [Route("inventory/products/{id}")]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = productService.GetProduct(id);

            HttpContext.Current.Response.AppendHeader("API-Version", apiVersion);
            HttpContext.Current.Response.AppendHeader("HTTP-Method", "GET");

            if (product == null)
            {
                HttpContext.Current.Response.AppendHeader("ERROR", "(404) Resource Not Found");
                return NotFound();
            }
            else
            {
                HttpContext.Current.Response.AppendHeader("Response-Type", "JSON");
                HttpContext.Current.Response.AppendHeader("Object-URL", Request.RequestUri.AbsoluteUri);
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
                return Json(product, serializerSettings);
            }
        }

        [HttpGet]
        [Route("inventory/products/name/{name}")]
        public IHttpActionResult GetProduct(string name)
        {
            Product product = productService.GetProduct(name);

            HttpContext.Current.Response.AppendHeader("API-Version", apiVersion);
            HttpContext.Current.Response.AppendHeader("HTTP-Method", "GET");

            if (product == null)
            {
                HttpContext.Current.Response.AppendHeader("ERROR", "(404) Resource Not Found");
                return NotFound();
            }
            else
            {
                HttpContext.Current.Response.AppendHeader("Response-Type", "JSON");
                HttpContext.Current.Response.AppendHeader("Object-URL", Request.RequestUri.AbsoluteUri);
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
                return Json(product, serializerSettings);
            }
        }

        [HttpPost]
        [Route("inventory/products")]
        public HttpResponseMessage PostProduct(Product product)
        {
            var createdProduct = productService.CreateProduct(product);

            // If product already exists within database, return 409.
            if (createdProduct == null)
            {
                var alreadyExistsResponse = Request.CreateResponse
                    (HttpStatusCode.Conflict, "(409) Product already exists", Configuration.Formatters.JsonFormatter);

                alreadyExistsResponse.Headers.Add("API-Version", apiVersion);
                alreadyExistsResponse.Headers.Add("ERROR", "(409) Resource already exists");

                return alreadyExistsResponse;
            }
            // Product doesn't exist inside the database, proceed.
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.Created, product, Configuration.Formatters.JsonFormatter);
                response.Headers.Add("API-Version", apiVersion);
                response.Headers.Add("HTTP-Method", "POST");
                response.Headers.Add("Response-Type", "JSON");

                if (Request.RequestUri.AbsoluteUri.EndsWith("/"))
                {
                    response.Headers.Add("New-Product-URL", Request.RequestUri.AbsoluteUri + createdProduct.ID);
                }
                else
                {
                    response.Headers.Add("New-Product-URL", Request.RequestUri.AbsoluteUri + "/" + createdProduct.ID);
                }
                return response;
            }
        }

        [HttpPut]
        [Route("inventory/products/{productID}")]
        public HttpResponseMessage UpdateProduct(int productID, Product product)
        {
            var updatedProduct = productService.UpdateProduct(productID, product);

            var response = Request.CreateResponse(HttpStatusCode.OK, updatedProduct, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API-Version", apiVersion);
            response.Headers.Add("HTTP-Method", "PUT");
            response.Headers.Add("Response-Type", "JSON");
            response.Headers.Add("Updated-Product-URL", Request.RequestUri.AbsoluteUri);

            if (updatedProduct == null)
            {
                var notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound,
                    "(404) Product Not Found",
                    Configuration.Formatters.JsonFormatter);

                notFoundResponse.Headers.Add("API-Version", apiVersion);

                return notFoundResponse;
            }
            else
            {
                return response;
            }
        }

        [HttpDelete]
        [Route("inventory/products/{productID}")]
        public HttpResponseMessage DeleteProduct(int productID)
        {
            var deletedProduct = productService.DeleteProduct(productID);

            var response = Request.CreateResponse(HttpStatusCode.OK, deletedProduct, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API-Version", apiVersion);
            response.Headers.Add("HTTP-Method", "DELETE");
            response.Headers.Add("Response-Type", "JSON");

            if (deletedProduct == null)
            {
                var notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound, "(404) Product not found",
                    Configuration.Formatters.JsonFormatter);

                notFoundResponse.Headers.Add("API-Version", apiVersion);

                return notFoundResponse;
            }
            else
            {
                return response;
            }
        }
    }
}
