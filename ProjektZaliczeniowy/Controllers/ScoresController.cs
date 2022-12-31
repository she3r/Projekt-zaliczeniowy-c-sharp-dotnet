using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        // GET /scores/user_id
        [HttpGet("{userID}")]
        public async Task<IEnumerable<ScoreDto>> GetScoresAsync(Guid userID)
        {
            var scores = (await _repository.GetScoresAsync()).Select(score => score.AsDto()).Where(score => score.Id == userID);
            return scores;
        }
        // GET /scores/userID/scoreID
        [HttpGet("{userID}/{scoreID}")]
        public async Task<ActionResult<ScoreDto>> GetScoreAsync(Guid userID,Guid scoreID)
        {
            var item = await _repository.GetScoreAsync(userID,scoreID);
            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
        }
        // POST /scores/userID
        [HttpPost("{userID}")]
        public async Task<ActionResult<ScoreDto>> CreateScoreAsync(Guid userID,CreateScoreDto itemDto)
        {
            Score item = new()
            {
                Id = Guid.NewGuid(), TeacherName = itemDto.TeacherName, Value = itemDto.Value,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await _repository.CreateScoreAsync(userID, item);
            
            // ReSharper disable once Mvc.ActionNotResolved
            return CreatedAtAction(nameof(GetScoreAsync), new { id = item.Id }, item.AsDto());
        }
        // PUT /scores/userID/scoreID
        [HttpPut("{userID}/{scoreID}")]
        public async Task<ActionResult> UpdateItemAsync(Guid userID, Guid scoreID, UpdateScoreDto scoreDto)
        {
            var existingItem = await _repository.GetScoreAsync(userID, scoreID);
            if (existingItem is null)
            {
                return NotFound();
            }

            var updatedItem = existingItem with
            {
                TeacherName = scoreDto.TeacherName,
                Value = scoreDto.Value
            };
            
            await _repository.UpdateScoreAsync(userID,scoreID,updatedItem);

            return NoContent();
        }
        // DELETE /scores/userID/scoreID 
        [HttpDelete("{userID}/{scoreID}")]
        public async Task<ActionResult> DeleteItemAsync(Guid userID, Guid scoreID)
        {
            var existingItem = await _repository.GetScoreAsync(userID, scoreID);
            if (existingItem is null)
            {
                return NotFound();
            }
            
            await _repository.DeleteScoreAsync(userID,scoreID);
            return NoContent();
        }
        
        // DELETE /scores/userID
        [HttpDelete("{userID}")]
        public async Task<ActionResult> DeleteUserScores(Guid userID)
        {
            await _repository.DeleteUserAsync(userID);
            return NoContent();

        }
        
    }
}