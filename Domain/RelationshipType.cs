using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DAL.Base;

namespace Domain
{
    public class RelationshipType : DomainEntity
    {

        [MaxLength(20)] public string RelationShipTypeDescription { get; set; } = default!;

        private ICollection<Relationship>? Relationships { get; set; }
    }
}