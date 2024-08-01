public class ListAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Controller"></param>
    /// <param name="Action"></param>
    /// <param name="ValueField"></param>
    /// <param name="TextField"></param>
    /// <param name="ID"></param>
    /// <param name="Area"></param>
    /// <param name="MultipleSelect"></param>
    /// <param name="OnSelectChange">The method to be invoked when the selection is changed</param>
    /// <param name="OnField">the field to be affected when the selection changes</param>
    /// <param name="FilterColumn"></param>
    /// <param name="FilterValue"></param>
    /// <param name="SortField"></param>
    public ListAttribute(string Controller = "", string Action = "ReadData", string ValueField = "id", string TextField = "name", [CallerMemberName] string ID = "", string Area = "", bool MultipleSelect = false, string OnSelectChange = "", string OnField = "", string FilterColumn = "", string FilterValue = null, string SortField = "", string QueryField = "")
    {
        this.ID = ID.FirstLetterToLower();
        this.Controller = Controller;
        this.Action = Action;
        this.ValueField = ValueField.FirstLetterToLower();
        this.TextField = TextField.FirstLetterToLower();
        this.Area = Area;
        this.FilterColumn = FilterColumn.FirstLetterToLower();
        this.FilterValue = FilterValue;
        this.MultipleSelect = MultipleSelect;
        if (!string.IsNullOrEmpty(OnSelectChange))
        {
            this.OnSelectedChange = OnSelectChange.FirstLetterToLower();
            this.OnField = string.IsNullOrEmpty(OnField) ? this.ID : OnField.FirstLetterToLower();
        }
        if (string.IsNullOrEmpty(SortField))
        {
            this.SortField = this.TextField;
        }
        else
        {
            this.SortField = SortField.FirstLetterToLower();
        }
        this.QueryField = QueryField.FirstLetterToLower();


    }

    public string ID { get; }
    public string Controller { get; }
    public string Action { get; }
    public string FilterColumn { get; }
    public string FilterValue { get; }
    public string ValueField { get; }
    public string TextField { get; }
    public string SortField { get; }
    public string Area { get; }
    public bool MultipleSelect { get; }
    /// <summary>
    /// The method to be invoked when the selection is changed
    /// </summary>
    public string OnSelectedChange { get; }
    /// <summary>
    /// the field to be affected when the selection changes
    /// </summary>
    public string OnField { get; }
    public string QueryField { get; set; }

}
