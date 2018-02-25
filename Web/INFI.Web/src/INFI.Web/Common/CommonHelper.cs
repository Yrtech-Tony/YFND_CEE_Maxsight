using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using INFI.Web.Common.Module;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace INFI.Web.Common
{
    public class CommonHelper
    {
        public const string OssBaseUrl = "http://infi.oss-cn-shanghai.aliyuncs.com/";
        private static CommonHelper current;
        public static CommonHelper Current
        {
            get
            {
                if (current == null)
                {
                    current = new CommonHelper();
                }
                return current;
            }
        }
        public static T DecodeString<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        public static string EncodeDto<T>(T t)
        {
            string jsonString = string.Empty;

            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };
                jsonString = JsonConvert.SerializeObject(t);
            }
            catch (Exception)
            {
            }
            return jsonString;
        }

        public static T GetListFromByte<T>(byte[] data)
        {
            string jsonStr = Encoding.UTF8.GetString(data);
            return DecodeString<T>(jsonStr);
        }

        public static byte[] GetByteFromList<T>(T t)
        {
            string jsonStr = EncodeDto<T>(t);
            return Encoding.UTF8.GetBytes(jsonStr);
        }

        private static HttpClient _httpClient;
        public static HttpClient GetHttpClient()
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GenerateToken());
            }
            return _httpClient;
        }

        public static HttpClient GetHttpClient1()
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
            return _httpClient;
        }

        public static string GenerateToken()
        {
            var now = DateTime.UtcNow;

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
            };

            var secretKey = "infisupersecret_secretkey!123";
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: "INFIIssuer",
                audience: "INFIAudience",
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(5)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        public IConfigurationRoot Configuration { get; set; }

        public string GetAPIBaseUrl
        {
            get
            {
                return Configuration["Data:APIBASEURL"];
            }
        }

        public string GetExcelAPIBaseUrl
        {
            get
            {
                return Configuration["Excel:APIBASEURL"];
            }
        }
        public static SiteMapNodeModel GetTopMenu(List<RoleInfo> menulist)
        {
            List<RoleInfo> topmenulist = new List<RoleInfo>();
            SiteMapNodeModelList topMenuModelList = new SiteMapNodeModelList();
            SiteMapNodeModelList childMenuModelList = new SiteMapNodeModelList();
            if (menulist != null)
            {
                topmenulist = menulist.FindAll(p => p.ParentId == 0);
            }
            if (topmenulist != null)
            {
                foreach (RoleInfo tmenu in topmenulist)
                {
                    childMenuModelList = new SiteMapNodeModelList();
                    foreach (RoleInfo pmenu in menulist.FindAll(p => p.ParentId == tmenu.Id))
                    {
                        childMenuModelList.Add(new SiteMapNodeModel
                        {
                            Action = pmenu.Action,
                            Controller = pmenu.Controller,
                            Title = pmenu.Name,
                            //Description = pmenu.Description
                        });
                    }
                    topMenuModelList.Add(new SiteMapNodeModel
                    {
                        Action = tmenu.Action,
                        Controller = tmenu.Controller,
                        Title = tmenu.Name,
                        //Description = tmenu.Description,
                        Children = childMenuModelList
                    });
                }
            }
            SiteMapNodeModelList result = new SiteMapNodeModelList();
            result.Add(new SiteMapNodeModel { Children = topMenuModelList });
            return result.FirstOrDefault();
        }

        #region Bytes Of File
        public async Task<byte[]> HttpGetFileBytes(string sourcepath = "")
        {
            var client = GetHttpClient();
            var response = await client.GetAsync(sourcepath);
            byte[] bytes = null;
            if (response.IsSuccessStatusCode == true)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                bytes = new byte[(int)stream.Length];
                stream.Position = 0;
                stream.Read(bytes, 0, bytes.Length);
                stream.Dispose();
            }
            return bytes;
        }

        #endregion
    }
}
