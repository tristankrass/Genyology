using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntity : IDomainEntity<Guid>
    {
    }

    public interface IDomainEntity<TKey> : IDomainBaseEntity<TKey>, IDomainEntityMetaData
        where TKey : struct, IEquatable<TKey>
    {
    }


}