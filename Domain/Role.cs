using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
  public class Role : DomainEntity
    {
        [MaxLength(100)]
        public string RoleDescription { get; set; } = default!;

        [InverseProperty(nameof(Relationship.RoleOne))]
        public ICollection<Relationship>? RelationshipsOne { get; set; }
        
        [InverseProperty(nameof(Relationship.RoleTwo))]
        public ICollection<Relationship>? RelationshipsTwo { get; set; }
    }
}
