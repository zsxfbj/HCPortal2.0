using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace HC.Utility.Converter
{
    /// <summary>
    /// Decimal数据类JSON序列化和反序列化的约定
    /// </summary>
    public class DecimalConverter : JsonConverter
    {
        /// <summary>
        ///     读取JSON
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Null && objectType == typeof(decimal?))
            {
                return decimal.Zero;
            }
            if (token.Type == JTokenType.Float || token.Type == JTokenType.Integer)
            {
                return token.ToObject<decimal>();
            }
            if (token.Type == JTokenType.String)
            {                
                // customize this to suit your needs
                decimal.TryParse(token.ToString(), out decimal outValue);
                return outValue;
            }           
            throw new JsonSerializationException("Unexpected token type: " + token.Type);
        }

        /// <summary>
        ///     判断是否可以转换
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(decimal) || objectType == typeof(decimal?));
        }

        /// <summary>
        ///     写到JSON里
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                decimal.TryParse(value.ToString(), out decimal destValue);
                serializer.Serialize(writer, destValue.ToString("#0.####"));
            }
            else
            {
                serializer.Serialize(writer, "");
            }
        }
    }
}