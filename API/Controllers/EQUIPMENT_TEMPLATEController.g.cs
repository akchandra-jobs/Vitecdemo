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
    /// Controller responsible for managing equipment_template related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting equipment_template information.
    /// </remarks>
    [Route("api/equipment_template")]
    [Authorize]
    public class EQUIPMENT_TEMPLATEController : BaseApiController
    {
        private readonly IEQUIPMENT_TEMPLATEService _eQUIPMENT_TEMPLATEService;

        /// <summary>
        /// Initializes a new instance of the EQUIPMENT_TEMPLATEController class with the specified context.
        /// </summary>
        /// <param name="iequipment_templateservice">The iequipment_templateservice to be used by the controller.</param>
        public EQUIPMENT_TEMPLATEController(IEQUIPMENT_TEMPLATEService iequipment_templateservice)
        {
            _eQUIPMENT_TEMPLATEService = iequipment_templateservice;
        }

        /// <summary>Adds a new equipment_template</summary>
        /// <param name="model">The equipment_template data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("EQUIPMENT_TEMPLATE", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] EQUIPMENT_TEMPLATE model)
        {
            var id = await _eQUIPMENT_TEMPLATEService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of equipment_templates based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of equipment_templates</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("EQUIPMENT_TEMPLATE", Entitlements.Read)]
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

            var result = await _eQUIPMENT_TEMPLATEService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific equipment_template by its primary key</summary>
        /// <param name="id">The primary key of the equipment_template</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The equipment_template data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("EQUIPMENT_TEMPLATE", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _eQUIPMENT_TEMPLATEService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific equipment_template by its primary key</summary>
        /// <param name="id">The primary key of the equipment_template</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("EQUIPMENT_TEMPLATE", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _eQUIPMENT_TEMPLATEService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific equipment_template by its primary key</summary>
        /// <param name="id">The primary key of the equipment_template</param>
        /// <param name="updatedEntity">The equipment_template data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("EQUIPMENT_TEMPLATE", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] EQUIPMENT_TEMPLATE updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _eQUIPMENT_TEMPLATEService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific equipment_template by its primary key</summary>
        /// <param name="id">The primary key of the equipment_template</param>
        /// <param name="updatedEntity">The equipment_template data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("EQUIPMENT_TEMPLATE", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<EQUIPMENT_TEMPLATE> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _eQUIPMENT_TEMPLATEService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}