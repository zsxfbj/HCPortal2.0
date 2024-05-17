using System.Collections.Generic;
using Newtonsoft.Json;

namespace HC.Model.VO
{
    /// <summary>
    /// Com组件返回的结果解析
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ComResultVO<T>
    {
        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("state")]
        public string State {  get; set; }

        /// <summary>
        /// 警告信息
        /// </summary>
        [JsonProperty("warnings")]
        public List<ErrorMessageVO> Warnings { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty("errors")]
        public List<ErrorMessageVO> Errors { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
