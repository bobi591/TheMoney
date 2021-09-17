using System;
using Microsoft.AspNetCore.Mvc;
using TheMoney.Shared.Entities.Validators.Implementations;
using TheMoney.Shared.UXServices;

namespace TheMoney.Shared.Entities.Validators
{
    public class EntityValidatorFactory<T> where T : EntityBase?
    {
        public IEntityValidator<T> CreateValidatorInstance(Controller controller, ITranslationService translationService)
        {
            Type genericParameterType = typeof(T);

            if(genericParameterType == typeof(Chart))
            {
                IEntityValidator<Chart> chartValidator = new ChartValidator(controller, translationService);
                return (IEntityValidator<T>)chartValidator;
            }
            else
            {
                throw new InvalidOperationException(typeof(T).Name + " has no supported validator.");
            }
        }
    }
}
