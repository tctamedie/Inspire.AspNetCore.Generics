public class NavigationAttribute : Attribute
{
    public NavigationAttribute(string displayName,[CallerMemberName] string FieldColumn = "", string source="")
    {
        Id = FieldColumn.FirstLetterToLower();
        DisplayName = displayName;
        Source = source;
    }
    public string Id { get; }
    public string DisplayName { get; }
    public string Source { get; }
}
