﻿using System;
using System.IdentityModel.Tokens;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using ConcreteAPI.Core.Constants;
using ConcreteAPI.Core.Extensions;
using ConcreteAPI.Core.Services;

namespace ConcreteAPI.Filters
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        public override async Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var token = actionContext.Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { ReasonPhrase = Messages.TokenNotFound };
                return;
            }

            var service = Startup.Container.GetInstance<UserService>();
            if (service == null)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                return;
            }

            var tokenDecoder = new JwtSecurityTokenHandler();
            var jwt = (JwtSecurityToken)tokenDecoder.ReadToken(token);

            if (jwt.Subject.IsEmpty())
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { ReasonPhrase = Messages.TokenNotFound };
                return;
            }

            var user = await service.GetByIdAsync(Guid.Parse(jwt.Subject));
            if (user == null)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { ReasonPhrase = Messages.TokenNotFound };
                return;
            }

            if (!user.Token.Equals(token))
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { ReasonPhrase = Messages.TokenNotFound };
                return;
            }

            if (user.LastLoginDate<DateTime.Now.AddMinutes(-30))
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { ReasonPhrase = Messages.ExpiredToken};
                return;
            }
        }
    }
}