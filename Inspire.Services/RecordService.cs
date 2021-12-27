using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Inspire.Services
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Inspire.Annotator;
    using Inspire.Annotator.Annotations;
    using Inspire.Modeller;
    using Inspire.Modeller.Models.Security;

    /// <summary>
    /// This service provides Crud operation for any database entity
    /// </summary>
    /// <typeparam name="TEntity">Entity that has been mapped to a database</typeparam>
    /// <typeparam name="TMap">Data Transfer Object</typeparam>
    /// <typeparam name="T">data type of the primary key</typeparam>
    /// <typeparam name="TDbContext">The database context to use for persistence, retrieval and deletion</typeparam>
    public interface IRecordService<TEntity, TMap, T, TDbContext, TFilter> : IAnnotationService<TEntity, TMap, T, TFilter>
        where TEntity : Record<T>
        where TMap : RecordDto<T>
        where T : IEquatable<T>
        where TFilter: RecordFilter
        where TDbContext : DbContext
    {
        /// <summary>
        /// Gets a single record given a primary key
        /// </summary>
        /// <param name="match"> the linq query expression</param>
        /// <returns>the matching record in the database</returns>
        Task<OutputModel> GetAsync(Expression<Func<TEntity, bool>> match);
        /// <summary>
        /// persists some information to the database
        /// </summary>
        /// <param name="row">the information to be persisted</param>
        /// <param name="createdBy">the user who will effect the persistence</param>
        /// <returns>the persisted record</returns>
        Task<OutputModel> AddAsync(TMap row, string createdBy);
        /// <summary>
        /// Deletes a record that matches a given key
        /// </summary>
        /// <param name="id">the key by which to delete</param>
        /// <param name="deletedBy">the user who will effect the deletion</param>
        /// <returns>the deleted record</returns>
        Task<OutputModel> DeleteAsync(T id, string deletedBy);
        /// <summary>
        /// Fetches records based on search criterion
        /// </summary>
        /// <param name="match">Search Criterion</param>
        /// <returns>Result of the fetching which may include data if successful otherwise an error Message is returned</returns>
        Task<OutputModel> GetAllAsync(TFilter model);
        /// <summary>
        /// Gets a single record given a primary key
        /// </summary>
        /// <param name="id">the value of the primary key</param>
        /// <returns>the matching record in the database</returns>
        Task<OutputModel> GetAsync(T id);
        /// <summary>
        /// updates a given database record with given information
        /// </summary>
        /// <param name="row"> the information to update the database with</param>
        /// <param name="updatedBy">the user who will effect the update</param>
        /// <returns>the updated record</returns>
        Task<OutputModel> UpdateAsync(TMap row, string updatedBy);
        /// <summary>
        /// Validates deletion of a given record on condition that it was created by the the user that wants to delete it
        /// </summary>
        /// <param name="id">primary key of the record to be deleted</param>
        /// <param name="user">the user who wants to delete the record</param>
        /// <returns></returns>
        bool ValidateDeleteOnCreator(T id, string user);
        /// <summary>
        /// Fetches records based on search modek
        /// </summary>
        /// <param name="model">model search Criterion</param>
        /// <returns>Results of the fetching which may include data if successful otherwise an error Message is returned</returns>
        Task<List<TEntity>> ReadAsync(TFilter model);
        TEntity Find(params object[] id);
        Task<OutputModel> ApproveAsync(T id, string approvedBy);
        Task<OutputModel> RejectAsync(T id, string approvedBy);
        public SecurityDetail SecurityDetail { get; set; }

    }
    /// <summary>
    /// Supports Crud operations on the database for a given entity
    /// </summary>
    /// <typeparam name="TEntity">Persistable Database Entity</typeparam>
    /// <typeparam name="TMap">the data transfer object for the database entity</typeparam>
    /// <typeparam name="T">the primary key datatatype for the entity</typeparam>
    /// <typeparam name="TDbContext">the database context to use when doing crud operations</typeparam>
    /// <typeparam name="TFilter">the model to use when creating table filters</typeparam>
    public abstract class RecordService<TEntity, TMap, T, TDbContext, TFilter> : AnnotationService<TEntity, TMap, T, TFilter>, IRecordService<TEntity, TMap, T, TDbContext, TFilter>
        where TEntity : Record<T>
        where TMap : RecordDto<T>
        where T : IEquatable<T>
        where TDbContext : DbContext
        where TFilter: RecordFilter
    {
        protected readonly TDbContext _context;
        public SecurityDetail SecurityDetail { get; set; }
        //private readonly IAuditTrailService _auditTrailService;
        protected string _tableHeader;
        protected string _modelHeader;
        public RecordService(TDbContext context
            //, IAuditTrailService auditTrailService
            )
        {
            _context = context;
            SecurityDetail = new SecurityDetail();
            //_auditTrailService = auditTrailService;
        }

        public async Task<OutputModel> GetAsync(T id)
        {
            var row = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id))                ;

            return new OutputModel
            {
                Data = row
            };
        }
        public async Task<OutputModel> GetAsync(Expression<Func<TEntity, bool>> match)
        {
            var row = await _context.Set<TEntity>().FirstOrDefaultAsync(match);
            return new OutputModel
            {
                Data = row
            };
        }
        /// <summary>
        /// Searches Database based on defined search model for the database
        /// </summary>
        /// <param name="model"> Defined model for the search </param>
        /// <param name="data">already fetched data for further filtering </param>
        /// <returns>Filtered data</returns>
        public virtual IQueryable<TEntity> SearchByFilterModel(TFilter model, IQueryable<TEntity> data=null)
        {
            if(data==null)
                data = _context.Set<TEntity>().AsQueryable();
            return data;
        }
        public virtual Task<List<TEntity>> ReadAsync(TFilter model)
        {
            
            var records = SearchByFilterModel(model);
            return records.ToListAsync();
        }
        public TMap CreateDto(TEntity source)
        {
            return CreateTarget<TMap, TEntity>(source);
        }
        public string GetSearchString(TFilter model)
        {
            return model == null || string.IsNullOrEmpty(model.Search) ? "" : model.Search.ToLower();
        }
        protected virtual void CreateAuditLog(TMap row, TEntity entity = null, [CallerMemberName] string action = "", string username="")
        {
            if (!SecurityDetail.TrailEnabled)
                return;
            string tablename = _context.Model.FindEntityType(typeof(TEntity)).ShortName();
            var primaryKey = _context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties.Select(s => s.Name).Single();
            var keyValue = row.GetType().GetProperty(primaryKey).GetValue(row, null);
            var keyValueString = keyValue == null ? "" : keyValue.ToString();
            var afterLog = row.GetObjectValues();

            var valueLog = afterLog;
            if (entity != null)
            {
                var keys = row.GetObjectProperties();
                var beforeLog = CreateDto(entity).GetObjectValues();
                valueLog = afterLog.GetChangedValues(beforeLog, keys);
            }
            List<string> values = new List<string>();
            foreach (var log in valueLog)
            {
                values.Add($"{log.Key}: {log.Value}");
            }
            string val = string.Join("::", values);
            _context.Add(new AuditLog
            {
                Action = action,
                AuditDate = DateTime.Now,
                RecordKey = keyValueString,
                Table = tablename,
                Username = username,
                Value = val
            });
            _context.SaveChanges();

        }
        public virtual TEntity Find(params object[] id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        public async Task<OutputModel> GetAllAsync(TFilter model)
        {
            return new OutputModel
            {
                Data = await SearchByFilterModel(model).ToListAsync()
            };
        
        }
        /// <summary>
        /// Validates deletion of a given record on condition that it was created by the the user that wants to delete it
        /// </summary>
        /// <param name="id">primary key of the record to be deleted</param>
        /// <param name="user">the user who wants to delete the record</param>
        /// <returns> true if the record can be deleted otherwise false</returns>
        public virtual bool ValidateDeleteOnCreator(T id, string user)
        {
            return true;
        }
        /// <summary>
        /// Validates deletion of a given record on condition that it was modified by the the user that wants to delete it
        /// </summary>
        /// <param name="id">primary key of the record to be deleted</param>
        /// <param name="user">the user who wants to delete the record</param>
        /// <returns>true if the record can be deleted otherwise false</returns>
        protected virtual bool ValidateDeleteOnModifier(T id, string user)
        {
            return true;
        }
        protected virtual OutputModel Validate(TMap row, [CallerMemberName] string caller = "")
        {
            if (caller.ToLower().StartsWith("add"))
            {
                if (Any(s => s.Id.Equals(row.Id)))
                {
                    return new OutputModel
                    {
                        Error = true,
                        Message = $" Key {row.Id}for {_modelHeader} already exist"
                    };
                }
            }
            if (caller.ToLower().StartsWith("update"))
            {
                if (!Any(s => s.Id.Equals(row.Id)))
                {
                    return new OutputModel
                    {
                        Error = true,
                        Message = $" Key {row.Id}for {_modelHeader} does not exist"
                    };
                }
            }
            return new OutputModel();

        }
        /// <summary>
        /// Validates approval of a given record on condition that it was not created by the the user that wants to approval it
        /// </summary>
        /// <param name="id">primary key of the record to be approved</param>
        /// <param name="user">the user who wants to approve the record</param>
        /// <returns>true if approval is allowed otherwise false</returns>
        protected virtual bool ValidateAuthoriseOnCreator(T id, string user)
        {
            return true;
        }
        /// <summary>
        /// Validates approval of a given record on condition that it was not modified by the the user that wants to approval it
        /// </summary>
        /// <param name="id">primary key of the record to be approved</param>
        /// <param name="user">the user who wants to approve the record</param>
        /// <returns>true if approval is allowed otherwise false</returns>
        protected virtual bool ValidateAuthoriseOnModifier(T id, string user)
        {
            return true;
        }

        protected virtual OutputModel Validate(T id, string user, [CallerMemberName] string caller = "")
        {
            if (!Any(s => s.Id.Equals(id)))
            {
                return new OutputModel
                {
                    Error = true,
                    Message = $" Key {id}for {_modelHeader} does not exist"
                };
            }
            if (caller.ToLower().StartsWith("delete"))
            {
                var validateCreator = ValidateDeleteOnCreator(id, user);
                var validateModifier = ValidateDeleteOnModifier(id, user);
                if (!(validateCreator || validateModifier))
                {
                    return new OutputModel
                    {
                        Error = true,
                        Message = $" {_modelHeader} was created by a different user. Deletion failed"
                    };
                }
            }
            if (caller.ToLower().StartsWith("approve"))
            {
                var validateCreator = ValidateAuthoriseOnCreator(id, user);
                var validateModifier = ValidateAuthoriseOnModifier(id, user);
                if (!(validateCreator || validateModifier))
                {
                    return new OutputModel
                    {
                        Error = true,
                        Message = $" {_modelHeader} was created by a different user. Approval failed"
                    };
                }
            }


            return new OutputModel();

        }
        protected TTarget CreateTarget<TTarget, TSource>(TSource source)
        {
            var config = new MapperConfiguration(s => s.CreateMap<TSource, TTarget>());
            var mapper = config.CreateMapper();
            return mapper.Map<TTarget>(source);
        }
        protected List<TTarget> CreateTarget<TTarget, TSource>(List<TSource> source)
        {
            var config = new MapperConfiguration(s => s.CreateMap<TSource, TTarget>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<TTarget>>(source);
        }
        /// <summary>
        /// updates a given record with information about the maker of the record
        /// </summary>
        /// <param name="row">the record to append the details to</param>
        /// <param name="createdBy">the user who will be marked as createby</param>
        protected virtual void AppendCreator(TEntity row, string createdBy)
        {

        }
        /// <summary>
        /// updates a given record with information about who approves the record
        /// </summary>
        /// <param name="row">the record to append the details to</param>
        /// <param name="authorisedBy">the user who will be marked as approver of the record</param>
        protected virtual void AppendAuthoriser(TEntity row, string authorisedBy)
        {

        }
        protected bool Any(Expression<Func<TEntity, bool>> match)
        {
            return _context.Set<TEntity>().Any(match);
        }
        /// <summary>
        /// updates a given record with information about who modified the record
        /// </summary>
        /// <param name="row">the record to append the details to</param>
        /// <param name="authorisedBy">the user who will be marked as modifier of the record</param>
        protected virtual void AppendModifier(TEntity row, string updatedBy)
        {

        }
        public int Count(Expression<Func<TEntity, bool>> match=null)
        {
            return Count<TEntity>(match);
        }
        public int Count<Entity>(Expression<Func<Entity, bool>> match=null ) where Entity: class
        {
            if (match == null)
                return _context.Set<Entity>().ToList().Count;
            return _context.Set<Entity>().Where(match).ToList().Count;
        }
        public virtual async Task<OutputModel> AddAsync(TMap row, string createBy)
        {
            //********* start validations *********************
            var validation = Validate(row);
            if (validation.Error)
                return validation;
            //********* end validations *********************
            var record = CreateTarget<TEntity, TMap>(row);
            AppendCreator(record, createBy);
            await _context.Set<TEntity>().AddAsync(record);
            await _context.SaveChangesAsync();

            //await _auditTrailService.SendAsync(new AuditTrailDTO
            //{
            //    ActionType = "CREATE",
            //    AfterImage = DataImageBuilder.BuildDataImageRow(newRow).ToString(),
            //    AuditDate = DateTime.UtcNow.AddHours(2),
            //    BeforeImage = string.Empty,
            //    DataTable = "IndicatorGroups",
            //    UserId = Country.CreatedBy
            //});

            return new OutputModel
            {
                Message = "Successfully Added Record"
            };
        }

        public virtual async Task<OutputModel> UpdateAsync(TMap row, string updatedBy)
        {
            //********* start validations *********************
            var validation = Validate(row);
            if (validation.Error)
                return validation;
            //********* end validations *********************
            var rowToUpdate = await _context.Set<TEntity>().FirstOrDefaultAsync(s => s.Id.Equals(row.Id));
            if (rowToUpdate is null)
            {
                return new OutputModel(true)
                {

                    Message = $"{_modelHeader} does not exists, update failed"
                };
            }
            //string beforeImage = DataImageBuilder.BuildDataImageRow(rowToUpdate).ToString();
            rowToUpdate.AssignPropertiesForm(row);
            AppendModifier(rowToUpdate, updatedBy);
            await _context.SaveChangesAsync();

            //await _auditTrailService.SendAsync(new AuditTrailDTO
            //{
            //    ActionType = "EDIT",
            //    AfterImage = DataImageBuilder.BuildDataImageRow(rowToUpdate).ToString(),
            //    AuditDate = DateTime.UtcNow.AddHours(2),
            //    BeforeImage = beforeImage,
            //    DataTable = "IndicatorGroups",
            //    UserId = Country.CreatedBy
            //});

            return new OutputModel
            {

                Message = "Successfully updated record"
            };
        }

        public async Task<OutputModel> DeleteAsync(T id, string deletedBy)
        {
            var row = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));

            /****** start validations ****/
            var validation = Validate(id, deletedBy);
            if (validation.Error)
                return validation;
            /****** end validations ****/

            _context.Set<TEntity>().Remove(row);
            await _context.SaveChangesAsync();

            //await _auditTrailService.SendAsync(new AuditTrailDTO
            //{
            //    ActionType = "DELETE",
            //    AfterImage = string.Empty,
            //    AuditDate = DateTime.UtcNow.AddHours(2),
            //    BeforeImage = DataImageBuilder.BuildDataImageRow(row).ToString(),
            //    DataTable = "IndicatorGroups",
            //    UserId = deletedBy
            //});

            return new OutputModel
            {
                Message = "Successfully Deleted record"
            };
        }

        public async Task<OutputModel> ApproveAsync(T id, string approvedBy)
        {
            /****** start validations ****/
            var validation = Validate(id, approvedBy);
            if (validation.Error)
                return validation;
            /****** end validations ****/
            var row = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
            AppendAuthoriser(row, approvedBy);
            _context.Set<TEntity>().Remove(row);
            await _context.SaveChangesAsync();

            //await _auditTrailService.SendAsync(new AuditTrailDTO
            //{
            //    ActionType = "DELETE",
            //    AfterImage = string.Empty,
            //    AuditDate = DateTime.UtcNow.AddHours(2),
            //    BeforeImage = DataImageBuilder.BuildDataImageRow(row).ToString(),
            //    DataTable = "IndicatorGroups",
            //    UserId = deletedBy
            //});

            return new OutputModel
            {
                Message = "Successfully approved record"
            };
        }

        public async Task<OutputModel> RejectAsync(T id, string approvedBy)
        {
            /****** start validations ****/
            var validation = Validate(id, approvedBy);
            if (validation.Error)
                return validation;
            /****** end validations ****/
            var row = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
            AppendAuthoriser(row, approvedBy);
            _context.Set<TEntity>().Remove(row);
            await _context.SaveChangesAsync();

            //await _auditTrailService.SendAsync(new AuditTrailDTO
            //{
            //    ActionType = "DELETE",
            //    AfterImage = string.Empty,
            //    AuditDate = DateTime.UtcNow.AddHours(2),
            //    BeforeImage = DataImageBuilder.BuildDataImageRow(row).ToString(),
            //    DataTable = "IndicatorGroups",
            //    UserId = deletedBy
            //});

            return new OutputModel
            {
                Message = "Successfully approved record"
            };
        }
    }
}
