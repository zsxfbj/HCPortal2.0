using HC.Utility.Converter;
using Newtonsoft.Json;

namespace HC.Model.VO
{
    /// <summary>
    /// 支付信息
    /// </summary>
    public class PaymentVO
    {
        /// <summary>
        /// 费用总金额，精确到小数点2位
        /// </summary>
        [JsonProperty("mzfee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 医保内总金额，精确到小数点2位
        /// </summary>
        [JsonProperty("mzfeein")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal InInsuranceAmount { get; set; }

        /// <summary>
        /// 医保外总金额，精确到小数点2位
        /// </summary>
        [JsonProperty("mzfeeout")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal OutInsuranceAmount { get; set; }

        /// <summary>
        /// 本次付起付线金额
        /// </summary>
        [JsonProperty("mzpayfirst")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal FirstPayAmount { get; set; }

        /// <summary>
        /// 个人自付2金额
        /// </summary>
        [JsonProperty("mzselfpay2")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal SelfPayAmount { get; set; }

        /// <summary>
        /// 门诊大额支付金额
        /// </summary>
        [JsonProperty("mzbigpay")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal BigPayAmount { get; set; }

        /// <summary>
        /// 门诊大额自付金额
        /// </summary>
        [JsonProperty("mzbigselfpay")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal SelfBigPayAmount { get; set; }

        /// <summary>
        /// 超大额自付金额
        /// </summary>
        [JsonProperty("mzoutofbig")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal OutOfBigPayAmount { get; set; }

        /// <summary>
        /// 补充保险支付金额
        /// </summary>
        [JsonProperty("bcpay")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal SupplementaryPayAmount { get; set; }

        /// <summary>
        /// 军残补助保险金额
        /// </summary>
        [JsonProperty("jcbz")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal MilitaryPayAmount { get; set; }

    }
}