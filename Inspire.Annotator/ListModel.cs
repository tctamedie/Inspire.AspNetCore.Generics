using Inspire.Annotator.Annotations;
using System;
using System.Runtime.CompilerServices;

public class ListAttribute : Attribute
{
    public ListAttribute(string Controller = "", string Action = "ReadData", string ValueField = "id", string TextField = "name", [CallerMemberName] string ID = "", string Area = "", bool MultipleSelect = false, string OnSelectChange = "", string OnField = "", string FilterColumn = "", string FilterValue = null, string SortField = "")
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
            this.SortField = SortField;
        }
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
    public string OnSelectedChange { get; }
    public string OnField { get; }
    
}
