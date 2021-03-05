using System.Web;
using System.Web.Mvc;

namespace DBSD_CW2_7510_8775_7912
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
