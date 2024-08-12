
namespace Inspire.Annotator.UI;
public class FormAttribute(string Controller, string Area, string Label) : MvcAtrribute(Controller, Area)
{
    public string Label { get; set; }
}
