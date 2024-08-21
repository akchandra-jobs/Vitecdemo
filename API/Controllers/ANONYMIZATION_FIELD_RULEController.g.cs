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
    /// Controller responsible for managing anonymization_field_rule related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting anonymization_field_rule information.
    /// </remarks>
    [Route("api/anonymization_field_rule")]
    [Authorize]
    public class ANONYMIZATION_FIELD_RULEController : BaseApiController
    {
        private readonly IANONYMIZATION_FIELD_RULEService _aNONYMIZATION_FIELD_RULEService;

        /// <summary>
        /// Initializes a new instance of the ANONYMIZATION_FIELD_RULEController class with the specified context.
        /// </summary>
        /// <param name="ianonymization_field_ruleservice">The ianonymization_field_ruleservice to be used by the controller.</param>
        public ANONYMIZATION_FIELD_RULEController(IANONYMIZATION_FIELD_RULEService ianonymization_field_ruleservice)
        {
            _aNONYMIZATION_FIELD_RULEService = ianonymization_field_ruleservice;
        }

        /// <summary>Adds a new anonymization_field_rule</summary>
        /// <param name="model">The anonymization_field_rule data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("ANONYMIZATION_FIELD_RULE", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] ANONYMIZATION_FIELD_RULE model)
        {
            var id = await _aNONYMIZATION_FIELD_RULEService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of anonymization_field_rules based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of anonymization_field_rules</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("ANONYMIZATION_FIELD_RULE", Entitlements.Read)]
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

            var result = await _aNONYMIZATION_FIELD_RULEService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific anonymization_field_rule by its primary key</summary>
        /// <param name="id">The primary key of the anonymization_field_rule</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The anonymization_field_rule data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("ANONYMIZATION_FIELD_RULE", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _aNONYMIZATION_FIELD_RULEService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific anonymization_field_rule by its primary key</summary>
        /// <param name="id">The primary key of the anonymization_field_rule</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("ANONYMIZATION_FIELD_RULE", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _aNONYMIZATION_FIELD_RULEService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific anonymization_field_rule by its primary key</summary>
        /// <param name="id">The primary key of the anonymization_field_rule</param>
        /// <param name="updatedEntity">The anonymization_field_rule data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("ANONYMIZATION_FIELD_RULE", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] ANONYMIZATION_FIELD_RULE updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _aNONYMIZATION_FIELD_RULEService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific anonymization_field_rule by its primary key</summary>
        /// <param name="id">The primary key of the anonymization_field_rule</param>
        /// <param name="updatedEntity">The anonymization_field_rule data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("ANONYMIZATION_FIELD_RULE", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<ANONYMIZATION_FIELD_RULE> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _aNONYMIZATION_FIELD_RULEService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}