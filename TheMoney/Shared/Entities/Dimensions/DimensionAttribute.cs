using System;
namespace TheMoney.Shared.Entities.Dimensions
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DimensionAttribute : Attribute
    {
        public string Description { get; set; }
        public DimensionAttribute(string description)
        {
            this.Description = description;
        }
    }
}
