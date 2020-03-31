using System;
using System.Security.Claims;
using DAL.Base;

namespace Extensions
{
    public static class MetaData
    {
        public static DomainEntity AddInitialClassMetaData(DomainEntity domainEntity, ClaimsPrincipal user)
        {
            domainEntity.CreatedAt = DateTime.Now;
            domainEntity.CreatedBy = user.UserId();
            domainEntity.ChangedAt = DateTime.Now;
            domainEntity.ChangedBy = user.UserId();
            return domainEntity;
        }

        public static DomainEntity AddInitialClassMetaDataSeeding(DomainEntity domainEntity, Guid user)
        {
            domainEntity.CreatedAt = DateTime.Now;
            domainEntity.CreatedBy = user;
            domainEntity.ChangedAt = DateTime.Now;
            domainEntity.ChangedBy = user;
            return domainEntity;
        }

    }
}