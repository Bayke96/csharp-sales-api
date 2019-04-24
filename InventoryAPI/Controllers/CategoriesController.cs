using InventoryAPI.Models;
using InventoryAPI.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace InventoryAPI.Controllers
{
    public class CategoriesController : ApiController
    {
        public string apiVersion = Resources.API_INFO.API_VERSION;
        private CategoryServices categoryService = new CategoryServices();
        
        [HttpGet]
        [Route("inventory/category")]
        public IHttpActionResult GetCategory()
        {
            var categoryList = categoryService.GetCategory();

            HttpContext.Current.Response.AppendHeader("API-Version", apiVersion);
            HttpContext.Current.Response.AppendHeader("HTTP-Method", "GET");
            HttpContext.Current.Response.AppendHeader("Response-Type", "JSON");
            HttpContext.Current.Response.AppendHeader("Response-Object-Length", categoryList.Count.ToString() + " Items");
            HttpContext.Current.Response.AppendHeader("List-URL", Request.RequestUri.AbsoluteUri);

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(categoryList, serializerSettings);
        }

        [HttpGet]
        [Route("inventory/category/{id}")]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = categoryService.GetCategory(id);

            HttpContext.Current.Response.AppendHeader("API-Version", apiVersion);
            HttpContext.Current.Response.AppendHeader("HTTP-Method", "GET");
            HttpContext.Current.Response.AppendHeader("Response-Type", "JSON");
            HttpContext.Current.Response.AppendHeader("Object-URL", Request.RequestUri.AbsoluteUri);

            if(category == null)
            {
                return Json(new { });
            }
            else
            {
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
                return Json(category, serializerSettings);
            }
        }

        [HttpPost]
        [Route("inventory/category")]
        public HttpResponseMessage PostCategory(Category category)
        {
            var createdCategory = categoryService.CreateCategory(category);

            var response = Request.CreateResponse(HttpStatusCode.Created, category, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API-Version", apiVersion);
            response.Headers.Add("HTTP-Method", "POST");
            response.Headers.Add("Response-Type", "JSON");
            response.Headers.Add("New-Category-URL", Request.RequestUri.AbsoluteUri + "/" + createdCategory.ID);

            return response;
        }

        [HttpPut]
        [Route("inventory/category/{categoryID}")]
        public HttpResponseMessage UpdateCategory(int categoryID, Category category)
        {
            var updatedCategory = categoryService.UpdateCategory(categoryID, category);

            var response = Request.CreateResponse(HttpStatusCode.OK, updatedCategory, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API-Version", apiVersion);
            response.Headers.Add("HTTP-Method", "PUT");
            response.Headers.Add("Response-Type", "JSON");
            response.Headers.Add("Updated-Category-URL", Request.RequestUri.AbsoluteUri);

            if(updatedCategory == null)
            {
                var notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound, "(404) Category not found", 
                    Configuration.Formatters.JsonFormatter);
                return notFoundResponse;
            }
            else
            {
                return response;
            }
        }

        [HttpDelete]
        [Route("inventory/category/{categoryID}")]
        public HttpResponseMessage DeleteCategory(int categoryID)
        {
            var deletedCategory = categoryService.DeleteCategory(categoryID);

            var response = Request.CreateResponse(HttpStatusCode.OK, deletedCategory, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API-Version", apiVersion);
            response.Headers.Add("HTTP-Method", "DELETE");
            response.Headers.Add("Response-Type", "JSON");

            if (deletedCategory == null)
            {
                var notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound, "(404) Category not found", 
                    Configuration.Formatters.JsonFormatter);
                return notFoundResponse;
            }
            else
            {
                return response;
            }
        }
    }
}