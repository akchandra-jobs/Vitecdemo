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
    /// The accountService responsible for managing account related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting account information.
    /// </remarks>
    public interface IACCOUNTService
    {
        /// <summary>Retrieves a specific account by its primary key</summary>
        /// <param name="id">The primary key of the account</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The account data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of accounts based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of accounts</returns>
        Task<List<ACCOUNT>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new account</summary>
        /// <param name="model">The account data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(ACCOUNT model);

        /// <summary>Updates a specific account by its primary key</summary>
        /// <param name="id">The primary key of the account</param>
        /// <param name="updatedEntity">The account data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, ACCOUNT updatedEntity);

        /// <summary>Updates a specific account by its primary key</summary>
        /// <param name="id">The primary key of the account</param>
        /// <param name="updatedEntity">The account data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<ACCOUNT> updatedEntity);

        /// <summary>Deletes a specific account by its primary key</summary>
        /// <param name="id">The primary key of the account</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The accountService responsible for managing account related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting account information.
    /// </remarks>
    public class ACCOUNTService : IACCOUNTService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the ACCOUNT class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public ACCOUNTService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific account by its primary key</summary>
        /// <param name="id">The primary key of the account</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The account data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.ACCOUNT.AsQueryable();
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

            string[] navigationProperties = ["GUID_USER_UPDATED_BY_USR","GUID_USER_CREATED_BY_USR","GUID_DATA_OWNER_DATA_OWNER"];
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

        /// <summary>Retrieves a list of accounts based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of accounts</returns>/// <exception cref="Exception"></exception>
        public async Task<List<ACCOUNT>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetACCOUNT(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new account</summary>
        /// <param name="model">The account data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(ACCOUNT model)
        {
            model.GUID = await CreateACCOUNT(model);
            return model.GUID;
        }

        /// <summary>Updates a specific account by its primary key</summary>
        /// <param name="id">The primary key of the account</param>
        /// <param name="updatedEntity">The account data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, ACCOUNT updatedEntity)
        {
            await UpdateACCOUNT(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific account by its primary key</summary>
        /// <param name="id">The primary key of the account</param>
        /// <param name="updatedEntity">The account data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<ACCOUNT> updatedEntity)
        {
            await PatchACCOUNT(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific account by its primary key</summary>
        /// <param name="id">The primary key of the account</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteACCOUNT(id);
            return true;
        }
        #region
        private async Task<List<ACCOUNT>> GetACCOUNT(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.ACCOUNT.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<ACCOUNT>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(ACCOUNT), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<ACCOUNT, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreateACCOUNT(ACCOUNT model)
        {
            _dbContext.ACCOUNT.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateACCOUNT(Guid id, ACCOUNT updatedEntity)
        {
            _dbContext.ACCOUNT.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteACCOUNT(Guid id)
        {
            var entityData = _dbContext.ACCOUNT.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.ACCOUNT.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchACCOUNT(Guid id, JsonPatchDocument<ACCOUNT> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.ACCOUNT.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.ACCOUNT.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}