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
    /// The person_roleService responsible for managing person_role related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting person_role information.
    /// </remarks>
    public interface IPERSON_ROLEService
    {
        /// <summary>Retrieves a specific person_role by its primary key</summary>
        /// <param name="id">The primary key of the person_role</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The person_role data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of person_roles based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of person_roles</returns>
        Task<List<PERSON_ROLE>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new person_role</summary>
        /// <param name="model">The person_role data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(PERSON_ROLE model);

        /// <summary>Updates a specific person_role by its primary key</summary>
        /// <param name="id">The primary key of the person_role</param>
        /// <param name="updatedEntity">The person_role data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, PERSON_ROLE updatedEntity);

        /// <summary>Updates a specific person_role by its primary key</summary>
        /// <param name="id">The primary key of the person_role</param>
        /// <param name="updatedEntity">The person_role data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<PERSON_ROLE> updatedEntity);

        /// <summary>Deletes a specific person_role by its primary key</summary>
        /// <param name="id">The primary key of the person_role</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The person_roleService responsible for managing person_role related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting person_role information.
    /// </remarks>
    public class PERSON_ROLEService : IPERSON_ROLEService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the PERSON_ROLE class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public PERSON_ROLEService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific person_role by its primary key</summary>
        /// <param name="id">The primary key of the person_role</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The person_role data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.PERSON_ROLE.AsQueryable();
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

        /// <summary>Retrieves a list of person_roles based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of person_roles</returns>/// <exception cref="Exception"></exception>
        public async Task<List<PERSON_ROLE>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetPERSON_ROLE(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new person_role</summary>
        /// <param name="model">The person_role data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(PERSON_ROLE model)
        {
            model.GUID = await CreatePERSON_ROLE(model);
            return model.GUID;
        }

        /// <summary>Updates a specific person_role by its primary key</summary>
        /// <param name="id">The primary key of the person_role</param>
        /// <param name="updatedEntity">The person_role data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, PERSON_ROLE updatedEntity)
        {
            await UpdatePERSON_ROLE(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific person_role by its primary key</summary>
        /// <param name="id">The primary key of the person_role</param>
        /// <param name="updatedEntity">The person_role data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<PERSON_ROLE> updatedEntity)
        {
            await PatchPERSON_ROLE(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific person_role by its primary key</summary>
        /// <param name="id">The primary key of the person_role</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeletePERSON_ROLE(id);
            return true;
        }
        #region
        private async Task<List<PERSON_ROLE>> GetPERSON_ROLE(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.PERSON_ROLE.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<PERSON_ROLE>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(PERSON_ROLE), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<PERSON_ROLE, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreatePERSON_ROLE(PERSON_ROLE model)
        {
            _dbContext.PERSON_ROLE.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdatePERSON_ROLE(Guid id, PERSON_ROLE updatedEntity)
        {
            _dbContext.PERSON_ROLE.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeletePERSON_ROLE(Guid id)
        {
            var entityData = _dbContext.PERSON_ROLE.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.PERSON_ROLE.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchPERSON_ROLE(Guid id, JsonPatchDocument<PERSON_ROLE> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.PERSON_ROLE.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.PERSON_ROLE.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}