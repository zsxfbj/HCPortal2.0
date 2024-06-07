using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HC.Model.VO
{
    /// <summary>
    /// 
    /// </summary>
    public class StateVO
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty("success")]
        public string Success { get; set; }

        /// <summary>
        /// 警告信息
        /// </summary>
        [JsonProperty("warning", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorMessageVO Warning { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorMessageVO Error { get; set; }

    }
}
