using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Family : DomainEntity
    {
        [MinLength(1), MaxLength(128)]
        public string FamilyName { get; set; } = default!;

        [MaxLength(60)]
        public string? FamilyDescription { get; set; }

        public DateTime? FamilyDateFrom { get; set; } = default!;
        public DateTime? FamilyDateTo { get; set; } = default!;

        public ICollection<FamilyRelationship>? Relationships { get; set; }
    }
}
