using System.ComponentModel.DataAnnotations;

namespace Inspire.Annotator.Annotations
{
    public class FieldLengthAttribute : StringLengthAttribute
    {
        public FieldLengthAttribute(int maximumLength, [CallerMemberName] string id = "") : base(maximumLength)
        {

            ID = id.FirstLetterToLower();
        }
        public string ID { get; }
    }
}