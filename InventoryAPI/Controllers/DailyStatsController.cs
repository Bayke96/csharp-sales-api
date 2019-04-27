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
    public class DailyStatsController : ApiController
    {
        private readonly string apiVersion = Resources.API_INFO.API_VERSION;
        private readonly DailyStatsServices dailyStatsService = new DailyStatsServices();

        [HttpGet]
        [Route("salestats/dailystats")]
        public IHttpActionResult GetDailyStats()
        {
            var dailyStatsList = dailyStatsService.GetDailyStats();

            HttpContext.Current.Response.AppendHeader("API-Version", apiVersion);
            HttpContext.Current.Response.AppendHeader("Response-Type", "JSON");
            HttpContext.Current.Response.AppendHeader("Response-Object-Length", dailyStatsList.Count.ToString() + " Items");
            HttpContext.Current.Response.AppendHeader("List-URL", Request.RequestUri.AbsoluteUri);

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(dailyStatsList, serializerSettings);
        }

        [HttpGet]
        [Route("salestats/dailystats/{id}")]
        public IHttpActionResult GetDailyStat(int id)
        {
            DailyStats dailyStat = dailyStatsService.GetDailyStat(id);

            HttpContext.Current.Response.AppendHeader("API-Version", apiVersion);

            if (dailyStat == null)
            {
                HttpContext.Current.Response.AppendHeader("ERROR", "(404) Resource Not Found");
                return NotFound();
            }
            else
            {
                HttpContext.Current.Response.AppendHeader("Response-Type", "JSON");
                HttpContext.Current.Response.AppendHeader("Object-URL", Request.RequestUri.AbsoluteUri);
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
                return Json(dailyStat, serializerSettings);
            }
        }

        [HttpPost]
        [Route("salestats/dailystats")]
        public HttpResponseMessage PostDailyStat(DailyStats dailyStat)
        {
            var createdDailyStat = dailyStatsService.CreateDailyStat(dailyStat);

            // If stats already exists within database, return 409.
            if (createdDailyStat == null)
            {
                var alreadyExistsResponse = Request.CreateResponse
                    (HttpStatusCode.Conflict, "(409) Daily Stat already exists", Configuration.Formatters.JsonFormatter);

                alreadyExistsResponse.Headers.Add("API-Version", apiVersion);
                alreadyExistsResponse.Headers.Add("ERROR", "(409) Resource already exists");

                return alreadyExistsResponse;
            }
            // Daily Stat doesn't exist inside the database, proceed.
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.Created, dailyStat, Configuration.Formatters.JsonFormatter);
                response.Headers.Add("API-Version", apiVersion);
                response.Headers.Add("Response-Type", "JSON");

                if (Request.RequestUri.AbsoluteUri.EndsWith("/"))
                {
                    response.Headers.Add("DailyStats-URL", Request.RequestUri.AbsoluteUri + createdDailyStat.ID);
                }
                else
                {
                    response.Headers.Add("DailyStats-URL", Request.RequestUri.AbsoluteUri + "/" + createdDailyStat.ID);
                }
                return response;
            }
        }

        [HttpPut]
        [Route("salestats/dailystats/{statID}")]
        public HttpResponseMessage UpdateDailyStats(int statID, DailyStats dailyStat)
        {
            var updatedDailyStat = dailyStatsService.UpdateDailyStat(statID, dailyStat);

            var response = Request.CreateResponse(HttpStatusCode.OK, updatedDailyStat, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API-Version", apiVersion);
            response.Headers.Add("Response-Type", "JSON");
            response.Headers.Add("DailyStats-URL", Request.RequestUri.AbsoluteUri);

            if (updatedDailyStat == null)
            {
                var notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound,
                    "(404) Daily Stats Not Found",
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
        [Route("salestats/dailystats/{statID}")]
        public HttpResponseMessage DeleteDailyStats(int statID)
        {
            var deletedDailyStat = dailyStatsService.DeleteDailyStat(statID);

            var response = Request.CreateResponse(HttpStatusCode.OK, deletedDailyStat, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("API-Version", apiVersion);
            response.Headers.Add("Response-Type", "JSON");

            if (deletedDailyStat == null)
            {
                var notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound, "(404) Daily Stats not found",
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
