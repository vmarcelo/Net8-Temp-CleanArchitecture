using FlatFinder.Application.Reservations.GetReservation;
using FlatFinder.Application.Reservations.ReserveReservation;
using FlatFinder.Contracts.Reservations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlatFinder.Api.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly ISender sender;

        public ReservationsController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetReservation(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetReservationQuery(id);
            var result = await sender.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ReserveReservation([FromBody]
            ReserveReservationRequest request,
            CancellationToken cancellationToken)
        {
            var command = new ReserveReservationCommand(
                request.FlatId,
                request.UserId,
                request.StartDate,
                request.EndDate
                );

            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetReservation), new { id = result.Value }, result.Value);
        }
    }
}
