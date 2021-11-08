
using System;

namespace Inspire.Annotator.Annotations
{
    public interface IAnnotationService<TEntity, TMap, T, TFilter> : IViewAnnotationService
        where T : IEquatable<T>
        where TEntity : Record<T>
        where TMap : RecordDto<T>
        where TFilter : RecordFilter
    {
        /// <summary>
        /// Retrievies and transforms Entity Configuration attribute on database entity to come up with a table model
        /// </summary>       
        /// <param name="foreignKey">the optional foreign key column in database entity</param>
        /// <returns>the table model from which to build the presentation html table and filters</returns>
        TableModel GetTableModel(string foreignKey = "");
        /// <summary>
        /// Retrievies and transforms Form Configuration attribute on database entity dto to come up with a user interface
        /// </summary>        
        /// <param name="foreignKey">the optional foreign key column in database entity</param>
        /// <returns>the form model from which to build the user interface</returns>
        FormModel GetFormModel(string foreignKey = "");
    }
    public class AnnotationService<TEntity, TMap, T, TFilter> : ViewAnnotationService, IAnnotationService<TEntity, TMap, T, TFilter>
    where T : IEquatable<T>
    where TEntity : Record<T>
    where TMap : RecordDto<T>
        where TFilter : RecordFilter
    {

        public TableModel GetTableModel(string foreignKey = "")
        {
            return GetTableModel<TEntity,TFilter, T>(foreignKey);
        }
        public FormModel GetFormModel(string foreignKey = "")
        {
            return GetFormModel<TMap, T>(foreignKey);
        }

    }
}
