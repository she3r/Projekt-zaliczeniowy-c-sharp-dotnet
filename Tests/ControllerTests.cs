using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjektZaliczeniowy.Repositories;
using Xunit;
using Moq;
using ProjektZaliczeniowy.Controllers;
using ProjektZaliczeniowy.Dtos;
using ProjektZaliczeniowy.Entities;

namespace Tests
{
    
    public class ControllerTests
    {
        private static readonly Random Random = new();

        private static string _RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
        [Fact]
        public async Task GetScoreAsync_NotExistingScore_ReturnNull()
        {
            // arrange
            var repoMoq = new Mock<IScoresRepository>();
            repoMoq.Setup(repo => repo.GetScoreOfStudentAsync(It.IsAny<string>(),
                It.IsAny<Guid>())).ReturnsAsync((Score)null);   // repo zwraca null na jakiekolwiek zapytanie GET
            var controller = new ScoresController(repoMoq.Object);
            // act
            var result = await controller.GetScoreAsync(String.Empty, Guid.NewGuid());    // kontroler ma zwrocic not found
            // assert
            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public async Task GetScoreAsync_ExistingScore_ReturnScoreDto()
        {
            // arrange
            var studentName = _RandomString(10);
            var teacherName = _RandomString(10);
            var id = Guid.NewGuid();
            var dateTime = DateTimeOffset.Now;
            var scoreValue = Random.Next(0, 100);   // poprawny zakres
            var repoMoq = new Mock<IScoresRepository>();
            var controller = new ScoresController(repoMoq.Object);
            repoMoq.Setup(repo => repo.GetScoreOfStudentAsync(studentName, id)).ReturnsAsync(new Score
            {
                CreatedDate = dateTime,
                Id = id,
                StudentName = studentName,
                TeacherName = teacherName,
                Value = scoreValue
                // we assume that the score exist 
            });
            // act
            var result = await controller.GetScoreAsync(studentName, id);
            // assert
            Assert.IsType<ScoreDto>(result.Value);
            Assert.Equal(new ScoreDto
            {
                Id = id,
                CreatedDate = dateTime,
                StudentName = null,
                TeacherName = teacherName,
                Value = scoreValue
            }, result.Value);
            
        }

        [Fact]
        public async Task DeleteScoreAsync_NotExistingScore_ReturnNull()
        {
            // arrange
            var repoMoq = new Mock<IScoresRepository>();
            repoMoq.Setup(repo => repo.GetScoreOfStudentAsync(It.IsAny<string>(),
                It.IsAny<Guid>())).ReturnsAsync((Score)null);   // repo zwraca null na jakiekolwiek zapytanie GET
            var controller = new ScoresController(repoMoq.Object);
            // act
            var result = await controller.DeleteScoreAsync(String.Empty, Guid.NewGuid());
            // assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task DeleteScoreAsync_ExistingItem_ReturnNoContent()
        {
            var studentName = _RandomString(10);
            var teacherName = _RandomString(10);
            var id = Guid.NewGuid();
            // arrange
            var repoMoq = new Mock<IScoresRepository>();
            repoMoq.Setup(repo => repo.GetScoreOfStudentAsync(studentName,
                It.IsAny<Guid>())).ReturnsAsync(new Score
            {
                CreatedDate = DateTimeOffset.Now,
                Id = id,
                StudentName = studentName,
                TeacherName = teacherName,
                Value = Random.Next(0,100)
            });   // repo zwraca null na jakiekolwiek zapytanie GET
            repoMoq.Setup(repo => repo.DeleteScoreAsync(studentName, id)).Returns(Task.CompletedTask);
            var controller = new ScoresController(repoMoq.Object);
            // act
            var result = await controller.DeleteScoreAsync(String.Empty, Guid.NewGuid());
            // assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}