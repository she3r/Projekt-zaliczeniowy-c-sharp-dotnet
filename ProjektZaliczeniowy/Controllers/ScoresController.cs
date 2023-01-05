using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Catalog;
using Microsoft.AspNetCore.Mvc;
using ProjektZaliczeniowy.Dtos;
using ProjektZaliczeniowy.Entities;
using ProjektZaliczeniowy.Repositories;

namespace ProjektZaliczeniowy.Controllers
{   
    [ApiController]
    [Route("scores")]
    public class ScoresController : ControllerBase
    {
        private readonly IScoresRepository _repository;

        public ScoresController(IScoresRepository repository)
        {
            _repository = repository;
        }
        // GET /scores
        // [HttpGet]
        // public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        // {
        //     var items = (await _repository.GetItemsAsync()).Select(item => item.AsDto());
        //     return items;
        // }
        
        // get all scores of student
        // GET scores/studentName
        [HttpGet("{studentName}")]
        public async Task<IEnumerable<ScoreDto>> GetScoresAsync(string studentName)
        {
            var scores = (await _repository.GetAllScoresOfStudentAsync(studentName)).Select(score => score.AsDto());
            return scores;
        }
        
        // get particular score
        // GET scores/studentName/Id
        [HttpGet("{studentName}/{Id}")]
        public async Task<ActionResult<ScoreDto>> GetScoreAsync(string studentName, Guid Id)
        {
            var score = await _repository.GetScoreOfStudentAsync(studentName, Id);
            if (score is null || !score.StudentName.Equals(studentName))
            {
                return NotFound();
            }

            return score.AsDto();
        }

        // post a score for a student
        // POST /scores     info of studentName in scoreDto
        [HttpPost]
        public async Task<ActionResult<ScoreDto>> CreateScoreAsync(CreateScoreDto scoreDto)
        {
            Score score = new()
            {
                Id = Guid.NewGuid(), StudentName = scoreDto.StudentName, TeacherName = scoreDto.TeacherName, Value = scoreDto.Value,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await _repository.CreateScoreAsync(score);
            
            // ReSharper disable once Mvc.ActionNotResolved
            return CreatedAtAction(nameof(GetScoreAsync), new { studentName = score.StudentName,id = score.Id }, score.AsDto());
        }
        // update score
        // PUT /items/id
        [HttpPut("{studentName}/{Id}")]
        public async Task<ActionResult> UpdateScoreAsync(string studentName, Guid id, UpdateScoreDto scoreDto)
        {
            var existingScore = await _repository.GetScoreOfStudentAsync(studentName, id);
            if (existingScore is null || !existingScore.StudentName.Equals(studentName))
            {
                return NotFound();
            }

            string teacherName = existingScore.TeacherName;

            if (scoreDto.TeacherName is not null)
            {
                teacherName = existingScore.StudentName;
            }

            Score updatedScore = existingScore with
            {
                Value = scoreDto.Value, TeacherName = teacherName
            };

            await _repository.UpdateScoreOfStudentAsync(updatedScore);

            return NoContent();
        }
        
        // delete all scores
        // DELETE /studentName
        [HttpDelete("{studentName}")]
        public async Task<ActionResult> DeleteAllScoresAsync(string studentName)
        {
            var existingScoresIds = (await _repository.GetAllScoresOfStudentAsync(studentName)).Select(score => score.Id);
            await _repository.DeleteAllScoresAsync(studentName);
            return NoContent();
        }
        
        // delete score
        // DELETE /studentName/id 
        [HttpDelete("{studentName}/{Id}")]
        public async Task<ActionResult> DeleteScoreAsync(string studentName, Guid id)
        {
            var existingScore = await _repository.GetScoreOfStudentAsync(studentName, id);
            if (existingScore is null || !existingScore.StudentName.Equals(studentName))
            {
                return NotFound();
            }

            await _repository.DeleteScoreAsync(studentName, id);
            return NoContent();
        }
    }
}