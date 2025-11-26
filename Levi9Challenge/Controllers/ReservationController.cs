using Levi9Challenge.DTOs;
using Levi9Challenge.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Levi9Challenge.Controllers
{
    [ApiController]
    [Route("reservations")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService) { 
            _reservationService = reservationService;
        }

        [HttpPost]
        public ActionResult<ReservationDto> CreateReservation([FromBody] ReservationCreateDto dto)
        {
            try
            {
                var createdReservation = _reservationService.Create(dto.StudentId, dto.CanteenId, dto.Date, dto.Time, dto.Duration.ToString());

                if (createdReservation == null || createdReservation.Id.Equals("")) 
                {
                    return BadRequest(new { error = "Invalid reservation input" });
                }

                return Created("", createdReservation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id, [FromHeader (Name = "studentId")] string studentId)
        {
            try
            {
                var reservationDto = _reservationService.Delete(id, studentId);

                if (reservationDto.Id.Equals(string.Empty))
                {
                    return NotFound();
                }

                return Ok(reservationDto);
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
    }
}
