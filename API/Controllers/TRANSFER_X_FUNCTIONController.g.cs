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
    /// Controller responsible for managing transfer_x_function related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting transfer_x_function information.
    /// </remarks>
    [Route("api/transfer_x_function")]
    [Authorize]
    public class TRANSFER_X_FUNCTIONController : BaseApiController
    {
        private readonly ITRANSFER_X_FUNCTIONService _tRANSFER_X_FUNCTIONService;

        /// <summary>
        /// Initializes a new instance of the TRANSFER_X_FUNCTIONController class with the specified context.
        /// </summary>
        /// <param name="itransfer_x_functionservice">The itransfer_x_functionservice to be used by the controller.</param>
        public TRANSFER_X_FUNCTIONController(ITRANSFER_X_FUNCTIONService itransfer_x_functionservice)
        {
            _tRANSFER_X_FUNCTIONService = itransfer_x_functionservice;
        }

        /// <summary>Adds a new transfer_x_function</summary>
        /// <param name="model">The transfer_x_function data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("TRANSFER_X_FUNCTION", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] TRANSFER_X_FUNCTION model)
        {
            var id = await _tRANSFER_X_FUNCTIONService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of transfer_x_functions based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of transfer_x_functions</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("TRANSFER_X_FUNCTION", Entitlements.Read)]
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

            var result = await _tRANSFER_X_FUNCTIONService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific transfer_x_function by its primary key</summary>
        /// <param name="id">The primary key of the transfer_x_function</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The transfer_x_function data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("TRANSFER_X_FUNCTION", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _tRANSFER_X_FUNCTIONService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific transfer_x_function by its primary key</summary>
        /// <param name="id">The primary key of the transfer_x_function</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("TRANSFER_X_FUNCTION", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _tRANSFER_X_FUNCTIONService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific transfer_x_function by its primary key</summary>
        /// <param name="id">The primary key of the transfer_x_function</param>
        /// <param name="updatedEntity">The transfer_x_function data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("TRANSFER_X_FUNCTION", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] TRANSFER_X_FUNCTION updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _tRANSFER_X_FUNCTIONService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific transfer_x_function by its primary key</summary>
        /// <param name="id">The primary key of the transfer_x_function</param>
        /// <param name="updatedEntity">The transfer_x_function data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("TRANSFER_X_FUNCTION", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<TRANSFER_X_FUNCTION> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _tRANSFER_X_FUNCTIONService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}