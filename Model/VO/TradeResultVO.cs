using HC.Utility.Converter;
using Newtonsoft.Json;

namespace HC.Model.VO
{
    /// <summary>
    /// 门诊交易确认返回结果
    /// </summary>
    public class TradeResultVO
    { 
        /// <summary>
        /// 本次结算后个人帐户余额，精确到小数点2位
        /// </summary>
        [JsonProperty("afterSubtractAmount")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal PersonAccountAfterSubtractAmount { get; set; }

        /// <summary>
        /// 个人数字证书编码
        /// </summary>
        [JsonProperty("certId")]
        public string CertId { get; set; }

        /// <summary>
        /// 交易数字签名
        /// </summary>
        [JsonProperty("sign")]
        public string Sign {  get; set; }


    }
}