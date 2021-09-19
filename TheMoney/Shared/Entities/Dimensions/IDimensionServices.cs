using System;
using System.Collections.Generic;

namespace TheMoney.Shared.Entities.Dimensions
{
    public interface IDimensionServices
    {
        public List<Dimension> GetDimensionsInEntity(EntityBase entity);
        public List<Measure> GetMeasuresInEntity(EntityBase entity);
    }
}
