using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntityMetaData
    {
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
