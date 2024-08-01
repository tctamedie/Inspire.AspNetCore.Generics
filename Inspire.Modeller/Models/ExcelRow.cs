namespace Inspire.Modeller
{
    public class ExcelRow
    {
        public ExcelRow()
        {
            Columns = new List<DataValue>();
        }
        public int RowNumber { get; set; }
        public List<DataValue> Columns { get; set; }
    }
}
