using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Student_Management.Common
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                string token = "";
                if (actionContext.Request.Headers.Authorization != null)
                {
                    token = actionContext.Request.Headers.Authorization.Parameter;
                }
                else
                {
                    token = actionContext.Request.Headers.GetValues("token").First();
                }
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var tokenS = jsonToken as JwtSecurityToken;
                string cguid = actionContext.Request.Headers.GetValues("Uid").First();
                var guid = tokenS.Claims.First(claim => claim.Type == "Uid").Value;
                var lifeTime = new JwtSecurityTokenHandler().ReadToken(token).ValidTo;
                var isTokenValid = ValidateToken(token);
                if (cguid == guid && isTokenValid && DateTime.UtcNow <= lifeTime)
                {
                    base.OnAuthorization(actionContext);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new { status = HttpStatusCode.Unauthorized, message = "Failed", data = "Unauthorized Access" });
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new { status = HttpStatusCode.Unauthorized, message = "Failed", data = "Unauthorized Access" });
            }
        }

        private static bool ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            return true;
        }

        private static TokenValidationParameters GetValidationParameters()
        {
            string key = "my_secret_key_12345";
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = "localhost",
                ValidAudience = "localhost",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) // The same key as the one that generate the token
            };
        }
    }
}