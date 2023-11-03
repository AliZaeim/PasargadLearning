using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PLCore.Services.Interfaces;


namespace PLCore.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IUserService _userService;
        private readonly int _permissionId = 0;
        public PermissionCheckerAttribute(int permissionId)
        {
            _permissionId = permissionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string username = context.HttpContext.User.Identity.Name;
                int roleId = int.Parse(context.HttpContext.User.FindFirst("RoleId").Value);
                if (!_userService.CheckPermission(_permissionId, username, roleId))
                {
                    context.Result = new RedirectResult("/Login/S");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Login/S");
            }
        }
    }
}
