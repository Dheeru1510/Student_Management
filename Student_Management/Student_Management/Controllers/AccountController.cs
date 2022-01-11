
using BusinessModel.Abstraction;
using EntityModel.UserEntity;
using Microsoft.IdentityModel.Tokens;
using SecurityLib;
using Student_Management.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Student_Management.Controllers
{

    public class AccountController : ApiController
    {
        private IUsersBO _objUsersBO = null;
        public AccountController(IUsersBO objUsersBo)
        {
            _objUsersBO = objUsersBo;
        }

        [HttpPost]
        [Route("api/UserLogin")]
        public Object UserLogin([FromBody] dynamic data)
        {
            try
            {
                FileLogger.Log("Login Start--Athenticate User");
                UsersModel objUsers = new UsersModel();
                if (Request.Content.IsMimeMultipartContent())
                {
                    var ret = SerializeHashTable();
                    objUsers = Newtonsoft.Json.JsonConvert.DeserializeObject<UsersModel>(ret.ToString());
                }
                else
                {
                    objUsers.UserName = data.UserName;
                    objUsers.Password = data.Password;
                }
                objUsers.Password = CryptoUtilily.GenerateHash(objUsers.Password);

                //var res = new UsersBo().UserVerify(objUsers.UserName, objUsers.Password);
                var res = _objUsersBO.UserVerify(objUsers.UserName, objUsers.Password);
                if (string.IsNullOrEmpty(res))
                {
                    FileLogger.Log("Login End--Athenticate User");
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { status = HttpStatusCode.Unauthorized, message = "Invalid User detail" });
                }
                else
                {
                    objUsers.Uid = res;
                    UserInfo.Uid = res;
                    var token = GenerateToken(res);
                    objUsers.UserName = objUsers.Password = "";
                    FileLogger.Log("Login End--Athenticate User");
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { status = HttpStatusCode.OK, message = "Login Successfully", token = token, data = objUsers });
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = HttpStatusCode.InternalServerError, message = "Login Failed", data = ex.Message });
            }
        }


        private static object SerializeHashTable()
        {
            Hashtable ht = new Hashtable();
            foreach (var key in HttpContext.Current.Request.Form.AllKeys)
            {
                foreach (var val in HttpContext.Current.Request.Form.GetValues(key))
                {
                    ht.Add(key, val);
                }
            }
            var output = Newtonsoft.Json.JsonConvert.SerializeObject(ht);
            return output;
        }
        private static string GenerateToken(string uID)
        {
            string key = "my_secret_key_12345";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("Uid", uID));
            var issuer = "localhost";

            var secToken = new JwtSecurityToken(issuer,
                issuer,
                permClaims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials);

            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(secToken);
        }
    }
}
