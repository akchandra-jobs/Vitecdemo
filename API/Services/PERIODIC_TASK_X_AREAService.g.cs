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
    /// The periodic_task_x_areaService responsible for managing periodic_task_x_area related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting periodic_task_x_area information.
    /// </remarks>
    public interface IPERIODIC_TASK_X_AREAService
    {
        /// <summary>Retrieves a specific periodic_task_x_area by its primary key</summary>
        /// <param name="id">The primary key of the periodic_task_x_area</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The periodic_task_x_area data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of periodic_task_x_areas based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of periodic_task_x_areas</returns>
        Task<List<PERIODIC_TASK_X_AREA>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new periodic_task_x_area</summary>
        /// <param name="model">The periodic_task_x_area data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(PERIODIC_TASK_X_AREA model);

        /// <summary>Updates a specific periodic_task_x_area by its primary key</summary>
        /// <param name="id">The primary key of the periodic_task_x_area</param>
        /// <param name="updatedEntity">The periodic_task_x_area data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, PERIODIC_TASK_X_AREA updatedEntity);

        /// <summary>Updates a specific periodic_task_x_area by its primary key</summary>
        /// <param name="id">The primary key of the periodic_task_x_area</param>
        /// <param name="updatedEntity">The periodic_task_x_area data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<PERIODIC_TASK_X_AREA> updatedEntity);

        /// <summary>Deletes a specific periodic_task_x_area by its primary key</summary>
        /// <param name="id">The primary key of the periodic_task_x_area</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The periodic_task_x_areaService responsible for managing periodic_task_x_area related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting periodic_task_x_area information.
    /// </remarks>
    public class PERIODIC_TASK_X_AREAService : IPERIODIC_TASK_X_AREAService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the PERIODIC_TASK_X_AREA class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public PERIODIC_TASK_X_AREAService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific periodic_task_x_area by its primary key</summary>
        /// <param name="id">The primary key of the periodic_task_x_area</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The periodic_task_x_area data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.PERIODIC_TASK_X_AREA.AsQueryable();
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

            string[] navigationProperties = ["GUID_DATA_OWNER_DATA_OWNER","GUID_AREA_AREA","GUID_PERIODIC_TASK_PERIODIC_TASK","GUID_USER_UPDATED_BY_USR","GUID_USER_CREATED_BY_USR"];
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

        /// <summary>Retrieves a list of periodic_task_x_areas based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of periodic_task_x_areas</returns>/// <exception cref="Exception"></exception>
        public async Task<List<PERIODIC_TASK_X_AREA>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetPERIODIC_TASK_X_AREA(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new periodic_task_x_area</summary>
        /// <param name="model">The periodic_task_x_area data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(PERIODIC_TASK_X_AREA model)
        {
            model.GUID = await CreatePERIODIC_TASK_X_AREA(model);
            return model.GUID;
        }

        /// <summary>Updates a specific periodic_task_x_area by its primary key</summary>
        /// <param name="id">The primary key of the periodic_task_x_area</param>
        /// <param name="updatedEntity">The periodic_task_x_area data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, PERIODIC_TASK_X_AREA updatedEntity)
        {
            await UpdatePERIODIC_TASK_X_AREA(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific periodic_task_x_area by its primary key</summary>
        /// <param name="id">The primary key of the periodic_task_x_area</param>
        /// <param name="updatedEntity">The periodic_task_x_area data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<PERIODIC_TASK_X_AREA> updatedEntity)
        {
            await PatchPERIODIC_TASK_X_AREA(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific periodic_task_x_area by its primary key</summary>
        /// <param name="id">The primary key of the periodic_task_x_area</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeletePERIODIC_TASK_X_AREA(id);
            return true;
        }
        #region
        private async Task<List<PERIODIC_TASK_X_AREA>> GetPERIODIC_TASK_X_AREA(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.PERIODIC_TASK_X_AREA.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<PERIODIC_TASK_X_AREA>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(PERIODIC_TASK_X_AREA), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<PERIODIC_TASK_X_AREA, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreatePERIODIC_TASK_X_AREA(PERIODIC_TASK_X_AREA model)
        {
            _dbContext.PERIODIC_TASK_X_AREA.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdatePERIODIC_TASK_X_AREA(Guid id, PERIODIC_TASK_X_AREA updatedEntity)
        {
            _dbContext.PERIODIC_TASK_X_AREA.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeletePERIODIC_TASK_X_AREA(Guid id)
        {
            var entityData = _dbContext.PERIODIC_TASK_X_AREA.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.PERIODIC_TASK_X_AREA.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchPERIODIC_TASK_X_AREA(Guid id, JsonPatchDocument<PERIODIC_TASK_X_AREA> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.PERIODIC_TASK_X_AREA.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.PERIODIC_TASK_X_AREA.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}