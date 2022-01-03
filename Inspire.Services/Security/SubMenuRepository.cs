

namespace Inspire.Services.Security
{
    using Inspire.Annotator.Annotations;
    using Inspire.DataAccess;
    using Inspire.Modeller;
    using Inspire.Modeller.Models.Security;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    public class SubMenuFilter: StandardFilter
    {
        [TableFilter(Order:1)]
        public string ParentMenuID { get; set; }
    }
    public interface ISubMenuService<TContext> : IStandardService<SubMenu, SubMenuDto, string, TContext, SubMenuFilter>
        where TContext: RecordContext
    {

    }

    public class SubMenuRepository<TContext> : StandardService<SubMenu, SubMenuDto, string,TContext, SubMenuFilter>, ISubMenuService<TContext>
        where TContext : RecordContext
    {
        public SubMenuRepository(TContext db) : base(db)
        {
        }
        public override IQueryable<SubMenu> SearchByFilterModel(SubMenuFilter model, IQueryable<SubMenu> data = null)
        {
            var search = GetSearchString(model);
            var parentMenuId = model==null?"":model.ParentMenuID??"";
            var name = model == null ? "" : model.Name??"";            
            return _context.Set<SubMenu>().Where(s => s.ParentMenuID == parentMenuId && s.Name.ToLower().Contains(search)).Include(s => s.ParentMenu);
        }
       

    }
}
