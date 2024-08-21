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
    /// The cleaningService responsible for managing cleaning related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting cleaning information.
    /// </remarks>
    public interface ICLEANINGService
    {
        /// <summary>Retrieves a specific cleaning by its primary key</summary>
        /// <param name="id">The primary key of the cleaning</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The cleaning data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of cleanings based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of cleanings</returns>
        Task<List<CLEANING>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new cleaning</summary>
        /// <param name="model">The cleaning data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(CLEANING model);

        /// <summary>Updates a specific cleaning by its primary key</summary>
        /// <param name="id">The primary key of the cleaning</param>
        /// <param name="updatedEntity">The cleaning data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, CLEANING updatedEntity);

        /// <summary>Updates a specific cleaning by its primary key</summary>
        /// <param name="id">The primary key of the cleaning</param>
        /// <param name="updatedEntity">The cleaning data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<CLEANING> updatedEntity);

        /// <summary>Deletes a specific cleaning by its primary key</summary>
        /// <param name="id">The primary key of the cleaning</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The cleaningService responsible for managing cleaning related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting cleaning information.
    /// </remarks>
    public class CLEANINGService : ICLEANINGService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the CLEANING class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public CLEANINGService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific cleaning by its primary key</summary>
        /// <param name="id">The primary key of the cleaning</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The cleaning data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.CLEANING.AsQueryable();
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

            string[] navigationProperties = ["GUID_DATA_OWNER_DATA_OWNER","GUID_INTERVAL_INTERVAL","GUID_USER_UPDATED_BY_USR","GUID_USER_CREATED_BY_USR"];
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

        /// <summary>Retrieves a list of cleanings based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of cleanings</returns>/// <exception cref="Exception"></exception>
        public async Task<List<CLEANING>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetCLEANING(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new cleaning</summary>
        /// <param name="model">The cleaning data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(CLEANING model)
        {
            model.GUID = await CreateCLEANING(model);
            return model.GUID;
        }

        /// <summary>Updates a specific cleaning by its primary key</summary>
        /// <param name="id">The primary key of the cleaning</param>
        /// <param name="updatedEntity">The cleaning data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, CLEANING updatedEntity)
        {
            await UpdateCLEANING(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific cleaning by its primary key</summary>
        /// <param name="id">The primary key of the cleaning</param>
        /// <param name="updatedEntity">The cleaning data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<CLEANING> updatedEntity)
        {
            await PatchCLEANING(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific cleaning by its primary key</summary>
        /// <param name="id">The primary key of the cleaning</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteCLEANING(id);
            return true;
        }
        #region
        private async Task<List<CLEANING>> GetCLEANING(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.CLEANING.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<CLEANING>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(CLEANING), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<CLEANING, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreateCLEANING(CLEANING model)
        {
            _dbContext.CLEANING.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateCLEANING(Guid id, CLEANING updatedEntity)
        {
            _dbContext.CLEANING.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteCLEANING(Guid id)
        {
            var entityData = _dbContext.CLEANING.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.CLEANING.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchCLEANING(Guid id, JsonPatchDocument<CLEANING> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.CLEANING.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.CLEANING.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}