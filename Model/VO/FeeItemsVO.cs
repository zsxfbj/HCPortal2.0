using Newtonsoft.Json;

namespace HC.Model.VO
{
    public class FeeItemsVO
    {
        /// <summary>
        /// 费用项目明细
        /// </summary>
        [JsonProperty("feeitem")]
        public FeeItemVO FeeItem { get; set; }
    }
}
