namespace Inspire.Services.Application.Common
{
    /// <summary>
    /// This service provides Crud operation for any database entity
    /// </summary>
    /// <typeparam name="TEntity">Entity that has been mapped to a database</typeparam>
    /// <typeparam name="TMap">Data Transfer Object</typeparam>
    /// <typeparam name="T">data type of the primary key</typeparam>
    /// <typeparam name="TDbContext">The database context to use for persistence, retrieval and deletion</typeparam>
    public interface IRecordService<TEntity, TMap, T, TFilter> : IAnnotationService<TEntity, TMap, T, TFilter>
        where TEntity : IRecord<T>
        where TMap : IRecordDto<T>
        where T : IEquatable<T>
        where TFilter : RecordFilter
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
        Task<OutputHandler> AddAsync(TMap row, string createdBy, int authorisers = 0, bool captureTrail = true, string authoriser = "");
        /// <summary>
        /// persists some information to the database
        /// </summary>
        /// <param name="row">the information to be persisted</param>
        /// <param name="createdBy">the user who will effect the persistence</param>
        /// <returns>the persisted record</returns>
        Task<OutputHandler> AddInScopeAsync(TMap row, string createdBy, int authorisers = 0, bool captureTrail = true, string authoriser = "");
        /// <summary>
        /// Deletes a record that matches a given key
        /// </summary>
        /// <param name="id">the key by which to delete</param>
        /// <param name="deletedBy">the user who will effect the deletion</param>
        /// <returns>the deleted record</returns>
        Task<OutputHandler> DeleteAsync(T id, string deletedBy, int authorisers = 0, bool captureTrail = true, string authoriser = "");
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
        Task<OutputHandler> UpdateAsync(TMap row, string username, int authorisers, bool captureAuditTrail);
        /// <summary>
        /// updates a given database record with given information
        /// </summary>
        /// <param name="row"> the information to update the database with</param>
        /// <param name="updatedBy">the user who will effect the update</param>
        /// <returns>the updated record</returns>
        Task<OutputHandler> UpdateInScopeAsync(TMap row, string username, int authorisers, bool captureAuditTrail);

        /// <summary>
        /// updates a given database record with given information
        /// </summary>
        /// <param name="row"> the information to update the database with</param>
        /// <param name="updatedBy">the user who will effect the update</param>
        /// <returns>the updated record</returns>
        Task<OutputHandler> UpdateInScopeAsync(List<TMap> row, string username, int authorisers, bool captureAuditTrail);
        /// <summary>
        /// persists some information to the database
        /// </summary>
        /// <param name="row">the information to be persisted</param>
        /// <param name="createdBy">the user who will effect the persistence</param>
        /// <returns>the persisted record</returns>
        Task<OutputHandler> AddInScopeAsync(List<TMap> row, string createdBy, int authorisers = 0, bool captureTrail = true, string authoriser = "");
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
        Task<OutputHandler> ApproveAsync(T id, string approvedBy, int authorisers = 0, bool captureTrail = true);
        Task<OutputHandler> ApproveInScopeAsync(T id, string approvedBy, int authorisers = 0, bool captureTrail = true);
        Task<OutputHandler> ApproveInScopeAsync(List<T> id, string approvedBy, int authorisers = 0, bool captureTrail = true);
        Task<OutputHandler> RejectAsync(T id, string approvedBy, int authorisers = 0, bool captureTrail = true);
        Task<OutputHandler> RejectInScopeAsync(T id, string approvedBy, int authorisers = 0, bool captureTrail = true);
        Task<OutputHandler> RejectInScopeAsync(List<T> id, string approvedBy, int authorisers = 0, bool captureTrail = true);
        public SecurityDetail SecurityDetail { get; set; }

    }

}
