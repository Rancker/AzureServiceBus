﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Quote.AzureFunction
{
    public static class QuotePreFilter
    {
        [FunctionName("WatchInfo")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Azure function to processed a quote prefilter request.");
            // Retrieve the model id from the query string
            string model = req.Query["model"];

            // If the user specified a model id, find the details of the model of watch
            if (model != null)
            {
                // Use dummy data for this example
                dynamic watchinfo = new
                {
                    Manufacturer = "Abc",
                    CaseType = "Solid",
                    Bezel = "Titanium",
                    Dial = "Roman",
                    CaseFinish = "Silver",
                    Jewels = 15
                };

                return (ActionResult)new OkObjectResult(
                    $"Watch Details: {watchinfo.Manufacturer}, {watchinfo.CaseType}, {watchinfo.Bezel}, {watchinfo.Dial}, {watchinfo.CaseFinish}, {watchinfo.Jewels}");
            }

            return new BadRequestObjectResult("Please provide a watch model in the query string");
        }
    }
}