using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer4.Contrib.Administration.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClaimsController : ControllerBase
    {
        [HttpGet("jwt")]
        public IReadOnlyDictionary<string, string> LoadClaims()
        {
            var type = typeof(JwtClaimTypes);

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

            var claims = fields
                .Where(field => field.IsLiteral && field.GetRawConstantValue() is string claim &&
                                !string.IsNullOrWhiteSpace(claim))
                .Select(field => new KeyValuePair<string, string>(field.Name, (string) field.GetRawConstantValue()!));

            return claims.ToDictionary(claimTypeByCommonName => claimTypeByCommonName.Key,
                claimTypeByCommonName => claimTypeByCommonName.Value);
        }
    }
}