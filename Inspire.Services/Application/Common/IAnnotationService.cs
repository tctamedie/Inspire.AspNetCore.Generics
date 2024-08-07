using Inspire.Modeller;

namespace Inspire.Services.Application.Common
{
    public interface IAnnotationService<TEntity, TMap, T, TFilter> : IViewAnnotationService
        where T : IEquatable<T>
        where TEntity : IRecord<T>
        where TMap : IRecordDto<T>
        where TFilter : FilterModel
    {
        /// <summary>
        /// Retrievies and transforms Entity Configuration attribute on database entity to come up with a table model
        /// </summary>       
        /// <param name="foreignKey">the optional foreign key column in database entity</param>
        /// <returns>the table model from which to build the presentation html table and filters</returns>
        TableModel GetTableModel(string foreignKey = "");
        /// <summary>
        /// Retrievies and transforms Entity Configuration attribute on database entity to come up with a table model
        /// </summary>       
        /// <param name="foreignKey">the optional foreign key column in database entity</param>
        /// <returns>the table model from which to build the presentation html table and filters</returns>
        TableModel GetTableModel<TModel, Key>(string foreignKey = "")
            where TModel : IRecord<Key>, new()
            where Key : IEquatable<Key>;
        /// <summary>
        /// Retrievies and transforms Form Configuration attribute on database entity dto to come up with a user interface
        /// </summary>        
        /// <param name="foreignKey">the optional foreign key column in database entity</param>
        /// <returns>the form model from which to build the user interface</returns>
        FormModel GetFormModel(string foreignKey = "");
        /// <summary>
        /// Gets the model that builds the Filter Form
        /// </summary>
        /// <param name="foreignKey">the foreign key being transmitted</param>
        /// <returns>the form</returns>
        List<TableFilterModel> GetTableFilters(string foreignKey = "");
    }
}
