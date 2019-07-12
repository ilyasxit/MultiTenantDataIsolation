using Finbuckle.MultiTenant;
using System;

namespace JobSite.Domain
{
    [MultiTenant]
    public class User : MultiTenantIdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsActive { get; set; }

        public string FullName() => $"{FirstName} {LastName}";
    }

    [MultiTenant]
    public class UserRole : MultiTenantIdentityUserRole<Guid>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }

    [MultiTenant]
    public class UserClaim : MultiTenantIdentityUserClaim<Guid>
    {
        public virtual User User { get; set; }
    }

    [MultiTenant]
    public class UserLogin : MultiTenantIdentityUserLogin<Guid>
    {
        public virtual User User { get; set; }
    }

    [MultiTenant]
    public class RoleClaim : MultiTenantIdentityRoleClaim<Guid>
    {
        public virtual Role Role { get; set; }
    }

    [MultiTenant]
    public class UserToken : MultiTenantIdentityUserToken<Guid>
    {
        public virtual User User { get; set; }
    }
}
