using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eureka.Extensions
{
    static class AppUserExtension
    {
        class AppClaimTypes
        {
            public const string Username = "Username";
            public const string AreaId = "AreaId";
            public const string Area = "Area";
            public const string Correo = "Correo";
        }

        internal static ClaimsPrincipal CreatePrincipal(this AppUser usr)
        {
            var principal = new ClaimsPrincipal();

            var claims = new ClaimsIdentity(
                new Claim[] {
                                new Claim(ClaimTypes.Name,usr.Username),
                                new Claim(AppClaimTypes.AreaId,usr.AreaId.ToString()),
                                new Claim(AppClaimTypes.Area,usr.Area),
                                new Claim(AppClaimTypes.Correo,usr.Correo),
                }, "Password");            

            principal.AddIdentity(claims);

            return principal;
        }

        internal static void LoadFromClaimsPrincipal(this AppUser usr, ClaimsPrincipal principal)
        {
            foreach (var claim in principal.Claims)
            {
                if (claim.Type == AppClaimTypes.Username)
                    usr.Username = Convert.ToString(claim.Value);

                if (claim.Type == AppClaimTypes.AreaId)
                    usr.AreaId = Convert.ToInt32(claim.Value);

                if (claim.Type == AppClaimTypes.Area)
                    usr.Area = Convert.ToString(claim.Value);

                if (claim.Type == AppClaimTypes.Correo)
                    usr.Correo = Convert.ToString(claim.Value);
            }
        }
    }

    public class AppUser : IAppUser
    {
        public string Username { get; set; }
        public int AreaId { get; set; }
        public string Area { get; set; }
        public string Correo { get; set; }

    }
}
