﻿namespace Inspire.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class EntityConfiguration : ConfigurationBase
    {
        /// <summary>
        /// Sets the mapping between the Database model and User interface
        /// </summary>
        /// <param name="controller">The name of the MVC controller that will manage crud operations for the entity</param>
        /// <param name="area">the MVC area for </param>
        /// <param name="header">the page header for the user interface, if left blank the name of the controller will be used as a header</param>
        public EntityConfiguration(string controller, string area, string header = "", string modal = "", string foreignKey = "", string foreignKeyDesc = "") : base(controller, area, header, modal, foreignKey, foreignKeyDesc)
        {


        }

    }
}
