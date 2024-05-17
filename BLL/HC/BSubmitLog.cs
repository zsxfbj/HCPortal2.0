using HC.Entity;
using HC.SQLServerDAL;
using HC.Utitily;

namespace HC.BLL.HC
{
    /// <summary>
    /// 
    /// </summary>
    public class BSubmitLog : Singleton<BSubmitLog>
    {
        private readonly static SubmitLogDAL dal = new SubmitLogDAL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public SubmitLog GetSubmitLogByFlag(int flag)
        {
            return dal.GetSubmitLogByFlag(flag);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultContent"></param>
        /// <param name="flag"></param>
        /// <param name="id"></param>
        public void Update(string resultContent, int flag, long id)
        {
            dal.Update(resultContent, flag, id);
        }
    }
}
