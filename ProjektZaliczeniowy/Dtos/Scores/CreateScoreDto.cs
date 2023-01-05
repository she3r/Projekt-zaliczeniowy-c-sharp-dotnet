using System.ComponentModel.DataAnnotations;

namespace ProjektZaliczeniowy.Dtos
{
    public record CreateScoreDto
    {
        [Required, Range(0,100,ErrorMessage = "* must be a percentage value 0-100%")]
        public decimal Value { get; init; }
        [Required,StringLength(maximumLength:100,MinimumLength = 2,ErrorMessage = "* must be between 2 and 100 in length")]
        public string StudentName { get; init; }
        
        [Required,StringLength(maximumLength:100,MinimumLength = 2,ErrorMessage = "* must be between 2 and 100 in length")]
        public string TeacherName { get; init; }
        
        // public Guid Id { get; init; }
        // public string StudentName { get; init; }
        // public string TeacherName { get; init; }
        // public decimal Value { get; init; }     // value of student's score
        // public DateTimeOffset CreatedDate { get; init; }
    }
}