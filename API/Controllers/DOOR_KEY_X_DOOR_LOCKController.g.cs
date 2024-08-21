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
    /// Controller responsible for managing door_key_x_door_lock related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting door_key_x_door_lock information.
    /// </remarks>
    [Route("api/door_key_x_door_lock")]
    [Authorize]
    public class DOOR_KEY_X_DOOR_LOCKController : BaseApiController
    {
        private readonly IDOOR_KEY_X_DOOR_LOCKService _dOOR_KEY_X_DOOR_LOCKService;

        /// <summary>
        /// Initializes a new instance of the DOOR_KEY_X_DOOR_LOCKController class with the specified context.
        /// </summary>
        /// <param name="idoor_key_x_door_lockservice">The idoor_key_x_door_lockservice to be used by the controller.</param>
        public DOOR_KEY_X_DOOR_LOCKController(IDOOR_KEY_X_DOOR_LOCKService idoor_key_x_door_lockservice)
        {
            _dOOR_KEY_X_DOOR_LOCKService = idoor_key_x_door_lockservice;
        }

        /// <summary>Adds a new door_key_x_door_lock</summary>
        /// <param name="model">The door_key_x_door_lock data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("DOOR_KEY_X_DOOR_LOCK", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] DOOR_KEY_X_DOOR_LOCK model)
        {
            var id = await _dOOR_KEY_X_DOOR_LOCKService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of door_key_x_door_locks based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of door_key_x_door_locks</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("DOOR_KEY_X_DOOR_LOCK", Entitlements.Read)]
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

            var result = await _dOOR_KEY_X_DOOR_LOCKService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific door_key_x_door_lock by its primary key</summary>
        /// <param name="id">The primary key of the door_key_x_door_lock</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The door_key_x_door_lock data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("DOOR_KEY_X_DOOR_LOCK", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _dOOR_KEY_X_DOOR_LOCKService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific door_key_x_door_lock by its primary key</summary>
        /// <param name="id">The primary key of the door_key_x_door_lock</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("DOOR_KEY_X_DOOR_LOCK", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _dOOR_KEY_X_DOOR_LOCKService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific door_key_x_door_lock by its primary key</summary>
        /// <param name="id">The primary key of the door_key_x_door_lock</param>
        /// <param name="updatedEntity">The door_key_x_door_lock data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("DOOR_KEY_X_DOOR_LOCK", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] DOOR_KEY_X_DOOR_LOCK updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _dOOR_KEY_X_DOOR_LOCKService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific door_key_x_door_lock by its primary key</summary>
        /// <param name="id">The primary key of the door_key_x_door_lock</param>
        /// <param name="updatedEntity">The door_key_x_door_lock data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("DOOR_KEY_X_DOOR_LOCK", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<DOOR_KEY_X_DOOR_LOCK> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _dOOR_KEY_X_DOOR_LOCKService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}