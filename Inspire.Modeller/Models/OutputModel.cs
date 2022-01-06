namespace Inspire.Modeller
{
    public class OutputModel
    {
        public OutputModel(bool errorOccured=false)
        {
            Error = errorOccured;
            Message = "";
            Data = null;
        }
        public bool Error { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
