using System;
using Microsoft.AspNetCore.Identity;

namespace Models
{
    /// <summary>
    ///     用户角色中间表
    /// </summary>
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}