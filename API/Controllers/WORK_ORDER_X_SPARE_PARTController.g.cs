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
    /// Controller responsible for managing work_order_x_spare_part related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting work_order_x_spare_part information.
    /// </remarks>
    [Route("api/work_order_x_spare_part")]
    [Authorize]
    public class WORK_ORDER_X_SPARE_PARTController : BaseApiController
    {
        private readonly IWORK_ORDER_X_SPARE_PARTService _wORK_ORDER_X_SPARE_PARTService;

        /// <summary>
        /// Initializes a new instance of the WORK_ORDER_X_SPARE_PARTController class with the specified context.
        /// </summary>
        /// <param name="iwork_order_x_spare_partservice">The iwork_order_x_spare_partservice to be used by the controller.</param>
        public WORK_ORDER_X_SPARE_PARTController(IWORK_ORDER_X_SPARE_PARTService iwork_order_x_spare_partservice)
        {
            _wORK_ORDER_X_SPARE_PARTService = iwork_order_x_spare_partservice;
        }

        /// <summary>Adds a new work_order_x_spare_part</summary>
        /// <param name="model">The work_order_x_spare_part data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("WORK_ORDER_X_SPARE_PART", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] WORK_ORDER_X_SPARE_PART model)
        {
            var id = await _wORK_ORDER_X_SPARE_PARTService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of work_order_x_spare_parts based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of work_order_x_spare_parts</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("WORK_ORDER_X_SPARE_PART", Entitlements.Read)]
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

            var result = await _wORK_ORDER_X_SPARE_PARTService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific work_order_x_spare_part by its primary key</summary>
        /// <param name="id">The primary key of the work_order_x_spare_part</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The work_order_x_spare_part data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("WORK_ORDER_X_SPARE_PART", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _wORK_ORDER_X_SPARE_PARTService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific work_order_x_spare_part by its primary key</summary>
        /// <param name="id">The primary key of the work_order_x_spare_part</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("WORK_ORDER_X_SPARE_PART", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _wORK_ORDER_X_SPARE_PARTService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific work_order_x_spare_part by its primary key</summary>
        /// <param name="id">The primary key of the work_order_x_spare_part</param>
        /// <param name="updatedEntity">The work_order_x_spare_part data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("WORK_ORDER_X_SPARE_PART", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] WORK_ORDER_X_SPARE_PART updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _wORK_ORDER_X_SPARE_PARTService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific work_order_x_spare_part by its primary key</summary>
        /// <param name="id">The primary key of the work_order_x_spare_part</param>
        /// <param name="updatedEntity">The work_order_x_spare_part data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("WORK_ORDER_X_SPARE_PART", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<WORK_ORDER_X_SPARE_PART> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _wORK_ORDER_X_SPARE_PARTService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}