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
    /// Controller responsible for managing spare_part_counting related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting spare_part_counting information.
    /// </remarks>
    [Route("api/spare_part_counting")]
    [Authorize]
    public class SPARE_PART_COUNTINGController : BaseApiController
    {
        private readonly ISPARE_PART_COUNTINGService _sPARE_PART_COUNTINGService;

        /// <summary>
        /// Initializes a new instance of the SPARE_PART_COUNTINGController class with the specified context.
        /// </summary>
        /// <param name="ispare_part_countingservice">The ispare_part_countingservice to be used by the controller.</param>
        public SPARE_PART_COUNTINGController(ISPARE_PART_COUNTINGService ispare_part_countingservice)
        {
            _sPARE_PART_COUNTINGService = ispare_part_countingservice;
        }

        /// <summary>Adds a new spare_part_counting</summary>
        /// <param name="model">The spare_part_counting data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("SPARE_PART_COUNTING", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] SPARE_PART_COUNTING model)
        {
            var id = await _sPARE_PART_COUNTINGService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of spare_part_countings based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of spare_part_countings</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("SPARE_PART_COUNTING", Entitlements.Read)]
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

            var result = await _sPARE_PART_COUNTINGService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific spare_part_counting by its primary key</summary>
        /// <param name="id">The primary key of the spare_part_counting</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The spare_part_counting data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("SPARE_PART_COUNTING", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _sPARE_PART_COUNTINGService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific spare_part_counting by its primary key</summary>
        /// <param name="id">The primary key of the spare_part_counting</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("SPARE_PART_COUNTING", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _sPARE_PART_COUNTINGService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific spare_part_counting by its primary key</summary>
        /// <param name="id">The primary key of the spare_part_counting</param>
        /// <param name="updatedEntity">The spare_part_counting data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("SPARE_PART_COUNTING", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] SPARE_PART_COUNTING updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _sPARE_PART_COUNTINGService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific spare_part_counting by its primary key</summary>
        /// <param name="id">The primary key of the spare_part_counting</param>
        /// <param name="updatedEntity">The spare_part_counting data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("SPARE_PART_COUNTING", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<SPARE_PART_COUNTING> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _sPARE_PART_COUNTINGService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}