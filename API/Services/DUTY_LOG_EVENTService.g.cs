using Vitecdemo.Models;
using Vitecdemo.Data;
using Vitecdemo.Filter;
using Vitecdemo.Entities;
using Vitecdemo.Logger;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using Task = System.Threading.Tasks.Task;

namespace Vitecdemo.Services
{
    /// <summary>
    /// The duty_log_eventService responsible for managing duty_log_event related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting duty_log_event information.
    /// </remarks>
    public interface IDUTY_LOG_EVENTService
    {
        /// <summary>Retrieves a specific duty_log_event by its primary key</summary>
        /// <param name="id">The primary key of the duty_log_event</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The duty_log_event data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of duty_log_events based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of duty_log_events</returns>
        Task<List<DUTY_LOG_EVENT>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new duty_log_event</summary>
        /// <param name="model">The duty_log_event data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(DUTY_LOG_EVENT model);

        /// <summary>Updates a specific duty_log_event by its primary key</summary>
        /// <param name="id">The primary key of the duty_log_event</param>
        /// <param name="updatedEntity">The duty_log_event data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, DUTY_LOG_EVENT updatedEntity);

        /// <summary>Updates a specific duty_log_event by its primary key</summary>
        /// <param name="id">The primary key of the duty_log_event</param>
        /// <param name="updatedEntity">The duty_log_event data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<DUTY_LOG_EVENT> updatedEntity);

        /// <summary>Deletes a specific duty_log_event by its primary key</summary>
        /// <param name="id">The primary key of the duty_log_event</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The duty_log_eventService responsible for managing duty_log_event related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting duty_log_event information.
    /// </remarks>
    public class DUTY_LOG_EVENTService : IDUTY_LOG_EVENTService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the DUTY_LOG_EVENT class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public DUTY_LOG_EVENTService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific duty_log_event by its primary key</summary>
        /// <param name="id">The primary key of the duty_log_event</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The duty_log_event data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.DUTY_LOG_EVENT.AsQueryable();
            List<string> allfields = new List<string>();
            if (!string.IsNullOrEmpty(fields))
            {
                allfields.AddRange(fields.Split(","));
                fields = $"GUID,{fields}";
            }
            else
            {
                fields = "GUID";
            }

            string[] navigationProperties = ["GUID_DATA_OWNER_DATA_OWNER","GUID_EQUIPMENT_EQUIPMENT","GUID_WORK_ORDER_WORK_ORDER","GUID_BUILDING_BUILDING","GUID_DUTY_LOG_CATEGORY_DUTY_LOG_CATEGORY","GUID_DUTY_LOG_DUTY_LOG","GUID_USER_UPDATED_BY_USR","GUID_USER_CREATED_BY_USR"];
            foreach (var navigationProperty in navigationProperties)
            {
                if (allfields.Any(field => field.StartsWith(navigationProperty + ".", StringComparison.OrdinalIgnoreCase)))
                {
                    query = query.Include(navigationProperty);
                }
            }

            query = query.Where(entity => entity.GUID == id);
            return _mapper.MapToFields(await query.FirstOrDefaultAsync(),fields);
        }

        /// <summary>Retrieves a list of duty_log_events based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of duty_log_events</returns>/// <exception cref="Exception"></exception>
        public async Task<List<DUTY_LOG_EVENT>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetDUTY_LOG_EVENT(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new duty_log_event</summary>
        /// <param name="model">The duty_log_event data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(DUTY_LOG_EVENT model)
        {
            model.GUID = await CreateDUTY_LOG_EVENT(model);
            return model.GUID;
        }

        /// <summary>Updates a specific duty_log_event by its primary key</summary>
        /// <param name="id">The primary key of the duty_log_event</param>
        /// <param name="updatedEntity">The duty_log_event data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, DUTY_LOG_EVENT updatedEntity)
        {
            await UpdateDUTY_LOG_EVENT(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific duty_log_event by its primary key</summary>
        /// <param name="id">The primary key of the duty_log_event</param>
        /// <param name="updatedEntity">The duty_log_event data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<DUTY_LOG_EVENT> updatedEntity)
        {
            await PatchDUTY_LOG_EVENT(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific duty_log_event by its primary key</summary>
        /// <param name="id">The primary key of the duty_log_event</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteDUTY_LOG_EVENT(id);
            return true;
        }
        #region
        private async Task<List<DUTY_LOG_EVENT>> GetDUTY_LOG_EVENT(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.DUTY_LOG_EVENT.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<DUTY_LOG_EVENT>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(DUTY_LOG_EVENT), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<DUTY_LOG_EVENT, object>>(Expression.Convert(property, typeof(object)), parameter);
                if (sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase))
                {
                    result = result.OrderBy(lambda);
                }
                else if (sortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase))
                {
                    result = result.OrderByDescending(lambda);
                }
                else
                {
                    throw new ApplicationException("Invalid sort order. Use 'asc' or 'desc'");
                }
            }

            var paginatedResult = await result.Skip(skip).Take(pageSize).ToListAsync();
            return paginatedResult;
        }

        private async Task<Guid> CreateDUTY_LOG_EVENT(DUTY_LOG_EVENT model)
        {
            _dbContext.DUTY_LOG_EVENT.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateDUTY_LOG_EVENT(Guid id, DUTY_LOG_EVENT updatedEntity)
        {
            _dbContext.DUTY_LOG_EVENT.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteDUTY_LOG_EVENT(Guid id)
        {
            var entityData = _dbContext.DUTY_LOG_EVENT.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.DUTY_LOG_EVENT.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchDUTY_LOG_EVENT(Guid id, JsonPatchDocument<DUTY_LOG_EVENT> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.DUTY_LOG_EVENT.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.DUTY_LOG_EVENT.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}