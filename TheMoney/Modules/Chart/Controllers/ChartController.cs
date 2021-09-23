using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheMoney.Modules.Chart.Data;
using TheMoney.Modules.Chart.Models;
using TheMoney.Shared.Entities;
using TheMoney.Shared.Entities.Validators;
using TheMoney.Shared.UXServices;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheMoney.Modules.Chart.Controllers
{
    public class ChartController : Controller
    {
        private IRepository repository;
        private ITranslationService translationService;

        public ChartController(IRepository repository, ITranslationService translationService)
        {
            this.repository = repository;
            this.translationService = translationService;
        }

        [Authorize]
        public IActionResult Charts()
        {
            List<Shared.Entities.Chart> allCharts = repository.GetChartsWhere(x => x.Id != null).ToList();
            Shared.Entities.User currentUser = GetCurrentUserInfoFromDatabase();

            List<EntityBase> chartableEntities = new List<EntityBase>();
            chartableEntities.Add(new MonetaryTransaction());


            ChartDataFiller chartDataFiller = new ChartDataFiller(currentUser, repository, translationService);
            chartDataFiller.PopulateChartsWithData(allCharts);

            ChartsPageModel pageModel = new ChartsPageModel(allCharts, chartableEntities);

            return View(pageModel);
        }

        [Authorize]
        public IActionResult Chart(string name)
        {
            var chart = repository.GetChartWhere(x => x.Name == name);
            Shared.Entities.User currentUser = GetCurrentUserInfoFromDatabase();

            ChartDataFiller chartDataFiller = new ChartDataFiller(currentUser, repository, translationService);
            chartDataFiller.PopulateChartWithData(chart);

            return View(chart);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create([FromForm]Shared.Entities.Chart chart)
        {
            IEntityValidator<Shared.Entities.Chart> chartValidator = new EntityValidatorFactory<Shared.Entities.Chart>().CreateValidatorInstance(this, translationService);

            bool isChartValidationOK = chartValidator.Validate(chart);

            if (isChartValidationOK)
            {
                repository.InsertChart(chart);
            }

            return Redirect("/Chart/Charts");
        }

        private Shared.Entities.User GetCurrentUserInfoFromDatabase()
        {
            string email = HttpContext.User.Claims.Where(claim => claim.Type == Shared.ClaimTypes.EMAIL).First().Value;
            return repository.GetUserWhere(x => x.Email == email);
        }
    }
}
