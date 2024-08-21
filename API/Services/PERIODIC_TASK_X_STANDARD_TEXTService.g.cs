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
    /// The periodic_task_x_standard_textService responsible for managing periodic_task_x_standard_text related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting periodic_task_x_standard_text information.
    /// </remarks>
    public interface IPERIODIC_TASK_X_STANDARD_TEXTService
    {
        /// <summary>Retrieves a specific periodic_task_x_standard_text by its primary key</summary>
        /// <param name="id">The primary key of the periodic_task_x_standard_text</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The periodic_task_x_standard_text data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of periodic_task_x_standard_texts based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of periodic_task_x_standard_texts</returns>
        Task<List<PERIODIC_TASK_X_STANDARD_TEXT>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new periodic_task_x_standard_text</summary>
        /// <param name="model">The periodic_task_x_standard_text data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(PERIODIC_TASK_X_STANDARD_TEXT model);

        /// <summary>Updates a specific periodic_task_x_standard_text by its primary key</summary>
        /// <param name="id">The primary key of the periodic_task_x_standard_text</param>
        /// <param name="updatedEntity">The periodic_task_x_standard_text data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, PERIODIC_TASK_X_STANDARD_TEXT updatedEntity);

        /// <summary>Updates a specific periodic_task_x_standard_text by its primary key</summary>
        /// <param name="id">The primary key of the periodic_task_x_standard_text</param>
        /// <param name="updatedEntity">The periodic_task_x_standard_text data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<PERIODIC_TASK_X_STANDARD_TEXT> updatedEntity);

        /// <summary>Deletes a specific periodic_task_x_standard_text by its primary key</summary>
        /// <param name="id">The primary key of the periodic_task_x_standard_text</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The periodic_task_x_standard_textService responsible for managing periodic_task_x_standard_text related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting periodic_task_x_standard_text information.
    /// </remarks>
    public class PERIODIC_TASK_X_STANDARD_TEXTService : IPERIODIC_TASK_X_STANDARD_TEXTService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the PERIODIC_TASK_X_STANDARD_TEXT class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public PERIODIC_TASK_X_STANDARD_TEXTService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific periodic_task_x_standard_text by its primary key</summary>
        /// <param name="id">The primary key of the periodic_task_x_standard_text</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The periodic_task_x_standard_text data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.PERIODIC_TASK_X_STANDARD_TEXT.AsQueryable();
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

            string[] navigationProperties = ["GUID_DATA_OWNER_DATA_OWNER","GUID_PERIODIC_TASK_PERIODIC_TASK","GUID_STANDARD_TEXT_STANDARD_TEXT","GUID_USER_UPDATED_BY_USR","GUID_USER_CREATED_BY_USR"];
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

        /// <summary>Retrieves a list of periodic_task_x_standard_texts based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of periodic_task_x_standard_texts</returns>/// <exception cref="Exception"></exception>
        public async Task<List<PERIODIC_TASK_X_STANDARD_TEXT>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetPERIODIC_TASK_X_STANDARD_TEXT(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new periodic_task_x_standard_text</summary>
        /// <param name="model">The periodic_task_x_standard_text data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(PERIODIC_TASK_X_STANDARD_TEXT model)
        {
            model.GUID = await CreatePERIODIC_TASK_X_STANDARD_TEXT(model);
            return model.GUID;
        }

        /// <summary>Updates a specific periodic_task_x_standard_text by its primary key</summary>
        /// <param name="id">The primary key of the periodic_task_x_standard_text</param>
        /// <param name="updatedEntity">The periodic_task_x_standard_text data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, PERIODIC_TASK_X_STANDARD_TEXT updatedEntity)
        {
            await UpdatePERIODIC_TASK_X_STANDARD_TEXT(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific periodic_task_x_standard_text by its primary key</summary>
        /// <param name="id">The primary key of the periodic_task_x_standard_text</param>
        /// <param name="updatedEntity">The periodic_task_x_standard_text data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<PERIODIC_TASK_X_STANDARD_TEXT> updatedEntity)
        {
            await PatchPERIODIC_TASK_X_STANDARD_TEXT(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific periodic_task_x_standard_text by its primary key</summary>
        /// <param name="id">The primary key of the periodic_task_x_standard_text</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeletePERIODIC_TASK_X_STANDARD_TEXT(id);
            return true;
        }
        #region
        private async Task<List<PERIODIC_TASK_X_STANDARD_TEXT>> GetPERIODIC_TASK_X_STANDARD_TEXT(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.PERIODIC_TASK_X_STANDARD_TEXT.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<PERIODIC_TASK_X_STANDARD_TEXT>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(PERIODIC_TASK_X_STANDARD_TEXT), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<PERIODIC_TASK_X_STANDARD_TEXT, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreatePERIODIC_TASK_X_STANDARD_TEXT(PERIODIC_TASK_X_STANDARD_TEXT model)
        {
            _dbContext.PERIODIC_TASK_X_STANDARD_TEXT.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdatePERIODIC_TASK_X_STANDARD_TEXT(Guid id, PERIODIC_TASK_X_STANDARD_TEXT updatedEntity)
        {
            _dbContext.PERIODIC_TASK_X_STANDARD_TEXT.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeletePERIODIC_TASK_X_STANDARD_TEXT(Guid id)
        {
            var entityData = _dbContext.PERIODIC_TASK_X_STANDARD_TEXT.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.PERIODIC_TASK_X_STANDARD_TEXT.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchPERIODIC_TASK_X_STANDARD_TEXT(Guid id, JsonPatchDocument<PERIODIC_TASK_X_STANDARD_TEXT> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.PERIODIC_TASK_X_STANDARD_TEXT.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.PERIODIC_TASK_X_STANDARD_TEXT.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}