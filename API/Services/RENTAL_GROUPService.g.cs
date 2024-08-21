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
    /// The rental_groupService responsible for managing rental_group related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting rental_group information.
    /// </remarks>
    public interface IRENTAL_GROUPService
    {
        /// <summary>Retrieves a specific rental_group by its primary key</summary>
        /// <param name="id">The primary key of the rental_group</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The rental_group data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of rental_groups based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of rental_groups</returns>
        Task<List<RENTAL_GROUP>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new rental_group</summary>
        /// <param name="model">The rental_group data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(RENTAL_GROUP model);

        /// <summary>Updates a specific rental_group by its primary key</summary>
        /// <param name="id">The primary key of the rental_group</param>
        /// <param name="updatedEntity">The rental_group data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, RENTAL_GROUP updatedEntity);

        /// <summary>Updates a specific rental_group by its primary key</summary>
        /// <param name="id">The primary key of the rental_group</param>
        /// <param name="updatedEntity">The rental_group data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<RENTAL_GROUP> updatedEntity);

        /// <summary>Deletes a specific rental_group by its primary key</summary>
        /// <param name="id">The primary key of the rental_group</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The rental_groupService responsible for managing rental_group related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting rental_group information.
    /// </remarks>
    public class RENTAL_GROUPService : IRENTAL_GROUPService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the RENTAL_GROUP class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public RENTAL_GROUPService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific rental_group by its primary key</summary>
        /// <param name="id">The primary key of the rental_group</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The rental_group data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.RENTAL_GROUP.AsQueryable();
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

            string[] navigationProperties = ["GUID_DATA_OWNER_DATA_OWNER","GUID_USER_UPDATED_BY_USR","GUID_USER_CREATED_BY_USR"];
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

        /// <summary>Retrieves a list of rental_groups based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of rental_groups</returns>/// <exception cref="Exception"></exception>
        public async Task<List<RENTAL_GROUP>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetRENTAL_GROUP(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new rental_group</summary>
        /// <param name="model">The rental_group data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(RENTAL_GROUP model)
        {
            model.GUID = await CreateRENTAL_GROUP(model);
            return model.GUID;
        }

        /// <summary>Updates a specific rental_group by its primary key</summary>
        /// <param name="id">The primary key of the rental_group</param>
        /// <param name="updatedEntity">The rental_group data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, RENTAL_GROUP updatedEntity)
        {
            await UpdateRENTAL_GROUP(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific rental_group by its primary key</summary>
        /// <param name="id">The primary key of the rental_group</param>
        /// <param name="updatedEntity">The rental_group data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<RENTAL_GROUP> updatedEntity)
        {
            await PatchRENTAL_GROUP(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific rental_group by its primary key</summary>
        /// <param name="id">The primary key of the rental_group</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteRENTAL_GROUP(id);
            return true;
        }
        #region
        private async Task<List<RENTAL_GROUP>> GetRENTAL_GROUP(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.RENTAL_GROUP.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<RENTAL_GROUP>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(RENTAL_GROUP), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<RENTAL_GROUP, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreateRENTAL_GROUP(RENTAL_GROUP model)
        {
            _dbContext.RENTAL_GROUP.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateRENTAL_GROUP(Guid id, RENTAL_GROUP updatedEntity)
        {
            _dbContext.RENTAL_GROUP.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteRENTAL_GROUP(Guid id)
        {
            var entityData = _dbContext.RENTAL_GROUP.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.RENTAL_GROUP.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchRENTAL_GROUP(Guid id, JsonPatchDocument<RENTAL_GROUP> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.RENTAL_GROUP.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.RENTAL_GROUP.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}