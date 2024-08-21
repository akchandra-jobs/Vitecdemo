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
    /// Controller responsible for managing general_options related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting general_options information.
    /// </remarks>
    [Route("api/general_options")]
    [Authorize]
    public class GENERAL_OPTIONSController : BaseApiController
    {
        private readonly IGENERAL_OPTIONSService _gENERAL_OPTIONSService;

        /// <summary>
        /// Initializes a new instance of the GENERAL_OPTIONSController class with the specified context.
        /// </summary>
        /// <param name="igeneral_optionsservice">The igeneral_optionsservice to be used by the controller.</param>
        public GENERAL_OPTIONSController(IGENERAL_OPTIONSService igeneral_optionsservice)
        {
            _gENERAL_OPTIONSService = igeneral_optionsservice;
        }

        /// <summary>Adds a new general_options</summary>
        /// <param name="model">The general_options data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("GENERAL_OPTIONS", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] GENERAL_OPTIONS model)
        {
            var id = await _gENERAL_OPTIONSService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of general_optionss based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of general_optionss</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("GENERAL_OPTIONS", Entitlements.Read)]
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

            var result = await _gENERAL_OPTIONSService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific general_options by its primary key</summary>
        /// <param name="id">The primary key of the general_options</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The general_options data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("GENERAL_OPTIONS", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _gENERAL_OPTIONSService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific general_options by its primary key</summary>
        /// <param name="id">The primary key of the general_options</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("GENERAL_OPTIONS", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _gENERAL_OPTIONSService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific general_options by its primary key</summary>
        /// <param name="id">The primary key of the general_options</param>
        /// <param name="updatedEntity">The general_options data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("GENERAL_OPTIONS", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] GENERAL_OPTIONS updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _gENERAL_OPTIONSService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific general_options by its primary key</summary>
        /// <param name="id">The primary key of the general_options</param>
        /// <param name="updatedEntity">The general_options data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("GENERAL_OPTIONS", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<GENERAL_OPTIONS> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _gENERAL_OPTIONSService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}