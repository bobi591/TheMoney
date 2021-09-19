using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheMoney.Shared.Entities.Dimensions;

namespace TheMoney.Shared.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public IEnumerable<Dimension> GetDimensions()
        {
            IDimensionServices dimensionServices = new DimensionServices();
            IEnumerable<Dimension> currentEntityDimensions = dimensionServices.GetDimensionsInEntity(this);

            return currentEntityDimensions;
        }

        public IEnumerable<Measure> GetMeasures()
        {
            IDimensionServices dimensionServices = new DimensionServices();
            IEnumerable<Measure> currentEntityMeasures = dimensionServices.GetMeasuresInEntity(this);

            return currentEntityMeasures;
        }
    }
}
