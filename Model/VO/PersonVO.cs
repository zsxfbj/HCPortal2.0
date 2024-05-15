using System.ComponentModel;
using Newtonsoft.Json;

namespace HC.Model.VO
{
    /// <summary>
    /// 参保人员信息
    /// </summary>
    public class PersonVO
    {
        /// <summary>
        /// 社保卡卡号
        /// </summary>
        [JsonProperty("cardNumber")]
        public string CardNumber { get; set; }

        /// <summary>
        /// 医保应用号
        /// </summary>
        [JsonProperty("icNumber")]
        public string IcNumber { get; set; }

        /// <summary>
        /// 公民身份证号
        /// </summary>        
        [JsonProperty("idNumber")]
        public string IdNumber { get; set; }

        /// <summary>
        /// 参保人姓名
        /// </summary>        
        [JsonProperty("personName")]
        public string PersonName { get; set; }

        /// <summary>
        /// 性别：1男；2女；9未说明性别
        /// </summary>      
        [JsonProperty("sex")]
        public int Sex { get; set; }

        /// <summary>
        /// 出生日期，格式为yyyyMMdd
        /// </summary>         
        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        /// <summary>
        /// 转诊医院编码（系统预留，目前为空）
        /// </summary>
        [JsonProperty("fromHopital")]
        public string FromHospital { get; set; }

        /// <summary>
        /// 转诊时限（系统预留 默认18991230）
        /// </summary>
        [JsonProperty("fromHopitalDate")]
        public string FromHospitalDate { get; set; }

        /// <summary>
        /// 参保人员类别（参见AKC021）
        /// </summary>
        [JsonProperty("personType")]
        public string PersonType { get; set; }

        /// <summary>
        /// 是否在红名单：true-在红名单；false-不在红名单
        /// </summary>
        [JsonProperty("isInRedList")]
        public string IsInRedList { get; set; }

        /// <summary>
        /// 是否本人定点医院：0-本地红名单，默认为本人定点医院；1-是本人定点医院、A类医院、专科医院、中医医院；2-不是本人定点医院；3-转诊医院
        /// </summary>
        [JsonProperty("isSpecifiedHospital")]
        public string IsSpecifiedHospital { get; set; }

        /// <summary>
        /// 是否本人慢病定点医院：
        /// </summary>
        [JsonProperty("isChronicHospital")]
        public string IsChronicHospital { get; set; }

        /// <summary>
        /// 个人帐户余额
        /// </summary>
        [JsonProperty("personCount")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal PersonCount { get; set; }

        /// <summary>
        /// 慢病编码
        /// </summary>
        [JsonProperty("chronicCode")]
        public string ChronicCode { get; set; }

        /// <summary>
        /// 险种类型：3城镇职工；31离休统筹；32 医疗照顾人员；33 超转人员；91 学生儿童；92 老年人；93 劳动年龄内居民
        /// </summary>        
        [JsonProperty("fundType")]
        public string FundType { get; set; }
    }
}
