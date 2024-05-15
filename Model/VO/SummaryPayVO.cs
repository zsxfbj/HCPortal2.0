using HC.Utility.Converter;
using Newtonsoft.Json;

namespace HC.Model.VO
{
    /// <summary>
    /// 汇总支付信息
    /// </summary>
    public class SummaryPayVO
    {
        /// <summary>
        /// 费用总金额，精确到小数点2位
        /// </summary>
        [JsonProperty("totalAmount")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal TotalAmount {  get; set; }

        /// <summary>
        /// 基金支付金额，精确到小数点2位
        /// </summary>
        [JsonProperty("fundAmount")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal FundAmount {  get; set; }

        /// <summary>
        /// 现金支付金额，精确到小数点2位
        /// </summary>
        [JsonProperty("cashAmount")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal CashAmount { get; set; }

        /// <summary>
        /// 个人帐户支付金额，精确到小数点2位
        /// </summary>
        [JsonProperty("personAccountAmount")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal PersonAccountAmount { get; set; }
    }
}