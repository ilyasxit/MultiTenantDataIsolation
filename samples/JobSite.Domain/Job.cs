using Finbuckle.MultiTenant;
using System;

namespace JobSite.Domain
{
    //[MultiTenant]
    public class Job
    {
        public Guid JobId { get; set; }
        public string JobTitle { get; set; }
        public Guid RecruiterId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Recruiter Recruiter { get; set; }
    }
}
