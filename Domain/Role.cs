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

        [InverseProperty(nameof(Relationship.PersonOne))]
        public ICollection<Relationship>? RelationshipsOne { get; set; }
        
        [InverseProperty(nameof(Relationship.PersonTwo))]
        public ICollection<Relationship>? RelationshipsTwo { get; set; }
    }
}
