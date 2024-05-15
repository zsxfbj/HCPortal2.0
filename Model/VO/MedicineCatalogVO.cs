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
        [JsonProperty("medicineFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal MedicineFee {  get; set; }

        /// <summary>
        /// 中成药费
        /// </summary>
        [JsonProperty("chineseMedicineFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal ChineseMedicineFee { get; set; }

        /// <summary>
        /// 中草药费
        /// </summary>
        [JsonProperty("chineseHerbalDrinkFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal ChineseHerbalDrinkFee { get; set; }

        /// <summary>
        /// 化验费
        /// </summary>
        [JsonProperty("labExamFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal LabExamFee { get; set; }

        /// <summary>
        /// 放射费用
        /// </summary>
        [JsonProperty("xRayFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal XRayFee { get; set; }

        /// <summary>
        /// B超费用
        /// </summary>
        [JsonProperty("ultrasonicFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal UltrasonicFee { get; set; }

        /// <summary>
        /// CT费用
        /// </summary>
        [JsonProperty("ctFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal CtFee { get; set; }

        /// <summary>
        /// 核磁费用
        /// </summary>
        [JsonProperty("mriFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal MriFee { get; set; }

        /// <summary>
        /// 检查费用
        /// </summary>
        [JsonProperty("examineFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal ExamineFee { get; set; }

        /// <summary>
        /// 治疗费用
        /// </summary>
        [JsonProperty("treatmentFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal TreatmentFee { get; set; }

        /// <summary>
        /// 材料费用
        /// </summary>
        [JsonProperty("materialFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal MaterialFee { get; set; }

        /// <summary>
        /// 手术费用
        /// </summary>
        [JsonProperty("operationFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal OperationFee { get; set; }

        /// <summary>
        /// 输氧费用
        /// </summary>
        [JsonProperty("oxygenFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal OxygenFee { get; set; }

        /// <summary>
        /// 输血费
        /// </summary>
        [JsonProperty("bloodTransfusionFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal BloodTransfusionFee { get; set; }

        /// <summary>
        /// 正畸费
        /// </summary>
        [JsonProperty("orthodonticsFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal OrthodonticsFee { get; set; }

        /// <summary>
        /// 镶牙费
        /// </summary>
        [JsonProperty("prosthesisFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal ProsthesisFee { get; set; }

        /// <summary>
        /// 司法鉴定费
        /// </summary>
        [JsonProperty("forensicExpertiseFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal ForensicExpertiseFee {  get; set; }

        /// <summary>
        /// 其他费
        /// </summary>
        [JsonProperty("otherFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal OtherFee { get; set; }

    }
}