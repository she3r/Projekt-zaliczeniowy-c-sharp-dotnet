using System;
using System.Collections.Generic;
using System.Linq;
using ProjektZaliczeniowy.Dtos;
using Xunit;
using System.ComponentModel.DataAnnotations;
namespace Tests
{
    public class DtoTests
    {
        private static readonly Random Random = new();

        private static string _RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
        // [Theory]
        // [InlineData(101,200)]
        // [InlineData(-100,-1)]
        // public void InvalidValue_CreateScoreDto(int min, int max)
        // {
        //     // arrange
        //     Action a = () => new CreateScoreDto
        //     {
        //         StudentName = _RandomString(10),
        //         TeacherName = _RandomString(10),
        //         Value = Random.Next(min,max)
        //     };
        //     Assert.Throws<ArgumentNullException>(() => a());
        // }
        //
        // [Fact]
        // public void InvalidValue_UpdateScoreDto_NoValidation()
        // {
        //     // arrange
        //
        //     var testScore = new UpdateScoreDto()
        //     {
        //         TeacherName = _RandomString(10)
        //     };
        //     var context = new ValidationContext(testScore);
        //     var results = new List<ValidationResult>();
        //     // act
        //     var result = Validator.TryValidateObject(testScore, context, results);
        //     // assert
        //     Assert.False(result);
        // }
        
        [Fact]
        public void NullTeacherName_CreateScoreDto_NoValidation()
        {
            // arrange

            var testScore = new CreateScoreDto
            {
                StudentName = _RandomString(10),
                TeacherName = null, // required not to be null
                Value = Random.Next(1,10)
            };
            var context = new ValidationContext(testScore);
            var results = new List<ValidationResult>();
            // act
            var result = Validator.TryValidateObject(testScore, context, results);
            // assert
            Assert.False(result);
        }
        
        [Fact]
        public void InvalidStudentName_CreateScoreDto_NoValidation()
        {
            // arrange
            var testScore = new CreateScoreDto
            {
                StudentName = null, // required not to be null
                TeacherName = _RandomString(10),
                Value = Random.Next(1,10)
            };
            var context = new ValidationContext(testScore);
            var results = new List<ValidationResult>();
            // act
            var result = Validator.TryValidateObject(testScore, context, results);
            // assert
            Assert.False(result);
        }
    }
}