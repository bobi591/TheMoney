using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheMoney.Shared.Entities
{
    public class Chart : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Label { get; set; }
        public List<string> Labels { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public List<float> Data { get; set; } = new List<float>();
    }
}
