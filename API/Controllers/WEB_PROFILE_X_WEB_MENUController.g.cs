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
    /// Controller responsible for managing web_profile_x_web_menu related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting web_profile_x_web_menu information.
    /// </remarks>
    [Route("api/web_profile_x_web_menu")]
    [Authorize]
    public class WEB_PROFILE_X_WEB_MENUController : BaseApiController
    {
        private readonly IWEB_PROFILE_X_WEB_MENUService _wEB_PROFILE_X_WEB_MENUService;

        /// <summary>
        /// Initializes a new instance of the WEB_PROFILE_X_WEB_MENUController class with the specified context.
        /// </summary>
        /// <param name="iweb_profile_x_web_menuservice">The iweb_profile_x_web_menuservice to be used by the controller.</param>
        public WEB_PROFILE_X_WEB_MENUController(IWEB_PROFILE_X_WEB_MENUService iweb_profile_x_web_menuservice)
        {
            _wEB_PROFILE_X_WEB_MENUService = iweb_profile_x_web_menuservice;
        }

        /// <summary>Adds a new web_profile_x_web_menu</summary>
        /// <param name="model">The web_profile_x_web_menu data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("WEB_PROFILE_X_WEB_MENU", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] WEB_PROFILE_X_WEB_MENU model)
        {
            var id = await _wEB_PROFILE_X_WEB_MENUService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of web_profile_x_web_menus based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of web_profile_x_web_menus</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("WEB_PROFILE_X_WEB_MENU", Entitlements.Read)]
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

            var result = await _wEB_PROFILE_X_WEB_MENUService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific web_profile_x_web_menu by its primary key</summary>
        /// <param name="id">The primary key of the web_profile_x_web_menu</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The web_profile_x_web_menu data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("WEB_PROFILE_X_WEB_MENU", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _wEB_PROFILE_X_WEB_MENUService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific web_profile_x_web_menu by its primary key</summary>
        /// <param name="id">The primary key of the web_profile_x_web_menu</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("WEB_PROFILE_X_WEB_MENU", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _wEB_PROFILE_X_WEB_MENUService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific web_profile_x_web_menu by its primary key</summary>
        /// <param name="id">The primary key of the web_profile_x_web_menu</param>
        /// <param name="updatedEntity">The web_profile_x_web_menu data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("WEB_PROFILE_X_WEB_MENU", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] WEB_PROFILE_X_WEB_MENU updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _wEB_PROFILE_X_WEB_MENUService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific web_profile_x_web_menu by its primary key</summary>
        /// <param name="id">The primary key of the web_profile_x_web_menu</param>
        /// <param name="updatedEntity">The web_profile_x_web_menu data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("WEB_PROFILE_X_WEB_MENU", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<WEB_PROFILE_X_WEB_MENU> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _wEB_PROFILE_X_WEB_MENUService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}