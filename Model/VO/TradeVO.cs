using System.Collections.Generic;
using Newtonsoft.Json;

namespace HC.Model.VO
{
    /// <summary>
    /// 交易信息
    /// </summary>
    public class TradeVO
    {
        /// <summary>
        /// 交易流水
        /// </summary>
        [JsonProperty("tradeNumber")]
        public string TradeNumber { get; set; }

        /// <summary>
        /// 收费单据号
        /// </summary>
        [JsonProperty("feeNumber")]
        public string FeeNumber { get; set; }

        /// <summary>
        /// 交易日期时间，格式为：yyyyMMddHHmmss
        /// </summary>
        [JsonProperty("tradeDate")]
        public string TradeDate { get; set; }

        /// <summary>
        /// 汇总支付信息
        /// </summary>
        [JsonProperty("summaryPay")]
        public SummaryPayVO SummaryPay { get; set; }

        /// <summary>
        /// 支付信息
        /// </summary>
        [JsonProperty("payment")]
        public PaymentVO Payment { get; set; }

        /// <summary>
        /// 明细分解信息列表
        /// </summary>
        [JsonProperty("feeItems")]
        public List<FeeItemVO> FeeItems { get; set; }

        /// <summary>
        /// 分类汇总信息
        /// </summary>
        [JsonProperty("medicineCatalog")]
        public MedicineCatalogVO MedicineCatalog { get; set; }

        /// <summary>
        /// 新单据分类汇总信息
        /// </summary>
        [JsonProperty("medicineCatalog2")]
        public MedicineCatalog2VO MedicineCatalog2 { get; set; }

    }
}