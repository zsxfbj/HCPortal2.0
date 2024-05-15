using System.Collections.Generic;
using Newtonsoft.Json;

namespace HC.Model.VO
{
    /// <summary>
    /// 退款交易内容
    /// </summary>
    public class RefundTradeVO
    {
        /// <summary>
        /// 社保卡号
        /// </summary>
        [JsonProperty("cardNumber")]
        public string CardNumber { get; set; }

        /// <summary>
        /// 退款交易流水号
        /// </summary>
        [JsonProperty("tradeNumber")]
        public string TradeNumber { get; set; }

        /// <summary>
        /// 就诊方式，目前都是0普通，取值须在字典范围内，且严禁使用汉字
        /// </summary>       
        [JsonProperty("illType")]
        public string IllType { get; set; }

        /// <summary>
        /// 医保应用号
        /// </summary>
        [JsonProperty("icNumber")]
        public string IcNumber { get; set; }

        /// <summary>
        /// 就诊类型，此处目前固定为17（互联网复诊费结算）
        /// </summary>        
        [JsonProperty("cureType")]
        public string CureType { get; set; }

        /// <summary>
        /// 原交易就诊时间，格式为：yyyyMMddHHmmss
        /// </summary>
        [JsonProperty("tradeDate")]
        public string TradeDate { get; set; }

        /// <summary>
        /// 明细分解信息列表
        /// </summary>
        [JsonProperty("feeItems")]
        public List<FeeItemVO> FeeItems { get; set; }

        /// <summary>
        /// 汇总支付信息
        /// </summary>
        [JsonProperty("summaryPay")]
        public SummaryPayVO SummaryPay { get; set; }

        /// <summary>
        /// 支付信息
        /// </summary>
        [JsonProperty("payment")]
        public PaymentVO Payment { get; set; }

    }
}