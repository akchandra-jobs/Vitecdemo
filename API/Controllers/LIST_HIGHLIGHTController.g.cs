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
    /// Controller responsible for managing list_highlight related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting list_highlight information.
    /// </remarks>
    [Route("api/list_highlight")]
    [Authorize]
    public class LIST_HIGHLIGHTController : BaseApiController
    {
        private readonly ILIST_HIGHLIGHTService _lIST_HIGHLIGHTService;

        /// <summary>
        /// Initializes a new instance of the LIST_HIGHLIGHTController class with the specified context.
        /// </summary>
        /// <param name="ilist_highlightservice">The ilist_highlightservice to be used by the controller.</param>
        public LIST_HIGHLIGHTController(ILIST_HIGHLIGHTService ilist_highlightservice)
        {
            _lIST_HIGHLIGHTService = ilist_highlightservice;
        }

        /// <summary>Adds a new list_highlight</summary>
        /// <param name="model">The list_highlight data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("LIST_HIGHLIGHT", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] LIST_HIGHLIGHT model)
        {
            var id = await _lIST_HIGHLIGHTService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of list_highlights based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of list_highlights</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("LIST_HIGHLIGHT", Entitlements.Read)]
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

            var result = await _lIST_HIGHLIGHTService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific list_highlight by its primary key</summary>
        /// <param name="id">The primary key of the list_highlight</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The list_highlight data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("LIST_HIGHLIGHT", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _lIST_HIGHLIGHTService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific list_highlight by its primary key</summary>
        /// <param name="id">The primary key of the list_highlight</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("LIST_HIGHLIGHT", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _lIST_HIGHLIGHTService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific list_highlight by its primary key</summary>
        /// <param name="id">The primary key of the list_highlight</param>
        /// <param name="updatedEntity">The list_highlight data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("LIST_HIGHLIGHT", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] LIST_HIGHLIGHT updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _lIST_HIGHLIGHTService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific list_highlight by its primary key</summary>
        /// <param name="id">The primary key of the list_highlight</param>
        /// <param name="updatedEntity">The list_highlight data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("LIST_HIGHLIGHT", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<LIST_HIGHLIGHT> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _lIST_HIGHLIGHTService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}