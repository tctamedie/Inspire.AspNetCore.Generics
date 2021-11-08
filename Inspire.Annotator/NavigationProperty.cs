using Inspire.Annotator.Annotations;
using System;
using System.Runtime.CompilerServices;

public class NavigationProperty : Attribute
{
    public NavigationProperty([CallerMemberName] string FieldColumn = "")
    {
        this.ForeignColum = ForeignColum.FirstLetterToLower();
    }
    public string ForeignColum { get; }
}