using Newtonsoft.Json;

namespace HC.Model.VO
{
    /// <summary>
    /// 退款申请结果
    /// </summary>
    public class RefundTradeResultVO
    {
        /// <summary>
        /// 交易信息
        /// </summary>
        [JsonProperty("tradeinfo")]
        public RefundTradeVO Trade { get; set; }

        /// <summary>
        /// 明细分解信息列表
        /// </summary>
        [JsonProperty("feeitemarray")]
        public FeeItemsVO FeeItems { get; set; }

        /// <summary>
        /// 汇总支付信息
        /// </summary>
        [JsonProperty("sumpay")]
        public SummaryPayVO SummaryPay { get; set; }

        /// <summary>
        /// 支付信息
        /// </summary>
        [JsonProperty("payinfo")]
        public PaymentVO Payment { get; set; }

    }
}
