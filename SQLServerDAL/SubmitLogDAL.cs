using System;
using System.Data;
using System.Data.SqlClient;
using HC.Entity;

namespace HC.SQLServerDAL
{
    /// <summary>
    /// 
    /// </summary>
    public class SubmitLogDAL
    {
        private const string SQL_UPDATE_SUBMIT_LOG = "UPDATE [SubmitLog] SET [ResultContent] = @ResultContent, [Flag] = @Flag, [UpdateTime] = GETDATE() WHERE [RequestId] = @RequestId";

        private const string SQL_SINGLE_SUBMIT_LOG_BY_FLAG = "SELECT TOP 1  [RequestId], [SubmitType], [SubmitContent],[ResultContent],[Flag],[CreateTime],[UpdateTime] FROM [SubmitLog] WHERE [Flag] = @Flag ORDER BY [CreateTime]";

        private const string PARM_REQUEST_ID = "@RequestId";

        private const string PARM_RESULT_CONTENT = "@ResultContent";

        private const string PARM_FLAG = "@Flag";

        #region public void Update(string resultContent, int flag, string requestId)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultContent">需要返回的内容</param>
        /// <param name="flag">标记</param>
        /// <param name="requestId">操作Id</param>
        public void Update(string resultContent, int flag, string requestId)
        {
            SqlParameter[] parms;
            if (ParamsCache.GetCachedParameterSet(Database.CONN_STRING_NON_DTC, "SQL_UPDATE_SUBMIT_LOG", out parms))
            {
                Database.AssignParameterValues(parms, resultContent,flag, requestId);
            }
            else
            {
                parms = new SqlParameter[]{
                    new SqlParameter(PARM_RESULT_CONTENT, SqlDbType.NText),
                    new SqlParameter(PARM_FLAG, SqlDbType.Int),
                    new SqlParameter(PARM_REQUEST_ID, SqlDbType.VarChar,128)

                };
                parms[0].Value = resultContent;
                parms[1].Value  = flag;
                parms[2].Value = requestId;

                ParamsCache.CacheParameterSet(Database.CONN_STRING_NON_DTC, "SQL_UPDATE_SUBMIT_LOG", parms);
            }
            Database.ExecuteNonQuery(Database.CONN_STRING_NON_DTC, CommandType.Text, SQL_UPDATE_SUBMIT_LOG, parms);
        }
        #endregion public void Update(string resultContent, int flag, long id)

        #region public SubmitLog GetSubmitLogByFlag(int flag)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public SubmitLog GetSubmitLogByFlag(int flag)
        {
            SqlParameter parm;
            if(ParamsCache.GetCachedParameterSet(Database.CONN_STRING_NON_DTC, "SQL_SINGLE_SUBMIT_LOG_BY_FLAG", out parm
                                ))
            {
                parm.Value = flag;
            }
            else
            {
                parm = new SqlParameter(PARM_FLAG, System.Data.SqlDbType.Int);
                parm.Value = flag;
                ParamsCache.CacheParameterSet(Database.CONN_STRING_NON_DTC, "SQL_SINGLE_SUBMIT_LOG_BY_FLAG", parm);
            }

            using(SqlDataReader dr = Database.ExecuteReader(Database.CONN_STRING_NON_DTC, CommandType.Text, SQL_SINGLE_SUBMIT_LOG_BY_FLAG, parm))
            {
                if (dr.Read())
                {
                    SubmitLog entity = new SubmitLog
                    {
                        RequestId = dr.IsDBNull(0) ? "" : dr.GetString(0),
                        SubmitType = dr.IsDBNull(1) ? 0 : dr.GetInt32(1),                       
                        SubmitContent = dr.IsDBNull(2) ? "" : dr.GetString(2),
                        ResultContent = dr.IsDBNull(3) ? "" : dr.GetString(3),
                        Flag = dr.IsDBNull(4) ? 0 : dr.GetInt32(4),
                        CreateTime = dr.IsDBNull(5) ? DateTime.Now : dr.GetDateTime(5),
                        UpdateTime = dr.IsDBNull(6) ? DateTime.Now : dr.GetDateTime(6)
                    };
                    return entity;
                }

                return null;
            }            
        }
        #endregion public SubmitLog GetSubmitLogByFlag(int flag)
    }
}
