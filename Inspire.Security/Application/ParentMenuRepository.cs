
namespace Inspire.Security.Application
{

    public interface IParentMenuService: ISortableStandardService<ParentMenu, ParentMenuDto, StandardStatusFilter, string>
    {
        List<GenericData<ParentMenuType>> GetParentMenuTypes();
        public List<GenericData<string>> GetMenus();
        public List<GenericData<string>> GetReport();
        public List<GenericData<string>> GetWorkflows();
    }
    
}
