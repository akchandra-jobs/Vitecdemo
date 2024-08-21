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
    /// The medical_ownershipService responsible for managing medical_ownership related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting medical_ownership information.
    /// </remarks>
    public interface IMEDICAL_OWNERSHIPService
    {
        /// <summary>Retrieves a specific medical_ownership by its primary key</summary>
        /// <param name="id">The primary key of the medical_ownership</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The medical_ownership data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of medical_ownerships based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of medical_ownerships</returns>
        Task<List<MEDICAL_OWNERSHIP>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new medical_ownership</summary>
        /// <param name="model">The medical_ownership data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(MEDICAL_OWNERSHIP model);

        /// <summary>Updates a specific medical_ownership by its primary key</summary>
        /// <param name="id">The primary key of the medical_ownership</param>
        /// <param name="updatedEntity">The medical_ownership data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, MEDICAL_OWNERSHIP updatedEntity);

        /// <summary>Updates a specific medical_ownership by its primary key</summary>
        /// <param name="id">The primary key of the medical_ownership</param>
        /// <param name="updatedEntity">The medical_ownership data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<MEDICAL_OWNERSHIP> updatedEntity);

        /// <summary>Deletes a specific medical_ownership by its primary key</summary>
        /// <param name="id">The primary key of the medical_ownership</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The medical_ownershipService responsible for managing medical_ownership related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting medical_ownership information.
    /// </remarks>
    public class MEDICAL_OWNERSHIPService : IMEDICAL_OWNERSHIPService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the MEDICAL_OWNERSHIP class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public MEDICAL_OWNERSHIPService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific medical_ownership by its primary key</summary>
        /// <param name="id">The primary key of the medical_ownership</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The medical_ownership data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.MEDICAL_OWNERSHIP.AsQueryable();
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

        /// <summary>Retrieves a list of medical_ownerships based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of medical_ownerships</returns>/// <exception cref="Exception"></exception>
        public async Task<List<MEDICAL_OWNERSHIP>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetMEDICAL_OWNERSHIP(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new medical_ownership</summary>
        /// <param name="model">The medical_ownership data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(MEDICAL_OWNERSHIP model)
        {
            model.GUID = await CreateMEDICAL_OWNERSHIP(model);
            return model.GUID;
        }

        /// <summary>Updates a specific medical_ownership by its primary key</summary>
        /// <param name="id">The primary key of the medical_ownership</param>
        /// <param name="updatedEntity">The medical_ownership data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, MEDICAL_OWNERSHIP updatedEntity)
        {
            await UpdateMEDICAL_OWNERSHIP(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific medical_ownership by its primary key</summary>
        /// <param name="id">The primary key of the medical_ownership</param>
        /// <param name="updatedEntity">The medical_ownership data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<MEDICAL_OWNERSHIP> updatedEntity)
        {
            await PatchMEDICAL_OWNERSHIP(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific medical_ownership by its primary key</summary>
        /// <param name="id">The primary key of the medical_ownership</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteMEDICAL_OWNERSHIP(id);
            return true;
        }
        #region
        private async Task<List<MEDICAL_OWNERSHIP>> GetMEDICAL_OWNERSHIP(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.MEDICAL_OWNERSHIP.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<MEDICAL_OWNERSHIP>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(MEDICAL_OWNERSHIP), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<MEDICAL_OWNERSHIP, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreateMEDICAL_OWNERSHIP(MEDICAL_OWNERSHIP model)
        {
            _dbContext.MEDICAL_OWNERSHIP.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateMEDICAL_OWNERSHIP(Guid id, MEDICAL_OWNERSHIP updatedEntity)
        {
            _dbContext.MEDICAL_OWNERSHIP.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteMEDICAL_OWNERSHIP(Guid id)
        {
            var entityData = _dbContext.MEDICAL_OWNERSHIP.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.MEDICAL_OWNERSHIP.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchMEDICAL_OWNERSHIP(Guid id, JsonPatchDocument<MEDICAL_OWNERSHIP> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.MEDICAL_OWNERSHIP.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.MEDICAL_OWNERSHIP.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}