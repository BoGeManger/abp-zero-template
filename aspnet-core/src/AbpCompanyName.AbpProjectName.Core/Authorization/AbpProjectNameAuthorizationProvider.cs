using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace AbpCompanyName.AbpProjectName.Authorization
{
    public class AbpProjectNameAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            #region 用户管理
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            #endregion

            #region 商品管理
            context.CreatePermission(PermissionNames.Pages_PdBrand, L("PdBrand"));
            #endregion
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpProjectNameConsts.LocalizationSourceName);
        }
    }
}
