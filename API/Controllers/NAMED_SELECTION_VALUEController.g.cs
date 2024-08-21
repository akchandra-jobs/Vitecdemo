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
    /// Controller responsible for managing named_selection_value related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting named_selection_value information.
    /// </remarks>
    [Route("api/named_selection_value")]
    [Authorize]
    public class NAMED_SELECTION_VALUEController : BaseApiController
    {
        private readonly INAMED_SELECTION_VALUEService _nAMED_SELECTION_VALUEService;

        /// <summary>
        /// Initializes a new instance of the NAMED_SELECTION_VALUEController class with the specified context.
        /// </summary>
        /// <param name="inamed_selection_valueservice">The inamed_selection_valueservice to be used by the controller.</param>
        public NAMED_SELECTION_VALUEController(INAMED_SELECTION_VALUEService inamed_selection_valueservice)
        {
            _nAMED_SELECTION_VALUEService = inamed_selection_valueservice;
        }

        /// <summary>Adds a new named_selection_value</summary>
        /// <param name="model">The named_selection_value data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("NAMED_SELECTION_VALUE", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] NAMED_SELECTION_VALUE model)
        {
            var id = await _nAMED_SELECTION_VALUEService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of named_selection_values based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of named_selection_values</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("NAMED_SELECTION_VALUE", Entitlements.Read)]
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

            var result = await _nAMED_SELECTION_VALUEService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific named_selection_value by its primary key</summary>
        /// <param name="id">The primary key of the named_selection_value</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The named_selection_value data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("NAMED_SELECTION_VALUE", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _nAMED_SELECTION_VALUEService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific named_selection_value by its primary key</summary>
        /// <param name="id">The primary key of the named_selection_value</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("NAMED_SELECTION_VALUE", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _nAMED_SELECTION_VALUEService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific named_selection_value by its primary key</summary>
        /// <param name="id">The primary key of the named_selection_value</param>
        /// <param name="updatedEntity">The named_selection_value data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("NAMED_SELECTION_VALUE", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] NAMED_SELECTION_VALUE updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _nAMED_SELECTION_VALUEService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific named_selection_value by its primary key</summary>
        /// <param name="id">The primary key of the named_selection_value</param>
        /// <param name="updatedEntity">The named_selection_value data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("NAMED_SELECTION_VALUE", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<NAMED_SELECTION_VALUE> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _nAMED_SELECTION_VALUEService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}