using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
  public class Relationship : DomainEntity
    {
        [MinLength(1), MaxLength(128)]
        public string? RelationshipName { get; set; }

        [MaxLength(256)]
        public string? RelationShipDetails { get; set; }

        public DateTime DateTimeFrom { get; set; } = default!;
        public DateTime DateTimeTo { get; set; } = default!;

        // Family relationship starts here
        public ICollection<FamilyRelationship>? Families { get; set; }

        // Person Relationship starts here
        public Guid PersonOneId { get; set; } = default!;
        public Person? PersonOne { get; set; }

        // [ForeignKey(nameof(PersonTwo))]
        public Guid PersonTwoId { get; set; } = default!;
        public Person? PersonTwo { get; set; }

        public Guid? RoleOneId { get; set; } = default!;

        public RelationshipRole? RoleOne { get; set; }

        public Guid? RoleTwoId { get; set; } = default!;
        public RelationshipRole? RoleTwo { get; set; }

        // Relationship here
        public Guid? RelationshipTypeId { get; set; } = default!;
        public RelationshipType? RelationshipType { get; set; }
        
    }
}
