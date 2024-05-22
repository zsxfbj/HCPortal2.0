using System.Collections.Generic;
using HC.Utility.Converter;
using Newtonsoft.Json;

namespace HC.Model.DTO
{
    /// <summary>
    /// 费用分解请求
    /// </summary>
    public class DivideReqDTO
    {
        /// <summary>
        /// 参保人员信息
        /// </summary>
        [JsonProperty("person")]
        public PersonInfoReqDTO Person { get; set; }

        /// <summary>
        /// 社保卡卡号
        /// </summary>        
        [JsonProperty("cardNumber")]
        public string CardNumber { get; set; }

        /// <summary>
        /// 医疗类别，此处目前固定为17（互联网复诊费结算）
        /// </summary>      
        [JsonProperty("cureType")]
        public string CureType { get; set; }

        /// <summary>
        /// 就诊方式，目前都是0普通，取值须在字典范围内，且严禁使用汉字
        /// </summary>       
        [JsonProperty("illType")]
        public string IllType { get; set; }

        /// <summary>
        /// 收费单据号
        /// </summary>
        [JsonProperty("feeNumber")]       
        public string FeeNumber { get; set; }

        /// <summary>
        /// 收费员
        /// </summary>
        [JsonProperty("operator")]      
        public string Operator { get; set; }


        /// <summary>
        /// 处方时间，格式为yyyyMMddHHmmss
        /// </summary>      
        [JsonProperty("recipeDate")]
        public string RecipeDate { get; set; }

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
        /// 单价，最多4位小数
        /// </summary>       
        [JsonConverter(typeof(DecimalConverter))]
        [JsonProperty("unitPrice")]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 项目总金额，该项目总金额，最多4位小数
        /// </summary>       
        [JsonConverter(typeof(DecimalConverter))]
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// 处方信息列表
        /// </summary>
        [JsonProperty("recipes")]
        public List<RecipeDTO> Recipes { get; set; }

        /// <summary>
        /// 明细信息列表
        /// </summary>
        [JsonProperty("feeItems")]
        public List<FeeItemDTO> FeeItems { get; set; }

    }
}
