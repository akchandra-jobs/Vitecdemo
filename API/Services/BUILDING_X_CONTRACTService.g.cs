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
    /// The building_x_contractService responsible for managing building_x_contract related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting building_x_contract information.
    /// </remarks>
    public interface IBUILDING_X_CONTRACTService
    {
        /// <summary>Retrieves a specific building_x_contract by its primary key</summary>
        /// <param name="id">The primary key of the building_x_contract</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The building_x_contract data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of building_x_contracts based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of building_x_contracts</returns>
        Task<List<BUILDING_X_CONTRACT>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new building_x_contract</summary>
        /// <param name="model">The building_x_contract data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(BUILDING_X_CONTRACT model);

        /// <summary>Updates a specific building_x_contract by its primary key</summary>
        /// <param name="id">The primary key of the building_x_contract</param>
        /// <param name="updatedEntity">The building_x_contract data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, BUILDING_X_CONTRACT updatedEntity);

        /// <summary>Updates a specific building_x_contract by its primary key</summary>
        /// <param name="id">The primary key of the building_x_contract</param>
        /// <param name="updatedEntity">The building_x_contract data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<BUILDING_X_CONTRACT> updatedEntity);

        /// <summary>Deletes a specific building_x_contract by its primary key</summary>
        /// <param name="id">The primary key of the building_x_contract</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The building_x_contractService responsible for managing building_x_contract related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting building_x_contract information.
    /// </remarks>
    public class BUILDING_X_CONTRACTService : IBUILDING_X_CONTRACTService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the BUILDING_X_CONTRACT class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public BUILDING_X_CONTRACTService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific building_x_contract by its primary key</summary>
        /// <param name="id">The primary key of the building_x_contract</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The building_x_contract data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.BUILDING_X_CONTRACT.AsQueryable();
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

            string[] navigationProperties = ["GUID_USER_UPDATED_BY_USR","GUID_USER_CREATED_BY_USR","GUID_DATA_OWNER_DATA_OWNER","GUID_BUILDING_BUILDING","GUID_CONTRACT_CONTRACT","GUID_PRICE_SHEET_PRICE_SHEET"];
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

        /// <summary>Retrieves a list of building_x_contracts based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of building_x_contracts</returns>/// <exception cref="Exception"></exception>
        public async Task<List<BUILDING_X_CONTRACT>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetBUILDING_X_CONTRACT(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new building_x_contract</summary>
        /// <param name="model">The building_x_contract data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(BUILDING_X_CONTRACT model)
        {
            model.GUID = await CreateBUILDING_X_CONTRACT(model);
            return model.GUID;
        }

        /// <summary>Updates a specific building_x_contract by its primary key</summary>
        /// <param name="id">The primary key of the building_x_contract</param>
        /// <param name="updatedEntity">The building_x_contract data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, BUILDING_X_CONTRACT updatedEntity)
        {
            await UpdateBUILDING_X_CONTRACT(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific building_x_contract by its primary key</summary>
        /// <param name="id">The primary key of the building_x_contract</param>
        /// <param name="updatedEntity">The building_x_contract data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<BUILDING_X_CONTRACT> updatedEntity)
        {
            await PatchBUILDING_X_CONTRACT(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific building_x_contract by its primary key</summary>
        /// <param name="id">The primary key of the building_x_contract</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteBUILDING_X_CONTRACT(id);
            return true;
        }
        #region
        private async Task<List<BUILDING_X_CONTRACT>> GetBUILDING_X_CONTRACT(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.BUILDING_X_CONTRACT.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<BUILDING_X_CONTRACT>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(BUILDING_X_CONTRACT), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<BUILDING_X_CONTRACT, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreateBUILDING_X_CONTRACT(BUILDING_X_CONTRACT model)
        {
            _dbContext.BUILDING_X_CONTRACT.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateBUILDING_X_CONTRACT(Guid id, BUILDING_X_CONTRACT updatedEntity)
        {
            _dbContext.BUILDING_X_CONTRACT.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteBUILDING_X_CONTRACT(Guid id)
        {
            var entityData = _dbContext.BUILDING_X_CONTRACT.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.BUILDING_X_CONTRACT.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchBUILDING_X_CONTRACT(Guid id, JsonPatchDocument<BUILDING_X_CONTRACT> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.BUILDING_X_CONTRACT.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.BUILDING_X_CONTRACT.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}