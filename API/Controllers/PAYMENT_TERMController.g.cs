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
    /// Controller responsible for managing payment_term related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting payment_term information.
    /// </remarks>
    [Route("api/payment_term")]
    [Authorize]
    public class PAYMENT_TERMController : BaseApiController
    {
        private readonly IPAYMENT_TERMService _pAYMENT_TERMService;

        /// <summary>
        /// Initializes a new instance of the PAYMENT_TERMController class with the specified context.
        /// </summary>
        /// <param name="ipayment_termservice">The ipayment_termservice to be used by the controller.</param>
        public PAYMENT_TERMController(IPAYMENT_TERMService ipayment_termservice)
        {
            _pAYMENT_TERMService = ipayment_termservice;
        }

        /// <summary>Adds a new payment_term</summary>
        /// <param name="model">The payment_term data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("PAYMENT_TERM", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] PAYMENT_TERM model)
        {
            var id = await _pAYMENT_TERMService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of payment_terms based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of payment_terms</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("PAYMENT_TERM", Entitlements.Read)]
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

            var result = await _pAYMENT_TERMService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific payment_term by its primary key</summary>
        /// <param name="id">The primary key of the payment_term</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The payment_term data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("PAYMENT_TERM", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _pAYMENT_TERMService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific payment_term by its primary key</summary>
        /// <param name="id">The primary key of the payment_term</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("PAYMENT_TERM", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _pAYMENT_TERMService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific payment_term by its primary key</summary>
        /// <param name="id">The primary key of the payment_term</param>
        /// <param name="updatedEntity">The payment_term data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("PAYMENT_TERM", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] PAYMENT_TERM updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _pAYMENT_TERMService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific payment_term by its primary key</summary>
        /// <param name="id">The primary key of the payment_term</param>
        /// <param name="updatedEntity">The payment_term data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("PAYMENT_TERM", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<PAYMENT_TERM> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _pAYMENT_TERMService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}