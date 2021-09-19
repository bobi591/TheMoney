using System;
namespace TheMoney.Shared.Entities.Dimensions
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class MeasureAttribute : Attribute
    {
        public string Description { get; set; }
        public MeasureAttribute(string description)
        {
            this.Description = description;
        }
    }
}
