using System.ComponentModel.DataAnnotations;

namespace ProjektZaliczeniowy.Dtos
{
    public record UpdateScoreDto
    {
        [Required, Range(0,100)]
        public decimal Value { get; init; }
        public string TeacherName { get; init; }
    }
}