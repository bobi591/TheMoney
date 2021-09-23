using System;
using System.Collections.Generic;
using TheMoney.Shared.Entities;
using TheMoney.Shared.UXServices;

namespace TheMoney.Modules.Chart.Data
{
    public class ChartDataFiller
    {
        private IRepository repository;
        private ITranslationService translationService;
        private Shared.Entities.User user;

        public ChartDataFiller(Shared.Entities.User user, IRepository repository, ITranslationService translationService)
        {
            this.repository = repository;
            this.translationService = translationService;
            this.user = user;
        }

        public void PopulateChartWithData(Shared.Entities.Chart chart)
        {
            string currentUserEmail = user.Email;
            IEnumerable<MonetaryTransaction> currentUserTransactions = repository.GetMonetaryTransactionsWhere(x => x.OwnerEmail == currentUserEmail);

            string requiredMeasureProperty = chart.Measure;
            string requiredDimensionProperty = chart.Dimension;

            chart.MeasureData = new List<string>();
            chart.DimensionData = new List<string>();

            foreach (var transaction in currentUserTransactions)
            {
                foreach (var property in transaction.GetType().GetProperties())
                {
                    if (property.Name == requiredMeasureProperty)
                    {
                        chart.MeasureData.Add(property.GetValue(transaction).ToString());
                    }
                    if (property.Name == requiredDimensionProperty)
                    {
                        chart.DimensionData.Add(property.GetValue(transaction).ToString());
                    }
                }
            }
        }

        public void PopulateChartsWithData(List<Shared.Entities.Chart> chartsToPopulate)
        {
            foreach (var chart in chartsToPopulate)
            {
                PopulateChartWithData(chart);
            }
        }
    }
}
