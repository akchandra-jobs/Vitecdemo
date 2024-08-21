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
    /// Controller responsible for managing cleaning_completion_attribute_value related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting cleaning_completion_attribute_value information.
    /// </remarks>
    [Route("api/cleaning_completion_attribute_value")]
    [Authorize]
    public class CLEANING_COMPLETION_ATTRIBUTE_VALUEController : BaseApiController
    {
        private readonly ICLEANING_COMPLETION_ATTRIBUTE_VALUEService _cLEANING_COMPLETION_ATTRIBUTE_VALUEService;

        /// <summary>
        /// Initializes a new instance of the CLEANING_COMPLETION_ATTRIBUTE_VALUEController class with the specified context.
        /// </summary>
        /// <param name="icleaning_completion_attribute_valueservice">The icleaning_completion_attribute_valueservice to be used by the controller.</param>
        public CLEANING_COMPLETION_ATTRIBUTE_VALUEController(ICLEANING_COMPLETION_ATTRIBUTE_VALUEService icleaning_completion_attribute_valueservice)
        {
            _cLEANING_COMPLETION_ATTRIBUTE_VALUEService = icleaning_completion_attribute_valueservice;
        }

        /// <summary>Adds a new cleaning_completion_attribute_value</summary>
        /// <param name="model">The cleaning_completion_attribute_value data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("CLEANING_COMPLETION_ATTRIBUTE_VALUE", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] CLEANING_COMPLETION_ATTRIBUTE_VALUE model)
        {
            var id = await _cLEANING_COMPLETION_ATTRIBUTE_VALUEService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of cleaning_completion_attribute_values based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of cleaning_completion_attribute_values</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("CLEANING_COMPLETION_ATTRIBUTE_VALUE", Entitlements.Read)]
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

            var result = await _cLEANING_COMPLETION_ATTRIBUTE_VALUEService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific cleaning_completion_attribute_value by its primary key</summary>
        /// <param name="id">The primary key of the cleaning_completion_attribute_value</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The cleaning_completion_attribute_value data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("CLEANING_COMPLETION_ATTRIBUTE_VALUE", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _cLEANING_COMPLETION_ATTRIBUTE_VALUEService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific cleaning_completion_attribute_value by its primary key</summary>
        /// <param name="id">The primary key of the cleaning_completion_attribute_value</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("CLEANING_COMPLETION_ATTRIBUTE_VALUE", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _cLEANING_COMPLETION_ATTRIBUTE_VALUEService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific cleaning_completion_attribute_value by its primary key</summary>
        /// <param name="id">The primary key of the cleaning_completion_attribute_value</param>
        /// <param name="updatedEntity">The cleaning_completion_attribute_value data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("CLEANING_COMPLETION_ATTRIBUTE_VALUE", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] CLEANING_COMPLETION_ATTRIBUTE_VALUE updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _cLEANING_COMPLETION_ATTRIBUTE_VALUEService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific cleaning_completion_attribute_value by its primary key</summary>
        /// <param name="id">The primary key of the cleaning_completion_attribute_value</param>
        /// <param name="updatedEntity">The cleaning_completion_attribute_value data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("CLEANING_COMPLETION_ATTRIBUTE_VALUE", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<CLEANING_COMPLETION_ATTRIBUTE_VALUE> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _cLEANING_COMPLETION_ATTRIBUTE_VALUEService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}