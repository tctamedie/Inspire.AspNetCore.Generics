using System;

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
        public FormConfiguration(string controller, string area, string header = "")
        {
            Controller = controller;
            Area = area;
            if (string.IsNullOrEmpty(header))
            {
                Header = controller.CamelSplit();
            }
            else
                Header = header;
           
        }
        public string Controller { get; }
        public string Area { get; }
        public string Header { get; }
    }
}
