using System;

namespace ProjektZaliczeniowy.Entities
{
    public record Score
    {
        public Guid Id { get; init; }
        public string StudentName { get; init; }
        public string TeacherName { get; init; }
        public decimal Value { get; init; }     // value of student's score
        public DateTimeOffset CreatedDate { get; init; }
    }
}