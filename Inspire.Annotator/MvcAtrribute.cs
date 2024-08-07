using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Annotator
{
    public class MvcAtrribute: Attribute
    {
        public MvcAtrribute(string Controller, string Area)
        {
            this.Controller = Controller;
            this.Area = Area;
        }
        public string Controller { get; }
        public string Area { get; }
    }
}
