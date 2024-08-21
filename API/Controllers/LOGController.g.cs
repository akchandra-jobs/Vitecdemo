using Microsoft.AspNetCore.Mvc;
using Vitecdemo.Models;
using Vitecdemo.Services;
using Vitecdemo.Entities;
using Vitecdemo.Filter;
using Vitecdemo.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Task = System.Threading.Tasks.Task;
using Vitecdemo.Authorization;

namespace Vitecdemo.Controllers
{
    /// <summary>
    /// Controller responsible for managing log related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting log information.
    /// </remarks>
    [Route("api/log")]
    [Authorize]
    public class LOGController : BaseApiController
    {
        private readonly ILOGService _lOGService;

        /// <summary>
        /// Initializes a new instance of the LOGController class with the specified context.
        /// </summary>
        /// <param name="ilogservice">The ilogservice to be used by the controller.</param>
        public LOGController(ILOGService ilogservice)
        {
            _lOGService = ilogservice;
        }

        /// <summary>Adds a new log</summary>
        /// <param name="model">The log data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("LOG", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] LOG model)
        {
            var id = await _lOGService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of logs based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of logs</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("LOG", Entitlements.Read)]
        public async Task<IActionResult> Get([FromQuery] string filters, string searchTerm, int pageNumber = 1, int pageSize = 10, string sortField = null, string sortOrder = "asc")
        {
            List<FilterCriteria> filterCriteria = null;
            if (pageSize < 1)
            {
                return BadRequest("Page size invalid.");
            }

            if (pageNumber < 1)
            {
                return BadRequest("Page mumber invalid.");
            }

            if (!string.IsNullOrEmpty(filters))
            {
                filterCriteria = JsonHelper.Deserialize<List<FilterCriteria>>(filters);
            }

            var result = await _lOGService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific log by its primary key</summary>
        /// <param name="id">The primary key of the log</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The log data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("LOG", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _lOGService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific log by its primary key</summary>
        /// <param name="id">The primary key of the log</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("LOG", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _lOGService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific log by its primary key</summary>
        /// <param name="id">The primary key of the log</param>
        /// <param name="updatedEntity">The log data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("LOG", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] LOG updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _lOGService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific log by its primary key</summary>
        /// <param name="id">The primary key of the log</param>
        /// <param name="updatedEntity">The log data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("LOG", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<LOG> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _lOGService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}