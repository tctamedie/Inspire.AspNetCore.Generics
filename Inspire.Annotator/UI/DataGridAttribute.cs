

using Inspire.Annotator.Common;

namespace Inspire.Annotator.UI
{
    public class DataGridAttribute(string Controller, string Area, string Label) : MvcAtrribute(Controller, Area)
    {
        public string Label { get; set; }
    }
}
