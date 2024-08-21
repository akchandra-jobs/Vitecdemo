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
    /// The eventService responsible for managing event related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting event information.
    /// </remarks>
    public interface IEVENTService
    {
        /// <summary>Retrieves a specific event by its primary key</summary>
        /// <param name="id">The primary key of the event</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The event data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of events based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of events</returns>
        Task<List<Entities.EVENT>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new event</summary>
        /// <param name="model">The event data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(Entities.EVENT model);

        /// <summary>Updates a specific event by its primary key</summary>
        /// <param name="id">The primary key of the event</param>
        /// <param name="updatedEntity">The event data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, Entities.EVENT updatedEntity);

        /// <summary>Updates a specific event by its primary key</summary>
        /// <param name="id">The primary key of the event</param>
        /// <param name="updatedEntity">The event data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<Entities.EVENT> updatedEntity);

        /// <summary>Deletes a specific event by its primary key</summary>
        /// <param name="id">The primary key of the event</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The eventService responsible for managing event related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting event information.
    /// </remarks>
    public class EVENTService : IEVENTService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the EVENT class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public EVENTService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific event by its primary key</summary>
        /// <param name="id">The primary key of the event</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The event data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.EVENT.AsQueryable();
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

            string[] navigationProperties = ["GUID_USER_UPDATED_BY_USR","GUID_USER_CREATED_BY_USR","GUID_DATA_OWNER_DATA_OWNER"];
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

        /// <summary>Retrieves a list of events based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of events</returns>/// <exception cref="Exception"></exception>
        public async Task<List<Entities.EVENT>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetEVENT(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new event</summary>
        /// <param name="model">The event data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(Entities.EVENT model)
        {
            model.GUID = await CreateEVENT(model);
            return model.GUID;
        }

        /// <summary>Updates a specific event by its primary key</summary>
        /// <param name="id">The primary key of the event</param>
        /// <param name="updatedEntity">The event data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, Entities.EVENT updatedEntity)
        {
            await UpdateEVENT(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific event by its primary key</summary>
        /// <param name="id">The primary key of the event</param>
        /// <param name="updatedEntity">The event data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<Entities.EVENT> updatedEntity)
        {
            await PatchEVENT(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific event by its primary key</summary>
        /// <param name="id">The primary key of the event</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteEVENT(id);
            return true;
        }
        #region
        private async Task<List<Entities.EVENT>> GetEVENT(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.EVENT.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<Entities.EVENT>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(Entities.EVENT), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<Entities.EVENT, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreateEVENT(Entities.EVENT model)
        {
            _dbContext.EVENT.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateEVENT(Guid id, Entities.EVENT updatedEntity)
        {
            _dbContext.EVENT.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteEVENT(Guid id)
        {
            var entityData = _dbContext.EVENT.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.EVENT.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchEVENT(Guid id, JsonPatchDocument<Entities.EVENT> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.EVENT.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.EVENT.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}