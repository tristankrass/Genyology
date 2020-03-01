using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DAL.Base;

namespace Domain
{
    public class Family : DomainEntity
    {

        public string FamilyName { get; set; } = default!;

        [MaxLength(60)]
        public string? FamilyDescription { get; set; }

        public DateTime FamilyDateFrom { get; set; } = default!;
        public DateTime FamilyDateTo { get; set; } = default!;

        public Guid PersonId { get; set; } = default!;
        public Person? Person { get; set; }

        public ICollection<Relationship>? Relationships { get; set; }
    }
}
