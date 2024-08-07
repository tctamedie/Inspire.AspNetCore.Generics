using DocumentFormat.OpenXml.Spreadsheet;
using Inspire;
using Inspire.Modeller.Models;
using Inspire.Services.Application.Common;
using Inspire.Services.Infrastructure.Common;

namespace Inspire.Services.Infrastructure.Common
{

    /// <summary>
    /// Supports Crud operations on the database for a given entity
    /// </summary>
    /// <typeparam name="TEntity">Persistable Database Entity</typeparam>
    /// <typeparam name="TMap">the data transfer object for the database entity</typeparam>
    /// <typeparam name="T">the primary key datatatype for the entity</typeparam>
    /// <typeparam name="TDbContext">the database context to use when doing crud operations</typeparam>
    /// <typeparam name="TFilter">the model to use when creating table filters</typeparam>
    public abstract class RecordService<TEntity, TMap, T, TDbContext, TFilter>(TDbContext _context) : AnnotationService<TEntity, TMap, T, TFilter>, IRecordService<TEntity, TMap, T, TFilter>
        where TEntity : Record<T>, new()
        where TMap : RecordDto<T>
        where T : IEquatable<T>
        where TDbContext : DbContext
        where TFilter : RecordFilter
    {

        public SecurityDetail SecurityDetail { get; set; }
        //private readonly IAuditTrailService _auditTrailService;
        protected string _tableHeader;
        protected string _modelHeader;


        public async Task<OutputModel> GetAsync(T id)
        {
            var row = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));

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
        public virtual IQueryable<TEntity> SearchByFilterModel(TFilter model, IQueryable<TEntity> data = null)
        {
            if (data == null)
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
        public List<GenericData<TEnum>> ConvertEnumToList<TEnum>() where TEnum : Enum
        {
            var data = Enum.GetValues(typeof(TEnum))
               .Cast<TEnum>()
               .Select(s => new GenericData<TEnum>
               {
                   ID = s,
                   Name = s.ToString().CamelSplit(),
               }).ToList();
            return data;

        }
        protected virtual void CreateAuditLog(TMap row, TEntity entity = null, string username = "", [CallerMemberName] string action = "")
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
        protected bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
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
        public IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> match=null)
        {
            if (match == null)
                return _context.Set<TEntity>();
            return _context.Set<TEntity>().Where(match);

        }
        public IQueryable<TClass> GetList<TClass>(Expression<Func<TClass, bool>> match = null) where TClass : class
        {
            if (match == null)
                return _context.Set<TClass>();
            return _context.Set<TClass>().Where(match);

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
        protected virtual OutputHandler Validate(TMap row, int a = 0, bool c = true, string capturer = "", [CallerMemberName] string caller = "")
        {
            if (caller.ToLower().StartsWith("add"))
            {
                if (Any(s => s.Id.Equals(row.Id)))
                {
                    return new OutputHandler
                    {
                        ErrorOccured = true,
                        Description = $" Key {row.Id}for {_modelHeader} already exist"
                    };
                }
            }
            if (caller.ToLower().StartsWith("update"))
            {
                if (!Any(s => s.Id.Equals(row.Id)))
                {
                    return new OutputHandler
                    {
                        ErrorOccured = true,
                        Description = $" Key {row.Id}for {_modelHeader} does not exist"
                    };
                }
            }
            return new OutputHandler();

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
        protected virtual TMap AlignDataTransferObject(TMap row, [CallerMemberName] string caller = "")
        {
            return row;
        }
        protected virtual List<TMap> AlignDataTransferObject(List<TMap> rows, [CallerMemberName] string caller = "")
        {
            return rows;
        }
        public virtual Task<OutputHandler> AddInScopeAsync(List<TMap> rows, string createBy, int authorisers = 0, bool captureTrail = true, string authoriser = "")
        {
            _modelHeader = GetTableName();
            return Task.FromResult(new OutputHandler());
        }
        public virtual async Task<OutputHandler> AddInScopeAsync(TMap row, string createBy, int authorisers = 0, bool captureTrail = true, string authoriser = "")
        {
            _modelHeader = GetTableName();
            row = AlignDataTransferObject(row);
            //********* start validations *********************
            var validation = Validate(row);
            if (validation.ErrorOccured)
                return validation;
            //********* end validations *********************
            var record = CreateTarget<TEntity, TMap>(row);
            //capture audit trail
            if (captureTrail)
                CreateAuditLog(row, null, createBy);
            AppendCreator(record, createBy);
            await _context.Set<TEntity>().AddAsync(record);
            await _context.SaveChangesAsync();
            record = Find(row.Id);
            var output = await ManageEntityCreation(record);
            output.Data = record;
            return new OutputHandler
            {
                Data = record,
                Description = "Successfully Added Record"
            };
        }
        public virtual Task<OutputHandler> UpdateInScopeAsync(List<TMap> row, string createBy, int authorisers = 0, bool captureTrail = true, string authoriser = "")
        {
            return Task.FromResult(new OutputHandler());
        }
        public virtual async Task<OutputHandler> UpdateInScopeAsync(TMap row, string createBy, int authorisers = 0, bool captureTrail = true, string authoriser = "")
        {
            _modelHeader = GetTableName();
            var validation = Validate(row);
            if (validation.ErrorOccured)
                return validation;
            //********* end validations *********************
            var rowToUpdate = await _context.Set<TEntity>().FirstOrDefaultAsync(s => s.Id.Equals(row.Id));
            if (rowToUpdate is null)
            {
                return new OutputHandler
                {
                    ErrorOccured = true,
                    Description = $"{_modelHeader} does not exists, update failed"
                };
            }
            if (captureTrail)
                CreateAuditLog(row, rowToUpdate, createBy);
            //string beforeImage = DataImageBuilder.BuildDataImageRow(rowToUpdate).ToString();
            rowToUpdate.AssignPropertiesForm(row);
            AppendModifier(rowToUpdate, createBy);
            await _context.SaveChangesAsync();


            return new OutputHandler
            {
                Data = rowToUpdate,
                Description = "Successfully updated record"
            };
        }
        protected virtual OutputHandler Validate(T id, string user, [CallerMemberName] string caller = "")
        {
            if (!Any(s => s.Id.Equals(id)))
            {
                return new OutputHandler
                {
                    ErrorOccured = true,
                    Description = $" Key {id} for {_modelHeader} does not exist"
                };
            }
            if (caller.ToLower().StartsWith("delete"))
            {
                var validateCreator = ValidateDeleteOnCreator(id, user);
                var validateModifier = ValidateDeleteOnModifier(id, user);
                if (!(validateCreator || validateModifier))
                {
                    return new OutputHandler
                    {
                        ErrorOccured = true,
                        Description = $" {_modelHeader} was created by a different user. Deletion failed"
                    };
                }
            }
            if (caller.ToLower().StartsWith("approve"))
            {
                var validateCreator = ValidateAuthoriseOnCreator(id, user);
                var validateModifier = ValidateAuthoriseOnModifier(id, user);
                if (!(validateCreator || validateModifier))
                {
                    return new OutputHandler
                    {
                        ErrorOccured = true,
                        Description = $" {_modelHeader} was created by a different user. Approval failed"
                    };
                }
            }


            return new OutputHandler();

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
        public int Count(Expression<Func<TEntity, bool>> match = null)
        {
            return Count<TEntity>(match);
        }
        public int Count<Entity>(Expression<Func<Entity, bool>> match = null) where Entity : class
        {
            if (match == null)
                return _context.Set<Entity>().ToList().Count;
            return _context.Set<Entity>().Where(match).ToList().Count;
        }
        public virtual async Task<OutputHandler> AddAsync(TMap row, string createBy, int authorisers = 0, bool captureTrail = true, string authoriser = "")
        {
            return await AddInScopeAsync(row, createBy, authorisers, captureTrail, authoriser);
        }

        public virtual async Task<OutputHandler> UpdateAsync(TMap row, string username, int authorisers, bool captureAuditTrail)
        {
            return await UpdateInScopeAsync(row, username, authorisers, captureAuditTrail);
        }
        protected virtual Task<OutputHandler> ManageEntityUpdate(TEntity row)
        {
            return Task.FromResult(new OutputHandler());
        }
        protected virtual Task<OutputHandler> ManageEntityApproval(TEntity row)
        {
            return Task.FromResult(new OutputHandler());
        }
        protected virtual Task<OutputHandler> ManageEntityCreation(TEntity row)
        {
            return Task.FromResult(new OutputHandler());
        }
        protected virtual Task<OutputHandler> ManageEntityDeletion(TEntity row)
        {
            return Task.FromResult(new OutputHandler());
        }
        protected virtual Task<OutputHandler> ManageEntityRejection(TEntity row)
        {
            return Task.FromResult(new OutputHandler());
        }
        public async Task<OutputHandler> DeleteAsync(T id, string deletedBy, int authorisers = 0, bool captureTrail = true, string authoriser = "")
        {
            var row = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));

            /****** start validations ****/
            var validation = Validate(id, deletedBy);
            if (validation.ErrorOccured)
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

            return new OutputHandler
            {
                Description = "Successfully Deleted record"
            };
        }

        public async Task<OutputHandler> ApproveAsync(T id, string approvedBy, int authorisers = 0, bool captureTrail = true)
        {
            /****** start validations ****/
            var validation = Validate(id, approvedBy);
            if (validation.ErrorOccured)
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

            return new OutputHandler
            {
                Description = "Successfully approved record"
            };
        }

        public async Task<OutputHandler> RejectAsync(T id, string approvedBy, int authorisers = 0, bool captureTrail = true)
        {
            /****** start validations ****/
            var validation = Validate(id, approvedBy);
            if (validation.ErrorOccured)
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

            return new OutputHandler
            {
                Description = "Successfully approved record"
            };
        }

        public Task<OutputHandler> UpdateInScopeAsync(TMap row, string username, int authorisers, bool captureAuditTrail)
        {
            throw new NotImplementedException();
        }

        public Task<OutputHandler> UpdateInScopeAsync(List<TMap> row, string username, int authorisers, bool captureAuditTrail)
        {
            throw new NotImplementedException();
        }

        public Task<OutputHandler> ApproveInScopeAsync(T id, string approvedBy, int authorisers = 0, bool captureTrail = true)
        {
            throw new NotImplementedException();
        }

        public Task<OutputHandler> ApproveInScopeAsync(List<T> id, string approvedBy, int authorisers = 0, bool captureTrail = true)
        {
            throw new NotImplementedException();
        }

        public Task<OutputHandler> RejectInScopeAsync(T id, string approvedBy, int authorisers = 0, bool captureTrail = true)
        {
            throw new NotImplementedException();
        }

        public Task<OutputHandler> RejectInScopeAsync(List<T> id, string approvedBy, int authorisers = 0, bool captureTrail = true)
        {
            throw new NotImplementedException();
        }
    }
}
