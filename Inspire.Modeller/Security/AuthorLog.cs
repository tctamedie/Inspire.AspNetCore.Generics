using System;

namespace Inspire.Modeller.Models.Security
{
    public class AuthorLog
    {
        public string Table { get; set; }
        public string Username { get; set; }
        public DateTime AuthorDate { get; set; }
        public string Value { get; set; }
        public int AuthorCount { get; set; }
        public string RecordKey { get; set; }
    }
}
