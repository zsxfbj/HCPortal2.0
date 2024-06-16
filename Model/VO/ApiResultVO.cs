using Newtonsoft.Json;

namespace HC.Model.VO
{
    /// <summary>
    /// 给API输出的结果内容
    /// </summary>
    public class ApiResultVO
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty("success")]
        public string Success { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty("errMsg")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 医保交易号
        /// </summary>
        [JsonProperty("tradeNumber")]
        public string TradeNumber { get; set; }

        /// <summary>
        /// 总费用
        /// </summary>
        [JsonProperty("totalAmount")]
        public string TotalAmount { get; set; }

        /// <summary>
        /// 医保内金额
        /// </summary>
        [JsonProperty("inInsuranceAmount")]
        public string InInsuranceAmount { get; set; }

        /// <summary>
        /// 交易具体内容（Base64编码）
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
