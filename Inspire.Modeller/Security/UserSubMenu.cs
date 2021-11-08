namespace Inspire.Modeller.Models.Security
{
    public class UserSubMenu
    {
        public string ID { get; set; }
        public string Description { get; set; }
        public int Authorizers { get; set; }
        public string Controller { get; set; }
        public string MainMenuID { get; set; }
        public int SortOrder { get; set; }
        public bool EnableAuditTrail { get; set; }
        public bool Visible { get; set; }
        public bool Creatable { get; set; }
        public bool Readable { get; set; }
        public bool Updatable { get; set; }
        public bool Deletable { get; set; }
        public bool Authorizable { get; set; }
        public bool RetrieveReports { get; set; }
    }
}
