﻿using System.ComponentModel.DataAnnotations;

namespace ProjektZaliczeniowy.Dtos
{
    public record CreateItemDto
    {
        [Required, Range(1,1000)]
        public decimal Price { get; init; }
        [Required]
        public string Name { get; init; }
    }
}