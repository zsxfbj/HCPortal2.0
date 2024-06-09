using System;
using HC.BLL.HC;
using HC.BLL;
using HC.Entity;
using HC.Model.DTO;
using HC.Model.VO;
using HC.Utitily;
using Newtonsoft.Json;
using System.Threading;

namespace HC.WinService
{
    /// <summary>
    /// 测试工作模式
    /// </summary>
    public class TestJobThread
    {
        /// <summary>
        /// 消息事件委托
        /// </summary>
        public event ShowMessageEventHandler OnShowMessage;

        /// <summary>
        /// 处理消息事件方法
        /// </summary>
        /// <param name="message"></param>
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
            LogHelper.WriteNotifyLogInfo("TestJobThread线程开始工作...");
            while (_running)
            {
                DoWork();
                Thread.Sleep(new TimeSpan(0, 0, 0, 0, 500));
            }
            LogHelper.WriteNotifyLogInfo("TestJobThread线程结束工作...");
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
                if (submitLog != null)
                {
                    //开始拆分任务                   
                    if (submitLog.SubmitType == 1)
                    {
                        //DivideTradeResultVO divideTradeResult = new DivideTradeResultVO();

                        DivideReqDTO req = JsonConvert.DeserializeObject<DivideReqDTO>(submitLog.SubmitContent);

                        ActionLog step1 = new ActionLog
                        {
                            SubmitId = submitLog.Id,
                            Step = 1,
                            ActionName = "GetPersonInWeb",
                            RequestData = BActionXml.GetInstance().GetPersonWebXml(req.Person)
                        };
                        ComResultVO<PersonVO> personResult = new ComResultVO<PersonVO>
                        {
                            State = new StateVO { Success = "true"},
                            Output = new PersonVO
                            {
                                Birthday = req.Person.Birthday,
                                IdNumber = req.Person.IdNumber,
                                CardNumber = req.Person.CardNumber,
                                PersonName = req.Person.PersonName,
                                PersonType = "",
                                FromHospitalDate = "18991230",
                                Sex = req.Person.Sex,
                                FundType = "91",
                                IcNumber = req.Person.CardNumber,
                                IsSpecifiedHospital = "1",
                                IsChronicHospital = "false",
                                IsInRedList = "true"
                            }
                        };

                        step1.ResponseData = JsonConvert.SerializeObject(personResult);

                        ShowMessage("GetPersonInWeb模拟请求返回结果：" + step1.RequestData);

                        BActionLog.GetInstance().Insert(step1);

                        if (personResult.State.Equals("true"))
                        {
                            //费用分解
                            ActionLog step2 = new ActionLog
                            {
                                SubmitId = submitLog.Id,
                                Step = 2,
                                ActionName = "DivideInWeb",
                                RequestData = BActionXml.GetInstance().GetDivideFeeXml(req)
                            };

                            ComResultVO<TradeDivideVO> tradeResult = new ComResultVO<TradeDivideVO>
                            {
                                State = new StateVO
                                {
                                   Success = "true"
                                }
                            };
                            tradeResult.Output = new TradeDivideVO
                            {
                                Trade = new TradeVO
                                {
                                    TradeNumber = IdHelper.IdWorker.NextId().ToString(),
                                    TradeDate = DateTime.Now.ToString("yyyyMMddHHmmss"),
                                    FeeNumber = req.FeeNumber
                                },
                                
                                SummaryPay = new SummaryPayVO
                                {
                                    TotalAmount = req.Fee,
                                    PersonAccountAmount = 0,
                                    CashAmount = req.Fee == 50 ? 10 : req.Fee,
                                    FundAmount = req.Fee == 50 ? 40 : 0
                                },
                                Payment = new PaymentVO
                                 {
                                     SelfPayAmount = req.Fee == 50 ? 10 : req.Fee,
                                     SupplementaryPayAmount = 0,
                                     BigPayAmount = req.Fee == 50 ? 40 : 0,
                                     FirstPayAmount = 0,
                                     TotalAmount = req.Fee,
                                     MilitaryPayAmount = 0,
                                     SelfBigPayAmount = 0,
                                     InInsuranceAmount = 0,
                                     OutInsuranceAmount = 0,
                                     OutOfBigPayAmount = 0
                                 },
                                 MedicineCatalog = new MedicineCatalogVO
                                 {

                                 },
                                 MedicineCatalog2 = new MedicineCatalog2VO
                                 {

                                 },
                                 FeeItems = new FeeItemsVO
                                 {
                                      FeeItem = new FeeItemVO
                                      {

                                      }
                                 }
                            };
                         
                            

                            step2.ResponseData = JsonConvert.SerializeObject(tradeResult);
                            BActionLog.GetInstance().Insert(step2);

                            

                            if (tradeResult.State.Equals("true"))
                            {
                                //交易确认
                                ActionLog step3 = new ActionLog
                                {
                                    SubmitId = submitLog.Id,
                                    Step = 3,
                                    ActionName = "Trade"
                                };
                                
                                step3.ResponseData = "";
                                BActionLog.GetInstance().Insert(step3);
                            }
                        }

                        //TODU 后续处理


                    }
                    else if (submitLog.SubmitType == 2)
                    {
                        RefundmentReqDTO req = JsonConvert.DeserializeObject<RefundmentReqDTO>(submitLog.SubmitContent);

                        ActionLog step1 = new ActionLog
                        {
                            SubmitId = submitLog.Id,
                            Step = 1,
                            ActionName = "GetPerson",
                            RequestData = BActionXml.GetInstance().GetPersonWebXml(req.Person)
                        };

                      

                        step1.ResponseData = "";
                        BActionLog.GetInstance().Insert(step1);


                        ComResultVO<PersonVO> personResult = new ComResultVO<PersonVO>();

                        if (personResult.State.Equals("true"))
                        {
                            //费用分解
                            ActionLog step2 = new ActionLog
                            {
                                SubmitId = submitLog.Id,
                                Step = 2,
                                ActionName = "Refundment",
                                RequestData = BActionXml.GetInstance().GetRefundmentXml(req)
                            };

                           

                            step2.ResponseData = "";
                            BActionLog.GetInstance().Insert(step2);

                            ComResultVO<RefundTradeVO> refundResult = new ComResultVO<RefundTradeVO>();

                            if (refundResult.State.Equals("true"))
                            {
                                //交易确认
                                ActionLog step3 = new ActionLog
                                {
                                    SubmitId = submitLog.Id,
                                    Step = 3,
                                    ActionName = "Trade"
                                };
                             
                                step3.ResponseData = "";
                                BActionLog.GetInstance().Insert(step3);
                            }
                        }

                        //TODU 后续处理
                    }
                    else
                    {
                        ActionLog step1 = new ActionLog
                        {
                            SubmitId = submitLog.Id,
                            Step = 1,
                            ActionName = "GetTradeState",
                            RequestData = submitLog.SubmitContent
                        };

                      

                        step1.ResponseData = "";
                        BActionLog.GetInstance().Insert(step1);


                        ComResultVO<TradeStateVO> tradeStateResult = new ComResultVO<TradeStateVO>();

                        //TODU 后续处理
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
