public class ListModel
{
    public ListModel(string Controller, string Action, string ValueField, string TextField, string ID, string Area, bool MultipleSelect, string OnSelectChange, string OnField, string FilterColumn, string FilterValue, string SortField, string QueryField)
    {
        this.ID = ID;
        this.Controller = Controller;
        this.Action = Action;
        this.ValueField = ValueField;
        this.TextField = TextField;
        this.Area = Area;
        this.FilterColumn = FilterColumn;
        this.FilterValue = FilterValue;
        this.MultipleSelect = MultipleSelect;
        this.OnSelectedChange = OnSelectChange;
        this.OnField = OnField;
        this.SortField = SortField;
        this.QueryField = QueryField;

    }

    public string ID { get; }
    public string Controller { get; }
    public string Action { get; }
    public string FilterColumn { get; set; }
    public string FilterValue { get; set; }
    public string ValueField { get; }
    public string TextField { get; }
    public string SortField { get; }
    public string Area { get; }
    public bool MultipleSelect { get; }
    public string OnSelectedChange { get; }
    public string OnField { get; }
    public string QueryField { get; set; }

}
