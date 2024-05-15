using HC.Utility.Converter;
using Newtonsoft.Json;

namespace HC.Model.VO
{
    /// <summary>
    /// 明细分解信息
    /// </summary>
    public class FeeItemVO
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
        /// 医保项目代码：药品、诊疗项目或服务设施医保编码
        /// </summary>    
        [JsonProperty("itemCode")]
        public string ItemCode { get; set; }

        /// <summary>
        /// 医保项目名称，本医院项目名称
        /// </summary>            
        [JsonProperty("itemName")]
        public string ItemName { get; set; }

        /// <summary>
        /// 项目类型：0-药品 1-诊疗项目 2-服务设施
        /// </summary>      
        [JsonProperty("itemType")]
        public int ItemType { get; set; }

        /// <summary>
        /// 单价，最多4位小数
        /// </summary>        
        [JsonProperty("unitPrice")]
        [JsonConverter(typeof(DecimalConverter))]
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
        /// 医保内总金额，精确到小数点2位
        /// </summary>
        [JsonProperty("inInsuranceFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal InInsuranceFee { get; set; }

        /// <summary>
        /// 医保外总金额，精确到小数点2位
        /// </summary>
        [JsonProperty("outInsuranceFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal OutInsuranceFee { get; set; }

        /// <summary>
        /// 个人自付2金额
        /// </summary>
        [JsonProperty("selfPayFee")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal SelfPayFee { get; set; }

        /// <summary>
        /// 分解状态，0-正常，1-不符合特殊标识，2-医保目录内不存在，3-对照错误，4-不符合特殊定额管理要求，5-未对照，6-医保外处方
        /// </summary>
        [JsonProperty("state")]
        public int State { get; set; }

        /// <summary>
        /// 收费类别，参见标准AKA063
        /// </summary>
        [JsonProperty("feeType")]
        public string FeeType { get; set; }

        /// <summary>
        /// 社区优惠金额
        /// </summary>
        [JsonConverter(typeof(DecimalConverter))]
        [JsonProperty("preferentialFee")]
        public decimal PreferentialFee { get; set; }

        /// <summary>
        /// 整数，如优惠10%，则表示为“10
        /// </summary>       
        [JsonProperty("preferentialScale")]
        public int PreferentialScale { get; set; }

    }
}