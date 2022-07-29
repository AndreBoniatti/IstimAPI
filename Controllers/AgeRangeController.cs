using System.Collections.Generic;
using System.Threading.Tasks;
using IstimAPI.Data.IRepositories;
using IstimAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace IstimAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AgeRangeController : ControllerBase
    {
        private readonly IAgeRangeRepository _ageRangeRepository;

        public AgeRangeController(IAgeRangeRepository ageRangeRepository)
        {
            _ageRangeRepository = ageRangeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<AgeRange>>> GetAllAgeRanges()
        {
            var ageRanges = await _ageRangeRepository.GetAllAgeRangesAsync();

            return Ok(ageRanges);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AgeRange>> GetAgeRangeById(int id)
        {
            if (!AgeRangeExists(id))
                return NotFound(new { Message = "Esta faixa etária não existe" });

            var ageRange = await _ageRangeRepository.GetAgeRangeByIdAsync(id);

            return Ok(ageRange);
        }

        [HttpPost]
        public async Task<IActionResult> PostAgeRange([FromBody] AgeRange ageRange)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _ageRangeRepository.CreateAgeRangeAsync(ageRange);
                return CreatedAtAction(nameof(GetAgeRangeById), new { id = ageRange.Id }, ageRange);
            }
            catch
            {
                return BadRequest(new { Message = "Não foi possível criar esta faixa etária" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAgeRange(int id, [FromBody] AgeRange ageRange)
        {
            if (!AgeRangeExists(id))
                return NotFound(new { Message = "Esta faixa etária não existe" });

            if (id != ageRange.Id)
                return NotFound(new { Message = "Faixa etária não encontrada" });

            try
            {
                await _ageRangeRepository.UpdateAgeRangeAsync(ageRange);
                return NoContent();
            }
            catch
            {
                return BadRequest(new { Message = "Não foi possível atualizar esta faixa etária" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAgeRange(int id)
        {
            if (!AgeRangeExists(id))
                return NotFound(new { Message = "Esta faixa etária não existe" });

            try
            {
                await _ageRangeRepository.DeleteAgeRangeAsync(id);
                return NoContent();
            }
            catch
            {
                return BadRequest(new { Message = "Não foi possível remover esta faixa etária" });
            }
        }

        private bool AgeRangeExists(int ageRangeId)
        {
            return _ageRangeRepository.AgeRangeExists(ageRangeId);
        }
    }
}