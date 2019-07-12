using Finbuckle.MultiTenant;
using System;
using System.Collections.Generic;

namespace JobSite.Domain
{
    [MultiTenant]
    public class Recruiter
    {
        public Recruiter()
        {
            RecruiterJobs = new HashSet<Job>();
        }

        public Guid RecruiterId { get; set; }
        public string RecruiterName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual ICollection<Job> RecruiterJobs { get; set; }
    }
}
