using System;
using Newtonsoft.Json;

namespace HC.Model.VO
{
    /// <summary>
    /// 警告信息视图
    /// </summary>
    public class ErrorMessageVO
    {
        /// <summary>
        /// 序号
        /// </summary>
        [JsonProperty("no")]
        public string Number { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [JsonProperty("info")]
        public string Message { get; set; }
    }
}
