using HC.Utility.Converter;
using Newtonsoft.Json;

namespace HC.Model.VO
{
    /// <summary>
    /// 新单据分类汇总信息
    /// </summary>
    public class MedicineCatalog2VO
    {
        /// <summary>
        /// 诊察费
        /// </summary>
        [JsonProperty("diagnosis")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal DiagnosisFee { get; set; }

        /// <summary>
        /// 检查费
        /// </summary>
        [JsonProperty("examine")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal ExamineFee { get; set; }

        /// <summary>
        /// 化验费
        /// </summary>
        [JsonProperty("labexam")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal LabExamFee { get; set; }

        /// <summary>
        /// 治疗费
        /// </summary>
        [JsonProperty("treatment")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal TreatmentFee { get; set; }

        /// <summary>
        /// 材料费
        /// </summary>
        [JsonProperty("material")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal MaterialFee { get; set; }

        /// <summary>
        /// 手术费
        /// </summary>
        [JsonProperty("operation")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal OperationFee { get; set; }

        /// <summary>
        /// 西药费
        /// </summary>
        [JsonProperty("medicine")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal MedicineFee { get; set; }

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
        /// 医事服务费
        /// </summary>
        [JsonProperty("medicalservice")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal MedicalServiceFee { get; set; }

        /// <summary>
        /// 一般诊疗费
        /// </summary>
        [JsonProperty("commonservice")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal CommonServiceFee {  get; set; }

        /// <summary>
        /// 挂号费
        /// </summary>
        [JsonProperty("registfee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal RegistFee { get; set; }

        /// <summary>
        /// 其他门诊收费
        /// </summary>
        [JsonProperty("otheropfee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal OtherOperationFee { get; set; }

    }
}