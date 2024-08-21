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
    /// The spare_part_withdrawalService responsible for managing spare_part_withdrawal related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting spare_part_withdrawal information.
    /// </remarks>
    public interface ISPARE_PART_WITHDRAWALService
    {
        /// <summary>Retrieves a specific spare_part_withdrawal by its primary key</summary>
        /// <param name="id">The primary key of the spare_part_withdrawal</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The spare_part_withdrawal data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of spare_part_withdrawals based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of spare_part_withdrawals</returns>
        Task<List<SPARE_PART_WITHDRAWAL>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new spare_part_withdrawal</summary>
        /// <param name="model">The spare_part_withdrawal data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(SPARE_PART_WITHDRAWAL model);

        /// <summary>Updates a specific spare_part_withdrawal by its primary key</summary>
        /// <param name="id">The primary key of the spare_part_withdrawal</param>
        /// <param name="updatedEntity">The spare_part_withdrawal data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, SPARE_PART_WITHDRAWAL updatedEntity);

        /// <summary>Updates a specific spare_part_withdrawal by its primary key</summary>
        /// <param name="id">The primary key of the spare_part_withdrawal</param>
        /// <param name="updatedEntity">The spare_part_withdrawal data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<SPARE_PART_WITHDRAWAL> updatedEntity);

        /// <summary>Deletes a specific spare_part_withdrawal by its primary key</summary>
        /// <param name="id">The primary key of the spare_part_withdrawal</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The spare_part_withdrawalService responsible for managing spare_part_withdrawal related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting spare_part_withdrawal information.
    /// </remarks>
    public class SPARE_PART_WITHDRAWALService : ISPARE_PART_WITHDRAWALService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the SPARE_PART_WITHDRAWAL class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public SPARE_PART_WITHDRAWALService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific spare_part_withdrawal by its primary key</summary>
        /// <param name="id">The primary key of the spare_part_withdrawal</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The spare_part_withdrawal data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.SPARE_PART_WITHDRAWAL.AsQueryable();
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

            string[] navigationProperties = ["GUID_USER_UPDATED_BY_USR","GUID_USER_CREATED_BY_USR","GUID_PAYMENT_ORDER_ITEM_PAYMENT_ORDER_ITEM","GUID_DATA_OWNER_DATA_OWNER","GUID_EQUIPMENT_EQUIPMENT","GUID_WORK_ORDER_X_SPARE_PART_WORK_ORDER_X_SPARE_PART","GUID_AREA_AREA","GUID_BUILDING_BUILDING","GUID_SPARE_PART_SPARE_PART","GUID_CONTRACT_ITEM_CONTRACT_ITEM"];
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

        /// <summary>Retrieves a list of spare_part_withdrawals based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of spare_part_withdrawals</returns>/// <exception cref="Exception"></exception>
        public async Task<List<SPARE_PART_WITHDRAWAL>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetSPARE_PART_WITHDRAWAL(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new spare_part_withdrawal</summary>
        /// <param name="model">The spare_part_withdrawal data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(SPARE_PART_WITHDRAWAL model)
        {
            model.GUID = await CreateSPARE_PART_WITHDRAWAL(model);
            return model.GUID;
        }

        /// <summary>Updates a specific spare_part_withdrawal by its primary key</summary>
        /// <param name="id">The primary key of the spare_part_withdrawal</param>
        /// <param name="updatedEntity">The spare_part_withdrawal data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, SPARE_PART_WITHDRAWAL updatedEntity)
        {
            await UpdateSPARE_PART_WITHDRAWAL(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific spare_part_withdrawal by its primary key</summary>
        /// <param name="id">The primary key of the spare_part_withdrawal</param>
        /// <param name="updatedEntity">The spare_part_withdrawal data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<SPARE_PART_WITHDRAWAL> updatedEntity)
        {
            await PatchSPARE_PART_WITHDRAWAL(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific spare_part_withdrawal by its primary key</summary>
        /// <param name="id">The primary key of the spare_part_withdrawal</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteSPARE_PART_WITHDRAWAL(id);
            return true;
        }
        #region
        private async Task<List<SPARE_PART_WITHDRAWAL>> GetSPARE_PART_WITHDRAWAL(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.SPARE_PART_WITHDRAWAL.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<SPARE_PART_WITHDRAWAL>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(SPARE_PART_WITHDRAWAL), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<SPARE_PART_WITHDRAWAL, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreateSPARE_PART_WITHDRAWAL(SPARE_PART_WITHDRAWAL model)
        {
            _dbContext.SPARE_PART_WITHDRAWAL.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateSPARE_PART_WITHDRAWAL(Guid id, SPARE_PART_WITHDRAWAL updatedEntity)
        {
            _dbContext.SPARE_PART_WITHDRAWAL.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteSPARE_PART_WITHDRAWAL(Guid id)
        {
            var entityData = _dbContext.SPARE_PART_WITHDRAWAL.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.SPARE_PART_WITHDRAWAL.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchSPARE_PART_WITHDRAWAL(Guid id, JsonPatchDocument<SPARE_PART_WITHDRAWAL> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.SPARE_PART_WITHDRAWAL.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.SPARE_PART_WITHDRAWAL.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}