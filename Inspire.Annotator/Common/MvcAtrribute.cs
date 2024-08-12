
namespace Inspire.Annotator.Common
{
    public class MvcAtrribute(string Controller, string Area) : Attribute
    {
        public string Controller { get; } = Controller;
        public string Area { get; } = Area;
    }
}
