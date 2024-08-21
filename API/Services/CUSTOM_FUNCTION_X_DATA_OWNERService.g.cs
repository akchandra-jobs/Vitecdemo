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
    /// The custom_function_x_data_ownerService responsible for managing custom_function_x_data_owner related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting custom_function_x_data_owner information.
    /// </remarks>
    public interface ICUSTOM_FUNCTION_X_DATA_OWNERService
    {
        /// <summary>Retrieves a specific custom_function_x_data_owner by its primary key</summary>
        /// <param name="id">The primary key of the custom_function_x_data_owner</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The custom_function_x_data_owner data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of custom_function_x_data_owners based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of custom_function_x_data_owners</returns>
        Task<List<CUSTOM_FUNCTION_X_DATA_OWNER>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new custom_function_x_data_owner</summary>
        /// <param name="model">The custom_function_x_data_owner data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(CUSTOM_FUNCTION_X_DATA_OWNER model);

        /// <summary>Updates a specific custom_function_x_data_owner by its primary key</summary>
        /// <param name="id">The primary key of the custom_function_x_data_owner</param>
        /// <param name="updatedEntity">The custom_function_x_data_owner data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, CUSTOM_FUNCTION_X_DATA_OWNER updatedEntity);

        /// <summary>Updates a specific custom_function_x_data_owner by its primary key</summary>
        /// <param name="id">The primary key of the custom_function_x_data_owner</param>
        /// <param name="updatedEntity">The custom_function_x_data_owner data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<CUSTOM_FUNCTION_X_DATA_OWNER> updatedEntity);

        /// <summary>Deletes a specific custom_function_x_data_owner by its primary key</summary>
        /// <param name="id">The primary key of the custom_function_x_data_owner</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The custom_function_x_data_ownerService responsible for managing custom_function_x_data_owner related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting custom_function_x_data_owner information.
    /// </remarks>
    public class CUSTOM_FUNCTION_X_DATA_OWNERService : ICUSTOM_FUNCTION_X_DATA_OWNERService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the CUSTOM_FUNCTION_X_DATA_OWNER class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public CUSTOM_FUNCTION_X_DATA_OWNERService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific custom_function_x_data_owner by its primary key</summary>
        /// <param name="id">The primary key of the custom_function_x_data_owner</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The custom_function_x_data_owner data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.CUSTOM_FUNCTION_X_DATA_OWNER.AsQueryable();
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

            string[] navigationProperties = ["GUID_CUSTOM_FUNCTION_CUSTOM_FUNCTION","GUID_DATA_OWNER_DATA_OWNER","GUID_USER_UPDATED_BY_USR","GUID_USER_CREATED_BY_USR"];
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

        /// <summary>Retrieves a list of custom_function_x_data_owners based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of custom_function_x_data_owners</returns>/// <exception cref="Exception"></exception>
        public async Task<List<CUSTOM_FUNCTION_X_DATA_OWNER>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetCUSTOM_FUNCTION_X_DATA_OWNER(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new custom_function_x_data_owner</summary>
        /// <param name="model">The custom_function_x_data_owner data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(CUSTOM_FUNCTION_X_DATA_OWNER model)
        {
            model.GUID = await CreateCUSTOM_FUNCTION_X_DATA_OWNER(model);
            return model.GUID;
        }

        /// <summary>Updates a specific custom_function_x_data_owner by its primary key</summary>
        /// <param name="id">The primary key of the custom_function_x_data_owner</param>
        /// <param name="updatedEntity">The custom_function_x_data_owner data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, CUSTOM_FUNCTION_X_DATA_OWNER updatedEntity)
        {
            await UpdateCUSTOM_FUNCTION_X_DATA_OWNER(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific custom_function_x_data_owner by its primary key</summary>
        /// <param name="id">The primary key of the custom_function_x_data_owner</param>
        /// <param name="updatedEntity">The custom_function_x_data_owner data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<CUSTOM_FUNCTION_X_DATA_OWNER> updatedEntity)
        {
            await PatchCUSTOM_FUNCTION_X_DATA_OWNER(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific custom_function_x_data_owner by its primary key</summary>
        /// <param name="id">The primary key of the custom_function_x_data_owner</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteCUSTOM_FUNCTION_X_DATA_OWNER(id);
            return true;
        }
        #region
        private async Task<List<CUSTOM_FUNCTION_X_DATA_OWNER>> GetCUSTOM_FUNCTION_X_DATA_OWNER(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.CUSTOM_FUNCTION_X_DATA_OWNER.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<CUSTOM_FUNCTION_X_DATA_OWNER>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(CUSTOM_FUNCTION_X_DATA_OWNER), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<CUSTOM_FUNCTION_X_DATA_OWNER, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreateCUSTOM_FUNCTION_X_DATA_OWNER(CUSTOM_FUNCTION_X_DATA_OWNER model)
        {
            _dbContext.CUSTOM_FUNCTION_X_DATA_OWNER.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateCUSTOM_FUNCTION_X_DATA_OWNER(Guid id, CUSTOM_FUNCTION_X_DATA_OWNER updatedEntity)
        {
            _dbContext.CUSTOM_FUNCTION_X_DATA_OWNER.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteCUSTOM_FUNCTION_X_DATA_OWNER(Guid id)
        {
            var entityData = _dbContext.CUSTOM_FUNCTION_X_DATA_OWNER.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.CUSTOM_FUNCTION_X_DATA_OWNER.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchCUSTOM_FUNCTION_X_DATA_OWNER(Guid id, JsonPatchDocument<CUSTOM_FUNCTION_X_DATA_OWNER> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.CUSTOM_FUNCTION_X_DATA_OWNER.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.CUSTOM_FUNCTION_X_DATA_OWNER.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}