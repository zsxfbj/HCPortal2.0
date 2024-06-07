using System.Collections.Generic;
using Newtonsoft.Json;

namespace HC.Model.VO
{
    /// <summary>
    /// 交易信息
    /// </summary>
    public class TradeDivideVO
    {
        /// <summary>
        /// 交易信息
        /// </summary>
        [JsonProperty("tradeinfo")]
        public TradeVO Trade { get; set; }

        /// <summary>
        /// 汇总支付信息
        /// </summary>
        [JsonProperty("sumpay")]
        public SummaryPayVO SummaryPay { get; set; }

        /// <summary>
        /// 支付信息
        /// </summary>
        [JsonProperty("payinfo")]
        public PaymentVO Payment { get; set; }

        /// <summary>
        /// 明细分解信息列表
        /// </summary>
        [JsonProperty("feeitemarray")]
        public FeeItemsVO FeeItems { get; set; }

        /// <summary>
        /// 分类汇总信息
        /// </summary>
        [JsonProperty("medicatalog")]
        public MedicineCatalogVO MedicineCatalog { get; set; }

        /// <summary>
        /// 新单据分类汇总信息
        /// </summary>
        [JsonProperty("medicatalog2")]
        public MedicineCatalog2VO MedicineCatalog2 { get; set; }

    }
}