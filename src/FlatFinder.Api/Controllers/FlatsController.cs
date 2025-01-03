using FlatFinder.Application.Flats.SearchFlats;
using FlatFinder.Contracts.Flats;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlatFinder.Api.Controllers
{
    [ApiController]
    [Route("api/flats")]
    public class FlatsController : ControllerBase
    {
        private readonly ISender sender;

        public FlatsController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> SearchFlats([FromQuery]SearchFlatsRequest request, 
            CancellationToken cancellationToken)
        {
            var query = new SearchFlatsQuery(request.startDate, request.endDate);
            var result = await sender.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
