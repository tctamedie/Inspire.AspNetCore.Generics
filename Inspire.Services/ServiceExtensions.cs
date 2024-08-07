using System.Data;
using System.Reflection;

namespace Inspire.Services
{
    public static class ServiceExtensions
    {
        public static DateTime LastDay(this DateTime date)
        {
            int ldate = DateTime.DaysInMonth(date.Year, date.Month);
            return new DateTime(date.Year, date.Month, ldate);
        }
        public static DateTime FirstDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }
        public static DataTable LINQToDataTable<T>(this IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others 
                //will follow 
                if (oProps == null)
                {
                    oProps = rec.GetType().GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if (colType.IsGenericType && colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>))
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
        public static int DaysDifference(this DateTime EndDate, DateTime StartDate)
        {
            DateTime _startDate = StartDate.AddDays(-1);
            return (EndDate - _startDate).Days;
        }
        public static int MonthDifference(this DateTime EndDate, DateTime StartDate)
        {
            return (EndDate - StartDate).Days / 30;
        }
        public static int YearDifference(this DateTime EndDate, DateTime StartDate)
        {
            return EndDate.Year - StartDate.Year;
        }
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        public static List<string> GetObjectProperties(this object source)
        {
            if (source == null) throw new ArgumentNullException();

            List<string> values = new List<string>();
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

            Dictionary<string, object> values = new Dictionary<string, object>();
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

            Dictionary<string, object> values = new Dictionary<string, object>();
            foreach (var key in keys)
            {
                source.TryGetValue(key, out var sourceVal);
                target.TryGetValue(key, out var targetVal);
                if (sourceVal != targetVal)
                {
                    values.Add(key, $"{sourceVal} => {targetVal}");
                }

            }
            return values;
        }
        public static string PropertiesToString(this object source)
        {
            if (source == null) throw new ArgumentNullException();

            List<string> values = new List<string>();
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
            return System.Text.RegularExpressions.Regex.Replace(
                input,
                $"(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])",
                " $1",
                System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
        }

    }
}
