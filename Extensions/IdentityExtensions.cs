using System;
using System.Linq;
using System.Security.Claims;

namespace Extensions
{
    public static class IdentityExtensions
    {
        public static Guid UserId(this ClaimsPrincipal user)
        {
            return Guid.Parse(user.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        }
    }
    
    
    

    

}
