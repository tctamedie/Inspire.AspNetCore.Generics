namespace Inspire.Annotator.Annotations
{
    public class ColumnAttribute: EntityAttribute
    {
        public ColumnAttribute([CallerMemberName] string id="", string displayName="", int order=1, bool isKey=false, int width=0):base(id,displayName,order, isKey,width:width)
        {  
        }
    }
}
