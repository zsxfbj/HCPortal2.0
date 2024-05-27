using System;
using System.Threading;
using HC.BLL;
using HC.BLL.HC;
using HC.Entity;
using HC.Model.DTO;
using HC.Model.VO;
using HC.Utitily;
using Newtonsoft.Json;
using WebRegComLib;

namespace HC.WinService
{
    /// <summary>
    /// 
    /// </summary>
    public class MainJobThread
    {

        public event ShowMessageEventHandler OnShowMessage;

        protected void ShowMessage(String message)
        {
            if (OnShowMessage != null)
            {
                OnShowMessage(message);
            }

        }

        #region Fields

        private bool _running = true;


        #endregion

        #region Public Methods

        #region public void Stop()

        /// <summary>
        ///     线程停止
        /// </summary>
        public void Stop()
        {
            _running = false;
        }

        #endregion public void Stop()

        #region public void Start()

        /// <summary>
        ///     线程开始工作
        /// </summary>
        public void Start()
        {
            _running = true;
            Thread thread = new Thread(Run);
            thread.IsBackground = true;
            thread.Start();           
        }

        #endregion public void Start()

        #endregion

        #region Private Methods


        #region private void run()

        private void Run()
        {
            LogHelper.WriteNotifyLogInfo("MainJobThread线程开始工作...");
            while (_running)
            {
                DoWork();
                Thread.Sleep(new TimeSpan(0, 0, 0, 0, 500));
            }
            LogHelper.WriteNotifyLogInfo("MainJobThread线程结束工作...");
        }

        #endregion private void run()

        #region public void doWork()

        // ReSharper disable InconsistentNaming
        public void DoWork()
        // ReSharper restore InconsistentNaming
        {
            //先读取数据库
            try
            {
                SubmitLog submitLog = BSubmitLog.GetInstance().GetSubmitLogByFlag(0);
                if(submitLog != null)
                {
                    DivideTradeResultVO divideTradeResult = new DivideTradeResultVO();
                    
                    //开始拆分任务
                    WebRegClass cd = new WebRegClass();
                    if(submitLog.SubmitType == 1)
                    {
                        DivideReqDTO req = JsonConvert.DeserializeObject<DivideReqDTO>(submitLog.SubmitContent);

                        ActionLog step1 = new ActionLog
                        {
                            SubmitId = submitLog.Id,
                            Step = 1,
                            ActionName = "GetPerson",
                            RequestData = BActionXml.GetInstance().GetPersonWebXml(req.Person)
                        };

                        cd.GetPersonInfo_Web(step1.RequestData, out string outXml);

                        step1.ResponseData = outXml;
                        BActionLog.GetInstance().Insert(step1);


                        ComResultVO<PersonVO> personResult = BActionXml.GetInstance().GetPersonVO(outXml);

                        if (personResult.State.Equals("success"))
                        {
                            //费用分解
                            ActionLog step2 = new ActionLog
                            {
                                SubmitId = submitLog.Id,
                                Step = 2,
                                ActionName = "Divide",
                                RequestData = BActionXml.GetInstance().GetDivideFeeXml(req)
                            };

                            cd.Divide_Web(step2.RequestData, out outXml);
                            
                            step2.ResponseData = outXml;
                            BActionLog.GetInstance().Insert(step2);

                            ComResultVO<TradeVO> tradeResult = BActionXml.GetInstance().GetTradeVO(outXml);

                            if(tradeResult.State.Equals("success"))
                            {
                                //交易确认
                                ActionLog step3 = new ActionLog
                                {
                                    SubmitId = submitLog.Id,
                                    Step = 3,
                                    ActionName = "Trade" 
                                };
                                cd.Trade_Web(out outXml);
                                step3.ResponseData = outXml;
                                BActionLog.GetInstance().Insert(step3);                              
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLogInfo(@"医保com处理服务错误", ex.Message);
                ShowMessage("医保com处理服务出现错误，等待10秒后，继续尝试提交……");
                Thread.Sleep(10000);
            }
        }

        #endregion private void doWork()

        #endregion Private Methods   

    }
}
