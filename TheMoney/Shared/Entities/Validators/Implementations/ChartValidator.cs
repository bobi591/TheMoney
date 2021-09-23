using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using TheMoney.Shared.UX.Alerts;

namespace TheMoney.Shared.Entities.Validators.Implementations
{
    public class ChartValidator : IEntityValidator<Chart>
    {
        private IUserAlertsService userAlertsService;

        public ChartValidator(IUserAlertsService userAlertsService)
        {
            this.userAlertsService = userAlertsService;
        }

        public bool Validate(Chart entityToValidate)
        {
            foreach(PropertyInfo chartProperty in typeof(Chart).GetProperties())
            {
                if (chartProperty.GetValue(entityToValidate) == null && chartProperty.Name != "MeasureData" && chartProperty.Name != "DimensionData")
                {
                    userAlertsService.ShowWarning("message.cannot_be_emtpy", CamelCaseToSentenceCase.Convert(chartProperty.Name));
                    return false;
                }
            }

            userAlertsService.ShowInfo("message.is_created_successfully", entityToValidate.Name);
            return true;
        }
    }
}
