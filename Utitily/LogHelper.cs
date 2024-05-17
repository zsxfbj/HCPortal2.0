using System;
using log4net;

namespace HC.Utitily
{
    /// <summary>
    /// Log日志类
    /// </summary>
    public sealed class LogHelper
    {
        private LogHelper(){}

        private static readonly ILog logErr = LogManager.GetLogger("error");
        private static readonly ILog logDeal = LogManager.GetLogger("deal");
        private static readonly ILog logNoti = LogManager.GetLogger("noti");

        #region public static void WriteErrorLogInfo(string spot, Exception ex )

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spot"></param>
        /// <param name="ex"></param>
        public static void WriteErrorLogInfo(string spot, Exception ex)
        {
            logErr.Info("***ERROR AT=" + spot + ", MSG=" + ex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spot"></param>
        /// <param name="ex"></param>
        public static void WriteErrorLogInfo(string spot, string ex)
        {
            logErr.Info("***ERROR AT=" + spot + ", MSG=" + ex);
        }

        #endregion public static void WriteErrorLogInfo(string spot, Exception ex )

        #region public static void WriteDealLogInfo(string dealinfo)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dealinfo"></param>
        public static void WriteDealLogInfo(string dealinfo)
        {
            logDeal.Info(dealinfo);
        }

        #endregion public static void WriteDealLogInfo(string dealinfo)

        #region public static void WriteNotifyLogInfo(string notiinfo)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notiinfo"></param>
        public static void WriteNotifyLogInfo(string notiinfo)
        {
            logNoti.Info(notiinfo);
        }

        #endregion public static void WriteNotifyLogInfo(string notiinfo)
    }
}
