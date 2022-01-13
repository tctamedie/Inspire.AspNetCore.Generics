namespace Inspire.Annotator.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class FormConfiguration : Attribute
    {
        /// <summary>
        /// Sets the mapping between the Database model dto and User interface
        /// </summary>
        /// <param name="controller">The name of the MVC controller that will manage crud operations for the entity</param>
        /// <param name="area">the MVC area for </param>
        /// <param name="header">the page header for the user interface, if left blank the name of the controller will be used as a header</param>
        /// <param name="modal">the modal header for the user interface, if left blank the name of the controller will be used as a header</param>
        /// <param name="foreignKey">This is the field that will be used for navigation especially when coming from another page. Leave it blank if this is ui is not a navigation UI</param>
        public FormConfiguration(string controller, string area, string header = "", string modal="", string foreignKey="")
        {
            Controller = controller;
            Area = area;
            ForeignKey = foreignKey;
            if (string.IsNullOrEmpty(header))
            {
                Header = controller.CamelSplit()+"s";
            }
            else
                Header = header;
            if (string.IsNullOrEmpty(modal))
            {
                Modal = controller.CamelSplit() + "s";
            }
            else
                Modal = modal;

        }
        public string Controller { get; }
        public string Area { get; }
        public string Header { get; }
        public string Modal { get; }
        public string ForeignKey { get; }
    }
}
