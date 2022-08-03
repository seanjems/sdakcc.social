using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace sdakcc.Web
{
    public class sdakKccTokenValidator : JwtSecurityTokenHandler
    {
        

            public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters,
                out SecurityToken validatedToken)
            {
                validatedToken = null;
                var rst = base.ValidateToken(token, validationParameters, out validatedToken);
                //var info = validatedToken;
                var claims = rst.Claims;

                return rst;
            }
        
    }
}
