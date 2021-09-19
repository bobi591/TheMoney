using System;
using System.Collections.Generic;
using TheMoney.Shared.Entities;
using TheMoney.Shared.Entities.Dimensions;

namespace TheMoney.Modules.Chart.Models
{
    public sealed class ChartsPageModel
    {
        public ChartsPageModel(IEnumerable<Shared.Entities.Chart> charts, List<EntityBase> chartableEntities)
        {
            this.Charts = charts;
            this.ChartableEntities = chartableEntities;
        }

        public IEnumerable<Shared.Entities.Chart> Charts { get; }
        public IEnumerable<EntityBase> ChartableEntities { get; }
    }
}
