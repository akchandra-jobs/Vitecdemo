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
    /// Controller responsible for managing registered_field related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting registered_field information.
    /// </remarks>
    [Route("api/registered_field")]
    [Authorize]
    public class REGISTERED_FIELDController : BaseApiController
    {
        private readonly IREGISTERED_FIELDService _rEGISTERED_FIELDService;

        /// <summary>
        /// Initializes a new instance of the REGISTERED_FIELDController class with the specified context.
        /// </summary>
        /// <param name="iregistered_fieldservice">The iregistered_fieldservice to be used by the controller.</param>
        public REGISTERED_FIELDController(IREGISTERED_FIELDService iregistered_fieldservice)
        {
            _rEGISTERED_FIELDService = iregistered_fieldservice;
        }

        /// <summary>Adds a new registered_field</summary>
        /// <param name="model">The registered_field data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("REGISTERED_FIELD", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] REGISTERED_FIELD model)
        {
            var id = await _rEGISTERED_FIELDService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of registered_fields based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of registered_fields</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("REGISTERED_FIELD", Entitlements.Read)]
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

            var result = await _rEGISTERED_FIELDService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific registered_field by its primary key</summary>
        /// <param name="id">The primary key of the registered_field</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The registered_field data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("REGISTERED_FIELD", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _rEGISTERED_FIELDService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific registered_field by its primary key</summary>
        /// <param name="id">The primary key of the registered_field</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("REGISTERED_FIELD", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _rEGISTERED_FIELDService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific registered_field by its primary key</summary>
        /// <param name="id">The primary key of the registered_field</param>
        /// <param name="updatedEntity">The registered_field data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("REGISTERED_FIELD", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] REGISTERED_FIELD updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _rEGISTERED_FIELDService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific registered_field by its primary key</summary>
        /// <param name="id">The primary key of the registered_field</param>
        /// <param name="updatedEntity">The registered_field data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("REGISTERED_FIELD", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<REGISTERED_FIELD> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _rEGISTERED_FIELDService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}