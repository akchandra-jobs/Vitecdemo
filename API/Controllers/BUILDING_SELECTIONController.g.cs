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
    /// Controller responsible for managing building_selection related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting building_selection information.
    /// </remarks>
    [Route("api/building_selection")]
    [Authorize]
    public class BUILDING_SELECTIONController : BaseApiController
    {
        private readonly IBUILDING_SELECTIONService _bUILDING_SELECTIONService;

        /// <summary>
        /// Initializes a new instance of the BUILDING_SELECTIONController class with the specified context.
        /// </summary>
        /// <param name="ibuilding_selectionservice">The ibuilding_selectionservice to be used by the controller.</param>
        public BUILDING_SELECTIONController(IBUILDING_SELECTIONService ibuilding_selectionservice)
        {
            _bUILDING_SELECTIONService = ibuilding_selectionservice;
        }

        /// <summary>Adds a new building_selection</summary>
        /// <param name="model">The building_selection data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("BUILDING_SELECTION", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] BUILDING_SELECTION model)
        {
            var id = await _bUILDING_SELECTIONService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of building_selections based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of building_selections</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("BUILDING_SELECTION", Entitlements.Read)]
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

            var result = await _bUILDING_SELECTIONService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific building_selection by its primary key</summary>
        /// <param name="id">The primary key of the building_selection</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The building_selection data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("BUILDING_SELECTION", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _bUILDING_SELECTIONService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific building_selection by its primary key</summary>
        /// <param name="id">The primary key of the building_selection</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("BUILDING_SELECTION", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _bUILDING_SELECTIONService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific building_selection by its primary key</summary>
        /// <param name="id">The primary key of the building_selection</param>
        /// <param name="updatedEntity">The building_selection data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("BUILDING_SELECTION", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] BUILDING_SELECTION updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _bUILDING_SELECTIONService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific building_selection by its primary key</summary>
        /// <param name="id">The primary key of the building_selection</param>
        /// <param name="updatedEntity">The building_selection data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("BUILDING_SELECTION", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<BUILDING_SELECTION> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _bUILDING_SELECTIONService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}