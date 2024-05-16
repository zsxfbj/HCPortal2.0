using System;
using System.Data.SqlClient;
using HC.Entity;

namespace HC.SQLServerDAL
{
    /// <summary>
    /// 
    /// </summary>
    public class SubmitLogDAL
    {
        private const string SQL_UPDATE_SUBMIT_LOG = "UPDATE [SubmitLog] SET [ResultContent] = @ResultContent, [Flag] = @Flag, [UpdateTime] = GETDATE() WHERE [Id] = @Id";

        private const string SQL_SINGLE_SUBMIT_LOG_BY_FLAG = "SELECT TOP 1 [Id], [ClientIp], [SubmitType], [SubmitContent],[ResultContent],[Flag],[CreateTime],[UpdateTime] FROM [SubmitLog] WHERE [Flag] = @Flag ORDER BY [Id]";

        private const string PARM_ID = "@Id";

        private const string PARM_RESULT_CONTENT = "@ResultContent";

        private const string PARM_FLAG = "@Flag";

        #region public void Update(string resultContent, int flag, long id)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultContent">需要返回的内容</param>
        /// <param name="flag">标记</param>
        /// <param name="id">操作Id</param>
        public void Update(string resultContent, int flag, long id)
        {
            SqlParameter[] parms;
            if (ParamsCache.GetCachedParameterSet(Database.CONN_STRING_NON_DTC, "SQL_UPDATE_SUBMIT_LOG", out parms))
            {
                Database.AssignParameterValues(parms, resultContent,flag, id);
            }
            else
            {
                parms = new SqlParameter[3];
                parms[0] = new SqlParameter(PARM_RESULT_CONTENT, System.Data.SqlDbType.NText);
                parms[0].Value = resultContent;

                parms[1] = new SqlParameter(PARM_FLAG, System.Data.SqlDbType.Int);
                parms[1].Value = flag;

                parms[2] = new SqlParameter(PARM_ID, System.Data.SqlDbType.BigInt);
                parms[2].Value = id;

                ParamsCache.CacheParameterSet(Database.CONN_STRING_NON_DTC, "SQL_UPDATE_SUBMIT_LOG", parms);
            }
            Database.ExecuteNonQuery(Database.CONN_STRING_NON_DTC, SQL_UPDATE_SUBMIT_LOG, parms);
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

            using(SqlDataReader dr = Database.ExecuteReader(Database.CONN_STRING_NON_DTC, SQL_SINGLE_SUBMIT_LOG_BY_FLAG, parm))
            {
                if (dr.Read())
                {
                    SubmitLog entity = new SubmitLog
                    {
                        Id = dr.IsDBNull(0) ? 0L : dr.GetInt64(0),
                        ClientIp = dr.IsDBNull(1) ? "" : dr.GetString(1),
                        SubmitType = dr.IsDBNull(2) ? 0 : dr.GetInt32(2),
                        SubmitContent = dr.IsDBNull(3) ? "" : dr.GetString(3),
                        ResultContent = dr.IsDBNull(4) ? "" : dr.GetString(4),
                        Flag = dr.IsDBNull(5) ? 0 : dr.GetInt32(5),
                        CreateTime = dr.IsDBNull(6) ? DateTime.Now : dr.GetDateTime(6),
                        UpdateTime = dr.IsDBNull(7) ? DateTime.Now : dr.GetDateTime(7)
                    };
                    return entity;
                }

                return null;
            }            
        }
        #endregion public SubmitLog GetSubmitLogByFlag(int flag)
    }
}
