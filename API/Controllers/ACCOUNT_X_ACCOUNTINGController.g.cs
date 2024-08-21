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
    /// Controller responsible for managing account_x_accounting related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting account_x_accounting information.
    /// </remarks>
    [Route("api/account_x_accounting")]
    [Authorize]
    public class ACCOUNT_X_ACCOUNTINGController : BaseApiController
    {
        private readonly IACCOUNT_X_ACCOUNTINGService _aCCOUNT_X_ACCOUNTINGService;

        /// <summary>
        /// Initializes a new instance of the ACCOUNT_X_ACCOUNTINGController class with the specified context.
        /// </summary>
        /// <param name="iaccount_x_accountingservice">The iaccount_x_accountingservice to be used by the controller.</param>
        public ACCOUNT_X_ACCOUNTINGController(IACCOUNT_X_ACCOUNTINGService iaccount_x_accountingservice)
        {
            _aCCOUNT_X_ACCOUNTINGService = iaccount_x_accountingservice;
        }

        /// <summary>Adds a new account_x_accounting</summary>
        /// <param name="model">The account_x_accounting data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("ACCOUNT_X_ACCOUNTING", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] ACCOUNT_X_ACCOUNTING model)
        {
            var id = await _aCCOUNT_X_ACCOUNTINGService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of account_x_accountings based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of account_x_accountings</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("ACCOUNT_X_ACCOUNTING", Entitlements.Read)]
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

            var result = await _aCCOUNT_X_ACCOUNTINGService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific account_x_accounting by its primary key</summary>
        /// <param name="id">The primary key of the account_x_accounting</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The account_x_accounting data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("ACCOUNT_X_ACCOUNTING", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _aCCOUNT_X_ACCOUNTINGService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific account_x_accounting by its primary key</summary>
        /// <param name="id">The primary key of the account_x_accounting</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("ACCOUNT_X_ACCOUNTING", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _aCCOUNT_X_ACCOUNTINGService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific account_x_accounting by its primary key</summary>
        /// <param name="id">The primary key of the account_x_accounting</param>
        /// <param name="updatedEntity">The account_x_accounting data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("ACCOUNT_X_ACCOUNTING", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] ACCOUNT_X_ACCOUNTING updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _aCCOUNT_X_ACCOUNTINGService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific account_x_accounting by its primary key</summary>
        /// <param name="id">The primary key of the account_x_accounting</param>
        /// <param name="updatedEntity">The account_x_accounting data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("ACCOUNT_X_ACCOUNTING", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<ACCOUNT_X_ACCOUNTING> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _aCCOUNT_X_ACCOUNTINGService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}