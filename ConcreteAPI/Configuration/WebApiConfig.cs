﻿using System.Web.Http;
using System.Web.Http.Tracing;
using ConcreteAPI.Handlers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http.ExceptionHandling;

namespace ConcreteAPI.Configuration
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {

            // Web API configuration and services
            config.Services.Replace(typeof(IExceptionHandler), new CustomExceptionHandler());
            config.MessageHandlers.Add(new CustomMessageHandler());

            //config.EnableSystemDiagnosticsTracing();
            //SystemDiagnosticsTraceWriter traceWriter = config.EnableSystemDiagnosticsTracing();
            //traceWriter.IsVerbose = true;
            //traceWriter.MinimumLevel = TraceLevel.Debug;

            // Remove o Formatador XML
            var formatters = config.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            // Configura o Serializador do Json
            var jsonSettings = formatters.JsonFormatter.SerializerSettings;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;

            // Habilita o Cross-Origin Resouce Sharing (CORS)
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "HandleErrors",
                routeTemplate: "{*url}"
            );
        }
    }
}
