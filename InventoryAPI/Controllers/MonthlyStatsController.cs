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
    public class MonthlyStatsController : ApiController
    {
        private readonly string apiVersion = Resources.API_INFO.API_VERSION;

        [HttpGet]
        [Route("salestats/monthlystats")]
        public IHttpActionResult GetMonthlyStats()
        {
            var MonthlyStatsList = MonthlyStatsServices.GetMonthlyStats();

            HttpContext.Current.Response.AppendHeader("API-Version", apiVersion);
            HttpContext.Current.Response.AppendHeader("Response-Type", "JSON");
            HttpContext.Current.Response.AppendHeader("Response-Object-Length", MonthlyStatsList.Count.ToString() + " Items");
            HttpContext.Current.Response.AppendHeader("List-URL", Request.RequestUri.AbsoluteUri);

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(MonthlyStatsList, serializerSettings);
        }

        [HttpGet]
        [Route("salestats/monthlystats/{id}")]
        public IHttpActionResult GetMonthlyStat(int id)
        {
            MonthlyStats monthlyStats = MonthlyStatsServices.GetMonthlyStat(id);

            HttpContext.Current.Response.AppendHeader("API-Version", apiVersion);

            if (monthlyStats == null)
            {
                HttpContext.Current.Response.AppendHeader("ERROR", "(404) Resource Not Found");
                return NotFound();
            }
            else
            {
                HttpContext.Current.Response.AppendHeader("Response-Type", "JSON");
                HttpContext.Current.Response.AppendHeader("Object-URL", Request.RequestUri.AbsoluteUri);
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
                return Json(monthlyStats, serializerSettings);
            }
        }

        [HttpPost]
        [Route("salestats/monthlystats")]
        public HttpResponseMessage PostMonthlyStat(MonthlyStats monthlyStat)
        {
            var createdMonthlyStat = MonthlyStatsServices.CreateMonthlyStat(monthlyStat);

            // If stats already exists within database, return 409.
            if (createdMonthlyStat == null)
            {
                var alreadyExistsResponse = Request.CreateResponse
                    (HttpStatusCode.Conflict, "(409) Monthly Stat already exists", Configuration.Formatters.JsonFormatter);

                alreadyExistsResponse.Headers.Add("API-Version", apiVersion);
                alreadyExistsResponse.Headers.Add("ERROR", "(409) Resource already exists");

                return alreadyExistsResponse;
            }
            // Daily Stat doesn't exist inside the database, proceed.
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.Created, monthlyStat, Configuration.Formatters.JsonFormatter);
                response.Headers.Add("API-Version", apiVersion);
                response.Headers.Add("Response-Type", "JSON");

                if (Request.RequestUri.AbsoluteUri.EndsWith("/"))
                {
                    response.Headers.Add("MonthlyStats-URL", Request.RequestUri.AbsoluteUri + monthlyStat.ID);
                }
                else
                {
                    response.Headers.Add("MonthlyStats-URL", Request.RequestUri.AbsoluteUri + "/" + monthlyStat.ID);
                }
                return response;
            }
        }

        [HttpPut]
        [Route("salestats/monthlystats/{statID}")]
        public HttpResponseMessage UpdateMonthlyStats(int statID, MonthlyStats monthlyStat)
        {
            var updatedMonthlyStat = MonthlyStatsServices.UpdateMonthlyStat(statID, monthlyStat);

            var response = Request.CreateResponse(HttpStatusCode.OK, updatedMonthlyStat, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API-Version", apiVersion);
            response.Headers.Add("Response-Type", "JSON");
            response.Headers.Add("MonthlyStats-URL", Request.RequestUri.AbsoluteUri);

            if (updatedMonthlyStat == null)
            {
                var notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound,
                    "(404) Monthly Stats Not Found",
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
        [Route("salestats/monthlystats/{statID}")]
        public HttpResponseMessage DeleteMonthlyStats(int statID)
        {
            var deletedMonthlyStat = MonthlyStatsServices.DeleteMonthlyStat(statID);

            var response = Request.CreateResponse(HttpStatusCode.OK, deletedMonthlyStat, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API-Version", apiVersion);
            response.Headers.Add("Response-Type", "JSON");

            if (deletedMonthlyStat == null)
            {
                var notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound, "(404) Monthly Stats not found",
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
