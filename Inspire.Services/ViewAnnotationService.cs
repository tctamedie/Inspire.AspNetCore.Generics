




using Inspire.Modeller;
using System.Data;
using Inspire.Services;

namespace Inspire.Services
{
    public enum ExcelDataType
    {
        Number = 1,
        Integer,
        Boolean,
        String,
        Date
    }
    public class ExcelConfiguration
    {
        public string Column { get; set; }
        public string Name { get; set; }
        public ExcelDataType DataType { get; set; }
        public bool IsRequired { get; set; }
        public int Order { get; set; }
    }
    public class ViewAnnotationService : IViewAnnotationService
    {
        public List<TAttribute> GetClassAttributes<TAttribute, TClass>(string area, string controller, bool specific = false) where TAttribute : Attribute
        {
            string name = typeof(TClass).Name;
            var data = typeof(TClass).Assembly.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(TAttribute), true).Any())
                .Select(t => new { t.Name, Attribute = t.GetCustomAttributes(typeof(TAttribute), true) })
                .ToList();
            if (specific)
                data = data.Where(s => s.Name == name).ToList();
            List<TAttribute> records = new();
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

        public virtual List<TAttribute> GetClassAttributes<TAttribute, TClass>(bool specific = false) where TAttribute : Attribute
        {
            string name = typeof(TClass).Name;
            var data = typeof(TClass).Assembly.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(TAttribute), true).Any())
                .Select(t => new { t.Name, Attribute = t.GetCustomAttributes(typeof(TAttribute), true) })
                .ToList();
            if (specific)
                data = data.Where(s => s.Name == name).ToList();
            List<TAttribute> records = new();
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
        public List<TAttribute> GetInterfaceAttributes<TAttribute, TClass>(bool specific = false, string area = "", string controller = "") where TAttribute : Attribute
        {
            string name = typeof(TClass).Name;
            var data = typeof(TClass).Assembly.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(TAttribute), true).Any() && t.GetInterfaces().Contains(typeof(TClass)))
                .Select(t => new { t.Name, Attribute = t.GetCustomAttributes(typeof(TAttribute), true), Config = t.GetCustomAttributes(typeof(FormConfiguration), true).FirstOrDefault() })
                .ToList();
            if (specific)
                data = data.Where(s => s.Name == name).ToList();
            List<TAttribute> records = new();
            if (data.Count > 0)
            {
                foreach (var attr in data)
                {
                    var config = (FormConfiguration)attr.Config;

                    foreach (var record in attr.Attribute)
                    {
                        var row = (TAttribute)record;
                        if (row != null)
                        {
                            if (!(string.IsNullOrEmpty(area) || string.IsNullOrEmpty(controller)))
                            {
                                if (config.Area == area && config.Controller == controller)
                                {
                                    records.Add(row);
                                }
                            }
                            else
                            {
                                records.Add(row);
                            }
                        }



                    }

                }
            }

            return records;
        }
        protected virtual void SetBreadcrumbHeaders(string key, List<BreadCrumb> data)
        {

        }
        public virtual List<BreadCrumb> GetBreadCrumbs<TEntity, T>(ConfigurationBase config, string foreignKey)
            where T : IEquatable<T>
        {
            var toolTip = GetPrependedHeader(foreignKey);
            var data = GetClassAttributes<BreadCrumbAttribute, TEntity>(true);
            List<BreadCrumb> breadcrumbs = new();
            foreach (var row in data)
            {
                var record = new BreadCrumb(row.Order, row.Controller, row.Area, row.ForeignKey, row.Action, row.Header.Trim(), toolTip);
                if (config.ForeignKey.ToLower() == row.ForeignKey.ToLower() && !string.IsNullOrEmpty(foreignKey))
                {
                    record.RecordID = foreignKey;
                }
                breadcrumbs.Add(record);
            }
            SetBreadcrumbHeaders(foreignKey, breadcrumbs);
            return breadcrumbs.OrderBy(s => s.Order).ToList();
        }
        public TableModel GetTableModel<TEntity, TFilter, T>(string foreignKey = "")
        where T : IEquatable<T>
        where TEntity : IRecord<T>, new()
            where TFilter : FilterModel


        {
            var config = GetClassAttributes<EntityConfiguration, TEntity>(true).FirstOrDefault();

            var filters = GetFilterModels<TFilter>(config, foreignKey);
            var columns = GetColumnModels<TEntity, T>().OrderBy(s => s.Order).ToList();
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
                Buttons = GetButtonModels<TEntity, T>(),
                ToggleButtons = GetToggleButtonModels<TEntity, T>(),
                SortButtons = GetSortButtonModels<TEntity, T>(),
            };
        }
        public List<Link> GetLinkModels<TEntity, T>(EntityConfiguration configuration)
            where TEntity : IRecord<T>
            where T : IEquatable<T>
        {
            List<Link> models = new();
            var properties = typeof(TEntity).GetProperties().Where(t => t.GetCustomAttributes(typeof(LinkAttribute), true).Any()).Select(s => new { Name = s.Name, DataType = s.PropertyType, Attribute = ((LinkAttribute)s.GetCustomAttributes(typeof(LinkAttribute), true).First()) }).ToList();
            foreach (var record in properties)
            {
                var attribute = record.Attribute;
                var area = string.IsNullOrEmpty(attribute.Area) ? configuration.Area : attribute.Area;
                var model = new Link(attribute.Controller, attribute.Order, attribute.LinkButtonTitle, attribute.LinkButtonTitle, attribute.LinkButtonIcon, attribute.LinkButtonClass, attribute.Action, attribute.ID, area, attribute.LinkType, attribute.ButtonType)
                ;
                model.Source = configuration.Controller;
                models.Add(model);
            }
            return models.OrderBy(s=>s.Order).ToList();
        }
        public List<ButtonModel> GetButtonModels<TEntity, T>()
            where TEntity : IRecord<T>
            where T : IEquatable<T>
        {
            List<ButtonModel> models = new();
            var data = GetClassAttributes<ButtonAttribute, TEntity>(true);
            foreach (var atrribute in data)
            {
                var model = new ButtonModel(atrribute.Icon, atrribute.Action, atrribute.Title, atrribute.Text, atrribute.Class, atrribute.ButtonType, atrribute.Order);
                models.Add(model);
            }
            return models.OrderBy(s => s.Order).ToList();
        }
        public List<ToggleButtonModel> GetToggleButtonModels<TEntity, T>()
            where TEntity : IRecord<T>
            where T : IEquatable<T>
        {
            List<ToggleButtonModel> models = new();
            var data = GetClassAttributes<ToggleButtonAttribute, TEntity>(true);
            foreach (var attr in data)
            {
                var model = new ToggleButtonModel(attr.CheckColumn, attr.TextWhenTrue, attr.TextWhenFalse, attr.IconWhenFalse, attr.IconWhenTrue, attr.Action, attr.Title, attr.Text, attr.Class);
                models.Add(model);
            }
            return models.OrderBy(s => s.ButtonType).ToList();
        }
        public List<SortButtonModel> GetSortButtonModels<TEntity, T>()
            where TEntity : IRecord<T>
            where T : IEquatable<T>
        {
            List<SortButtonModel> models = new();
            var data = GetClassAttributes<SortButtonAttribute, TEntity>(true);
            foreach (var attr in data)
            {
                var model = new SortButtonModel(attr.Order, attr.CheckColumn, attr.Icon, attr.Action, attr.Title, attr.Text, attr.Class);
                models.Add(model);
            }
            return models.OrderBy(s => s.Order).ToList();
        }

        public List<ColumnModel> GetColumnModels<TEntity, T>()
            where TEntity : IRecord<T>
            where T : IEquatable<T>
        {
            List<ColumnModel> models = new();
            var properties = typeof(TEntity).GetProperties().Where(t => t.GetCustomAttributes(typeof(TableColumnAttribute), true).Any()).Select(s => new { Name = s.Name, DataType = s.PropertyType, Attribute = ((TableColumnAttribute)s.GetCustomAttributes(typeof(TableColumnAttribute), true).First()) }).ToList();
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
                var model = new ColumnModel(atrribute.Order, atrribute.Width, atrribute.Id, atrribute.IsKey, atrribute.DisplayName, atrribute.EntityId, atrribute.IsVisible, atrribute.DefaultValue)
                {
                    DataType = dataType,
                    Alignment = alignment
                };
                models.Add(model);
            }
            return models.OrderBy(s => s.Order).ToList();
        }
        public List<TableFilterModel> GetFilterModels<TFilter>(EntityConfiguration configuration, string foreignKey)
        where TFilter : FilterModel
        {
            List<TableFilterModel> models = new();
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
                object defaultValue = attr.DefaultValue;
                if (attr.Id.ToLower() == configuration.ForeignKey && !string.IsNullOrEmpty(foreignKey))
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
                        lsAttr.SortField,
                        lsAttr.QueryField);
                }
                models.Add(model);
            }
            return models;
        }
        public List<NavigationModel> GetNavigationModels<TMap, T>()
            where T : IEquatable<T>
        where TMap : IRecordDto<T>
        {
            List<NavigationModel> models = new();
            var properties = typeof(TMap).GetProperties();
            foreach (var property in properties)
            {
                var attr = (NavigationAttribute)property.GetCustomAttributes(typeof(NavigationAttribute), true).FirstOrDefault();
                if (attr != null)
                    models.Add(new NavigationModel(attr.Id, attr.DisplayName, attr.Source));

            }
            return models;
        }
        public virtual List<FieldModel> GetFieldModels<TMap, T>(FormConfiguration configuration)
            where T : IEquatable<T>
        where TMap : IRecordDto<T>
        {
            List<FieldModel> models = new();
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
                var model = new FieldModel(attr.Row, attr.Order, attr.Width, attr.Id, attr.IsKey, attr.DisplayName, attr.Autogenerated, attr.Type, attr.TabId, attr.EntityId, required != null, attr.DefaultValue,attr.Placeholder)
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
                        lsAttr.SortField,
                        lsAttr.QueryField);

                }
                models.Add(model);
            }
            return models;
        }
        public virtual string GetPrependedHeader(string foreignKey)
        {
            return foreignKey;
        }
        public virtual FormModel GetFormModel<TEntity, TMap, T>(string foreignKey = "")
        where T : IEquatable<T>
        where TMap : IRecordDto<T>
            where TEntity : IRecord<T>
        {
            var config = GetClassAttributes<FormConfiguration, TMap>(true).FirstOrDefault();
            var tabs = GetFormTabs<TMap, T>(config, foreignKey);
            var breadcrumbs = GetBreadCrumbs<TEntity, T>(config, foreignKey);
            var breadCrumb = new BreadCrumb(breadcrumbs.Count + 1, config.Controller, config.Area, config.ForeignKey, "Index", config.Header, "");
            breadCrumb.RecordID = foreignKey;
            breadcrumbs.Add(breadCrumb);
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
                Tabs = tabs.Tabs,
                BreadCrumbs = breadcrumbs
            };
        }

        public virtual FormModel GetExcelFormModel<TEntity, TMap, T>(string foreignKey = "")
        where T : IEquatable<T>
        where TMap : IRecordDto<T>
            where TEntity : IMakerChecker<T>
        {
            var config = GetClassAttributes<FormConfiguration, TMap>(true).FirstOrDefault();
            var tabs = GetFormTabs<TMap, T>(config, foreignKey);
            var breadcrumbs = GetBreadCrumbs<TEntity, T>(config, foreignKey);
            var breadCrumb = new BreadCrumb(breadcrumbs.Count + 1, config.Controller, config.Area, config.ForeignKey, "Index", config.Header, "");
            breadCrumb.RecordID = foreignKey;
            breadcrumbs.Add(breadCrumb);
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
                Tabs = tabs.Tabs,
                BreadCrumbs = breadcrumbs
            };
        }

        public virtual FormModel GetExcelUploadModel<TEntity, TMap, T>(string area, string controller, string foreignKey = "")
        where T : IEquatable<T>
        where TMap : IRecordDto<T>
            where TEntity : IMakerChecker<T>
        {
            var config = GetInterfaceAttributes<FormConfiguration, IExcelUpload>(true).Where(s => s.Area == area && s.Controller == controller).FirstOrDefault();
            var tabs = GetFormTabs<TMap, T>(config, foreignKey);
            var breadcrumbs = GetBreadCrumbs<TEntity, T>(config, foreignKey);
            var breadCrumb = new BreadCrumb(breadcrumbs.Count + 1, config.Controller, config.Area, config.ForeignKey, "Index", config.Header, "");
            breadCrumb.RecordID = foreignKey;
            breadcrumbs.Add(breadCrumb);
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
                Tabs = tabs.Tabs,
                BreadCrumbs = breadcrumbs
            };
        }

        public virtual (List<TabModel> Tabs, string KeyField) GetFormTabs<TMap, T>(FormConfiguration configuration, string foreignKey)
            where T : IEquatable<T>
            where TMap : IRecordDto<T>
        {
            var data = GetClassAttributes<TabAttribute, TMap>(true);
            List<TabModel> models = new();
            foreach (var row in data)
            {
                models.Add(new TabModel(row.Order, row.ID, row.Name, row.IsActiveTab, row.Field, row.IsHidden));
            }
            if (models.Count == 0)
            {
                models.Add(new TabModel(1, "", "NotApplicable", true, ""));
            }
            var fieldModels = GetFieldModels<TMap, T>(configuration);
            string keyField = fieldModels == null ? "" : fieldModels.Where(s => s.IsKey).Select(s => s.Id).FirstOrDefault();
            models.ForEach(tab =>
            {
                var fields = fieldModels == null ? new List<FieldModel>() : fieldModels.Where(s => s.TabId == tab.ID).ToList();
                var rows = fields.Select(s => new { s.Row }).Distinct().OrderBy(s => s.Row).ToList();
                foreach (var row in rows)
                {
                    var rowFields = fields.Where(s => s.Row == row.Row).OrderBy(s => s.Order).ToList();
                    if (!string.IsNullOrEmpty(foreignKey) && !string.IsNullOrEmpty(configuration.ForeignKey))
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

        public EntityConfiguration GetTableConfiguration<TEntity>()
        {
            return GetClassAttributes<EntityConfiguration, TEntity>(true).FirstOrDefault();
        }
        public List<FormConfiguration> GetUploadConfiguration()
        {
            return GetInterfaceAttributes<FormConfiguration, IExcelUpload>();
        }
        public virtual List<CellValidation> ValidateExcel(int row, SLDocument doc)
        {
            var (configurations, headerRow) = GetConfiguration();
            List<CellValidation> data = new();
            int count = 1;
            foreach (var configuration in configurations.OrderBy(s => s.Order))
            {
                var val = doc.GetCellValueAsString(configuration.Column + row);
                if (string.IsNullOrEmpty(val) && configuration.IsRequired)
                {
                    data.Add(new CellValidation { Message = $"{configuration.Name} cannot be blank", Correction = $"Please enter {configuration.Name}", Column = count, Row = row });
                }
                count++;
            }
            return data;
        }
        public virtual int InsertValidation(SLDocument doc)
        {
            return 0;
        }

        public virtual string Extract(int row, SLDocument doc, string data)
        {
            throw new NotImplementedException();
        }

        public virtual Task<OutputHandler> AddInScopeAsync(string rows, string createBy, int authorisers = 0, bool captureTrail = true)
        {
            return Task.FromResult(new OutputHandler());
        }

        public virtual (SLTable Table, int Count) Generate(SLDocument doc, int records)
        {
            var (config, headerRow) = GetConfiguration();
            var configurations = config.OrderBy(s => s.Order).ToList();
            foreach (var row in configurations)
            {
                doc.SetCellValue($"{row.Column}{headerRow}", row.Name);
            }
            var firstColumn = configurations.FirstOrDefault();
            var lastColumn = configurations.LastOrDefault();
            SLTable table = doc.CreateTable($"{firstColumn.Column}){headerRow}", $"{lastColumn.Column}{records + 1}");
            return (table, records);
        }
        protected virtual void ValidateColumn<T>(List<GenericData<T>> data, SLDocument doc, string entryWorksheet, string validationSheet, string validationColumn, string title, string errorMessage, bool inCellDropDown = false) where T : IEquatable<T>
        {
            doc.AddWorksheet(validationSheet);
            SLDataValidation validation = doc.CreateDataValidation($"{validationColumn}2", $"{validationColumn}{data.Count - 1}");

            doc.SelectWorksheet(validationSheet);
            DataTable validationTable = data.LINQToDataTable();
            doc.ImportDataTable($"A1", validationTable, true);
            SLTable table = doc.CreateTable(1, 1, data.Count, 2);
            table.SetTableStyle(SLTableStyleTypeValues.Light20);
            doc.InsertTable(table);
            doc.AutoFitColumn("A", "B");
            doc.SelectWorksheet(entryWorksheet);
            validation.AllowList($"={validationSheet}!$A$2:$A${data.Count}", true, inCellDropDown);
            //validation.SetInputMessage(title, errorMessage);
            validation.SetErrorAlert(title.ToUpper(), errorMessage);
            //doc.AddDataValidation(validation);
        }
        public virtual (List<ExcelConfiguration> Config, int HeaderRow) GetConfiguration()
        {
            return (new List<ExcelConfiguration>(), 1);
        }
        public virtual List<string> GetSheetNames(FormModel model)
        {
            List<string> sheetNames = new()
            {
                model.Modal
            };
            return sheetNames;
        }
    }
}
