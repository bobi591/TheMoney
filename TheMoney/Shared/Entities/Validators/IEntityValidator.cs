using System;
namespace TheMoney.Shared.Entities.Validators
{
    public interface IEntityValidator<T> where T: EntityBase?
    {
        public bool Validate(T entityToValidate);
    }
}
