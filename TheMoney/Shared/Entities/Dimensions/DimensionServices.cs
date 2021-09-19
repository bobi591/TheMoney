using System;
using System.Collections.Generic;
using System.Reflection;

namespace TheMoney.Shared.Entities.Dimensions
{
    public sealed class DimensionServices : IDimensionServices
    {
        List<Dimension> IDimensionServices.GetDimensionsInEntity(EntityBase entity)
        {
            Type entityType = entity.GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();
            List<Dimension> dimensionsInEntity = new List<Dimension>();

            foreach(PropertyInfo property in entityProperties)
            {
                DimensionAttribute dimensionAttribute = property.GetCustomAttribute<DimensionAttribute>();
                if(dimensionAttribute != null)
                {
                    Dimension foundDimension = new Dimension(dimensionAttribute.Description, property.Name, entity);
                    dimensionsInEntity.Add(foundDimension);
                }
            }

            return dimensionsInEntity;
        }

        List<Measure> IDimensionServices.GetMeasuresInEntity(EntityBase entity)
        {
            Type entityType = entity.GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();
            List<Measure> measuresInEntity = new List<Measure>();

            foreach (PropertyInfo property in entityProperties)
            {
                MeasureAttribute measureAttribute = property.GetCustomAttribute<MeasureAttribute>();
                if (measureAttribute != null)
                {
                    Measure foundMeasure = new Measure(measureAttribute.Description, property.Name, entity);
                    measuresInEntity.Add(foundMeasure);
                }
            }

            return measuresInEntity;
        }
    }
}
