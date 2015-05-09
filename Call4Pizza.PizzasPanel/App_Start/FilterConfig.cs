using System.Web;
using System.Web.Mvc;

namespace Call4Pizza.PizzasPanel
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
