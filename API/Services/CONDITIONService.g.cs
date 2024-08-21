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
    /// The conditionService responsible for managing condition related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting condition information.
    /// </remarks>
    public interface ICONDITIONService
    {
        /// <summary>Retrieves a specific condition by its primary key</summary>
        /// <param name="id">The primary key of the condition</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The condition data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of conditions based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of conditions</returns>
        Task<List<CONDITION>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new condition</summary>
        /// <param name="model">The condition data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(CONDITION model);

        /// <summary>Updates a specific condition by its primary key</summary>
        /// <param name="id">The primary key of the condition</param>
        /// <param name="updatedEntity">The condition data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, CONDITION updatedEntity);

        /// <summary>Updates a specific condition by its primary key</summary>
        /// <param name="id">The primary key of the condition</param>
        /// <param name="updatedEntity">The condition data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<CONDITION> updatedEntity);

        /// <summary>Deletes a specific condition by its primary key</summary>
        /// <param name="id">The primary key of the condition</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The conditionService responsible for managing condition related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting condition information.
    /// </remarks>
    public class CONDITIONService : ICONDITIONService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the CONDITION class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public CONDITIONService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific condition by its primary key</summary>
        /// <param name="id">The primary key of the condition</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The condition data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.CONDITION.AsQueryable();
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

            string[] navigationProperties = ["GUID_PREVIOUS_VERSION_CONDITION","GUID_USER_CONFIRMED_BY_USR","GUID_AREA_AREA","GUID_ESTATE_ESTATE","GUID_USER_UPDATED_BY_USR","GUID_USER_CREATED_BY_USR","GUID_BUILDING_BUILDING","GUID_ADJUSTED_BY_USER_USR","GUID_BASE_RECORD_CONDITION","GUID_PRIORITY_PRIORITY","GUID_CONSTRUCTION_TYPE_CONSTRUCTION_TYPE","GUID_MASTER_RECORD_CONDITION","GUID_DATA_OWNER_DATA_OWNER","GUID_WORK_ORDER_WORK_ORDER","GUID_EQUIPMENT_EQUIPMENT","GUID_CONSEQUENCE_CONSEQUENCE","GUID_CONSEQUENCE_TYPE_CONSEQUENCE_TYPE","GUID_CONDITION_TYPE_CONDITION_TYPE","GUID_INSPECTION_WORK_ORDER_WORK_ORDER","GUID_CORRECTIVE_WORK_ORDER_WORK_ORDER","GUID_USER_CHECKED_BY_USR"];
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

        /// <summary>Retrieves a list of conditions based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of conditions</returns>/// <exception cref="Exception"></exception>
        public async Task<List<CONDITION>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetCONDITION(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new condition</summary>
        /// <param name="model">The condition data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(CONDITION model)
        {
            model.GUID = await CreateCONDITION(model);
            return model.GUID;
        }

        /// <summary>Updates a specific condition by its primary key</summary>
        /// <param name="id">The primary key of the condition</param>
        /// <param name="updatedEntity">The condition data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, CONDITION updatedEntity)
        {
            await UpdateCONDITION(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific condition by its primary key</summary>
        /// <param name="id">The primary key of the condition</param>
        /// <param name="updatedEntity">The condition data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<CONDITION> updatedEntity)
        {
            await PatchCONDITION(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific condition by its primary key</summary>
        /// <param name="id">The primary key of the condition</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteCONDITION(id);
            return true;
        }
        #region
        private async Task<List<CONDITION>> GetCONDITION(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.CONDITION.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<CONDITION>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(CONDITION), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<CONDITION, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreateCONDITION(CONDITION model)
        {
            _dbContext.CONDITION.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateCONDITION(Guid id, CONDITION updatedEntity)
        {
            _dbContext.CONDITION.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteCONDITION(Guid id)
        {
            var entityData = _dbContext.CONDITION.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.CONDITION.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchCONDITION(Guid id, JsonPatchDocument<CONDITION> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.CONDITION.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.CONDITION.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}