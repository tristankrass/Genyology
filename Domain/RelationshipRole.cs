using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
  public class RelationshipRole : DomainEntity
  {

        [MinLength(1), MaxLength(64)]
        public string RelationshipRoleName { get; set; } = default!;

        [MinLength(1), MaxLength(256)]
        public string? RelationshipRoleDescription { get; set; } = default!;

        [InverseProperty(nameof(Relationship.RoleOne))]
        public ICollection<Relationship>? RelationshipsOne { get; set; }
        
        [InverseProperty(nameof(Relationship.RoleTwo))]
        public ICollection<Relationship>? RelationshipsTwo { get; set; }
    }
}
