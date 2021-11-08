namespace Inspire.Annotator
{
    using Annotations;
    public abstract class RecordFilter
    {
        [TableFilter(Order: 1)]
        public virtual string Search { get; set; }
    }

}
