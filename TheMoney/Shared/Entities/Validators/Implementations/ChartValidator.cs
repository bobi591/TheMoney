using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using TheMoney.Shared.UXServices;

namespace TheMoney.Shared.Entities.Validators.Implementations
{
    public class ChartValidator : IEntityValidator<Chart>
    {
        private ITranslationService translationService;
        private Controller controller;

        public ChartValidator(Controller controller, ITranslationService translationService)
        {
            this.translationService = translationService;
            this.controller = controller;
        }

        public bool Validate(Chart entityToValidate)
        {
            foreach(PropertyInfo chartProperty in typeof(Chart).GetProperties())
            {
                if(chartProperty.GetValue(entityToValidate) == null)
                {
                    translationService.ShowWarning(controller, "message.cannot_be_emtpy", CamelCaseToSentenceCase.Convert(chartProperty.Name));
                    return false;
                }
            }

            translationService.ShowInfo(controller, "message.is_created_successfully", entityToValidate.Name);
            return true;
        }
    }
}
