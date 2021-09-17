using Microsoft.AspNetCore.Mvc;
using Okta.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using TheMoney.Shared.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheMoney.Modules.User.Controllers
{
    public class UserController : Controller
    {
        private IRepository repository;

        public UserController(IRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn([FromForm] string sessionToken)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                var properties = new AuthenticationProperties();
                properties.Items.Add("sessionToken", sessionToken);
                properties.RedirectUri = "/User/UpdateUserDatabaseInfoOnLogin";

                return Challenge(properties, OktaDefaults.MvcAuthenticationScheme);
            }

            return Redirect("/Home/");
        }

        [Authorize]
        public IActionResult Logout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync();
            }

            return Redirect("/Home/");
        }

        [Authorize]
        public IActionResult Details()
        {
            return View(GetCurrentUserInfoFromDatabase());
        }

        [Authorize]
        public IActionResult UpdateUserDatabaseInfoOnLogin()
        {
            Shared.Entities.User userDataFromClaims = GenerateUserObjectFromHttpContextUserClaims();

            bool isUserExisting = repository.GetUserWhere(x => x.Email == userDataFromClaims.Email) != null ? true : false;

            if (isUserExisting)
            {
                repository.ReplaceUserWhere(x => x.Email == userDataFromClaims.Email, userDataFromClaims);
            }
            else
            {
                repository.InsertUser(userDataFromClaims);
            }

            return Redirect("/Home/");
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
