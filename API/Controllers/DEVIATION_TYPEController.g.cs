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
    /// Controller responsible for managing deviation_type related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting deviation_type information.
    /// </remarks>
    [Route("api/deviation_type")]
    [Authorize]
    public class DEVIATION_TYPEController : BaseApiController
    {
        private readonly IDEVIATION_TYPEService _dEVIATION_TYPEService;

        /// <summary>
        /// Initializes a new instance of the DEVIATION_TYPEController class with the specified context.
        /// </summary>
        /// <param name="ideviation_typeservice">The ideviation_typeservice to be used by the controller.</param>
        public DEVIATION_TYPEController(IDEVIATION_TYPEService ideviation_typeservice)
        {
            _dEVIATION_TYPEService = ideviation_typeservice;
        }

        /// <summary>Adds a new deviation_type</summary>
        /// <param name="model">The deviation_type data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("DEVIATION_TYPE", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] DEVIATION_TYPE model)
        {
            var id = await _dEVIATION_TYPEService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of deviation_types based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of deviation_types</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("DEVIATION_TYPE", Entitlements.Read)]
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

            var result = await _dEVIATION_TYPEService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific deviation_type by its primary key</summary>
        /// <param name="id">The primary key of the deviation_type</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The deviation_type data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("DEVIATION_TYPE", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _dEVIATION_TYPEService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific deviation_type by its primary key</summary>
        /// <param name="id">The primary key of the deviation_type</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("DEVIATION_TYPE", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _dEVIATION_TYPEService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific deviation_type by its primary key</summary>
        /// <param name="id">The primary key of the deviation_type</param>
        /// <param name="updatedEntity">The deviation_type data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("DEVIATION_TYPE", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] DEVIATION_TYPE updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _dEVIATION_TYPEService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific deviation_type by its primary key</summary>
        /// <param name="id">The primary key of the deviation_type</param>
        /// <param name="updatedEntity">The deviation_type data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("DEVIATION_TYPE", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<DEVIATION_TYPE> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _dEVIATION_TYPEService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}