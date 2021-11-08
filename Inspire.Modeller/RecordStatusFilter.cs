namespace Inspire.Modeller
{
    using Inspire.Annotator;
    using Inspire.Annotator.Annotations;

    public abstract class RecordStatusFilter: RecordFilter
    {
        [TableFilter(Order: 2)]
        [List(Action:"GetRecordStatus")]
        public virtual string AuthStatus { get; set; }
    }

}
