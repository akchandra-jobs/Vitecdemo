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
    /// Controller responsible for managing user_x_web_list_view related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting user_x_web_list_view information.
    /// </remarks>
    [Route("api/user_x_web_list_view")]
    [Authorize]
    public class USER_X_WEB_LIST_VIEWController : BaseApiController
    {
        private readonly IUSER_X_WEB_LIST_VIEWService _uSER_X_WEB_LIST_VIEWService;

        /// <summary>
        /// Initializes a new instance of the USER_X_WEB_LIST_VIEWController class with the specified context.
        /// </summary>
        /// <param name="iuser_x_web_list_viewservice">The iuser_x_web_list_viewservice to be used by the controller.</param>
        public USER_X_WEB_LIST_VIEWController(IUSER_X_WEB_LIST_VIEWService iuser_x_web_list_viewservice)
        {
            _uSER_X_WEB_LIST_VIEWService = iuser_x_web_list_viewservice;
        }

        /// <summary>Adds a new user_x_web_list_view</summary>
        /// <param name="model">The user_x_web_list_view data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("USER_X_WEB_LIST_VIEW", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] USER_X_WEB_LIST_VIEW model)
        {
            var id = await _uSER_X_WEB_LIST_VIEWService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of user_x_web_list_views based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of user_x_web_list_views</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("USER_X_WEB_LIST_VIEW", Entitlements.Read)]
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

            var result = await _uSER_X_WEB_LIST_VIEWService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific user_x_web_list_view by its primary key</summary>
        /// <param name="id">The primary key of the user_x_web_list_view</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The user_x_web_list_view data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("USER_X_WEB_LIST_VIEW", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _uSER_X_WEB_LIST_VIEWService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific user_x_web_list_view by its primary key</summary>
        /// <param name="id">The primary key of the user_x_web_list_view</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("USER_X_WEB_LIST_VIEW", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _uSER_X_WEB_LIST_VIEWService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific user_x_web_list_view by its primary key</summary>
        /// <param name="id">The primary key of the user_x_web_list_view</param>
        /// <param name="updatedEntity">The user_x_web_list_view data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("USER_X_WEB_LIST_VIEW", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] USER_X_WEB_LIST_VIEW updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _uSER_X_WEB_LIST_VIEWService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific user_x_web_list_view by its primary key</summary>
        /// <param name="id">The primary key of the user_x_web_list_view</param>
        /// <param name="updatedEntity">The user_x_web_list_view data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("USER_X_WEB_LIST_VIEW", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<USER_X_WEB_LIST_VIEW> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _uSER_X_WEB_LIST_VIEWService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}