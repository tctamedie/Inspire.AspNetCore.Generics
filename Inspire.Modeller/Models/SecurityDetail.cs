namespace Inspire.Modeller
{
    public class SecurityDetail
    {
        public bool IsMaker { get; set; }
        public bool IsViewer { get; set; }
        public bool IsAuthoriser { get; set; }
        public bool Creatable { get; set; }
        public bool Editable { get; set; }
        public bool Deletable { get; set; }
        public bool Approvable { get; set; }
        public bool Viewable { get; set; }
        public int Authorisers { get; set; }
        public bool TrailEnabled { get; set; }
    }
}
