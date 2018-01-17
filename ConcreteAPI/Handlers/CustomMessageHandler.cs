﻿﻿using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using ConcreteAPI.Entities;

namespace ConcreteAPI.Handlers
{
    public class CustomMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.OK)
                return response;

            var content = response.Content as ObjectContent;
            var values = content?.Value as HttpError;

            response.Content = new ObjectContent(typeof(Error), new Error
            {
                StatusCode = (int) response.StatusCode,
                Mensagem = values?.ExceptionMessage ?? values?.Message ?? response.ReasonPhrase
            }, new JsonMediaTypeFormatter());

            return response;
        }

    }
}