using System;
using Microsoft.AspNetCore.Identity;

namespace Models
{
    /// <summary>
    ///     自定义角色
    /// </summary>
    public class ApplicationRole : IdentityRole<Guid>
    {
        /// <summary>
        ///     是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     排序
        /// </summary>
        public int OrderSort { get; set; }

        /// <summary>
        ///     是否激活
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///     创建ID
        /// </summary>
        public int? CreateId { get; set; }

        /// <summary>
        ///     创建者
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     修改ID
        /// </summary>
        public int? ModifyId { get; set; }

        /// <summary>
        ///     修改者
        /// </summary>
        public string ModifyBy { get; set; }

        /// <summary>
        ///     修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; } = DateTime.Now;
    }
}