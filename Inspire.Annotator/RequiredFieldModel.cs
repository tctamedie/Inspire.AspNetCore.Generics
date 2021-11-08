namespace Inspire.Annotator.Annotations
{
    public class RequiredFieldModel 
    {
        public RequiredFieldModel(string id = "", string ErrorMessage = "", bool allowEmptyStrings=false) 
        {
            AllowEmptyStrings = allowEmptyStrings;
            ID = id.FirstLetterToLower();
            Message = ErrorMessage;
        }
        public string ID { get; set; }
        public string Message { get; }
        public bool AllowEmptyStrings { get; }
    }
}