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
    /// The relationship_typeService responsible for managing relationship_type related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting relationship_type information.
    /// </remarks>
    public interface IRELATIONSHIP_TYPEService
    {
        /// <summary>Retrieves a specific relationship_type by its primary key</summary>
        /// <param name="id">The primary key of the relationship_type</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The relationship_type data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of relationship_types based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of relationship_types</returns>
        Task<List<RELATIONSHIP_TYPE>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new relationship_type</summary>
        /// <param name="model">The relationship_type data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(RELATIONSHIP_TYPE model);

        /// <summary>Updates a specific relationship_type by its primary key</summary>
        /// <param name="id">The primary key of the relationship_type</param>
        /// <param name="updatedEntity">The relationship_type data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, RELATIONSHIP_TYPE updatedEntity);

        /// <summary>Updates a specific relationship_type by its primary key</summary>
        /// <param name="id">The primary key of the relationship_type</param>
        /// <param name="updatedEntity">The relationship_type data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<RELATIONSHIP_TYPE> updatedEntity);

        /// <summary>Deletes a specific relationship_type by its primary key</summary>
        /// <param name="id">The primary key of the relationship_type</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The relationship_typeService responsible for managing relationship_type related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting relationship_type information.
    /// </remarks>
    public class RELATIONSHIP_TYPEService : IRELATIONSHIP_TYPEService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the RELATIONSHIP_TYPE class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public RELATIONSHIP_TYPEService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific relationship_type by its primary key</summary>
        /// <param name="id">The primary key of the relationship_type</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The relationship_type data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.RELATIONSHIP_TYPE.AsQueryable();
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

            string[] navigationProperties = ["GUID_USER_UPDATED_BY_USR","GUID_USER_CREATED_BY_USR"];
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

        /// <summary>Retrieves a list of relationship_types based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of relationship_types</returns>/// <exception cref="Exception"></exception>
        public async Task<List<RELATIONSHIP_TYPE>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetRELATIONSHIP_TYPE(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new relationship_type</summary>
        /// <param name="model">The relationship_type data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(RELATIONSHIP_TYPE model)
        {
            model.GUID = await CreateRELATIONSHIP_TYPE(model);
            return model.GUID;
        }

        /// <summary>Updates a specific relationship_type by its primary key</summary>
        /// <param name="id">The primary key of the relationship_type</param>
        /// <param name="updatedEntity">The relationship_type data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, RELATIONSHIP_TYPE updatedEntity)
        {
            await UpdateRELATIONSHIP_TYPE(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific relationship_type by its primary key</summary>
        /// <param name="id">The primary key of the relationship_type</param>
        /// <param name="updatedEntity">The relationship_type data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<RELATIONSHIP_TYPE> updatedEntity)
        {
            await PatchRELATIONSHIP_TYPE(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific relationship_type by its primary key</summary>
        /// <param name="id">The primary key of the relationship_type</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteRELATIONSHIP_TYPE(id);
            return true;
        }
        #region
        private async Task<List<RELATIONSHIP_TYPE>> GetRELATIONSHIP_TYPE(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.RELATIONSHIP_TYPE.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<RELATIONSHIP_TYPE>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(RELATIONSHIP_TYPE), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<RELATIONSHIP_TYPE, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreateRELATIONSHIP_TYPE(RELATIONSHIP_TYPE model)
        {
            _dbContext.RELATIONSHIP_TYPE.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateRELATIONSHIP_TYPE(Guid id, RELATIONSHIP_TYPE updatedEntity)
        {
            _dbContext.RELATIONSHIP_TYPE.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteRELATIONSHIP_TYPE(Guid id)
        {
            var entityData = _dbContext.RELATIONSHIP_TYPE.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.RELATIONSHIP_TYPE.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchRELATIONSHIP_TYPE(Guid id, JsonPatchDocument<RELATIONSHIP_TYPE> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.RELATIONSHIP_TYPE.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.RELATIONSHIP_TYPE.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}