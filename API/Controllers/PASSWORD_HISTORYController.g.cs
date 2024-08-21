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
    /// Controller responsible for managing password_history related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting password_history information.
    /// </remarks>
    [Route("api/password_history")]
    [Authorize]
    public class PASSWORD_HISTORYController : BaseApiController
    {
        private readonly IPASSWORD_HISTORYService _pASSWORD_HISTORYService;

        /// <summary>
        /// Initializes a new instance of the PASSWORD_HISTORYController class with the specified context.
        /// </summary>
        /// <param name="ipassword_historyservice">The ipassword_historyservice to be used by the controller.</param>
        public PASSWORD_HISTORYController(IPASSWORD_HISTORYService ipassword_historyservice)
        {
            _pASSWORD_HISTORYService = ipassword_historyservice;
        }

        /// <summary>Adds a new password_history</summary>
        /// <param name="model">The password_history data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("PASSWORD_HISTORY", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] PASSWORD_HISTORY model)
        {
            var id = await _pASSWORD_HISTORYService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of password_historys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of password_historys</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("PASSWORD_HISTORY", Entitlements.Read)]
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

            var result = await _pASSWORD_HISTORYService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific password_history by its primary key</summary>
        /// <param name="id">The primary key of the password_history</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The password_history data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("PASSWORD_HISTORY", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _pASSWORD_HISTORYService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific password_history by its primary key</summary>
        /// <param name="id">The primary key of the password_history</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("PASSWORD_HISTORY", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _pASSWORD_HISTORYService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific password_history by its primary key</summary>
        /// <param name="id">The primary key of the password_history</param>
        /// <param name="updatedEntity">The password_history data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("PASSWORD_HISTORY", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] PASSWORD_HISTORY updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _pASSWORD_HISTORYService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific password_history by its primary key</summary>
        /// <param name="id">The primary key of the password_history</param>
        /// <param name="updatedEntity">The password_history data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("PASSWORD_HISTORY", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<PASSWORD_HISTORY> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _pASSWORD_HISTORYService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}