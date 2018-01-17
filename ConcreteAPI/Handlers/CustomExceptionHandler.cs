﻿﻿using System;
using System.Web.Http.ExceptionHandling;

namespace ConcreteAPI.Handlers
{
    public class CustomExceptionHandler: ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            Console.WriteLine(context.Exception);
            base.Handle(context);
        }

    }
}