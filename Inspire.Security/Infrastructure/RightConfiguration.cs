namespace Inspire.Security.Infrastructure
{
    public class RightConfiguration
    {
        public static List<MenuRight> GetMenuRights()
        {
            return new List<MenuRight>()
            {
                new MenuRight {RightCode="C",RightDescription="Create",Order=1, Rank=2 },
                new MenuRight {RightCode="R",RightDescription="Read",Order=2, Rank=1},
                new MenuRight {RightCode="U",RightDescription="Update",Order=3, Rank=2 },
                new MenuRight {RightCode="D",RightDescription="Delete",Order=4, Rank=2 },
                new MenuRight {RightCode="A",RightDescription="Authorise",Order=5, Rank=3 },
                new MenuRight {RightCode="V",RightDescription="View Reports",Order=6, Rank=1 },
            };
        }
        public static string GetDescription(string actionType)
        {
            var rights = GetMenuRights();
            var menuRight = rights.Where(r => actionType.Contains(r.RightCode)).ToList();
            return string.Join(", ", menuRight.OrderBy(s => s.Order).Select(s => s.RightDescription)); ;
        }
        public static int GetMenuOrder(string actionType)
        {
            var rights = GetMenuRights();
            return rights.Where(r => actionType.Contains(r.RightCode)).Sum(s => s.Rank);
        }
        public static List<GenericData<string>> GetAllowableRights(string allowedRights)
        {
            allowedRights = string.IsNullOrEmpty(allowedRights) ? "CRUDAV" : allowedRights;
            var allowedMenuRights = GetMenuRights().Where(s => allowedRights.Contains(s.RightCode)).ToList();

            var result = Enumerable
                .Range(1, (1 << allowedMenuRights.Count) - 1)
                .Select(index => allowedMenuRights.Where((item, idx) => ((1 << idx) & index) != 0).ToList());
            List<GenericData<string>> data = new();
            foreach (var item in result)
            {
                string id = string.Join("", item.OrderBy(s => s.Order).Select(s => s.RightCode));
                string name = string.Join(",", item.OrderBy(s => s.Order).Select(s => s.RightDescription));
                data.Add(new GenericData<string>
                {
                    ID = id,
                    Name = name,
                });

            }
            return data;
        }
    }
}