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
        private readonly string apiVersion = Resources.API_INFO.API_VERSION;
        
        [HttpGet]
        [Route("inventory/categories")]
        public IHttpActionResult GetCategory()
        {
            var categoryList = CategoryServices.GetCategory();

            HttpContext.Current.Response.AppendHeader("API-Version", apiVersion);
            HttpContext.Current.Response.AppendHeader("Response-Type", "JSON");
            HttpContext.Current.Response.AppendHeader("Response-Object-Length", categoryList.Count.ToString() + " Items");
            HttpContext.Current.Response.AppendHeader("List-URL", Request.RequestUri.AbsoluteUri);

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(categoryList, serializerSettings);
        }

        [HttpGet]
        [Route("inventory/categories/{order}/{orderby}")]
        public IHttpActionResult GetCategoryOrder(string order, string orderBy)
        {
            var categoryList = CategoryServices.GetCategoryOrder(order.Trim(), orderBy.Trim());

            HttpContext.Current.Response.AppendHeader("API-Version", apiVersion);
            HttpContext.Current.Response.AppendHeader("Response-Type", "JSON");
            HttpContext.Current.Response.AppendHeader("Response-Object-Length", categoryList.Count.ToString() + " Items");
            HttpContext.Current.Response.AppendHeader("List-URL", Request.RequestUri.AbsoluteUri);

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(categoryList, serializerSettings);
        }

        [HttpGet]
        [Route("inventory/categories/{id}")]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = CategoryServices.GetCategory(id);

            HttpContext.Current.Response.AppendHeader("API-Version", apiVersion);

            if(category == null)
            {
                HttpContext.Current.Response.AppendHeader("ERROR", "(404) Resource Not Found");
                return NotFound();
            }
            else
            {
                HttpContext.Current.Response.AppendHeader("Response-Type", "JSON");
                HttpContext.Current.Response.AppendHeader("Object-URL", Request.RequestUri.AbsoluteUri);
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
                return Json(category, serializerSettings);
            }
        }

        [HttpGet]
        [Route("inventory/categories/name/{name}")]
        public IHttpActionResult GetCategory(string name)
        {
            Category category = CategoryServices.GetCategory(name);

            HttpContext.Current.Response.AppendHeader("API-Version", apiVersion);

            if (category == null)
            {
                HttpContext.Current.Response.AppendHeader("ERROR", "(404) Resource Not Found");
                return NotFound();
            }
            else
            {
                HttpContext.Current.Response.AppendHeader("Response-Type", "JSON");
                HttpContext.Current.Response.AppendHeader("Object-URL", Request.RequestUri.AbsoluteUri);
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
                return Json(category, serializerSettings);
            }
        }

        [HttpPost]
        [Route("inventory/categories")]
        public HttpResponseMessage PostCategory(Category category)
        {
            var createdCategory = CategoryServices.CreateCategory(category);

            // If product already exists within database, return 409.
            if(createdCategory == null)
            {
                var alreadyExistsResponse = Request.CreateResponse
                    (HttpStatusCode.Conflict, "(409) Category already exists", Configuration.Formatters.JsonFormatter);

                alreadyExistsResponse.Headers.Add("API-Version", apiVersion);
                alreadyExistsResponse.Headers.Add("ERROR", "(409) Resource already exists");

                return alreadyExistsResponse;
            }
            // Product doesn't exist inside the database, proceed.
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.Created, category, Configuration.Formatters.JsonFormatter);
                response.Headers.Add("API-Version", apiVersion);
                response.Headers.Add("Response-Type", "JSON");

                if (Request.RequestUri.AbsoluteUri.EndsWith("/"))
                {
                    response.Headers.Add("New-Category-URL", Request.RequestUri.AbsoluteUri + createdCategory.ID);
                }
                else
                {
                    response.Headers.Add("New-Category-URL", Request.RequestUri.AbsoluteUri + "/" + createdCategory.ID);
                }
                return response;
            }
        }

        [HttpPut]
        [Route("inventory/categories/{categoryID}")]
        public HttpResponseMessage UpdateCategory(int categoryID, Category category)
        {
            var updatedCategory = CategoryServices.UpdateCategory(categoryID, category);

            var response = Request.CreateResponse(HttpStatusCode.OK, updatedCategory, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API-Version", apiVersion);
            response.Headers.Add("Response-Type", "JSON");
            response.Headers.Add("Updated-Category-URL", Request.RequestUri.AbsoluteUri);

            if(updatedCategory == null)
            {
                var notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound, 
                    "(404) Category Not Found", 
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
        [Route("inventory/categories/{categoryID}")]
        public HttpResponseMessage DeleteCategory(int categoryID)
        {
            var deletedCategory = CategoryServices.DeleteCategory(categoryID);

            var response = Request.CreateResponse(HttpStatusCode.OK, deletedCategory, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API-Version", apiVersion);
            response.Headers.Add("Response-Type", "JSON");

            if (deletedCategory == null)
            {
                var notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound, "(404) Category not found", 
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