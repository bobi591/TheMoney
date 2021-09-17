using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheMoney.Modules.Data.Actions.DSKBank;
using TheMoney.Shared.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheMoney.Modules.Data.Controllers
{
    public class DataController : Controller
    {
        private IRepository repository;

        public DataController(IRepository repository)
        {
            this.repository = repository;
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Import(IFormFile dataFile)
        {
            Shared.Entities.User currentUser = GetCurrentUserInfoFromDatabase();

            return Redirect("/");
        }

        private Shared.Entities.User GetCurrentUserInfoFromDatabase()
        {
            string username = GenerateUserObjectFromHttpContextUserClaims().Username;
            return repository.GetUserWhere(x => x.Username == username);
        }

        private Shared.Entities.User GenerateUserObjectFromHttpContextUserClaims()
        {
            IEnumerable<Claim> claims = HttpContext.User.Claims;
            Shared.Entities.User currentUser = new Shared.Entities.User()
            {
                Email = claims.Where(claim => claim.Type == Shared.ClaimTypes.EMAIL).FirstOrDefault().Value,
                Name = claims.Where(claim => claim.Type == Shared.ClaimTypes.NAME).FirstOrDefault().Value,
                Username = claims.Where(claim => claim.Type == Shared.ClaimTypes.USERNAME).FirstOrDefault().Value,
                ZoneInfo = claims.Where(claim => claim.Type == Shared.ClaimTypes.ZONEINFO).FirstOrDefault().Value,
            };

            return currentUser;
        }
    }
}
