
using Inspire.DataAccess.Security;
using Inspire.Modeller;
using Inspire.Modeller.Models.Security;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Inspire.Services.Security
{
    public interface IEncryptionRepository
    {
        string Decrypt(string decryptedText, string pwd);
        string Encrypt(string text, string pwd);
    }
    public interface IParentMenuService<TContext>: IStandardService<ParentMenu, ParentMenuDto, string, TContext, StandardFilter>
        where TContext: SecurityContext
    {

    }
    public class ParentMenuRepository<TContext> : StandardService<ParentMenu,ParentMenuDto, string, TContext,  StandardFilter>, IParentMenuService<TContext>
        where TContext : SecurityContext
    {
        public ParentMenuRepository(TContext db) : base(db)
        {
        }
        public override IQueryable<ParentMenu> SearchByFilterModel(StandardFilter model, IQueryable<ParentMenu> data = null)
        {
            return base.SearchByFilterModel(model, data).Include(s => s.SubMenus); ;
        }
        
    }
}
