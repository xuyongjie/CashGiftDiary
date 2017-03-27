using CashGiftDiary.Web.Repo;
using Entity.ResultModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CashGiftDiary.Web.Middlewares
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/token";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromDays(30);

        public SigningCredentials SigningCredentials { get; set; }
    }
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;

        public TokenProviderMiddleware(
          RequestDelegate next,
          IOptions<TokenProviderOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        public Task Invoke(HttpContext context)
        {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                //use new JwtSecurityTokenHandler().ValidateToken() to valid token
                return _next(context);
            }

            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!context.Request.Method.Equals("POST")
              || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }
            return GenerateToken(context);
        }
        private async Task GenerateToken(HttpContext context)
        {
            TokenResult result = new TokenResult();
            var username = context.Request.Form["Phone"];
            var password = context.Request.Form["Password"];

            var identity = await GetIdentity(username, password,context.RequestServices.GetRequiredService<IUserRepository>());
            if (identity == null)
            {
                result.StatusCode = Constant.STATUS_CODE_ERROR;
                result.Desc = "手机号或密码错误";
                // Serialize and return the response
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                return;
            }

            var now = DateTime.UtcNow;

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(ClaimTypes.Name,username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
            };

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
              issuer: _options.Issuer,
              audience: _options.Audience,
              claims: claims,
              notBefore: now,
              expires: now.Add(_options.Expiration),
              signingCredentials: _options.SigningCredentials);
            //var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var encodedJwt = new JwtSecurityTokenHandler().CreateEncodedJwt(
                issuer: _options.Issuer,
                audience: _options.Audience,
                subject: new ClaimsIdentity(claims),
                notBefore: now,
                expires: now.Add(_options.Expiration),
                issuedAt:now,
              signingCredentials: _options.SigningCredentials);
            result.StatusCode = Constant.STATUS_CODE_OK;
            result.Desc = "Success";
            result.ResponseData = new TokenModel()
            {
                AccessToken = encodedJwt,
                ExpiresIn = (int)_options.Expiration.TotalSeconds
            };
            // Serialize and return the response
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(result, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private Task<ClaimsIdentity> GetIdentity(string username, string password,IUserRepository userRepo)
        {
            // DON'T do this in production, obviously!
            if(!string.IsNullOrEmpty(username)&&!string.IsNullOrEmpty(password))
            {
                var identity = userRepo.CheckUser(username, password);
                if(identity!=null&&identity.StatusCode.Equals(Constant.STATUS_CODE_OK))
                {
                    return Task.FromResult(new ClaimsIdentity(new System.Security.Principal.GenericIdentity(username, "Token"), new Claim[] { }));
                }
            }
            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        }

        public static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


    }
}
