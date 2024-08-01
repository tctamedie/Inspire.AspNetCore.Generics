using Inspire.Annotations;

namespace Inspire.Services
{
    public class AnnotationService<TEntity, TMap, T, TFilter> : ViewAnnotationService, IAnnotationService<TEntity, TMap, T, TFilter>
    where T : IEquatable<T>
    where TEntity : IRecord<T>, new()
    where TMap : IRecordDto<T>
        where TFilter : FilterModel
    {

        public virtual TableModel GetTableModel(string foreignKey = "")
        {
            return GetTableModel<TEntity, TFilter, T>(foreignKey);
        }
        public TableModel GetTableModel<TModel, Key>(string foreignKey = "")
            where TModel : IRecord<Key>,new()
            where Key : IEquatable<Key>
        {
            return GetTableModel<TModel, TFilter, Key>(foreignKey);
        }
        public virtual FormModel GetFormModel(string foreignKey = "")
        {
            return GetFormModel<TEntity, TMap, T>(foreignKey);
        }
        public virtual List<TableFilterModel> GetTableFilters(string foreignKey = "")
        {
            var config = GetClassAttributes<EntityConfiguration, TEntity>(true).FirstOrDefault();
            return GetFilterModels<TFilter>(config, foreignKey);
        }

    }
}
