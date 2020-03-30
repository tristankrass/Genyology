using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class RelationshipType : DomainEntity
    {

        [MinLength(1), MaxLength(128)]
        public string RelationshipTypeName { get; set; } = default!;

        [MinLength(1), MaxLength(256)] public string? RelationShipTypeDescription { get; set; }

        private ICollection<Relationship>? Relationships { get; set; }
    }
}