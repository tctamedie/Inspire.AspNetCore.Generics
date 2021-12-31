namespace Inspire.Modeller
{
    
    public abstract class RecordFilter
    {
        [TableFilter(Order: 1)]
        public virtual string Search { get; set; }
    }

}
