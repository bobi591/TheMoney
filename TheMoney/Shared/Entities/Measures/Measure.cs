using System;
namespace TheMoney.Shared.Entities.Dimensions
{
    public class Measure
    {
        public string Description { get; set; }
        public string FieldName { get; set; }
        public EntityBase ParentEntity { get; set; }

        public Measure(string description, string containingFieldName, EntityBase parentEntity)
        {
            this.Description = description;
            this.FieldName = containingFieldName;
            this.ParentEntity = parentEntity;
        }
    }
}
