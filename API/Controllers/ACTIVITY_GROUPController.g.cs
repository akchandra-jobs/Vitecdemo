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
    /// Controller responsible for managing activity_group related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting activity_group information.
    /// </remarks>
    [Route("api/activity_group")]
    [Authorize]
    public class ACTIVITY_GROUPController : BaseApiController
    {
        private readonly IACTIVITY_GROUPService _aCTIVITY_GROUPService;

        /// <summary>
        /// Initializes a new instance of the ACTIVITY_GROUPController class with the specified context.
        /// </summary>
        /// <param name="iactivity_groupservice">The iactivity_groupservice to be used by the controller.</param>
        public ACTIVITY_GROUPController(IACTIVITY_GROUPService iactivity_groupservice)
        {
            _aCTIVITY_GROUPService = iactivity_groupservice;
        }

        /// <summary>Adds a new activity_group</summary>
        /// <param name="model">The activity_group data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("ACTIVITY_GROUP", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] ACTIVITY_GROUP model)
        {
            var id = await _aCTIVITY_GROUPService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of activity_groups based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of activity_groups</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("ACTIVITY_GROUP", Entitlements.Read)]
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

            var result = await _aCTIVITY_GROUPService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific activity_group by its primary key</summary>
        /// <param name="id">The primary key of the activity_group</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The activity_group data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("ACTIVITY_GROUP", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _aCTIVITY_GROUPService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific activity_group by its primary key</summary>
        /// <param name="id">The primary key of the activity_group</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("ACTIVITY_GROUP", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _aCTIVITY_GROUPService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific activity_group by its primary key</summary>
        /// <param name="id">The primary key of the activity_group</param>
        /// <param name="updatedEntity">The activity_group data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("ACTIVITY_GROUP", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] ACTIVITY_GROUP updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _aCTIVITY_GROUPService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific activity_group by its primary key</summary>
        /// <param name="id">The primary key of the activity_group</param>
        /// <param name="updatedEntity">The activity_group data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("ACTIVITY_GROUP", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<ACTIVITY_GROUP> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _aCTIVITY_GROUPService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}