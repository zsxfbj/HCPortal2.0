using System.Collections.Generic;
using HC.Model.VO;
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
        /// 处方类型：1-医保内处方；2-医保外处方
        /// </summary>        
        [JsonProperty("recipeType")]
        public int RecipeType { get; set; }

        /// <summary>
        /// 处方时间，格式为yyyyMMddHHmmss
        /// </summary>      
        [JsonProperty("recipeDate")]
        public int RecipeDate { get; set; }

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
        /// 项目总金额，该项目总金额，最多4位小数
        /// </summary>       
        [JsonConverter(typeof(DecimalConverter))]
        [JsonProperty("fee")]
        public decimal Fee { get; set; }
    }
}
