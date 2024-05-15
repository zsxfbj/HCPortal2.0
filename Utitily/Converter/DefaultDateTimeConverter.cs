using Newtonsoft.Json.Converters;

namespace HC.Utility.Converter
{
    /// <summary>
    /// 自定义的时间格式转化类
    /// </summary>
    public class DefaultDateTimeConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DefaultDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }
    }
}