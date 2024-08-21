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
    /// The customer_categoryService responsible for managing customer_category related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting customer_category information.
    /// </remarks>
    public interface ICUSTOMER_CATEGORYService
    {
        /// <summary>Retrieves a specific customer_category by its primary key</summary>
        /// <param name="id">The primary key of the customer_category</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The customer_category data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of customer_categorys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of customer_categorys</returns>
        Task<List<CUSTOMER_CATEGORY>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new customer_category</summary>
        /// <param name="model">The customer_category data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(CUSTOMER_CATEGORY model);

        /// <summary>Updates a specific customer_category by its primary key</summary>
        /// <param name="id">The primary key of the customer_category</param>
        /// <param name="updatedEntity">The customer_category data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, CUSTOMER_CATEGORY updatedEntity);

        /// <summary>Updates a specific customer_category by its primary key</summary>
        /// <param name="id">The primary key of the customer_category</param>
        /// <param name="updatedEntity">The customer_category data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<CUSTOMER_CATEGORY> updatedEntity);

        /// <summary>Deletes a specific customer_category by its primary key</summary>
        /// <param name="id">The primary key of the customer_category</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The customer_categoryService responsible for managing customer_category related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting customer_category information.
    /// </remarks>
    public class CUSTOMER_CATEGORYService : ICUSTOMER_CATEGORYService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the CUSTOMER_CATEGORY class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public CUSTOMER_CATEGORYService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific customer_category by its primary key</summary>
        /// <param name="id">The primary key of the customer_category</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The customer_category data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.CUSTOMER_CATEGORY.AsQueryable();
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

        /// <summary>Retrieves a list of customer_categorys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of customer_categorys</returns>/// <exception cref="Exception"></exception>
        public async Task<List<CUSTOMER_CATEGORY>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetCUSTOMER_CATEGORY(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new customer_category</summary>
        /// <param name="model">The customer_category data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(CUSTOMER_CATEGORY model)
        {
            model.GUID = await CreateCUSTOMER_CATEGORY(model);
            return model.GUID;
        }

        /// <summary>Updates a specific customer_category by its primary key</summary>
        /// <param name="id">The primary key of the customer_category</param>
        /// <param name="updatedEntity">The customer_category data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, CUSTOMER_CATEGORY updatedEntity)
        {
            await UpdateCUSTOMER_CATEGORY(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific customer_category by its primary key</summary>
        /// <param name="id">The primary key of the customer_category</param>
        /// <param name="updatedEntity">The customer_category data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<CUSTOMER_CATEGORY> updatedEntity)
        {
            await PatchCUSTOMER_CATEGORY(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific customer_category by its primary key</summary>
        /// <param name="id">The primary key of the customer_category</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteCUSTOMER_CATEGORY(id);
            return true;
        }
        #region
        private async Task<List<CUSTOMER_CATEGORY>> GetCUSTOMER_CATEGORY(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.CUSTOMER_CATEGORY.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<CUSTOMER_CATEGORY>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(CUSTOMER_CATEGORY), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<CUSTOMER_CATEGORY, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreateCUSTOMER_CATEGORY(CUSTOMER_CATEGORY model)
        {
            _dbContext.CUSTOMER_CATEGORY.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateCUSTOMER_CATEGORY(Guid id, CUSTOMER_CATEGORY updatedEntity)
        {
            _dbContext.CUSTOMER_CATEGORY.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteCUSTOMER_CATEGORY(Guid id)
        {
            var entityData = _dbContext.CUSTOMER_CATEGORY.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.CUSTOMER_CATEGORY.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchCUSTOMER_CATEGORY(Guid id, JsonPatchDocument<CUSTOMER_CATEGORY> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.CUSTOMER_CATEGORY.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.CUSTOMER_CATEGORY.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}