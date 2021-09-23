using System;
using Microsoft.AspNetCore.Mvc;
using TheMoney.Shared.Entities.Validators.Implementations;
using TheMoney.Shared.UX.Alerts;

namespace TheMoney.Shared.Entities.Validators
{
    public class EntityValidatorFactory<T> where T : EntityBase?
    {
        public IEntityValidator<T> CreateValidatorInstance(IUserAlertsService userAlertsService)
        {
            Type genericParameterType = typeof(T);

            if(genericParameterType == typeof(Chart))
            {
                IEntityValidator<Chart> chartValidator = new ChartValidator(userAlertsService);
                return (IEntityValidator<T>)chartValidator;
            }
            else
            {
                throw new InvalidOperationException(typeof(T).Name + " has no supported validator.");
            }
        }
    }
}
