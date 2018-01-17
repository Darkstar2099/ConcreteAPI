﻿using System.Web.Http;
using ConcreteAPI.Core.Commands.Handlers;
using ConcreteAPI.Core.Repositories.Interfaces;
using ConcreteAPI.Core.Services;
using ConcreteAPI.Core.Services.Interfaces;
using ConcreteAPI.Core.FluentVal;
using ConcreteAPI.Persistence.ADO.NET;
using ConcreteAPI.Persistence.ADO.NET.Repositories;
using ConcreteAPI.Persistence.EF.Context;
using ConcreteAPI.Persistence.EF.Repositories;
using ConcreteAPI.Services;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace ConcreteAPI.Configuration
{
    public static class SimpleInjectorConfig
    {
        public static Container Configure(HttpConfiguration config)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

			SetEntries(container);
            container.RegisterWebApiControllers(config);
            container.Verify();
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            return container;
        }

        // Método para registrar as Instâncias que serão retornadas quando solicitadas na DI
        private static void SetEntries(Container container)
        {
            // context
            container.Register<ConcreteAPIContext>(Lifestyle.Scoped);

            // dbConnection
            container.Register<ConcreteAPIDbConnection>(Lifestyle.Scoped);

            // commands
            container.Register<UserCommandHandler>(Lifestyle.Transient);
            container.Register<SignUpCommandHandler>(Lifestyle.Transient);

            // domain services
            container.Register<UserService>(Lifestyle.Transient);

            // other services
            container.Register<IToken, Token>();

            // repositories
            container.Register<IUserRepository, UserRepository>(Lifestyle.Transient);
            container.Register<ISignUpRepository, SignUpRepository>();

            // validators
            container.Register<EmailValidator>(Lifestyle.Singleton);
            container.Register<UserValidator>(Lifestyle.Singleton);
            container.Register<PhoneValidator>(Lifestyle.Singleton);
        }
    }
}