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
    /// Controller responsible for managing project_category related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting project_category information.
    /// </remarks>
    [Route("api/project_category")]
    [Authorize]
    public class PROJECT_CATEGORYController : BaseApiController
    {
        private readonly IPROJECT_CATEGORYService _pROJECT_CATEGORYService;

        /// <summary>
        /// Initializes a new instance of the PROJECT_CATEGORYController class with the specified context.
        /// </summary>
        /// <param name="iproject_categoryservice">The iproject_categoryservice to be used by the controller.</param>
        public PROJECT_CATEGORYController(IPROJECT_CATEGORYService iproject_categoryservice)
        {
            _pROJECT_CATEGORYService = iproject_categoryservice;
        }

        /// <summary>Adds a new project_category</summary>
        /// <param name="model">The project_category data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("PROJECT_CATEGORY", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] PROJECT_CATEGORY model)
        {
            var id = await _pROJECT_CATEGORYService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of project_categorys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of project_categorys</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("PROJECT_CATEGORY", Entitlements.Read)]
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

            var result = await _pROJECT_CATEGORYService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific project_category by its primary key</summary>
        /// <param name="id">The primary key of the project_category</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The project_category data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("PROJECT_CATEGORY", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _pROJECT_CATEGORYService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific project_category by its primary key</summary>
        /// <param name="id">The primary key of the project_category</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("PROJECT_CATEGORY", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _pROJECT_CATEGORYService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific project_category by its primary key</summary>
        /// <param name="id">The primary key of the project_category</param>
        /// <param name="updatedEntity">The project_category data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("PROJECT_CATEGORY", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] PROJECT_CATEGORY updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _pROJECT_CATEGORYService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific project_category by its primary key</summary>
        /// <param name="id">The primary key of the project_category</param>
        /// <param name="updatedEntity">The project_category data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("PROJECT_CATEGORY", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<PROJECT_CATEGORY> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _pROJECT_CATEGORYService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}