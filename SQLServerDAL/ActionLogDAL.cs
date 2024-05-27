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
        private const string SQL_INSERT_ACTION_LOG = "INSERT INTO [ActionLog] ([SubmitId],[Step],[ActionName],[RequestData],[ResponseData],[CreateTime]) VALUES ( @SubmitId, @Step, @ActionName,@RequestData, @ResponseData,GETDATE())";

        private const string PARM_SUBMIT_ID = "@SubmitId";

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
            if(ParamsCache.GetCachedParameterSet(Database.CONN_STRING_NON_DTC, "SQL_INSERT_ACTION_LOG", out parms))
            {
                Database.AssignParameterValues(parms, entity.SubmitId, entity.Step, entity.ActionName, entity.RequestData, entity.ResponseData);
            }
            else
            {
                parms = new SqlParameter[5];
                parms[0] = new SqlParameter(PARM_SUBMIT_ID, System.Data.SqlDbType.BigInt);
                parms[0].Value = entity.SubmitId;

                parms[1] = new SqlParameter(PARM_STEP, System.Data.SqlDbType.Int);
                parms[1].Value = entity.Step;

                parms[2] = new SqlParameter(PARM_ACTION_NAME, System.Data.SqlDbType.VarChar, 32);
                parms[2].Value = entity.ActionName;

                parms[3] = new SqlParameter(PARM_REQUEST_DATA, System.Data.SqlDbType.NText);
                parms[3].Value = entity.RequestData;

                parms[4] = new SqlParameter(PARM_RESPONSE_DATE, System.Data.SqlDbType.NText);
                parms[4].Value = entity.ResponseData;
                ParamsCache.CacheParameterSet(Database.CONN_STRING_NON_DTC, "SQL_INSERT_ACTION_LOG", parms);
            }
            Database.ExecuteNonQuery(Database.CONN_STRING_NON_DTC, CommandType.Text, SQL_INSERT_ACTION_LOG, parms);
        }
        #endregion public void Insert(ActionLog entity)

    }
}
