

namespace Inspire.Security.Infrastructure
{

    public class MenuSettingRepository(SecurityContext db) : SortableStandardService<SubMenu, SubMenuDto, ParentFilterModel, string, SecurityContext>(db), IMenuSettingRepository
    {
        public List<MenuRight> MenuRights { get; set; }
        

        public override IQueryable<SubMenu> SearchByFilterModel(ParentFilterModel model, IQueryable<SubMenu> data = null)
        {
            var parentMenuId = model == null ? "" : model.ParentMenuId;
            var records = base.SearchByFilterModel(model, data).Where(s => s.ParentMenuID == parentMenuId);

            return records;
        }

        public override IQueryable<SubMenu> Filter(string id)
        {
            var menu = query.FirstOrDefault(s => s.Id == id);
            if (menu == null)
                return base.Filter(id);
            return query.Where(s => s.ParentMenuID == menu.ParentMenuID);
        }

        protected override OutputHandler Validate(SubMenuDto row, int a = 0, bool c = true, string capturer = "", [CallerMemberName] string caller = "")
        {
            if (row.Authorisers < 0)
                return "A negative value is not allowed for Number of Authorisers".Formator(true);
            if (string.IsNullOrEmpty(row.Name))
                return "Please Enter Menu Name".Formator(true);
            var data = Count(s => s.Name.ToLower() == row.Name.ToLower() && !s.Id.Equals(row.Id) && s.ParentMenuID == row.ParentMenuID);
            if (data > 0)
                return "Record Already Exists".Formator(true);
            return new OutputHandler();
        }
        public List<GenericData<string>> GetAllowableRights(string menuId)
        {
            var allowedMenuRights = MenuRights;
            var record = Find(menuId);
            string rights = "";
            if (record != null)
            {
                rights = record.ActionType;
            }
            return RightConfiguration.GetAllowableRights(rights);
        }
        public List<GenericData<int>> GetAuditPrinciples()
        {
            List<GenericData<int>> data = new();
            data.Add(new GenericData<int>
            {
                ID = 0,
                Name = "Two Eyes"
            });
            data.Add(new GenericData<int>
            {
                ID = 1,
                Name = "Four Eyes"
            });
            data.Add(new GenericData<int>
            {
                ID = 2,
                Name = "Six Eyes"
            });

            return data;
        }
        public List<GenericData<string>> GetAllowableMenus(string parentMenuId)
        {
            return db.Set<SubMenu>().Where(s => s.ParentMenuID == parentMenuId && s.AuthStatus == "A")
                .Select(s => new GenericData<string>
                {
                    ID = s.Id,
                    Name = s.Name
                }).ToList();
        }
        public List<GenericData<string>> GetReportGroups()
        {
            return db.Set<SubMenu>().Where(s => s.ParentMenu.ParentMenuType == ParentMenuType.Report).Select(s => new GenericData<string>
            {
                ID = s.Id,
                Name = s.Name
            }).ToList();
        }
        public List<GenericData<string>> GetWorkflowGroups()
        {
            return db.Set<SubMenu>().Where(s => s.ParentMenu.ParentMenuType == ParentMenuType.Workflow).Select(s => new GenericData<string>
            {
                ID = s.Id,
                Name = s.Name
            }).ToList();
        }
    }
}