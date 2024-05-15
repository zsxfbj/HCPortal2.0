using System;

namespace HC.Entity
{
    /// <summary>
    /// 请求日志
    /// </summary>
    public class RequestLog
    {
        private long _id;

        private DateTime _requestTime;

        private string _clientIp;

        private string _userAgent;



        /// <summary>
        /// LogId，自增
        /// </summary>
        public long Id { get { return _id; } set { _id = value; } }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime { get {  return _requestTime; } set { _requestTime = value; } }

        /// <summary>
        /// 请求的客户端Id
        /// </summary>
        public string ClientIp { get { return _clientIp; } set { _clientIp = value; } }

        /// <summary>
        /// 客户端类型
        /// </summary>
        public string UserAgent { get { return _userAgent; } set { _userAgent = value; } }
    }
}
