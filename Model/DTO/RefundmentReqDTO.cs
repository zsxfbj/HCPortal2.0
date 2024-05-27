using Newtonsoft.Json;

namespace HC.Model.DTO
{
    /// <summary>
    /// 退款请求
    /// </summary>
    public class RefundmentReqDTO
    {
        /// <summary>
        /// 参保人员信息
        /// </summary>
        [JsonProperty("person")]
        public PersonInfoReqDTO Person { get; set; }

        /// <summary>
        /// 交易流水
        /// </summary>
        [JsonProperty("tradeNumber")]
        public string TradeNumber { get; set; }

        /// <summary>
        /// 收费员
        /// </summary>
        [JsonProperty("operator")]
        public string Operator { get; set; }
    }
}
