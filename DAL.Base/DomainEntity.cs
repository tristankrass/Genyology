using System;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class DomainEntity : DomainEntity<Guid>
    {
    }

    public abstract class DomainEntity<TKey> : IDomainEntity<TKey>
        where TKey : struct, IComparable
    {
        public virtual TKey Id { get; set; }
        public virtual string? CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string? ChangedBy { get; set; }
        public virtual DateTime ChangedAt { get; set; }
    }
}
