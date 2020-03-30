using System;
using DAL.Base;

namespace Domain
{
    public class PersonFamily : DomainEntity
    {
        public Guid PersonId { get; set; } = default!;
        public Person? Person { get; set; } 
        public Guid FamilyId { get; set; } = default!;
        public Family? Family { get; set; } = default!;
    }
}