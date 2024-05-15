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
        [JsonProperty("totalAmount")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 医保内总金额，精确到小数点2位
        /// </summary>
        [JsonProperty("inInsuranceAmount")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal InInsuranceAmount { get; set; }

        /// <summary>
        /// 医保外总金额，精确到小数点2位
        /// </summary>
        [JsonProperty("outInsuranceAmount")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal OutInsuranceAmount { get; set; }

        /// <summary>
        /// 本次付起付线金额
        /// </summary>
        [JsonProperty("firstPayAmount")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal FirstPayAmount { get; set; }

        /// <summary>
        /// 个人自付2金额
        /// </summary>
        [JsonProperty("selfPayAmount")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal SelfPayAmount { get; set; }

        /// <summary>
        /// 门诊大额支付金额
        /// </summary>
        [JsonProperty("bigPayAmount")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal BigPayAmount { get; set; }

        /// <summary>
        /// 门诊大额自付金额
        /// </summary>
        [JsonProperty("selfBigPayAmount")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal SelfBigPayAmount { get; set; }


        /// <summary>
        /// 超大额自付金额
        /// </summary>
        [JsonProperty("outOfBigPayAmount")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal OutOfBigPayAmount { get; set; }

        /// <summary>
        /// 补充保险支付金额
        /// </summary>
        [JsonProperty("supplementaryPayAmount")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal SupplementaryPayAmount { get; set; }

        /// <summary>
        /// 军残补助保险金额
        /// </summary>
        [JsonProperty("militaryPayAmount")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal MilitaryPayAmount { get; set; }

    }
}