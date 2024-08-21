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
    /// The organization_sectionService responsible for managing organization_section related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting organization_section information.
    /// </remarks>
    public interface IORGANIZATION_SECTIONService
    {
        /// <summary>Retrieves a specific organization_section by its primary key</summary>
        /// <param name="id">The primary key of the organization_section</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The organization_section data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of organization_sections based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of organization_sections</returns>
        Task<List<ORGANIZATION_SECTION>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new organization_section</summary>
        /// <param name="model">The organization_section data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(ORGANIZATION_SECTION model);

        /// <summary>Updates a specific organization_section by its primary key</summary>
        /// <param name="id">The primary key of the organization_section</param>
        /// <param name="updatedEntity">The organization_section data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, ORGANIZATION_SECTION updatedEntity);

        /// <summary>Updates a specific organization_section by its primary key</summary>
        /// <param name="id">The primary key of the organization_section</param>
        /// <param name="updatedEntity">The organization_section data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<ORGANIZATION_SECTION> updatedEntity);

        /// <summary>Deletes a specific organization_section by its primary key</summary>
        /// <param name="id">The primary key of the organization_section</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The organization_sectionService responsible for managing organization_section related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting organization_section information.
    /// </remarks>
    public class ORGANIZATION_SECTIONService : IORGANIZATION_SECTIONService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the ORGANIZATION_SECTION class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public ORGANIZATION_SECTIONService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific organization_section by its primary key</summary>
        /// <param name="id">The primary key of the organization_section</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The organization_section data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.ORGANIZATION_SECTION.AsQueryable();
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

            string[] navigationProperties = ["GUID_USER_UPDATED_BY_USR","GUID_USER_CREATED_BY_USR","GUID_DATA_OWNER_DATA_OWNER","GUID_ORGANIZATION_UNIT_ORGANIZATION_UNIT"];
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

        /// <summary>Retrieves a list of organization_sections based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of organization_sections</returns>/// <exception cref="Exception"></exception>
        public async Task<List<ORGANIZATION_SECTION>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetORGANIZATION_SECTION(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new organization_section</summary>
        /// <param name="model">The organization_section data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(ORGANIZATION_SECTION model)
        {
            model.GUID = await CreateORGANIZATION_SECTION(model);
            return model.GUID;
        }

        /// <summary>Updates a specific organization_section by its primary key</summary>
        /// <param name="id">The primary key of the organization_section</param>
        /// <param name="updatedEntity">The organization_section data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, ORGANIZATION_SECTION updatedEntity)
        {
            await UpdateORGANIZATION_SECTION(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific organization_section by its primary key</summary>
        /// <param name="id">The primary key of the organization_section</param>
        /// <param name="updatedEntity">The organization_section data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<ORGANIZATION_SECTION> updatedEntity)
        {
            await PatchORGANIZATION_SECTION(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific organization_section by its primary key</summary>
        /// <param name="id">The primary key of the organization_section</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteORGANIZATION_SECTION(id);
            return true;
        }
        #region
        private async Task<List<ORGANIZATION_SECTION>> GetORGANIZATION_SECTION(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.ORGANIZATION_SECTION.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<ORGANIZATION_SECTION>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(ORGANIZATION_SECTION), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<ORGANIZATION_SECTION, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreateORGANIZATION_SECTION(ORGANIZATION_SECTION model)
        {
            _dbContext.ORGANIZATION_SECTION.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateORGANIZATION_SECTION(Guid id, ORGANIZATION_SECTION updatedEntity)
        {
            _dbContext.ORGANIZATION_SECTION.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteORGANIZATION_SECTION(Guid id)
        {
            var entityData = _dbContext.ORGANIZATION_SECTION.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.ORGANIZATION_SECTION.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchORGANIZATION_SECTION(Guid id, JsonPatchDocument<ORGANIZATION_SECTION> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.ORGANIZATION_SECTION.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.ORGANIZATION_SECTION.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}