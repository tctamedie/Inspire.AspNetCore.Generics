using Inspire.Annotator.Annotations;

namespace Inspire.Modeller
{
    public class StandardStatusFilter : RecordStatusFilter
    {
        [TableFilter(Order: 3)]
        public virtual string Name { get; set; }

    }
}
