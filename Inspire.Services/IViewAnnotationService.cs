using Inspire.Annotations;
using SpreadsheetLight;

namespace Inspire.Services
{
    public interface IViewAnnotationService
    {
        /// <summary>
        /// Retrievies and transforms Entity Configuration attribute on database entity to come up with a table model
        /// </summary>
        /// <typeparam name="TEntity">The database entity from which the entity configurations are retrieved</typeparam>
        /// <typeparam name="TFilter">The filter which will provide filter criterion</typeparam>
        /// <typeparam name="T">the datatype of the key of the entity</typeparam>
        /// <param name="foreignKey">the optional foreign key column in database entity</param>
        /// <returns>the table model from which to build the presentation html table and filters</returns>
        TableModel GetTableModel<TEntity, TFilter, T>(string foreignKey = "")
            where T : IEquatable<T>
        where TEntity : IRecord<T>, new()
        where TFilter : FilterModel;
        /// <summary>
        /// Retrievies and transforms Form Configuration attribute on database entity dto to come up with a user interface
        /// </summary>
        /// <typeparam name="TMap">The database entity dto</typeparam>
        /// <typeparam name="T"> the database entity primary key data type</typeparam>
        /// <param name="foreignKey">the optional foreign key column in database entity</param>
        /// <returns>the form model from which to build the user interface</returns>
        FormModel GetFormModel<TEntity, TMap, T>(string foreignKey = "")
            where T : IEquatable<T>
        where TMap : IRecordDto<T>
            where TEntity : IRecord<T>;

        EntityConfiguration GetTableConfiguration<TEntity>();
        List<CellValidation> ValidateExcel(int row, SLDocument doc);
        string Extract(int row, SLDocument doc, string data);
        (SLTable Table, int Count) Generate(SLDocument doc, int records);
        int InsertValidation(SLDocument doc);
        List<string> GetSheetNames(FormModel model);
        Task<OutputHandler> AddInScopeAsync(string rows, string createBy, int authorisers = 0, bool captureTrail = true);
    }
}
