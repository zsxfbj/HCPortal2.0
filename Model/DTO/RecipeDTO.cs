using Newtonsoft.Json;

namespace HC.Model.DTO
{
    /// <summary>
    /// 处方信息
    /// </summary>
    public class RecipeDTO
    {
        /// <summary>
        /// 诊断序号。严禁使用汉字。
        /// </summary>

        [JsonProperty("diagnoseNumber")]
        public string DiagnoseNumber { get; set; }

        /// <summary>
        /// 处方序号
        /// </summary>        
        [JsonProperty("recipeNumber")]
        public string RecipeNumber { get; set; }


        /// <summary>
        /// 处方时间，格式为yyyyMMddHHmmss
        /// </summary>      
        [JsonProperty("recipeDate")]
        public string RecipeDate { get; set; }

        /// <summary>
        /// 诊断编码，采用ICD国际诊断标准，或采用ICPC诊断标准(使用门诊医生工作站者为“必填项”) ，严禁使用汉字，字母严禁使用全角字符。
        /// </summary>      
        [JsonProperty("diagnoseCode")]
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称，与编码对应的诊断名称(使用门诊医生工作站者为“必填项”)，长度最多41个汉字。
        /// </summary>       
        [JsonProperty("diagnoseName")]
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 病历信息，HIS能采集的所有本次就诊的门诊病历信息。最多400个汉字
        /// </summary>      
        [JsonProperty("medicalRecord")]
        public string MedicalRecord { get; set; }

        /// <summary>
        /// 就诊科别代码
        /// </summary>       
        [JsonProperty("sectionCode")]
        public string SectionCode { get; set; }

        /// <summary>
        /// 就诊科别名称
        /// </summary>       
        [JsonProperty("sectionName")]
        public string SectionName { get; set; }

        /// <summary>
        /// 本院科别名称
        /// </summary>     
        [JsonProperty("hisSectionName")]
        public string HisSectionName { get; set; }

        /// <summary>
        /// 医师编码，北京市卫生局标准15位，部队16位，严禁使用汉字。
        /// </summary>      
        [JsonProperty("doctorId")]
        public string DoctorId { get; set; }

        /// <summary>
        /// 医师名称
        /// </summary>        
        [JsonProperty("doctorName")]
        public string DoctorName { get; set; }

        /// <summary>
        /// 处方类型：1-医保内处方；2-医保外处方
        /// </summary>     
        [JsonProperty("recipeType")]
        public int RecipeType { get; set; }

        /// <summary>
        /// 代开药标识：0：普通处方；1:代开药处方。不需要区分代开药的医院。
        /// </summary>       
        [JsonProperty("helpMedicineFlag")]      
        public string HelpMedicineFlag { get; set; }

        /// <summary>
        /// 备注
        /// </summary>      
        [JsonProperty("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 挂号交易流水号，本次门诊对应的挂号交易流水号
        /// </summary>       
        [JsonProperty("registerTradeNumber")]
        public string RegisterTradeNumber { get; set; }

        /// <summary>
        /// 单据类型，本次互联网复诊费结算的单据类型；1-互联网复诊费
        /// </summary>
        [JsonProperty("billsType")]      
        public string BillsType { get; set; }
    }
}
