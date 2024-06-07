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
        /// 版本号
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("state")]
        public StateVO State {  get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        [JsonProperty("output")]
        public T Output { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ComResultVO() {
            Version = "1.10.0004";
        }
    }
}
