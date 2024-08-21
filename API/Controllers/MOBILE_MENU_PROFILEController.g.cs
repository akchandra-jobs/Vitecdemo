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
    /// Controller responsible for managing mobile_menu_profile related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting mobile_menu_profile information.
    /// </remarks>
    [Route("api/mobile_menu_profile")]
    [Authorize]
    public class MOBILE_MENU_PROFILEController : BaseApiController
    {
        private readonly IMOBILE_MENU_PROFILEService _mOBILE_MENU_PROFILEService;

        /// <summary>
        /// Initializes a new instance of the MOBILE_MENU_PROFILEController class with the specified context.
        /// </summary>
        /// <param name="imobile_menu_profileservice">The imobile_menu_profileservice to be used by the controller.</param>
        public MOBILE_MENU_PROFILEController(IMOBILE_MENU_PROFILEService imobile_menu_profileservice)
        {
            _mOBILE_MENU_PROFILEService = imobile_menu_profileservice;
        }

        /// <summary>Adds a new mobile_menu_profile</summary>
        /// <param name="model">The mobile_menu_profile data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("MOBILE_MENU_PROFILE", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] MOBILE_MENU_PROFILE model)
        {
            var id = await _mOBILE_MENU_PROFILEService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of mobile_menu_profiles based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of mobile_menu_profiles</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("MOBILE_MENU_PROFILE", Entitlements.Read)]
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

            var result = await _mOBILE_MENU_PROFILEService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific mobile_menu_profile by its primary key</summary>
        /// <param name="id">The primary key of the mobile_menu_profile</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The mobile_menu_profile data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("MOBILE_MENU_PROFILE", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _mOBILE_MENU_PROFILEService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific mobile_menu_profile by its primary key</summary>
        /// <param name="id">The primary key of the mobile_menu_profile</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("MOBILE_MENU_PROFILE", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _mOBILE_MENU_PROFILEService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific mobile_menu_profile by its primary key</summary>
        /// <param name="id">The primary key of the mobile_menu_profile</param>
        /// <param name="updatedEntity">The mobile_menu_profile data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("MOBILE_MENU_PROFILE", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] MOBILE_MENU_PROFILE updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _mOBILE_MENU_PROFILEService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific mobile_menu_profile by its primary key</summary>
        /// <param name="id">The primary key of the mobile_menu_profile</param>
        /// <param name="updatedEntity">The mobile_menu_profile data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("MOBILE_MENU_PROFILE", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<MOBILE_MENU_PROFILE> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _mOBILE_MENU_PROFILEService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}