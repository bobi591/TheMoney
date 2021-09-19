using System;
namespace TheMoney.Shared.Entities.Dimensions
{
    public class Dimension
    {
        public string Description { get; set; }
        public string FieldName { get; set; }
        public EntityBase ParentEntity { get; set; }

        public Dimension(string description, string containingFieldName, EntityBase parentEntity)
        {
            this.Description = description;
            this.FieldName = containingFieldName;
            this.ParentEntity = parentEntity;
        }
    }
}
