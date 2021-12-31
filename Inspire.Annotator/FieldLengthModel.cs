namespace Inspire.Annotator.Annotations
{
    public class FieldLengthModel
    {
        public FieldLengthModel(int maximumLength, string id, int? minimumLength)
        {

            ID = id.FirstLetterToLower();
            MaximumLength = maximumLength;
            MinimumLength = minimumLength;
        }
        public string ID { get; set; }
        public int? MinimumLength { get; }
        public int MaximumLength { get; }
    }
}