using System.Security.Claims;

namespace Blog.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static int CompanyId(this ClaimsPrincipal claims)
        {
            try
            {
                var value = claims?.FindFirst("CompanyId")?.Value;
                return int.Parse(value);
            }
            catch
            {
                return 0;
            }
        }
    }
}