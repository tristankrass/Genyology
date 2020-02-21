using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Relationship
    {
        public Guid RelationshipId { get; set; }

        public DateTime DateTimeFrom { get; set; } = default!;
        public DateTime DateTimeTo { get; set; } = default!;

        [MaxLength(60)]
        public string? RelationShipDetails { get; set; }

        public Guid FamilyId { get; set; } = default!;
        public Family? Family { get; set; }

        public Guid PersonOneId { get; set; } = default!;
        public Person? PersonOne { get; set; }

        public Guid PersonTwoId { get; set; } = default!;
        public Person? PersonTwo { get; set; }

        public Guid RoleOneId { get; set; } = default!;
        public Role? RoleOne { get; set; }
        public Guid RoleTwoId { get; set; } = default!;
        public Role? RoleTwo { get; set; }

        public Guid RelationshipTypeId { get; set; } = default!;
        public RelationshipType? RelationshipType { get; set; }
    }
}
