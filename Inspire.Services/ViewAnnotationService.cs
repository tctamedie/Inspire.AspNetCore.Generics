
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
        where TEntity : Record<T>
        where TFilter : RecordFilter;
        /// <summary>
        /// Retrievies and transforms Form Configuration attribute on database entity dto to come up with a user interface
        /// </summary>
        /// <typeparam name="TMap">The database entity dto</typeparam>
        /// <typeparam name="T"> the database entity primary key data type</typeparam>
        /// <param name="foreignKey">the optional foreign key column in database entity</param>
        /// <returns>the form model from which to build the user interface</returns>
        FormModel GetFormModel<TMap, T>(string foreignKey = "")
            where T : IEquatable<T>
        where TMap : RecordDto<T>;
    }
    public class ViewAnnotationService : IViewAnnotationService
    {
        public List<TAttribute> GetClassAttributes<TAttribute, TClass>(bool specific = false) where TAttribute : Attribute where TClass : class
        {
            string name = typeof(TClass).Name;
            var data = typeof(TClass).Assembly.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(TAttribute), true).Any())
                .Select(t => new { t.Name, Attribute = t.GetCustomAttributes(typeof(TAttribute), true) })
                .ToList();
            if (specific)
                data = data.Where(s => s.Name == name).ToList();
            List<TAttribute> records = new List<TAttribute>();
            if (data.Count > 0)
            {
                foreach (var attr in data)
                {
                    foreach (var record in attr.Attribute)
                    {
                        var row = (TAttribute)record;
                        if (row != null)
                            records.Add(row);
                    }

                }
            }

            return records;
        }
        List<BreadCrumb> GetBreadCrumbs<TEntity, T>(EntityConfiguration config, string foreignKey)
            where T: IEquatable<T>
            where TEntity: Record<T>
        {
            var header = GetPrependedHeader(foreignKey);
            var data = GetClassAttributes<BreadCrumbAttribute, TEntity>(true);
            List<BreadCrumb> breadcrumbs = new();
            foreach(var row in data)
            {
                header += " " + row.Header;
                var record = new BreadCrumb(row.Order, row.Controller, row.Area, row.ForeignKey, row.Action, header.Trim());
                if (config.ForeignKey.ToLower() == row.ForeignKey.ToLower()&&!string.IsNullOrEmpty(foreignKey))
                {
                    record.RecordID = foreignKey;
                }
                breadcrumbs.Add(record);
            }

            return breadcrumbs.OrderBy(s=>s.Order).ToList();
        }
        public TableModel GetTableModel<TEntity,TFilter, T>(string foreignKey = "")
        where T : IEquatable<T>
        where TEntity : Record<T>
            where TFilter: RecordFilter

        {
            var config = GetClassAttributes<EntityConfiguration, TEntity>(true).FirstOrDefault();
            
            var filters = GetFilterModels<TFilter>(config, foreignKey);
            var columns = GetColumnModels<TEntity, T>().OrderBy(s=>s.Order).ToList();
            var keyField = columns.Where(s => s.IsKey).Select(s => s.Id).FirstOrDefault();
            return new TableModel
            {
                ForeignKey = config.ForeignKey,
                ForeignKeyDesc = config.ForeignKeyDesc,
                KeyField = keyField,
                Area = config.Area,                
                Controller = config.Controller,
                Header = config.Header,
                Modal = config.Modal,
                Columns = columns,
                Filters = filters,
                BreadCrumbs = GetBreadCrumbs<TEntity, T>(config, foreignKey),
                NavigationLinks = GetLinkModels<TEntity, T>(config),
                Buttons = GetButtonModels<TEntity, T>()
            };
        }
        public List<Link> GetLinkModels<TEntity, T>(EntityConfiguration configuration)
            where TEntity : Record<T>
            where T : IEquatable<T>
        {
            List<Link> models = new List<Link>();
            var properties = typeof(TEntity).GetProperties().Where(t => t.GetCustomAttributes(typeof(LinkAttribute), true).Any()).Select(s => new { Name = s.Name, DataType = s.PropertyType, Attribute = ((LinkAttribute)s.GetCustomAttributes(typeof(LinkAttribute), true).First()) }).ToList();
            foreach (var record in properties)
            {
                var atrribute = record.Attribute;
                var area = string.IsNullOrEmpty(atrribute.Area) ? configuration.Area : atrribute.Area;
                var model = new Link(atrribute.Controller, atrribute.LinkButtonTitle, atrribute.LinkButtonTitle, atrribute.LinkButtonIcon, atrribute.LinkButtonClass, atrribute.Action, atrribute.ID,area)
                ;
                model.Source = configuration.Controller;
                models.Add(model);
            }
            return models;
        }
        public List<ButtonModel> GetButtonModels<TEntity, T>()
            where TEntity : Record<T>
            where T : IEquatable<T>
        {
            List<ButtonModel> models = new List<ButtonModel>();
            var data = GetClassAttributes<ButtonAttribute, TEntity>(true);
            foreach (var atrribute in data)
            {
                var model = new ButtonModel(atrribute.Icon, atrribute.Action, atrribute.Title, atrribute.Text, atrribute.Class, atrribute.ButtonType);
                models.Add(model);
            }
            return models.OrderBy(s=>s.ButtonType).ToList();
        }

        public List<ColumnModel> GetColumnModels<TEntity, T>()
            where TEntity : Record<T>
            where T : IEquatable<T>
        {
            List<ColumnModel> models = new List<ColumnModel>();
            var properties = typeof(TEntity).GetProperties().Where(t => t.GetCustomAttributes(typeof(ColumnAttribute), true).Any()).Select(s => new { Name = s.Name, DataType = s.PropertyType, Attribute = ((ColumnAttribute)s.GetCustomAttributes(typeof(ColumnAttribute), true).First()) }).ToList();
            foreach (var record in properties)
            {
                string dataType = "string";
                string alignment = "left";
                var atrribute = record.Attribute;
                if (record.DataType == typeof(decimal) || record.DataType == typeof(decimal?))
                {
                    dataType = "number";
                    alignment = "right";
                }
                else if (record.DataType == typeof(int) || record.DataType == typeof(int?))
                {
                    dataType = "int";
                    alignment = "right";
                }
                else if (record.DataType == typeof(DateTime) || record.DataType == typeof(DateTime?))
                {
                    dataType = "date";
                    alignment = "center";
                }
                var model = new ColumnModel(atrribute.Order, atrribute.Width, atrribute.Id, atrribute.IsKey, atrribute.DisplayName, atrribute.EntityId)
                {
                    DataType = dataType,
                    Alignment = alignment
                };
                models.Add(model);
            }
            return models.OrderBy(s=>s.Order).ToList();
        }
        public List<TableFilterModel> GetFilterModels<TFilter>(EntityConfiguration configuration, string foreignKey)            
        where TFilter : RecordFilter
        {
            List<TableFilterModel> models = new List<TableFilterModel>();
            var properties = typeof(TFilter).GetProperties();
            foreach (var property in properties)
            {
                var attr = (TableFilterAttribute)property.GetCustomAttributes(typeof(TableFilterAttribute), true).FirstOrDefault();
                var lsAttr = (ListAttribute)property.GetCustomAttributes(typeof(ListAttribute), true).FirstOrDefault();
                string dataType = "string";
                var record = property.PropertyType;
                if (record == typeof(decimal) || record == typeof(decimal?))
                {
                    dataType = "number";
                }
                else if (record == typeof(int) || record == typeof(int?))
                    dataType = "int";
                else if (record == typeof(DateTime) || record == typeof(DateTime?))
                    dataType = "date";
                var defaultValue = attr.DefaultValue;
                if (attr.Id.ToLower() == configuration.ForeignKey&&!string.IsNullOrEmpty(foreignKey))
                {
                    defaultValue = foreignKey;
                }
                var model = new TableFilterModel(attr.Row, attr.Order, attr.Width, attr.Id, attr.DisplayName, defaultValue, attr.ControlType, attr.OnChangeAction, attr.EntityId);
                model.DataType = dataType;
                
                if (lsAttr != null)
                {
                    string controller = lsAttr.Controller;
                    if (string.IsNullOrEmpty(lsAttr.Controller))
                        controller = configuration.Controller;
                    string area = lsAttr.Area;
                    if (string.IsNullOrEmpty(lsAttr.Area))
                        area = configuration.Area;
                    model.List = new ListModel(
                        controller,
                        lsAttr.Action,
                        lsAttr.ValueField,
                        lsAttr.TextField,
                        lsAttr.ID,
                        area,
                        lsAttr.MultipleSelect,
                        lsAttr.OnSelectedChange,
                        lsAttr.OnField,
                        lsAttr.FilterColumn,
                        lsAttr.FilterValue,
                        lsAttr.SortField);
                }
                models.Add(model);
            }
            return models;
        }
        public List<NavigationModel> GetNavigationModels<TMap, T>()
            where T : IEquatable<T>
        where TMap : RecordDto<T>
        {
            List<NavigationModel> models = new List<NavigationModel>();
            var properties = typeof(TMap).GetProperties();
            foreach (var property in properties)
            {
                var attr = (NavigationAttribute)property.GetCustomAttributes(typeof(NavigationAttribute), true).FirstOrDefault();
                if(attr!=null)
                    models.Add(new NavigationModel(attr.Id, attr.DisplayName, attr.Source));
               
            }
            return models;
        }
        public List<FieldModel> GetFieldModels<TMap, T>(FormConfiguration configuration)
            where T : IEquatable<T>
        where TMap : RecordDto<T>
        {
            List<FieldModel> models = new List<FieldModel>();
            var properties = typeof(TMap).GetProperties();
            foreach (var property in properties)
            {
                var attr = (FieldAttribute)property.GetCustomAttributes(typeof(FieldAttribute), true).FirstOrDefault();
                var lsAttr = (ListAttribute)property.GetCustomAttributes(typeof(ListAttribute), true).FirstOrDefault();
                var required = (RequiredFieldAttribute)property.GetCustomAttributes(typeof(RequiredFieldAttribute), true).FirstOrDefault();
                string dataType = "string";
                var record = property.PropertyType;
                if (record == typeof(decimal) || record == typeof(decimal?))
                {
                    dataType = "number";
                }
                else if (record == typeof(int) || record == typeof(int?))
                    dataType = "int";
                else if (record == typeof(DateTime) || record == typeof(DateTime?))
                    dataType = "date";
                else if (record == typeof(bool) || record == typeof(bool?))
                    dataType = "boolean";

                if (attr == null)
                    continue;
                var model = new FieldModel(attr.Row, attr.Order, attr.Width, attr.Id, attr.IsKey, attr.DisplayName, attr.Autogenerated, attr.Type, attr.TabId, attr.EntityId, required != null)
                {
                    DataType = dataType
                };
                //only if the control type in input type of text look for field length attribute
                if (attr.Type.ToString().ToLower() == "text")
                {
                    var length = (FieldLengthAttribute)property.GetCustomAttributes(typeof(FieldLengthAttribute), true).FirstOrDefault();
                    if (length != null)
                    {
                        model.MaximumLength = length.MaximumLength;
                        model.MinimumLength = length.MinimumLength;
                    }
                }
                
                if (lsAttr!=null)
                {
                    string controller = lsAttr.Controller;
                    if (string.IsNullOrEmpty(lsAttr.Controller))
                        controller = configuration.Controller;
                    string area = lsAttr.Area;
                    if (string.IsNullOrEmpty(lsAttr.Area))
                        area = configuration.Area;
                    model.List = new ListModel(
                        controller,
                        lsAttr.Action,
                        lsAttr.ValueField,
                        lsAttr.TextField,
                        lsAttr.ID,
                        area,
                        lsAttr.MultipleSelect,
                        lsAttr.OnSelectedChange,
                        lsAttr.OnField,
                        lsAttr.FilterColumn,
                        lsAttr.FilterValue,
                        lsAttr.SortField);
                    
                }
                models.Add(model);
            }
            return models;
        }
        public virtual string GetPrependedHeader(string foreignKey)
        {
            return foreignKey;
        }
        public FormModel GetFormModel<TMap, T>(string foreignKey = "")
        where T : IEquatable<T>
        where TMap : RecordDto<T>
        {
            var config = GetClassAttributes<FormConfiguration, TMap>(true).FirstOrDefault();
            var tabs = GetFormTabs<TMap, T>(config, foreignKey);
            var navigationModels = GetNavigationModels<TMap, T>();
            return new FormModel
            {
                NavigationModels = navigationModels,
                ForegnKey = config.ForeignKey,
                ForegnKeyDesc = config.ForeignKeyDesc,
                KeyField = tabs.KeyField,
                Area = config.Area,
                Controller = config.Controller,
                Header = config.Header,
                Modal = config.Modal,
                Tabs = tabs.Tabs
            };
        }
        public virtual (List<TabModel> Tabs, string KeyField) GetFormTabs<TMap, T>(FormConfiguration configuration, string foreignKey)
            where T : IEquatable<T>
            where TMap : RecordDto<T>
        {
            var data = GetClassAttributes<TabAttribute, TMap>(true);
            List<TabModel> models = new List<TabModel>();
            foreach (var row in data)
            {
                models.Add(new TabModel(row.Order, row.ID, row.Name, row.IsActiveTab, row.Field, row.IsHidden));
            }
            if (models.Count == 0)
            {
                models.Add(new TabModel(1, "", "NotApplicable", true, ""));
            }
            var fieldModels = GetFieldModels<TMap, T>(configuration);
            string keyField = fieldModels==null?"": fieldModels.Where(s => s.IsKey).Select(s => s.Id).FirstOrDefault();
            models.ForEach(tab =>
            {
                var fields = fieldModels==null?new List<FieldModel>(): fieldModels.Where(s => s.TabId == tab.ID).ToList();
                var rows = fields.Select(s => new { s.Row }).Distinct().OrderBy(s=>s.Row).ToList();
                foreach (var row in rows)
                {
                    var rowFields = fields.Where(s => s.Row == row.Row).OrderBy(s=>s.Order).ToList();
                    if (!string.IsNullOrEmpty(foreignKey)&&!string.IsNullOrEmpty(configuration.ForeignKey))
                    {
                        rowFields.ForEach(r =>
                        {
                            if (r.Id.ToLower() == configuration.ForeignKey.ToLower())
                                r.DefaultValue = foreignKey;
                        });
                    }
                    tab.Rows.Add(new TabRow
                    {
                        Order = row.Row,
                        Fields = rowFields
                    });
                }

            });
            return (models, keyField);
        }
    }
}
