using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Role
    {
        public Guid RoleId { get; set; }

        [MaxLength(20)]
        public string RoleDescription { get; set; } = default!;

        public ICollection<Relationship>? RelationshipsOne { get; set; }
        public ICollection<Relationship>? RelationshipsTwo { get; set; }
    }
}
