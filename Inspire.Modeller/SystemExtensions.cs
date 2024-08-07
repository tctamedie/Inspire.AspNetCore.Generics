using DocumentFormat.OpenXml.Spreadsheet;
using Inspire.Modeller;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Inspire
{
    public static class UIHelpers
    {
        public const string READ_GRID_ERROR_MESSAGE = "An Error Occured While Retrieving Data. Try Again";
        public const string READ_RECORD_ERROR_MESSAGE = "An Error Occured While Retrieving record. Try Again";
        public const string VALIDATION_GENERAL_MESSAGE = "An Error occured while validating the record, Please try again";
        public const string ALERT_ERROR_CSS_CLASS = "alert alert-danger";
        public const string ALERT_WARNING_CSS_CLASS = "alert alert-warning";
        public const string ALERT_SUCCESS_CSS_CLASS = "alert alert-success";
        public const string FA_ERROR_ICON = "fa fa-warning";
        public const string FA_CRUD_SAVE_ICON = "fa fa-floppy-o";
        public const string FA_CRUD_DELETE_ICON = "fa fa-trash-o";
        public const string FA_CRUD_AUTHORISE_ICON = "fa fa-check";
        public const string FA_SUCCESS_ICON = "fa fa-check";
        public const string ADD_SUCCESS_MESSAGE = "Succesfully Added Record";
        public const string ADD_ERROR_MESSAGE = "An error occured while saving record";
        public const string UPDATE_SUCCESS_MESSAGE = "Succesffuly Updated Record";
        public const string DELETE_SUCCESS_MESSAGE = "Succesffuly Deleted Record";
        public const string DELETE_ERROR_MESSAGE = "An Error Occured while Deleting Record";
        public const string UPDATE_ERROR_MESSAGE = "An error occured while updating record";
        public const string AUTHORISE_SUCCESS_MESSAGE = "Succesffuly Approved Record";
        public const string AUTHORISE_ERROR_MESSAGE = "An error occured while updating record";
        public const string VALIDATION_ALREADY_AUTH_MESSAGE = "The record has already been authorised.";
        public const string VALIDATION_AUTH_NO_RECORD_MESSAGE = "No Records found for authorisation";
        public const string VALIDATION_EMPTY_FIELDS_MESSAGE = "Some Mandatory Fields are Empty.";
        //public static void LoadGridView(this GridView grid, IEnumerable source)
        //{
        //    try
        //    {
        //        grid.DataSource = source;
        //        grid.DataBind();
        //    }
        //    catch (Exception e)
        //    {

        //        e.Catch("UIHelpers", "LoadGridView");

        //    }
        //}
        public static bool IsNotEmpty(params dynamic[] control)
        {

            foreach (var item in control)
            {
                if (string.IsNullOrEmpty(item.Text))
                {
                    return false;
                }
            }
            return true;
        }

        //public static void HandleGridButtons(int initGridColWidth, int columnIndex, GridView grdData, object sender, GridViewRowEventArgs e, IEnumerable<ButtonRightViewModel> buttonRights, int buttonWith)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //hover row formatting
        //        e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';this.style.backgroundColor='#FEF4BB';");
        //        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");

        //        foreach(var button in buttonRights)
        //        {
        //            LinkButton grdButton = (LinkButton)e.Row.FindControl(button.ButtonName);
        //            if(grdButton!=null)
        //            SetVisible(false, grdButton);
        //            if (!string.IsNullOrEmpty(button.AccessCodeProvided))
        //            {
        //                if (button.DifferentPageAccess == true)
        //                {
        //                    if (button.RequiredAccessCode.Contains(button.AccessCodeProvided))
        //                    {
        //                        SetVisible(true, grdButton);
        //                        initGridColWidth += buttonWith;
        //                    }
        //                }
        //                else
        //                {
        //                    if (button.AccessCodeProvided.Contains(button.RequiredAccessCode))
        //                    {
        //                        SetVisible(true, grdButton);
        //                        initGridColWidth += buttonWith;
        //                    }
        //                }
        //            }
        //        }

        //        grdData.Columns//[ColumnIndex].ItemStyle.Width = initGridColWidth;
        //    }
        //}
        //public static void ClearText(string text,params dynamic[] control)
        //{
        //    foreach(var item in control)
        //    {
        //        if (item is TextBox|| item is Label)
        //            item.Text = text;
        //        else if( item is HtmlGenericControl)
        //            item.InnerText = text;


        //    }
        //}
        //public static void ClearText( params dynamic[] control)
        //{
        //    foreach (var item in control)
        //    {

        //        if (item is TextBox)
        //            item.Text = string.Empty;
        //        else
        //        {
        //            try
        //            {
        //                item.InnerText = string.Empty;

        //            }catch
        //            {

        //            }
        //        }

        //    }
        //}
        public static string CreateCRUDText(string text, string cssIcon)
        {
            return string.Format("<i class='{0}'></i>&nbsp;&nbsp; {1}", cssIcon, text);
        }

        public static string GenerateUIMessage(string message, string cssClass = ALERT_ERROR_CSS_CLASS, string cssIcon = FA_ERROR_ICON, bool enableCloseButton = true)
        {
            return string.Format("<div class='{0} fade in' role='alert'>{3}<i class='{1}'></i>&nbsp {2}</div>", cssClass, cssIcon, message, enableCloseButton ? "<a href = '#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>" : "");

        }
        //public static void LoadDropdownList(DropDownList cbo, string text, string value,IEnumerable source)
        //{
        //    try
        //    {
        //        cbo.DataTextField = text;
        //        cbo.DataValueField = value;
        //        cbo.DataSource = source;
        //        cbo.DataBind();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //public static void SetReadOnly(bool status, params TextBox[] control)
        //{
        //    try
        //    {
        //        foreach(var item in control)
        //        {
        //            item.ReadOnly = status;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //public static void SetEnable(bool status, params dynamic[] control)
        //{
        //    try
        //    {
        //        foreach (var item in control)
        //        {
        //            item.Enabled = status;
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        e.Catch("UIHelpers", "SetEnable");
        //    }
        //}
        public static void SetVisible(bool status, params dynamic[] control)
        {
            try
            {
                foreach (var item in control)
                {
                    item.Visible = status;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        //public static void HandleGridButtons(GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //hover row formatting
        //        e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';this.style.backgroundColor='#FEF4BB';");
        //        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
        //    }
        //}
        //public static void SetChecked(bool status, params CheckBox[] control)
        //{
        //    try
        //    {
        //        foreach (var item in control)
        //        {
        //            item.Checked = status;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public static OutputHandler ValidateRowData(string userID, string actionStatus, string AuthStatus, string capturedBy, int AuthCount)
        {
            OutputHandler output = new();
            if (actionStatus == "DELETE")
            {
                if (AuthStatus == "A" && AuthCount > 0)
                {
                    output = "You cannot delete an authorised record".Warn();
                }
                else if (AuthStatus == "U" && AuthCount > 0)
                {
                    output = "You cannot delete a record that was once authorised".Warn();
                }
                else if (userID != capturedBy)
                {
                    output = "You can only delete own transaction. This record was created by someone else".Warn();
                }
            }
            else if (actionStatus == "AUTHORISE")
            {
                if (AuthStatus == "A")
                {
                    output = VALIDATION_ALREADY_AUTH_MESSAGE.Warn();
                }
                else if (userID == capturedBy)
                {
                    output = "You cannot authorise own transaction".Warn();
                }
            }
            return output;
        }
    }
    public static class SystemExtensions
    {
        private static Regex _unCamelRegex = new("([a-z])([A-Z])", RegexOptions.Compiled);
        public static OutputHandler Catch(this Exception ex, string source, string method, string message = "", string cssClass = "", string cssIcon = "", string userId = "")
        {
            var output = new OutputHandler();
            output = (message == "" ? "An error Occured while performing action" : message).Formator(true);

            string eXmessage = ex.Message;
            Exception ex2 = ex.InnerException;
            while (ex2 != null)
            {
                eXmessage += ex2.Message;
                ex2 = ex2.InnerException;
            }

            try
            {
                string user = userId;
            }
            catch (Exception)
            {


            }

            //Utils.InsertErrorLog(source, ex.Message, method, string.Format("{0:HH:mm:ss}", DateTime.Now), user, eXmessage);

            return output;
        }
        public static OutputHandler CatchDisplayError(this Exception ex, string path, string userId)
        {
            var output = new OutputHandler();
            output.ErrorOccured = true;
            output.Description = UIHelpers.GenerateUIMessage(UIHelpers.READ_RECORD_ERROR_MESSAGE, UIHelpers.ALERT_ERROR_CSS_CLASS, UIHelpers.FA_ERROR_ICON);
            output.ErrorOccured = true;
            string eXmessage = ex.Message;
            Exception ex2 = ex.InnerException;
            while (ex2 != null)
            {
                eXmessage = ex2.Message;
                ex2 = ex2.InnerException;
            }

            try
            {
                string user = userId;
            }
            catch (Exception)
            {


            }

            //Utils.InsertErrorLog(path, ex.Message, "DisplayRecord", string.Format("{0:HH:mm:ss}", DateTime.Now), user, eXmessage);

            return output;
        }
        public static DataTable LINQToDataTable<T>(this IEnumerable<T> varlist)
        {
            DataTable dtReturn = new();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others 
                //will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        public static (string Label, string Icon) CreateCRUDText(this string action, string text, string icon = null)
        {
            string label = text;

            if (text == null)
            {

                switch (action)
                {
                    case "ADD":
                    case "EDIT":
                        label = "Save";
                        if (icon == null)
                        {
                            icon = UIHelpers.FA_CRUD_SAVE_ICON;
                        }
                        break;
                    case "DELETE":
                        label = "Delete";
                        if (icon == null)
                        {
                            icon = UIHelpers.FA_CRUD_DELETE_ICON;
                        }
                        break;
                    case "AUTHORISE":
                        label = "Authorise";
                        if (icon == null)
                        {
                            icon = UIHelpers.FA_CRUD_AUTHORISE_ICON;
                        }
                        break;
                }
            }
            return (label, icon);
        }
        public static IEnumerable<TEntity> OrderBy<TEntity>(this IEnumerable<TEntity> source,
                                                    string orderByProperty, bool desc)
        {
            string command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command,
                                                   new[] { type, property.PropertyType },
                                                   source.AsQueryable().Expression,
                                                   Expression.Quote(orderByExpression));
            return source.AsQueryable().Provider.CreateQuery<TEntity>(resultExpression);
        }
        public static string UnCamelCase(this string str)
        {
            return _unCamelRegex.Replace(str, "$1 $2");
        }
        
        public static int IntValue(this string text)
        {
            int.TryParse(text, out int num);
            return num;
        }
        public static string SetDate(this string text, DateTime? date, string format = "dd-MMM-yyyy")
        {
            string result = "";
            if (date != null)
            {
                string formated = "{0:" + format + "}";
                result = string.Format(formated, date);
            }
            return result;

        }


        public static (string Label, bool IsVisible) SetAuditLabelValue(string UserID, DateTime? transactionDate)
        {
            string result = "";
            bool isVisible = true;

            if (transactionDate != null)
            {

                result = string.Format("{0} - {1:dd-MMM-yyyy HH:mm:ss}", UserID, transactionDate);
            }
            else
            {
                isVisible = false;
            }
            return (result, isVisible);

        }

        public static decimal DecimalValue(this string value)
        {
            decimal num;
            decimal.TryParse(value, out num);
            return num;
        }
        public static double DoubleValue(this string value)
        {
            double num;
            double.TryParse(value, out num);
            return num;
        }

        public static bool BooleanValue(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            if (value == "1" || value == "true")
                return true;
            if (value == "0" || value == "false")
                return false;
            return false;
        }


        public static DateTime Date(this string value)
        {
            DateTime date = DateTime.Now;
            bool isDate = DateTime.TryParse(value, out date);
            if (!isDate)
                date = new DateTime(1900, 1, 1);

            return date;

        }
        

        public static DateTime LastDay(this DateTime date, string frequency = "", int year = 0)
        {
            DateTime lastDay = DateTime.Now;
            int ldate = DateTime.DaysInMonth(date.Year, date.Month);           
            lastDay = new DateTime(date.Year, date.Month, ldate);            
            return lastDay;
        }

        public static DateTime LastDay(this DateTime date, string Frequency, DateTime? LastPayroll = null, string firstDayOfWeek = "SUN")
        {



            DateTime lastDay = DateTime.Now;
            switch (Frequency)
            {
                case "M":
                    lastDay = date.LastDay();
                    break;
                case "F":
                    int diff;
                    if (firstDayOfWeek == "SUN")
                        diff = DayOfWeek.Sunday - date.DayOfWeek;
                    else
                        diff = DayOfWeek.Monday - date.DayOfWeek;
                    bool fromJanuary = false;
                    if (LastPayroll == null)
                    {
                        LastPayroll = new DateTime(date.Year, 1, 1);
                        fromJanuary = true;
                    }
                    int days = date.DaysDifference((DateTime)LastPayroll);
                    if (fromJanuary == true)
                    {
                        int weeks = days / 7;
                    }


                    break;
            }
            int ldate = DateTime.DaysInMonth(date.Year, date.Month);
            lastDay = new DateTime(date.Year, date.Month, ldate);
            return lastDay;
        }

        //private static DateTime LastFrequency(DateTime Date, DateTime lastPayrollDate,int days)
        //{
        //    if (Date <= lastPayrollDate)
        //        return Date;
        //    else
        //    {

        //        return LastFrequency(Date.AddDays(days), lastPayrollDate);
        //    }
        //}
        public static int DaysDifference(this DateTime EndDate, DateTime StartDate)
        {
            DateTime _startDate = StartDate.AddDays(-1);
            return (EndDate - _startDate).Days;
        }
        public static int NumberOfMonths(this DateTime EndDate, DateTime StartDate)
        {
            DateTime _startDate = StartDate.LastDay();
            int months = 0;
            while (EndDate.LastDay() > _startDate)
            {
                months++;
                _startDate = _startDate.AddMonths(1).LastDay();
            }
            return months;
        }
        public static int YearDifference(this DateTime EndDate, DateTime StartDate)
        {
            return (EndDate.Year - StartDate.Year);
        }
        public static decimal Age(this DateTime EndDate, DateTime StartDate)
        {
            return (decimal)(EndDate - StartDate).TotalDays / 365;
        }
        public static DateTime FirstDay(this DateTime date, string frequency = "", int year = 0)
        {
            DateTime lastDay = new DateTime(date.Year, date.Month, 1);
            return lastDay;
        }

        //custom distinct by
        public static IEnumerable<TSource> DistinctByColumn<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }


        public static OutputHandler Formator(this string message, bool ErrorOccured = false, object data = null)
        {
            var output = new OutputHandler();
            string cssClass = UIHelpers.ALERT_SUCCESS_CSS_CLASS;
            string cssIcon = UIHelpers.FA_SUCCESS_ICON;
            if (ErrorOccured)
            {
                cssClass = UIHelpers.ALERT_ERROR_CSS_CLASS;
                cssIcon = UIHelpers.FA_ERROR_ICON;
            }
            if (data != null)
                output.Data = data;
            output.Description = UIHelpers.GenerateUIMessage(message, cssClass, cssIcon);
            output.ErrorOccured = ErrorOccured;
            return output;
        }
        public static OutputHandler Warn(this string message)
        {
            var output = new OutputHandler();
            string cssClass = UIHelpers.ALERT_WARNING_CSS_CLASS;
            string cssIcon = UIHelpers.FA_ERROR_ICON;

            output.Description = UIHelpers.GenerateUIMessage(message, cssClass, cssIcon);
            output.ErrorOccured = true;
            return output;
        }

        public static string StripHTML(this string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty).Replace("&times;", string.Empty).Replace("&nbsp", string.Empty);
        }

        public static void AssignPropertiesForm(this object target, object source)
        {
            if (target == null || source == null) throw new ArgumentNullException();
            var targetPropertiesDic = target.GetType().GetProperties().Where(p => p.CanWrite).ToDictionary(p => p.Name, StringComparer.CurrentCultureIgnoreCase);
            foreach (var sourceProp in source.GetType().GetProperties().Where(p => p.CanRead))
            {
                PropertyInfo targetProp;
                if (targetPropertiesDic.TryGetValue(sourceProp.Name, out targetProp))
                {
                    targetProp.SetValue(target, sourceProp.GetValue(source, null), null);
                }
            }

        }

        public static List<string> GetObjectProperties(this object source)
        {
            if (source == null) throw new ArgumentNullException();

            List<string> values = new();
            foreach (var sourceProp in source.GetType().GetProperties().Where(p => p.CanRead))
            {

                var name = sourceProp.Name;
                values.Add(name);
            }
            return values;
        }
        public static Dictionary<string, object> GetObjectValues(this object source)
        {
            if (source == null) throw new ArgumentNullException();

            Dictionary<string, object> values = new();
            foreach (var sourceProp in source.GetType().GetProperties().Where(p => p.CanRead))
            {
                var val = sourceProp.GetValue(source, null);
                var name = sourceProp.Name;
                values.Add(name, val);
            }
            return values;
        }
        public static Dictionary<string, object> GetChangedValues(this Dictionary<string, object> source, Dictionary<string, object> target, List<string> keys)
        {
            if (source == null) throw new ArgumentNullException();

            Dictionary<string, object> values = new();
            foreach (var key in keys)
            {
                
                source.TryGetValue(key, out var sourceVal);
                target.TryGetValue(key, out var targetVal);
                var st = sourceVal?.GetType();
                var sourceStr = sourceVal?.ToString();
                var targetStr = targetVal?.ToString();
                if (st == typeof(DateTime))
                {
                    sourceStr = $"{sourceVal:dd-MMM-yyyy Hms}";
                    targetStr = $"{targetVal:dd-MMM-yyyy Hms}";
                }
                if (st == typeof(decimal))
                {
                    sourceStr = $"{sourceVal:N2}";
                    targetStr = $"{targetVal:N2}";
                }
                
                    if (sourceStr!=targetStr)
                    {
                        values.Add(key, $"BEFORE: {targetStr}, AFTER: {sourceStr}");
                    }
                

            }
            return values;
        }
        public static string PropertiesToString(this object source)
        {
            if (source == null) throw new ArgumentNullException();

            List<string> values = new();
            foreach (var sourceProp in source.GetType().GetProperties().Where(p => p.CanRead))
            {
                var val = sourceProp.GetValue(source, null);
                var name = sourceProp.Name;
                values.Add($"{name}:{val}");
            }
            return string.Join('~', values);
        }
        public static string FirstLetterToLower(this string text)
        {
            //Check for empty string.
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            // Return char and concat substring.  
            var result = char.ToLower(text[0]) + text.Substring(1);
            return result;
        }
        public static string CamelSplit(this string input)
        {
            return Regex.Replace(
                input,
                $"(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])",
                " $1",
                RegexOptions.Compiled).Trim();
        }

    }
}
