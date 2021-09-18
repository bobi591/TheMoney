using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheMoney.Modules.Data.Actions;
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
            ImportAction importAction = new ImportAction(currentUser, dataFile, repository, ImportSource.REVOLUT);
            importAction.Execute();

            return Redirect("/");
        }

        private Shared.Entities.User GetCurrentUserInfoFromDatabase()
        {
            string email = HttpContext.User.Claims.Where(claim => claim.Type == Shared.ClaimTypes.EMAIL).FirstOrDefault().Value;
            return repository.GetUserWhere(x => x.Username == email);
        }
    }
}
