using System.Collections.Generic;

namespace Models
{
    /// <summary>
    ///     通用分页信息类
    /// </summary>
    public class PageModel<T>
    {
        /// <summary>
        ///     当前页标
        /// </summary>
        public int CurrentIndex { get; set; } = 1;

        /// <summary>
        ///     总页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        ///     数据总数
        /// </summary>
        public int DataCount { get; set; }

        /// <summary>
        ///     当前查询数据总数
        /// </summary>
        public int CurrentDataCount { get; set; }

        /// <summary>
        ///     每页大小
        /// </summary>
        public int PageSize { set; get; } = 10;

        /// <summary>
        ///     返回数据
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        ///     是否执行成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///     信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        ///     状态码
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    ///     动态表单分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DynamicFormPageModel<T> : PageModel<T>
    {
        //表头数据
        public string TitleData { get; set; }
    }
}