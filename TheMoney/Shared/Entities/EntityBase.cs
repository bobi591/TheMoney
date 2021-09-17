using System;
using System.ComponentModel.DataAnnotations;

namespace TheMoney.Shared.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
