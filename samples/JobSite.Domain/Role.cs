using Finbuckle.MultiTenant;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobSite.Domain
{
    [MultiTenant]
    public class Role : MultiTenantIdentityRole<Guid>
    {

    }
}
