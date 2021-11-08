using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Inspire.Annotator.Annotations
{
    public class RequiredFieldAttribute : RequiredAttribute
    {
        public RequiredFieldAttribute([CallerMemberName] string id = "", string ErrorMessage = "", bool allowEmptyStrings = false) : base()
        {
            AllowEmptyStrings = false;
            ID = id.FirstLetterToLower();
            Message = ErrorMessage;
        }
        public string ID { get; set; }
        public string Message { get; }
    }
}