using System;
using HC.Utility.Converter;
using Newtonsoft.Json;

namespace HC.Model.VO
{
    /// <summary>
    /// 分解交易结果
    /// </summary>
    public class DivideTradeResultVO
    {
        /// <summary>
        /// 医保卡号
        /// </summary>
        [JsonProperty("cardNumber")]    
        public String CardNumber {  get; set; }

        /// <summary>
        /// 医院内部的交易流水号
        /// </summary>
        [JsonProperty("feeNumber")]
        public string FeeNumber { get; set; }

        /// <summary>
        /// 医保交易订单号
        /// </summary>
        [JsonProperty("tradeNumber")]
        public string TradeNumber { get; set; }

        /// <summary>
        /// 交易时间，yyyyMMddHHmmss
        /// </summary>
        [JsonProperty("tradeDate")]
        public string TradeDate { get; set; }

        /// <summary>
        /// 自付金额
        /// </summary>
        [JsonProperty("selfPaymentAmount")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal SelfPaymentAmount { get; set; }

        /// <summary>
        /// 交易状态：success表示成功
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }


        /// <summary>
        /// 错误或者警告消息
        /// </summary>
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

    }
}
