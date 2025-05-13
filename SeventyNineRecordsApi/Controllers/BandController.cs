using Microsoft.AspNetCore.Mvc;
using SeventyModel;
using SeventyBusiness;
using Swashbuckle.AspNetCore.Annotations;

namespace SeventyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BandController : ControllerBase
    {
        private readonly BandService _bandService;

        public BandController(BandService bandService)
        {
            _bandService = bandService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "List all bands",
            Description = "Returns a list of all registered bands. If none are found, returns 204 No Content.",
            OperationId = "GetAllBands",
            Tags = new[] { "Band" }
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            var bands = await _bandService.GetAllAsync();
            return bands.Count == 0 ? NoContent() : Ok(bands);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get a band by ID",
            Description = "Returns a band by its ID. If not found, returns 404 Not Found.",
            OperationId = "GetBandById",
            Tags = new[] { "Band" }
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var band = await _bandService.GetByIdAsync(id);
            return band == null ? NotFound() : Ok(band);
        }

        [HttpGet("search")]
        [SwaggerOperation(
            Summary = "Search bands by name",
            Description = "Returns a list of bands that contain the given name. If none are found, returns 404 Not Found.",
            OperationId = "SearchBandsByName",
            Tags = new[] { "Band" }
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var results = await _bandService.SearchByNameAsync(name);
            return results.Count == 0 ? NotFound() : Ok(results);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new band",
            Description = "Adds a new band to the database and returns the created band.",
            OperationId = "CreateBand",
            Tags = new[] { "Band" }
        )]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBand([FromBody] BandModel band)
        {
            if (band == null)
                return BadRequest("Invalid data");

            var created = await _bandService.AddBandAsync(band);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update an existing band",
            Description = "Updates the details of an existing band. Returns 204 No Content if successful, or 404 Not Found.",
            OperationId = "UpdateBand",
            Tags = new[] { "Band" }
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] BandModel band)
        {
            if (band == null || band.Id != id)
                return BadRequest("Inconsistent data.");

            var success = await _bandService.UpdateAsync(band);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete a band",
            Description = "Removes a band based on the provided ID. Returns 204 No Content if successful, or 404 Not Found.",
            OperationId = "DeleteBand",
            Tags = new[] { "Band" }
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _bandService.RemoveAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
