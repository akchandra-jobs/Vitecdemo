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
    /// Controller responsible for managing log_performance related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting log_performance information.
    /// </remarks>
    [Route("api/log_performance")]
    [Authorize]
    public class LOG_PERFORMANCEController : BaseApiController
    {
        private readonly ILOG_PERFORMANCEService _lOG_PERFORMANCEService;

        /// <summary>
        /// Initializes a new instance of the LOG_PERFORMANCEController class with the specified context.
        /// </summary>
        /// <param name="ilog_performanceservice">The ilog_performanceservice to be used by the controller.</param>
        public LOG_PERFORMANCEController(ILOG_PERFORMANCEService ilog_performanceservice)
        {
            _lOG_PERFORMANCEService = ilog_performanceservice;
        }

        /// <summary>Adds a new log_performance</summary>
        /// <param name="model">The log_performance data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("LOG_PERFORMANCE", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] LOG_PERFORMANCE model)
        {
            var id = await _lOG_PERFORMANCEService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of log_performances based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of log_performances</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("LOG_PERFORMANCE", Entitlements.Read)]
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

            var result = await _lOG_PERFORMANCEService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific log_performance by its primary key</summary>
        /// <param name="id">The primary key of the log_performance</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The log_performance data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("LOG_PERFORMANCE", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _lOG_PERFORMANCEService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific log_performance by its primary key</summary>
        /// <param name="id">The primary key of the log_performance</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("LOG_PERFORMANCE", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _lOG_PERFORMANCEService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific log_performance by its primary key</summary>
        /// <param name="id">The primary key of the log_performance</param>
        /// <param name="updatedEntity">The log_performance data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("LOG_PERFORMANCE", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] LOG_PERFORMANCE updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _lOG_PERFORMANCEService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific log_performance by its primary key</summary>
        /// <param name="id">The primary key of the log_performance</param>
        /// <param name="updatedEntity">The log_performance data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("LOG_PERFORMANCE", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<LOG_PERFORMANCE> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _lOG_PERFORMANCEService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}