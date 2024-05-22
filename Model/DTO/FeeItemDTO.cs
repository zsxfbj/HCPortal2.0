using HC.Utility.Converter;
using Newtonsoft.Json;

namespace HC.Model.DTO
{
    /// <summary>
    /// 明细信息
    /// </summary>
    public class FeeItemDTO
    {
        /// <summary>
        /// 项目序号，为顺序号
        /// </summary>         
        [JsonProperty("itemNumber")]
        public int ItemNumber { get; set; }

        /// <summary>
        /// 处方序号
        /// </summary>      
        [JsonProperty("recipeNumber")]
        public string RecipeNumber { get; set; }

        /// <summary>
        /// HIS项目代码：药品、诊疗项目或服务设施编码
        /// </summary>       
        [JsonProperty("hisCode")]
        public string HisCode { get; set; }

        /// <summary>
        /// HIS项目名称，本医院项目名称
        /// </summary>      
        [JsonProperty("itemName")]
        public string ItemName { get; set; }

        /// <summary>
        /// 项目类型：0-药品；1-诊疗项目和服务设施
        /// </summary>       
        [JsonProperty("itemType")]
        public int ItemType { get; set; }

        /// <summary>
        /// 单价，最多4位小数
        /// </summary>       
        [JsonConverter(typeof(DecimalConverter))]
        [JsonProperty("unitPrice")]       
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 数量，最多两位小数，适用于中药饮片需要精确到小数的情况
        /// </summary>        
        [JsonConverter(typeof(DecimalConverter))]       
        [JsonProperty("count")]
        public decimal Count { get; set; }

        /// <summary>
        /// 项目总金额，该项目总金额，最多4位小数
        /// </summary>       
        [JsonConverter(typeof(DecimalConverter))]     
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// 剂型，参见字典，取值须在字典范围内，且严禁使用汉字。
        /// </summary>       
        [JsonProperty("dose")]
        public string Dose { get; set; }

        /// <summary>
        /// 规格
        /// </summary>       
        [JsonProperty("specification")]
        public string Specification { get; set; }

        /// <summary>
        /// 单位
        /// </summary>       
        [JsonProperty("unit")]
        public string Unit { get; set; }

        /// <summary>
        /// 用法（用药频次）,参见字典 BKC229。药品非空，诊疗服务设施可空。取值须在字典范围内，且严禁使用汉字
        /// </summary>    
        [JsonProperty("howToUse")]
        public string HowToUse { get; set; }

        /// <summary>
        /// 单次用量，药品非空，诊疗服务设施可空（数值型，含小数点最大20位）
        /// </summary>
        //[MaxLength(20, ErrorMessage = "单次用量最多20个字符")]
        [JsonProperty("dosage")]
        public string Dosage { get; set; }

        /// <summary>
        /// 包装单位，门诊零售包装单位，药品非空，诊疗服务设施可空
        /// </summary>       
        [JsonProperty("packaging")]
        public string Packaging { get; set; }

        /// <summary>
        /// 最小包装，最小（住院）零售包装单位，药品非空，诊疗服务设施可空
        /// </summary>      
        [JsonProperty("minPackage")]
        public string MinPackage { get; set; }

        /// <summary>
        /// 换算率，包装单位与最小包装单位之间的换算率，药品非空，诊疗服务设施可空； （数值型，含小数点最大10位）
        /// </summary>        
        [JsonProperty("conversion")]
        public string Conversion { get; set; }

        /// <summary>
        /// 用药天数
        /// </summary>
        [JsonProperty("days")]      
        public int Days { get; set; }

        /// <summary>
        /// 生育费用标识：0-普通费用；1-生育类费用；对生育类门诊费用没有特殊处理的医院，可以不生成该节点；如使用并生成该节点，则不能为空。
        /// </summary>       
        [JsonProperty("babyFlag")]
        public string BabyFlag { get; set; }

        /// <summary>
        /// 药品批准文号，当项目类别为0-药品时：传入本条药品费用对应的药品包装上的批准文号（其中，中药饮片及颗粒无需传入）；当项目类别为1诊疗项目和服务设施时：无需传入。
        /// </summary>
        [JsonProperty("drugApprovalNumber")]       
        public string DrugApprovalNumber { get; set; }


    }
}
