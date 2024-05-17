using Newtonsoft.Json;

namespace HC.Model.DTO
{
    /// <summary>
    /// 费用分解请求
    /// </summary>
    public class DivideReqDTO
    {
        /// <summary>
        /// 参保人员信息
        /// </summary>
        [JsonProperty("person")]
        public PersonInfoReqDTO Person { get; set; }
    }
}
