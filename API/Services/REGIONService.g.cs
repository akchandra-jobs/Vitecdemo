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
    /// The regionService responsible for managing region related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting region information.
    /// </remarks>
    public interface IREGIONService
    {
        /// <summary>Retrieves a specific region by its primary key</summary>
        /// <param name="id">The primary key of the region</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The region data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of regions based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of regions</returns>
        Task<List<REGION>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new region</summary>
        /// <param name="model">The region data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(REGION model);

        /// <summary>Updates a specific region by its primary key</summary>
        /// <param name="id">The primary key of the region</param>
        /// <param name="updatedEntity">The region data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, REGION updatedEntity);

        /// <summary>Updates a specific region by its primary key</summary>
        /// <param name="id">The primary key of the region</param>
        /// <param name="updatedEntity">The region data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<REGION> updatedEntity);

        /// <summary>Deletes a specific region by its primary key</summary>
        /// <param name="id">The primary key of the region</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The regionService responsible for managing region related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting region information.
    /// </remarks>
    public class REGIONService : IREGIONService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the REGION class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public REGIONService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific region by its primary key</summary>
        /// <param name="id">The primary key of the region</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The region data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.REGION.AsQueryable();
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

        /// <summary>Retrieves a list of regions based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of regions</returns>/// <exception cref="Exception"></exception>
        public async Task<List<REGION>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetREGION(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new region</summary>
        /// <param name="model">The region data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(REGION model)
        {
            model.GUID = await CreateREGION(model);
            return model.GUID;
        }

        /// <summary>Updates a specific region by its primary key</summary>
        /// <param name="id">The primary key of the region</param>
        /// <param name="updatedEntity">The region data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, REGION updatedEntity)
        {
            await UpdateREGION(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific region by its primary key</summary>
        /// <param name="id">The primary key of the region</param>
        /// <param name="updatedEntity">The region data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<REGION> updatedEntity)
        {
            await PatchREGION(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific region by its primary key</summary>
        /// <param name="id">The primary key of the region</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteREGION(id);
            return true;
        }
        #region
        private async Task<List<REGION>> GetREGION(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.REGION.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<REGION>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(REGION), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<REGION, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreateREGION(REGION model)
        {
            _dbContext.REGION.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateREGION(Guid id, REGION updatedEntity)
        {
            _dbContext.REGION.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteREGION(Guid id)
        {
            var entityData = _dbContext.REGION.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.REGION.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchREGION(Guid id, JsonPatchDocument<REGION> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.REGION.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.REGION.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}