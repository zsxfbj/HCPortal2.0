using System;

namespace HC.Entity
{
    /// <summary>
    /// 操作日志表
    /// </summary>
    public class ActionLog
    {
        /// <summary>
        /// 操作流水号
        /// </summary>
        public long ActionId { get; set; }

        /// <summary>
        /// 提交的事务Id
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 操作步骤。默认 1
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// 动作名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 请求的内容
        /// </summary>
        public string RequestData { get; set; }

        /// <summary>
        /// 响应的内容
        /// </summary>
        public string ResponseData { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
