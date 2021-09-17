using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TheMoney.Shared.Entities
{
    public sealed class User : EntityBase
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ZoneInfo { get; set; }
    }
}
