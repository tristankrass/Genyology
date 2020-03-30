using System;
using DAL.Base;

namespace Domain
{
    public class FamilyRelationship : DomainEntity
    {
        public Guid RelationshipId { get; set; }
        public Relationship? Relationship { get; set; }

        public Guid FamilyId { get; set; }
        public Family? Family { get; set; }
    }
}
