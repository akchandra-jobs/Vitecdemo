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
    /// The invoicingService responsible for managing invoicing related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting invoicing information.
    /// </remarks>
    public interface IINVOICINGService
    {
        /// <summary>Retrieves a specific invoicing by its primary key</summary>
        /// <param name="id">The primary key of the invoicing</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The invoicing data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of invoicings based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of invoicings</returns>
        Task<List<INVOICING>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new invoicing</summary>
        /// <param name="model">The invoicing data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(INVOICING model);

        /// <summary>Updates a specific invoicing by its primary key</summary>
        /// <param name="id">The primary key of the invoicing</param>
        /// <param name="updatedEntity">The invoicing data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, INVOICING updatedEntity);

        /// <summary>Updates a specific invoicing by its primary key</summary>
        /// <param name="id">The primary key of the invoicing</param>
        /// <param name="updatedEntity">The invoicing data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<INVOICING> updatedEntity);

        /// <summary>Deletes a specific invoicing by its primary key</summary>
        /// <param name="id">The primary key of the invoicing</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The invoicingService responsible for managing invoicing related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting invoicing information.
    /// </remarks>
    public class INVOICINGService : IINVOICINGService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the INVOICING class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public INVOICINGService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific invoicing by its primary key</summary>
        /// <param name="id">The primary key of the invoicing</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The invoicing data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.INVOICING.AsQueryable();
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

        /// <summary>Retrieves a list of invoicings based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of invoicings</returns>/// <exception cref="Exception"></exception>
        public async Task<List<INVOICING>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetINVOICING(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new invoicing</summary>
        /// <param name="model">The invoicing data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(INVOICING model)
        {
            model.GUID = await CreateINVOICING(model);
            return model.GUID;
        }

        /// <summary>Updates a specific invoicing by its primary key</summary>
        /// <param name="id">The primary key of the invoicing</param>
        /// <param name="updatedEntity">The invoicing data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, INVOICING updatedEntity)
        {
            await UpdateINVOICING(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific invoicing by its primary key</summary>
        /// <param name="id">The primary key of the invoicing</param>
        /// <param name="updatedEntity">The invoicing data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<INVOICING> updatedEntity)
        {
            await PatchINVOICING(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific invoicing by its primary key</summary>
        /// <param name="id">The primary key of the invoicing</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteINVOICING(id);
            return true;
        }
        #region
        private async Task<List<INVOICING>> GetINVOICING(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.INVOICING.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<INVOICING>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(INVOICING), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<INVOICING, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreateINVOICING(INVOICING model)
        {
            _dbContext.INVOICING.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateINVOICING(Guid id, INVOICING updatedEntity)
        {
            _dbContext.INVOICING.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteINVOICING(Guid id)
        {
            var entityData = _dbContext.INVOICING.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.INVOICING.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchINVOICING(Guid id, JsonPatchDocument<INVOICING> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.INVOICING.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.INVOICING.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}