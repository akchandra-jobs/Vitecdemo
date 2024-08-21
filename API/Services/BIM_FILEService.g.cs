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
    /// The bim_fileService responsible for managing bim_file related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting bim_file information.
    /// </remarks>
    public interface IBIM_FILEService
    {
        /// <summary>Retrieves a specific bim_file by its primary key</summary>
        /// <param name="id">The primary key of the bim_file</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The bim_file data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of bim_files based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of bim_files</returns>
        Task<List<BIM_FILE>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new bim_file</summary>
        /// <param name="model">The bim_file data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(BIM_FILE model);

        /// <summary>Updates a specific bim_file by its primary key</summary>
        /// <param name="id">The primary key of the bim_file</param>
        /// <param name="updatedEntity">The bim_file data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, BIM_FILE updatedEntity);

        /// <summary>Updates a specific bim_file by its primary key</summary>
        /// <param name="id">The primary key of the bim_file</param>
        /// <param name="updatedEntity">The bim_file data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<BIM_FILE> updatedEntity);

        /// <summary>Deletes a specific bim_file by its primary key</summary>
        /// <param name="id">The primary key of the bim_file</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The bim_fileService responsible for managing bim_file related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting bim_file information.
    /// </remarks>
    public class BIM_FILEService : IBIM_FILEService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the BIM_FILE class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public BIM_FILEService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific bim_file by its primary key</summary>
        /// <param name="id">The primary key of the bim_file</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The bim_file data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.BIM_FILE.AsQueryable();
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

            string[] navigationProperties = ["GUID_BIM_PROJECT_BIM_PROJECT","GUID_DATA_OWNER_DATA_OWNER","GUID_USER_UPDATED_BY_USR","GUID_USER_CREATED_BY_USR"];
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

        /// <summary>Retrieves a list of bim_files based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of bim_files</returns>/// <exception cref="Exception"></exception>
        public async Task<List<BIM_FILE>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetBIM_FILE(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new bim_file</summary>
        /// <param name="model">The bim_file data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(BIM_FILE model)
        {
            model.GUID = await CreateBIM_FILE(model);
            return model.GUID;
        }

        /// <summary>Updates a specific bim_file by its primary key</summary>
        /// <param name="id">The primary key of the bim_file</param>
        /// <param name="updatedEntity">The bim_file data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, BIM_FILE updatedEntity)
        {
            await UpdateBIM_FILE(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific bim_file by its primary key</summary>
        /// <param name="id">The primary key of the bim_file</param>
        /// <param name="updatedEntity">The bim_file data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<BIM_FILE> updatedEntity)
        {
            await PatchBIM_FILE(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific bim_file by its primary key</summary>
        /// <param name="id">The primary key of the bim_file</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteBIM_FILE(id);
            return true;
        }
        #region
        private async Task<List<BIM_FILE>> GetBIM_FILE(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.BIM_FILE.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<BIM_FILE>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(BIM_FILE), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<BIM_FILE, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreateBIM_FILE(BIM_FILE model)
        {
            _dbContext.BIM_FILE.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateBIM_FILE(Guid id, BIM_FILE updatedEntity)
        {
            _dbContext.BIM_FILE.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteBIM_FILE(Guid id)
        {
            var entityData = _dbContext.BIM_FILE.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.BIM_FILE.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchBIM_FILE(Guid id, JsonPatchDocument<BIM_FILE> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.BIM_FILE.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.BIM_FILE.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}