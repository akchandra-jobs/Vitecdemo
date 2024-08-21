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
    /// Controller responsible for managing bcf_project related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting bcf_project information.
    /// </remarks>
    [Route("api/bcf_project")]
    [Authorize]
    public class BCF_PROJECTController : BaseApiController
    {
        private readonly IBCF_PROJECTService _bCF_PROJECTService;

        /// <summary>
        /// Initializes a new instance of the BCF_PROJECTController class with the specified context.
        /// </summary>
        /// <param name="ibcf_projectservice">The ibcf_projectservice to be used by the controller.</param>
        public BCF_PROJECTController(IBCF_PROJECTService ibcf_projectservice)
        {
            _bCF_PROJECTService = ibcf_projectservice;
        }

        /// <summary>Adds a new bcf_project</summary>
        /// <param name="model">The bcf_project data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("BCF_PROJECT", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] BCF_PROJECT model)
        {
            var id = await _bCF_PROJECTService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of bcf_projects based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of bcf_projects</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("BCF_PROJECT", Entitlements.Read)]
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

            var result = await _bCF_PROJECTService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific bcf_project by its primary key</summary>
        /// <param name="id">The primary key of the bcf_project</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The bcf_project data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("BCF_PROJECT", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _bCF_PROJECTService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific bcf_project by its primary key</summary>
        /// <param name="id">The primary key of the bcf_project</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("BCF_PROJECT", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _bCF_PROJECTService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific bcf_project by its primary key</summary>
        /// <param name="id">The primary key of the bcf_project</param>
        /// <param name="updatedEntity">The bcf_project data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("BCF_PROJECT", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] BCF_PROJECT updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _bCF_PROJECTService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific bcf_project by its primary key</summary>
        /// <param name="id">The primary key of the bcf_project</param>
        /// <param name="updatedEntity">The bcf_project data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("BCF_PROJECT", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<BCF_PROJECT> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _bCF_PROJECTService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}