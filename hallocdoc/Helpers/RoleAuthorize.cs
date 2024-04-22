using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

using DataLayer.DataContext;

namespace HalloDoc.Auth
{

    [AttributeUsage(AttributeTargets.All)]
    public class RoleAuthorize : Attribute, IAuthorizationFilter
    {
        private readonly int _menuId;

        private readonly HellodocPrjContext _context = new();

        public RoleAuthorize(int menuId = 0)
        {
            _menuId = menuId;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var request = context.HttpContext.Request;
            var roleCookie = request.Cookies["RoleMenu"];

            var Role = _context.RoleMenus.Where(u => u.Roleid == Int32.Parse(roleCookie!)).ToList();
            bool flag = false;

            if (Role.Any(u => u.Menuid == _menuId))
            {

                flag = true;
            }

            if (flag == false)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Admin", action = "AccessPage" }));
                return;
            }

        }

    }

}