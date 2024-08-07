namespace Inspire.Security.Models;
public class ReportParamater
{
    public string Name { get; set; }
    public FieldValueType ParameterType { get; set; }
    public int Order { get; set; }
}
public enum FieldValueType
{
    Int8sField = 0,
    Int8uField = 1,
    Int16sField = 2,
    Int16uField = 3,
    Int32sField = 4,
    Int32uField = 5,
    NumberField = 6,
    CurrencyField = 7,
    BooleanField = 8,
    DateField = 9,
    TimeField = 10,
    StringField = 11,
    TransientMemoField = 12,
    PersistentMemoField = 13,
    BlobField = 14,
    DateTimeField = 0xF,
    BitmapField = 20,
    IconField = 21,
    PictureField = 22,
    OleField = 23,
    ChartField = 24,
    SameAsInputField = 250,
    UnknownField = 0xFF
}
public class ReportParameterModel
{
    public string Key { get; set; }
    public string Value { get; set; }
}
public class ReportModel
{
    public int ReportId { get; set; }
    public string ReportFileName { get; set; }
    public string ReportName { get; set; }
    public string Format { get; set; }
    public ExportFormatType ExportFormat { get; set; }
    public DatabaseCredential DatabaseCredential { get; set; }
    public List<ReportParameterModel> Parameters { get; set; }
}
public class FreshReportModel : ReportModel
{
    public byte[] FileContent { get; set; }
    public string ContentType { get; set; }
}
public class ReportServer
{
    public string BaseUrl { get; set; }
    public DatabaseCredential DatabaseCredential { get; set; }
}
public class DatabaseCredential
{
    public string Server { get; set; }
    public string Database { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
public class ReportParameterBundle
{
    public ReportDto Report { get; set; }
    public Dictionary<string, object> Parameters { get; set; }
    public ExportFormatType ExportFormat { get; set; }
    public DatabaseCredential DatabaseCredential { get; set; }
}
public class ReportResult
{
    public string ContentType { get; set; }
    public byte[] FileContents { get; set; }
    public Exception Exception { get; set; }
    public bool ErrorOccured { get; set; }
    public string ErrorMessage { get; set; }
    public string Extension { get; set; }
}