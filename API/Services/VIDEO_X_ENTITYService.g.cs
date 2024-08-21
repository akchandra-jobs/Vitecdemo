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
    /// The video_x_entityService responsible for managing video_x_entity related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting video_x_entity information.
    /// </remarks>
    public interface IVIDEO_X_ENTITYService
    {
        /// <summary>Retrieves a specific video_x_entity by its primary key</summary>
        /// <param name="id">The primary key of the video_x_entity</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The video_x_entity data</returns>
        Task<dynamic> GetById(Guid id, string fields);

        /// <summary>Retrieves a list of video_x_entitys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of video_x_entitys</returns>
        Task<List<VIDEO_X_ENTITY>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new video_x_entity</summary>
        /// <param name="model">The video_x_entity data to be added</param>
        /// <returns>The result of the operation</returns>
        Task<Guid> Create(VIDEO_X_ENTITY model);

        /// <summary>Updates a specific video_x_entity by its primary key</summary>
        /// <param name="id">The primary key of the video_x_entity</param>
        /// <param name="updatedEntity">The video_x_entity data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Update(Guid id, VIDEO_X_ENTITY updatedEntity);

        /// <summary>Updates a specific video_x_entity by its primary key</summary>
        /// <param name="id">The primary key of the video_x_entity</param>
        /// <param name="updatedEntity">The video_x_entity data to be updated</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Patch(Guid id, JsonPatchDocument<VIDEO_X_ENTITY> updatedEntity);

        /// <summary>Deletes a specific video_x_entity by its primary key</summary>
        /// <param name="id">The primary key of the video_x_entity</param>
        /// <returns>The result of the operation</returns>
        Task<bool> Delete(Guid id);
    }

    /// <summary>
    /// The video_x_entityService responsible for managing video_x_entity related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting video_x_entity information.
    /// </remarks>
    public class VIDEO_X_ENTITYService : IVIDEO_X_ENTITYService
    {
        private readonly VitecdemoContext _dbContext;
        private readonly IFieldMapperService _mapper;

        /// <summary>
        /// Initializes a new instance of the VIDEO_X_ENTITY class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        /// <param name="mapper">mapper value to set.</param>
        public VIDEO_X_ENTITYService(VitecdemoContext dbContext, IFieldMapperService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>Retrieves a specific video_x_entity by its primary key</summary>
        /// <param name="id">The primary key of the video_x_entity</param>
        /// <param name="fields">The fields is fetch data of selected fields</param>
        /// <returns>The video_x_entity data</returns>
        public async Task<dynamic> GetById(Guid id, string fields)
        {
            var query = _dbContext.VIDEO_X_ENTITY.AsQueryable();
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

            string[] navigationProperties = ["GUID_DATA_OWNER_DATA_OWNER","GUID_VIDEO_VIDEO","GUID_CLEANING_TASK_CLEANING_TASK","GUID_USER_UPDATED_BY_USR","GUID_USER_CREATED_BY_USR"];
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

        /// <summary>Retrieves a list of video_x_entitys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of video_x_entitys</returns>/// <exception cref="Exception"></exception>
        public async Task<List<VIDEO_X_ENTITY>> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = await GetVIDEO_X_ENTITY(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new video_x_entity</summary>
        /// <param name="model">The video_x_entity data to be added</param>
        /// <returns>The result of the operation</returns>
        public async Task<Guid> Create(VIDEO_X_ENTITY model)
        {
            model.GUID = await CreateVIDEO_X_ENTITY(model);
            return model.GUID;
        }

        /// <summary>Updates a specific video_x_entity by its primary key</summary>
        /// <param name="id">The primary key of the video_x_entity</param>
        /// <param name="updatedEntity">The video_x_entity data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(Guid id, VIDEO_X_ENTITY updatedEntity)
        {
            await UpdateVIDEO_X_ENTITY(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific video_x_entity by its primary key</summary>
        /// <param name="id">The primary key of the video_x_entity</param>
        /// <param name="updatedEntity">The video_x_entity data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Patch(Guid id, JsonPatchDocument<VIDEO_X_ENTITY> updatedEntity)
        {
            await PatchVIDEO_X_ENTITY(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific video_x_entity by its primary key</summary>
        /// <param name="id">The primary key of the video_x_entity</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(Guid id)
        {
            await DeleteVIDEO_X_ENTITY(id);
            return true;
        }
        #region
        private async Task<List<VIDEO_X_ENTITY>> GetVIDEO_X_ENTITY(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.VIDEO_X_ENTITY.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<VIDEO_X_ENTITY>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(VIDEO_X_ENTITY), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<VIDEO_X_ENTITY, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private async Task<Guid> CreateVIDEO_X_ENTITY(VIDEO_X_ENTITY model)
        {
            _dbContext.VIDEO_X_ENTITY.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.GUID;
        }

        private async Task UpdateVIDEO_X_ENTITY(Guid id, VIDEO_X_ENTITY updatedEntity)
        {
            _dbContext.VIDEO_X_ENTITY.Update(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> DeleteVIDEO_X_ENTITY(Guid id)
        {
            var entityData = _dbContext.VIDEO_X_ENTITY.FirstOrDefault(entity => entity.GUID == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.VIDEO_X_ENTITY.Remove(entityData);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task PatchVIDEO_X_ENTITY(Guid id, JsonPatchDocument<VIDEO_X_ENTITY> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.VIDEO_X_ENTITY.FirstOrDefault(t => t.GUID == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.VIDEO_X_ENTITY.Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}