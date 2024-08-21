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
    /// The language_field_entryService responsible for managing language_field_entry related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting language_field_entry information.
    /// </remarks>
    public interface ILANGUAGE_FIELD_ENTRYService
    {
        /// <summary>Retrieves a specific language_field_entry by its primary key</summary>
        /// <param name="id">The primary key of the language_field_entry</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The language_field_entry data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of language_field_entrys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of language_field_entrys</returns>
        Task<List<LANGUAGE_FIELD_ENTRY>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new language_field_entry</summary>
        /// <param name="model">The language_field_entry data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(LANGUAGE_FIELD_ENTRY model);

        /// <summary>Updates a specific language_field_entry by its primary key</summary>
        /// <param name="id">The primary key of the language_field_entry</param>
        /// <param name="updatedEntity">The language_field_entry data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, LANGUAGE_FIELD_ENTRY updatedEntity);

        /// <summary>Updates a specific language_field_entry by its primary key</summary>
        /// <param name="id">The primary key of the language_field_entry</param>
        /// <param name="updatedEntity">The language_field_entry data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<LANGUAGE_FIELD_ENTRY> updatedEntity);

        /// <summary>Deletes a specific language_field_entry by its primary key</summary>
        /// <param name="id">The primary key of the language_field_entry</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The language_field_entryService responsible for managing language_field_entry related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting language_field_entry information.
    /// </remarks>
    public class LANGUAGE_FIELD_ENTRYService : ILANGUAGE_FIELD_ENTRYService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the LANGUAGE_FIELD_ENTRY class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public LANGUAGE_FIELD_ENTRYService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific language_field_entry by its primary key</summary>
        /// <param name="id">The primary key of the language_field_entry</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The language_field_entry data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.LANGUAGE_FIELD_ENTRY.AsQueryable();
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

        /// <summary>Retrieves a list of language_field_entrys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of language_field_entrys</returns>/// <exception cref="Exception"></exception>
        public async Task<List<LANGUAGE_FIELD_ENTRY>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetLANGUAGE_FIELD_ENTRY(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new language_field_entry</summary>
        /// <param name="model">The language_field_entry data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(LANGUAGE_FIELD_ENTRY model)
        {
            model.GUID = await CreateLANGUAGE_FIELD_ENTRY(model);
            return model.GUID;
        }

        /// <summary>Updates a specific language_field_entry by its primary key</summary>
        /// <param name="id">The primary key of the language_field_entry</param>
        /// <param name="updatedEntity">The language_field_entry data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, LANGUAGE_FIELD_ENTRY updatedEntity)
        {
            await UpdateLANGUAGE_FIELD_ENTRY(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific language_field_entry by its primary key</summary>
        /// <param name="id">The primary key of the language_field_entry</param>
        /// <param name="updatedEntity">The language_field_entry data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<LANGUAGE_FIELD_ENTRY> updatedEntity)
        {
            await PatchLANGUAGE_FIELD_ENTRY(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific language_field_entry by its primary key</summary>
        /// <param name="id">The primary key of the language_field_entry</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteLANGUAGE_FIELD_ENTRY(id);
            return true;
        }
        #region
        private async Task<List<LANGUAGE_FIELD_ENTRY>> GetLANGUAGE_FIELD_ENTRY(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.LANGUAGE_FIELD_ENTRY.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<LANGUAGE_FIELD_ENTRY>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(LANGUAGE_FIELD_ENTRY), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<LANGUAGE_FIELD_ENTRY, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreateLANGUAGE_FIELD_ENTRY(LANGUAGE_FIELD_ENTRY model)
        {
            _dbContext.LANGUAGE_FIELD_ENTRY.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateLANGUAGE_FIELD_ENTRY(Guid id, LANGUAGE_FIELD_ENTRY updatedEntity)
        {
            _dbContext.LANGUAGE_FIELD_ENTRY.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteLANGUAGE_FIELD_ENTRY(Guid id)
        {
            var entityData = _dbContext.LANGUAGE_FIELD_ENTRY.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.LANGUAGE_FIELD_ENTRY.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchLANGUAGE_FIELD_ENTRY(Guid id, JsonPatchDocument<LANGUAGE_FIELD_ENTRY> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.LANGUAGE_FIELD_ENTRY.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.LANGUAGE_FIELD_ENTRY.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}