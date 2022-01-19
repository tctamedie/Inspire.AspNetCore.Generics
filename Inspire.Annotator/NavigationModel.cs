public class NavigationModel
{
    public NavigationModel(string id, string name, string source)
    {
        Id = id;
        Name = name;
        Source = source;
    }
    public string Id { get; }
    public string Name { get; }
    public string Source { get; }
}