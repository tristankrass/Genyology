using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.V1
{
    public class RelationshipRoleDTO
    {
        public Guid Id { get; set; }

        [MinLength(1), MaxLength(64)]
        public string RelationshipRoleName { get; set; } = default!;

        [MinLength(1), MaxLength(256)]
        public string? RelationshipRoleDescription { get; set; } = default!;

    }
}