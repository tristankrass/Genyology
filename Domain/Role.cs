using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  public class Role
    {
        public Guid RoleId { get; set; }

        [MaxLength(100)]
        public string RoleDescription { get; set; } = default!;

        [InverseProperty(nameof(Relationship.PersonOne))]
        public ICollection<Relationship>? RelationshipsOne { get; set; }
        
        [InverseProperty(nameof(Relationship.PersonTwo))]
        public ICollection<Relationship>? RelationshipsTwo { get; set; }
    }
}
