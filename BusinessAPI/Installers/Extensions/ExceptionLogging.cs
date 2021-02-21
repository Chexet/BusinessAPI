using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Installers.Extensions
{
    public static class ExceptionLogging
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static void UseDevelopmentExceptionHandling(this IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var exDateTime = DateTime.Now;
                    var exceptionHandlerPathFeature = ExceptionHandlerPathFeature(context, exDateTime);
                    await context.Response.WriteAsync(
                        $"Error: {exDateTime}\n" +
                        $"Provided Link: {exceptionHandlerPathFeature.Error.HelpLink}\n\n" +
                        $"Message: {exceptionHandlerPathFeature.Error.Message}\n\n" +
                        $"StackTrace:{exceptionHandlerPathFeature.Error.StackTrace}\n\n" +
                        $"Inner Exception: {exceptionHandlerPathFeature.Error.InnerException?.Message}\n\n" +
                        $"Inner StackTrace: { exceptionHandlerPathFeature.Error.InnerException?.StackTrace}\n\n");
                });
            });
        }
        public static void UseProdExceptionHandling(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var exDateTime = DateTime.Now;
                    var exceptionHandlerPathFeature = ExceptionHandlerPathFeature(context, exDateTime);
                    await context.Response.WriteAsync($"Internal server error {exceptionHandlerPathFeature.Error.HelpLink}");
                });
            });
        }
        private static IExceptionHandlerPathFeature ExceptionHandlerPathFeature(HttpContext context, DateTime exDateTime)
        {
            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();
            exceptionHandlerPathFeature.Error.HelpLink = Guid.NewGuid().ToString();
            Logger.Error(
                $"Error: {exDateTime}\n" +
                $"Provided Link: {exceptionHandlerPathFeature.Error.HelpLink}\n\n" +
                $"Message: {exceptionHandlerPathFeature.Error.Message}\n\n" +
                $"Path: {exceptionHandlerPathFeature.Path}\n\n" +
                $"StackTrace:{exceptionHandlerPathFeature.Error.StackTrace}\n\n" +
                $"Inner Exception: {exceptionHandlerPathFeature.Error.InnerException?.Message}\n\n" +
                $"Inner StackTrace: {exceptionHandlerPathFeature.Error.InnerException?.StackTrace}\n\n");
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/html";
            return exceptionHandlerPathFeature;
        }
    }
}
