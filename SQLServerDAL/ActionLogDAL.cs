using System.Data;
using System.Data.SqlClient;
using HC.Entity;

namespace HC.SQLServerDAL
{
    /// <summary>
    /// 数据访问类
    /// </summary>
    public class ActionLogDAL
    {
        private const string SQL_INSERT_ACTION_LOG = "INSERT INTO [ActionLog] ([RequestId],[Step],[ActionName],[RequestData],[ResponseData],[CreateTime]) VALUES ( @RequestId, @Step, @ActionName,@RequestData, @ResponseData,GETDATE())";

        private const string PARM_REQUEST_ID = "@RequestId";

        private const string PARM_STEP = "@Step";

        private const string PARM_ACTION_NAME = "@ActionName";

        private const string PARM_REQUEST_DATA = "@RequestData";

        private const string PARM_RESPONSE_DATE = "@ResponseData";

        #region public void Insert(ActionLog entity)
        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(ActionLog entity)
        {
            SqlParameter[] parms;
            if (ParamsCache.GetCachedParameterSet(Database.CONN_STRING_NON_DTC, "SQL_INSERT_ACTION_LOG", out parms))
            {
                Database.AssignParameterValues(parms, entity.RequestId, entity.Step, entity.ActionName, entity.RequestData, entity.ResponseData);
            }
            else
            {
                parms = new SqlParameter[] {
                    new SqlParameter(PARM_REQUEST_ID, SqlDbType.VarChar,128),
                    new SqlParameter(PARM_STEP, SqlDbType.Int),
                    new SqlParameter(PARM_ACTION_NAME, SqlDbType.VarChar, 32),
                    new SqlParameter(PARM_REQUEST_DATA, SqlDbType.NText),
                    new SqlParameter(PARM_RESPONSE_DATE, SqlDbType.NText)
                };

                parms[0].Value = entity.RequestId;
                parms[1].Value = entity.Step;
                parms[2].Value = entity.ActionName;
                parms[3].Value = entity.RequestData;
                parms[4].Value = entity.ResponseData;

                ParamsCache.CacheParameterSet(Database.CONN_STRING_NON_DTC, "SQL_INSERT_ACTION_LOG", parms);
            }
            Database.ExecuteNonQuery(Database.CONN_STRING_NON_DTC, CommandType.Text, SQL_INSERT_ACTION_LOG, parms);
        }
        #endregion public void Insert(ActionLog entity)

    }
}
