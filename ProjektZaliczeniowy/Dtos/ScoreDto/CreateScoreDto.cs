using System.ComponentModel.DataAnnotations;

namespace ProjektZaliczeniowy.Dtos
{
    public record CreateScoreDto
    {
        [Required, Range(0,100)]
        public decimal Value { get; init; }
        [Required]
        public string TeacherName { get; init; }
    }
}