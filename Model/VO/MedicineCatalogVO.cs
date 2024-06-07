using HC.Utility.Converter;
using Newtonsoft.Json;

namespace HC.Model.VO
{
    /// <summary>
    /// 分类汇总信息
    /// </summary>
    public class MedicineCatalogVO
    {
        /// <summary>
        /// 西药费
        /// </summary>
        [JsonProperty("medicine")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal MedicineFee {  get; set; }

        /// <summary>
        /// 中成药费
        /// </summary>
        [JsonProperty("tmedicine")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal ChineseMedicineFee { get; set; }

        /// <summary>
        /// 中草药费
        /// </summary>
        [JsonProperty("therb")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal ChineseHerbalDrinkFee { get; set; }

        /// <summary>
        /// 化验费
        /// </summary>
        [JsonProperty("labexam")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal LabExamFee { get; set; }

        /// <summary>
        /// 放射费用
        /// </summary>
        [JsonProperty("xray")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal XRayFee { get; set; }

        /// <summary>
        /// B超费用
        /// </summary>
        [JsonProperty("ultrasonic")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal UltrasonicFee { get; set; }

        /// <summary>
        /// CT费用
        /// </summary>
        [JsonProperty("ct")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal CtFee { get; set; }

        /// <summary>
        /// 核磁费用
        /// </summary>
        [JsonProperty("mri")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal MriFee { get; set; }

        /// <summary>
        /// 检查费用
        /// </summary>
        [JsonProperty("examine")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal ExamineFee { get; set; }

        /// <summary>
        /// 治疗费用
        /// </summary>
        [JsonProperty("treatment")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal TreatmentFee { get; set; }

        /// <summary>
        /// 材料费用
        /// </summary>
        [JsonProperty("material")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal MaterialFee { get; set; }

        /// <summary>
        /// 手术费用
        /// </summary>
        [JsonProperty("operation")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal OperationFee { get; set; }

        /// <summary>
        /// 输氧费用
        /// </summary>
        [JsonProperty("oxygen")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal OxygenFee { get; set; }

        /// <summary>
        /// 输血费
        /// </summary>
        [JsonProperty("bloodt")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal BloodTransfusionFee { get; set; }

        /// <summary>
        /// 正畸费
        /// </summary>
        [JsonProperty("orthodontics")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal OrthodonticsFee { get; set; }

        /// <summary>
        /// 镶牙费
        /// </summary>
        [JsonProperty("prosthesis")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal ProsthesisFee { get; set; }

        /// <summary>
        /// 司法鉴定费
        /// </summary>
        [JsonProperty("forensic_expertise")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal ForensicExpertiseFee {  get; set; }

        /// <summary>
        /// 其他费
        /// </summary>
        [JsonProperty("other")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal OtherFee { get; set; }

    }
}