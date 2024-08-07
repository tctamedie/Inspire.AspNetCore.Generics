namespace Inspire.Security.Models
{
    [EntityConfiguration("RecordActions", "Security")]
    public class RecordAction : Standard<string>
    {
        public string ButtonClass { get; set; }
        public string ButtonIcon { get; set; }
        public string ButtonAction { get; set; }
        public string ButtonTitle { get; set; }

    }
    [FormConfiguration("RecordActions", "Security")]
    public class RecordActionDto : StandardDto<string>
    {
        public string ButtonClass { get; set; }
        public string ButtonIcon { get; set; }
        public string ButtonAction { get; set; }
        public string ButtonTitle { get; set; }

    }
}
