﻿using System;

namespace ProjektZaliczeniowy.Dtos
{
    public record ScoreDto
    {
        public Guid Id { get; init; }
        public string TeacherName { get; init; }
        public decimal Value { get; init; }     // value of student's score
        public DateTimeOffset CreatedDate { get; init; }
    }
}