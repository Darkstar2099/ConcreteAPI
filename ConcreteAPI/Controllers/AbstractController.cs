﻿using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConcreteAPI.Core.Extensions;
using ConcreteAPI.Core.Common;

namespace ConcreteAPI.Controllers
{
    public abstract class AbstractController : ApiController
    {
        protected IHttpActionResult ProcessResult(CommandReturn methodReturn)
        {
            if (methodReturn.Success)
            {
                if (methodReturn.Data != null)
                    return Ok(methodReturn.Data);
                // Retorna 200 OK
                return Ok();
            }

            var errorMsgs = methodReturn.Failures.Select(x => (x.PropertyName.IsEmpty() ? "" : ": " + x.PropertyName) + x.ErrorMessage);
            var messages = string.Join("<br>", errorMsgs);

            return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = messages });
        }

    }
}