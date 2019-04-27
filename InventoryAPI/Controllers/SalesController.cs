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
    public class SalesController : ApiController
    {
        private readonly string apiVersion = Resources.API_INFO.API_VERSION;

        [HttpGet]
        [Route("sales")]
        public IHttpActionResult GetSales()
        {
            var salesList = SaleServices.GetSales();

            HttpContext.Current.Response.AppendHeader("API-Version", apiVersion);
            HttpContext.Current.Response.AppendHeader("Response-Type", "JSON");
            HttpContext.Current.Response.AppendHeader("Response-Object-Length", salesList.Count.ToString() + " Items");
            HttpContext.Current.Response.AppendHeader("List-URL", Request.RequestUri.AbsoluteUri);

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(salesList, serializerSettings);
        }

        [HttpGet]
        [Route("sales/{order}/{orderby}")]
        public IHttpActionResult GetSaleOrder(string order, string orderBy)
        {
            var salesList = SaleServices.GetSaleOrder(order.Trim(), orderBy.Trim());

            HttpContext.Current.Response.AppendHeader("API-Version", apiVersion);
            HttpContext.Current.Response.AppendHeader("Response-Type", "JSON");
            HttpContext.Current.Response.AppendHeader("Response-Object-Length", salesList.Count.ToString() + " Items");
            HttpContext.Current.Response.AppendHeader("List-URL", Request.RequestUri.AbsoluteUri);

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(salesList, serializerSettings);
        }

        [HttpGet]
        [Route("sales/{id}")]
        public IHttpActionResult GetSale(int id)
        {
            Sale sale = SaleServices.GetSale(id);

            HttpContext.Current.Response.AppendHeader("API-Version", apiVersion);

            if (sale == null)
            {
                HttpContext.Current.Response.AppendHeader("ERROR", "(404) Resource Not Found");
                return NotFound();
            }
            else
            {
                HttpContext.Current.Response.AppendHeader("Response-Type", "JSON");
                HttpContext.Current.Response.AppendHeader("Object-URL", Request.RequestUri.AbsoluteUri);
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
                return Json(sale, serializerSettings);
            }
        }

        [HttpPost]
        [Route("sales")]
        public HttpResponseMessage PostSale(Sale sale)
        {
            var createdSale = SaleServices.CreateSale(sale);

            var response = Request.CreateResponse(HttpStatusCode.Created, sale, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API-Version", apiVersion);
            response.Headers.Add("HTTP-Method", "POST");

            if (Request.RequestUri.AbsoluteUri.EndsWith("/"))
            {
                response.Headers.Add("New-Sale-URL", Request.RequestUri.AbsoluteUri + createdSale.ID);
            }
            else
            {
                response.Headers.Add("New-Sale-URL", Request.RequestUri.AbsoluteUri + "/" + createdSale.ID);
            }
            return response;
        }
        

        [HttpPut]
        [Route("sales/{saleID}")]
        public HttpResponseMessage UpdaleSale(int saleID, Sale sale)
        {
            var updatedSale = SaleServices.UpdateSale(saleID, sale);

            var response = Request.CreateResponse(HttpStatusCode.OK, updatedSale, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API-Version", apiVersion);
            response.Headers.Add("Response-Type", "JSON");
            response.Headers.Add("Updated-Product-URL", Request.RequestUri.AbsoluteUri);

            if (updatedSale == null)
            {
                var notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound,
                    "(404) Sale Not Found",
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
        [Route("sales/{saleID}")]
        public HttpResponseMessage DeleteSale(int saleID)
        {
            var deletedSale = SaleServices.DeleteSale(saleID);

            var response = Request.CreateResponse(HttpStatusCode.OK, deletedSale, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API-Version", apiVersion);
            response.Headers.Add("Response-Type", "JSON");

            if (deletedSale == null)
            {
                var notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound, "(404) Sale not found",
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