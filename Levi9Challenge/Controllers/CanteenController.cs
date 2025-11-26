using Levi9Challenge.DTOs;
using Levi9Challenge.Models;
using Levi9Challenge.Repositories.Interfaces;
using Levi9Challenge.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Levi9Challenge.Controllers
{
    [ApiController]
    [Route("canteens")]
    public class CanteenController : ControllerBase
    {

        private readonly ICanteenService _canteenService;

        public CanteenController(ICanteenService canteenService)
        {
            _canteenService = canteenService;
        }

        [HttpPost]
        public ActionResult<Canteen> CreateCanteen(
            [FromBody] Canteen canteen,
            [FromHeader(Name = "studentId")] string studentId)
        {
            try
            {
                var createdCanteen = _canteenService.Create(canteen, studentId);

                if (createdCanteen.Id.Equals("0"))
                {

                    return StatusCode(403, new { error = "Only admin students can create canteens" });

                }
                return Created("", createdCanteen);

            }catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public ActionResult<List<Canteen>> GetAll()
        {
            try
            {
                var canteens = _canteenService.GetAll();
                return Ok(canteens);

            }catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Canteen> GetById(string id)
        {
            try
            {
                var canteen = _canteenService.GetById(id);

                if(canteen.Id.Equals("0"))
                {
                    return NotFound();
                }

                return Ok(canteen);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Canteen> Update(string id, [FromBody] CanteenDto canteenDto,
    [FromHeader(Name = "studentId")] string studentId)
        {
            try
            {
                canteenDto.Id = id;

                var updated = _canteenService.Update(canteenDto, studentId);

                if (updated.Id.Equals("0"))
                {
                    return NotFound();
                }

                return Ok(updated);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(403, "You are not allowed to perform this action.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id, [FromHeader(Name = "studentId")] string studentId)
        {
            try
            {
                var success = _canteenService.Delete(id, studentId);

                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(403, "You are not allowed to perform this action.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("status")]
        public ActionResult<IEnumerable<CanteenStatusDto>> GetCanteensStatus(
    [FromQuery] DateTime startDate,
    [FromQuery] DateTime endDate,
    [FromQuery] TimeSpan startTime,
    [FromQuery] TimeSpan endTime,
    [FromQuery] int duration)
        {

            if (duration != 30 && duration != 60)
                return BadRequest("Duration must be 30 or 60 minutes.");

            var status = _canteenService.GetAllCanteensStatus(startDate, endDate, startTime, endTime, duration);

            return Ok(status);
        }

        [HttpGet("{id}/status")]
        public ActionResult<CanteenStatusDto> GetCanteenStatus(
    int id,
    [FromQuery] DateTime startDate,
    [FromQuery] DateTime endDate,
    [FromQuery] TimeSpan startTime,
    [FromQuery] TimeSpan endTime,
    [FromQuery] int duration)
        {
            if (duration != 30 && duration != 60)
                return BadRequest("Duration must be 30 or 60 minutes.");

            var status = _canteenService.GetCanteenStatus(id, startDate, endDate, startTime, endTime, duration);

            if (status == null)
                return NotFound();

            return Ok(status);
        }


    }
}
