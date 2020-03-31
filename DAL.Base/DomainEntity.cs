using System;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class DomainEntity : DomainEntity<Guid>
    {
    }

    public abstract class DomainEntity<TKey> : IDomainEntity<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }
        public virtual Guid? CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual Guid? ChangedBy { get; set; }
        public virtual DateTime ChangedAt { get; set; }
    }
}
