




namespace Inspire.Services.Security
{

    
    public class ParentMenuRepository(SecurityContext db) : SortableStandardService<ParentMenu,ParentMenuDto, StandardStatusFilter, string, SecurityContext>(db), IParentMenuService 
    {

        public List<GenericData<ParentMenuType>> GetParentMenuTypes()
        {
            return ConvertEnumToList<ParentMenuType>();
        }

        public override IQueryable<ParentMenu> SearchByFilterModel(StandardStatusFilter model, IQueryable<ParentMenu> data = null)
        {
            return base.SearchByFilterModel(model, data).Include(s => s.SubMenus); ;
        }
        public List<GenericData<string>> GetMenus()
        {
            return db.Set<ParentMenu>().Where(s => s.AuthStatus == "A")
               .Select(s => new GenericData<string>
               {
                   ID = s.Id,
                   Name = s.Name
               }).ToList();
        }

        public List<GenericData<string>> GetReport()
        {
            return db.Set<ParentMenu>().Where(s => s.AuthStatus == "A" && s.ParentMenuType == ParentMenuType.Report)
               .Select(s => new GenericData<string>
               {
                   ID = s.Id,
                   Name = s.Name
               }).ToList();
        }

        public List<GenericData<string>> GetWorkflows()
        {
            return db.Set<ParentMenu>().Where(s => s.AuthStatus == "A" && s.ParentMenuType == ParentMenuType.Workflow)
               .Select(s => new GenericData<string>
               {
                   ID = s.Id,
                   Name = s.Name
               }).ToList();
        }

    }
}
