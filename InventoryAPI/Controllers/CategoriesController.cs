using InventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace InventoryAPI.Controllers
{
    public class CategoriesController : ApiController
    {

        private string apiVersion = Resources.API_INFO.API_VERSION;

        [HttpGet]
        public HttpResponseMessage GetCategory()
        {
            var objeto = new Category();
   
            return Request.CreateResponse(HttpStatusCode.OK, objeto, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        public HttpResponseMessage GetCategory(int id)
        {
            var objeto = new Category();

            var response = Request.CreateResponse(HttpStatusCode.OK, objeto, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API Version", apiVersion);

            return response;
        }

        [HttpPost]
        public HttpResponseMessage PostCategory(Category category)
        {
            var response = Request.CreateResponse(HttpStatusCode.Created, category, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API Version", apiVersion);

            return response;
        }

        [HttpPut]
        public HttpResponseMessage UpdateCategory(int categoryID, Category category)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, category, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API Version", apiVersion);

            return response;
        }

        [HttpDelete]
        public HttpResponseMessage DeleteCategory(int categoryID)
        {
            var objeto = new Category();

            var response = Request.CreateResponse(HttpStatusCode.OK, objeto, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API Version", apiVersion);

            return response;
        }
    }
}