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
    /// Controller responsible for managing scheduled_job_execution related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting scheduled_job_execution information.
    /// </remarks>
    [Route("api/scheduled_job_execution")]
    [Authorize]
    public class SCHEDULED_JOB_EXECUTIONController : BaseApiController
    {
        private readonly ISCHEDULED_JOB_EXECUTIONService _sCHEDULED_JOB_EXECUTIONService;

        /// <summary>
        /// Initializes a new instance of the SCHEDULED_JOB_EXECUTIONController class with the specified context.
        /// </summary>
        /// <param name="ischeduled_job_executionservice">The ischeduled_job_executionservice to be used by the controller.</param>
        public SCHEDULED_JOB_EXECUTIONController(ISCHEDULED_JOB_EXECUTIONService ischeduled_job_executionservice)
        {
            _sCHEDULED_JOB_EXECUTIONService = ischeduled_job_executionservice;
        }

        /// <summary>Adds a new scheduled_job_execution</summary>
        /// <param name="model">The scheduled_job_execution data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("SCHEDULED_JOB_EXECUTION", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] SCHEDULED_JOB_EXECUTION model)
        {
            var id = await _sCHEDULED_JOB_EXECUTIONService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of scheduled_job_executions based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of scheduled_job_executions</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("SCHEDULED_JOB_EXECUTION", Entitlements.Read)]
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

            var result = await _sCHEDULED_JOB_EXECUTIONService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific scheduled_job_execution by its primary key</summary>
        /// <param name="id">The primary key of the scheduled_job_execution</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The scheduled_job_execution data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("SCHEDULED_JOB_EXECUTION", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _sCHEDULED_JOB_EXECUTIONService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific scheduled_job_execution by its primary key</summary>
        /// <param name="id">The primary key of the scheduled_job_execution</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("SCHEDULED_JOB_EXECUTION", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _sCHEDULED_JOB_EXECUTIONService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific scheduled_job_execution by its primary key</summary>
        /// <param name="id">The primary key of the scheduled_job_execution</param>
        /// <param name="updatedEntity">The scheduled_job_execution data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("SCHEDULED_JOB_EXECUTION", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] SCHEDULED_JOB_EXECUTION updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _sCHEDULED_JOB_EXECUTIONService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific scheduled_job_execution by its primary key</summary>
        /// <param name="id">The primary key of the scheduled_job_execution</param>
        /// <param name="updatedEntity">The scheduled_job_execution data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("SCHEDULED_JOB_EXECUTION", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<SCHEDULED_JOB_EXECUTION> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _sCHEDULED_JOB_EXECUTIONService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}