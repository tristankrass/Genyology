using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.V1
{
    public class RelationshipTypeDTO
    {
        public Guid Id { get; set; }

        [MinLength(1), MaxLength(128)]
        public string RelationshipTypeName { get; set; } = default!;

        [MinLength(1), MaxLength(256)] public string? RelationShipTypeDescription { get; set; }

    }
}