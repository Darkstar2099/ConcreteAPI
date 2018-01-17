﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using ConcreteAPI.Core.Entities;
using ConcreteAPI.Core.Services.Interfaces;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Thinktecture.IdentityModel.Tokens;

namespace ConcreteAPI.Services
{
    public class Token : IToken
    {       
         private const string _issuer = "http://localhost:8080";
        //private const string _issuer = "https://concreteapi.azurewebsites.net:443";
        private const string Base64Secret = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw";

        public string GenerateToken(User user)
        {
            var ticket = GenerateTicket(user);

            var accessToken = Protect(ticket);

            return accessToken;
        }

        public static AuthenticationTicket GenerateTicket(User user)
        {
            if (user == null)
                throw new Exception("Usuário inválido");

            var identity = new ClaimsIdentity("JWT");

            identity.AddClaim(new Claim("sub", user.Id.ToString("N")));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));

            var props = new AuthenticationProperties(new Dictionary<string, string>())
            {
                ExpiresUtc = DateTimeOffset.Now.AddMinutes(30)
            };

            var ticket = new AuthenticationTicket(identity, props);
            return ticket;
        }

        public static string Protect(AuthenticationTicket data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var keyByteArray = TextEncodings.Base64Url.Decode(Base64Secret);
            var signingKey = new HmacSigningCredentials(keyByteArray);
            var issued = data.Properties.IssuedUtc?.UtcDateTime;
            var expires = data.Properties.ExpiresUtc?.UtcDateTime;

            var token = new JwtSecurityToken(_issuer, null, data.Identity.Claims, issued, expires, signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

    }
}