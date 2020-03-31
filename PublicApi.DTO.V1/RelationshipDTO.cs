using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.V1
{
    public class RelationshipDTO
    {
        public Guid Id { get; set; }

        [MinLength(1), MaxLength(128)]
        public string? RelationshipName { get; set; }

        [MaxLength(256)]
        public string? RelationShipDetails { get; set; }

        public DateTime DateTimeFrom { get; set; } = default!;
        public DateTime DateTimeTo { get; set; } = default!;

        public Guid PersonOneId { get; set; } = default!;

        public Guid PersonTwoId { get; set; } = default!;

        public Guid? RoleOneId { get; set; } = default!;

        public Guid? RoleTwoId { get; set; } = default!;

        public Guid? RelationshipTypeId { get; set; } = default!;
    }
}