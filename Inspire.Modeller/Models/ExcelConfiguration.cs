namespace Inspire.Modeller
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
}
