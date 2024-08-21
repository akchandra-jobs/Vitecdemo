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
    /// Controller responsible for managing relationship related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting relationship information.
    /// </remarks>
    [Route("api/relationship")]
    [Authorize]
    public class RELATIONSHIPController : BaseApiController
    {
        private readonly IRELATIONSHIPService _rELATIONSHIPService;

        /// <summary>
        /// Initializes a new instance of the RELATIONSHIPController class with the specified context.
        /// </summary>
        /// <param name="irelationshipservice">The irelationshipservice to be used by the controller.</param>
        public RELATIONSHIPController(IRELATIONSHIPService irelationshipservice)
        {
            _rELATIONSHIPService = irelationshipservice;
        }

        /// <summary>Adds a new relationship</summary>
        /// <param name="model">The relationship data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("RELATIONSHIP", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] RELATIONSHIP model)
        {
            var id = await _rELATIONSHIPService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of relationships based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of relationships</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("RELATIONSHIP", Entitlements.Read)]
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

            var result = await _rELATIONSHIPService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific relationship by its primary key</summary>
        /// <param name="id">The primary key of the relationship</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The relationship data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("RELATIONSHIP", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _rELATIONSHIPService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific relationship by its primary key</summary>
        /// <param name="id">The primary key of the relationship</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("RELATIONSHIP", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _rELATIONSHIPService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific relationship by its primary key</summary>
        /// <param name="id">The primary key of the relationship</param>
        /// <param name="updatedEntity">The relationship data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("RELATIONSHIP", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] RELATIONSHIP updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _rELATIONSHIPService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific relationship by its primary key</summary>
        /// <param name="id">The primary key of the relationship</param>
        /// <param name="updatedEntity">The relationship data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("RELATIONSHIP", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<RELATIONSHIP> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _rELATIONSHIPService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}