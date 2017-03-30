using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GsmRanking.Common
{
    public static class ViewContextExtension
    {
        public const string SuccessKey = "SuccessMessage";
        public const string ErrorKey = "ErrorMessage";
        //private static readonly string[] AdminControllers;

        //static ViewContextExtension()
        //{
        //    var controllerTypes = GetAdminControllers();
        //    AdminControllers = controllerTypes.Select(x => x.Key.Name).ToArray();
        //}

        public static string GetController(this ViewContext viewContext)
        {
            return $"{viewContext.RouteData.Values["controller"]}Controller";
        }

        public static string GetAction(this ViewContext viewContext)
        {
            return viewContext.RouteData.Values["action"].ToString();
        }

        //public static bool IsAdminController(this ViewContext viewContext)
        //{
        //    return AdminControllers.Contains(viewContext.GetController());
        //}

        public static bool UserHasRole(this ClaimsPrincipal claimsPrincipal, string[] roles)
        {
            return claimsPrincipal
                ?.FindAll(ClaimTypes.Role)
                ?.Select(x => x.Value)
                .Intersect(roles, StringComparer.OrdinalIgnoreCase)
                ?.Any() ?? false;
        }

        public static bool UserHasRole(this ClaimsPrincipal claimsPrincipal, string role)
        {
            return UserHasRole(claimsPrincipal, new[] { role });
        }

        //public static IEnumerable<KeyValuePair<TypeInfo, sbyte>> GetAdminControllers()
        //{
        //    var controllersNamespace = typeof(NetCoreShopController<>).Namespace;

        //    var controllers = typeof(NetCoreShopController<>)
        //        .GetTypeInfo()
        //        .Assembly
        //        .DefinedTypes
        //        .Where(x => x.Namespace == controllersNamespace)
        //        .ToDictionary(x => x, x => x.GetCustomAttribute<AdminSidebarController>().Order)
        //        .OrderBy(x => x.Value);
        //    return controllers;
        //}

        public static string GetExceptionMessage(this Exception e)
        {
            return e.InnerException == null ? e.Message : GetExceptionMessage(e.InnerException);
        }

        public static bool IsLoggedIn(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims?.Any() ?? false;
        }
    }
}
