using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class RelationshipType
    {
        public Guid RelationshipTypeId { get; set; }

        [MaxLength(20)] public string RelationShipTypeDescription { get; set; } = default!;

        private ICollection<Relationship>? Relationships { get; set; }
    }
}