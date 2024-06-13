using Newtonsoft.Json;

namespace HC.Model.VO
{
    /// <summary>
    /// 退款交易内容
    /// </summary>
    public class RefundTradeVO
    { 
        /// <summary>
        /// 退款交易流水号
        /// </summary>
        [JsonProperty("tradeno")]
        public string TradeNumber { get; set; }

        /// <summary>
        /// 就诊方式，目前都是0普通，取值须在字典范围内，且严禁使用汉字
        /// </summary>       
        [JsonProperty("illtype")]
        public string IllType { get; set; }

        /// <summary>
        /// 医保应用号
        /// </summary>
        [JsonProperty("ic_no")]
        public string IcNumber { get; set; }

        /// <summary>
        /// 就诊类型，此处目前固定为17（互联网复诊费结算）
        /// </summary>        
        [JsonProperty("curetype")]
        public string CureType { get; set; }

        /// <summary>
        /// 原交易就诊时间，格式为：yyyyMMddHHmmss
        /// </summary>
        [JsonProperty("tradedate")]
        public string TradeDate { get; set; }
    }
}