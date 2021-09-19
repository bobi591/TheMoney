using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheMoney.Modules.Chart.Models;
using TheMoney.Shared.Entities;
using TheMoney.Shared.Entities.Dimensions;
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
            IEnumerable<Shared.Entities.Chart> allCharts = repository.GetChartsWhere(x => x.Id != null).ToList();

            List<EntityBase> chartableEntities = new List<EntityBase>();

            chartableEntities.Add(new MonetaryTransaction());

            PopulateChartWithData(allCharts.ToList());

            ChartsPageModel pageModel = new ChartsPageModel(allCharts, chartableEntities);

            return View(pageModel);
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

        private void PopulateChartWithData(List<Shared.Entities.Chart> chartsToPopulate)
        {
            string currentUserEmail = GetCurrentUserInfoFromDatabase().Email;
            IEnumerable<MonetaryTransaction> currentUserTransactions = repository.GetMonetaryTransactionsWhere(x => x.OwnerEmail == currentUserEmail);

            foreach(var chart in chartsToPopulate)
            {
                string requiredMeasureProperty = chart.Measure;
                string requiredDimensionProperty = chart.Dimension;

                chart.MeasureData = new List<string>();
                chart.DimensionData = new List<string>();

                foreach(var transaction in currentUserTransactions)
                {
                    foreach(var property in transaction.GetType().GetProperties())
                    {
                        if(property.Name == requiredMeasureProperty)
                        {
                            chart.MeasureData.Add(property.GetValue(transaction).ToString());
                        }
                        if(property.Name == requiredDimensionProperty)
                        {
                            chart.DimensionData.Add(property.GetValue(transaction).ToString());
                        }
                    }
                }
            }
        }
    }
}
