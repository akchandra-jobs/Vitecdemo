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
    /// Controller responsible for managing energy_block related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting energy_block information.
    /// </remarks>
    [Route("api/energy_block")]
    [Authorize]
    public class ENERGY_BLOCKController : BaseApiController
    {
        private readonly IENERGY_BLOCKService _eNERGY_BLOCKService;

        /// <summary>
        /// Initializes a new instance of the ENERGY_BLOCKController class with the specified context.
        /// </summary>
        /// <param name="ienergy_blockservice">The ienergy_blockservice to be used by the controller.</param>
        public ENERGY_BLOCKController(IENERGY_BLOCKService ienergy_blockservice)
        {
            _eNERGY_BLOCKService = ienergy_blockservice;
        }

        /// <summary>Adds a new energy_block</summary>
        /// <param name="model">The energy_block data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("ENERGY_BLOCK", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] ENERGY_BLOCK model)
        {
            var id = await _eNERGY_BLOCKService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of energy_blocks based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of energy_blocks</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("ENERGY_BLOCK", Entitlements.Read)]
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

            var result = await _eNERGY_BLOCKService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific energy_block by its primary key</summary>
        /// <param name="id">The primary key of the energy_block</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The energy_block data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("ENERGY_BLOCK", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _eNERGY_BLOCKService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific energy_block by its primary key</summary>
        /// <param name="id">The primary key of the energy_block</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("ENERGY_BLOCK", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _eNERGY_BLOCKService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific energy_block by its primary key</summary>
        /// <param name="id">The primary key of the energy_block</param>
        /// <param name="updatedEntity">The energy_block data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("ENERGY_BLOCK", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] ENERGY_BLOCK updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _eNERGY_BLOCKService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific energy_block by its primary key</summary>
        /// <param name="id">The primary key of the energy_block</param>
        /// <param name="updatedEntity">The energy_block data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("ENERGY_BLOCK", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<ENERGY_BLOCK> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _eNERGY_BLOCKService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}