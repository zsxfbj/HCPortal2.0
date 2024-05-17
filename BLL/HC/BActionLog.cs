using HC.Entity;
using HC.SQLServerDAL;
using HC.Utitily;

namespace HC.BLL.HC
{
    /// <summary>
    /// 
    /// </summary>
    public class BActionLog : Singleton<BActionLog>
    {

        private readonly static ActionLogDAL dal = new ActionLogDAL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(ActionLog entity)
        {
            dal.Insert(entity); 
        }

    }
}
