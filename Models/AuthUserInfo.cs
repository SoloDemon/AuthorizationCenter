using System;

namespace Models
{
    /// <summary>
    ///     授权用户信息
    /// </summary>
    public class AuthUserInfo
    {
        public long ManufacturerId { get; set; }
        public Guid UserId { get; set; }
    }
}