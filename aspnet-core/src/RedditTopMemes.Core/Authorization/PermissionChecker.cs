using Abp.Authorization;
using RedditTopMemes.Authorization.Roles;
using RedditTopMemes.Authorization.Users;

namespace RedditTopMemes.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
