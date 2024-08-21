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
    /// Controller responsible for managing payment_order_item_x_service related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting payment_order_item_x_service information.
    /// </remarks>
    [Route("api/payment_order_item_x_service")]
    [Authorize]
    public class PAYMENT_ORDER_ITEM_X_SERVICEController : BaseApiController
    {
        private readonly IPAYMENT_ORDER_ITEM_X_SERVICEService _pAYMENT_ORDER_ITEM_X_SERVICEService;

        /// <summary>
        /// Initializes a new instance of the PAYMENT_ORDER_ITEM_X_SERVICEController class with the specified context.
        /// </summary>
        /// <param name="ipayment_order_item_x_serviceservice">The ipayment_order_item_x_serviceservice to be used by the controller.</param>
        public PAYMENT_ORDER_ITEM_X_SERVICEController(IPAYMENT_ORDER_ITEM_X_SERVICEService ipayment_order_item_x_serviceservice)
        {
            _pAYMENT_ORDER_ITEM_X_SERVICEService = ipayment_order_item_x_serviceservice;
        }

        /// <summary>Adds a new payment_order_item_x_service</summary>
        /// <param name="model">The payment_order_item_x_service data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("PAYMENT_ORDER_ITEM_X_SERVICE", Entitlements.Create)]
        public async Task<IActionResult> Post([FromBody] PAYMENT_ORDER_ITEM_X_SERVICE model)
        {
            var id = await _pAYMENT_ORDER_ITEM_X_SERVICEService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of payment_order_item_x_services based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of payment_order_item_x_services</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("PAYMENT_ORDER_ITEM_X_SERVICE", Entitlements.Read)]
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

            var result = await _pAYMENT_ORDER_ITEM_X_SERVICEService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific payment_order_item_x_service by its primary key</summary>
        /// <param name="id">The primary key of the payment_order_item_x_service</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The payment_order_item_x_service data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("PAYMENT_ORDER_ITEM_X_SERVICE", Entitlements.Read)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, string fields = null)
        {
            var result = await _pAYMENT_ORDER_ITEM_X_SERVICEService.GetById( id, fields);
            return Ok(result);
        }

        /// <summary>Deletes a specific payment_order_item_x_service by its primary key</summary>
        /// <param name="id">The primary key of the payment_order_item_x_service</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        [UserAuthorize("PAYMENT_ORDER_ITEM_X_SERVICE", Entitlements.Delete)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var status = await _pAYMENT_ORDER_ITEM_X_SERVICEService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific payment_order_item_x_service by its primary key</summary>
        /// <param name="id">The primary key of the payment_order_item_x_service</param>
        /// <param name="updatedEntity">The payment_order_item_x_service data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("PAYMENT_ORDER_ITEM_X_SERVICE", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] PAYMENT_ORDER_ITEM_X_SERVICE updatedEntity)
        {
            if (id != updatedEntity.GUID)
            {
                return BadRequest("Mismatched GUID");
            }

            var status = await _pAYMENT_ORDER_ITEM_X_SERVICEService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific payment_order_item_x_service by its primary key</summary>
        /// <param name="id">The primary key of the payment_order_item_x_service</param>
        /// <param name="updatedEntity">The payment_order_item_x_service data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [UserAuthorize("PAYMENT_ORDER_ITEM_X_SERVICE", Entitlements.Update)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] JsonPatchDocument<PAYMENT_ORDER_ITEM_X_SERVICE> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = await _pAYMENT_ORDER_ITEM_X_SERVICEService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}