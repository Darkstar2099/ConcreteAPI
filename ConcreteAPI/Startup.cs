﻿using System.Globalization;
using System.Web.Http;
using ConcreteAPI.Configuration;
using FluentValidation;
using Owin;
using Microsoft.Owin;
using SimpleInjector;

namespace ConcreteAPI
{
    public class Startup
    {
        public static Container Container;

        public void Configuration(IAppBuilder app)
        {
            // Define linguagem para o Fluent Validation
            ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-BR");

            var config = new HttpConfiguration();

            Container = SimpleInjectorConfig.Configure(config);
            WebApiConfig.Register(config);

            //app.UseWelcomePage("/");
            app.UseWebApi(config);
        }
    }
}
