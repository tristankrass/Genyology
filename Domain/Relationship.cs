using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
  public class Relationship : DomainEntity
    {
        public DateTime DateTimeFrom { get; set; } = default!;
        public DateTime DateTimeTo { get; set; } = default!;

        [MaxLength(60)]
        public string? RelationShipDetails { get; set; }


        // Family relationship starts here
        public Guid FamilyId { get; set; } = default!;
        public Family? Family { get; set; }


        // Person Relationship starts here
        [ForeignKey(nameof(PersonOne))]
        public Guid PersonOneId { get; set; } = default!;
        public Person? PersonOne { get; set; }

        [ForeignKey(nameof(PersonTwo))]
        public Guid PersonTwoId { get; set; } = default!;
        public Person? PersonTwo { get; set; }

        // Role Relationship starts here
        [ForeignKey(nameof(RoleOne))]
        public Guid RoleOneId { get; set; } = default!;

        public Role? RoleOne { get; set; }

        [ForeignKey(nameof(RoleTwo))]
        public Guid RoleTwoId { get; set; } = default!;
        public Role? RoleTwo { get; set; }

        // Relationship here
        public Guid RelationshipTypeId { get; set; } = default!;
        public RelationshipType? RelationshipType { get; set; }
        
    }
}
