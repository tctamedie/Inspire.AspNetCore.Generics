namespace Inspire.Modeller
{
    public class StandardFilter : RecordFilter
    {
        [TableFilter(Order: 1)]
        public virtual string Name { get; set; }

    }
}
