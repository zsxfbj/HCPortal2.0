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
    /// 主要的线程服务
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
                //每次轮询就处理一个医保分解请求或退费请求或查询交易结果请求
                SubmitLog submitLog = BSubmitLog.GetInstance().GetSubmitLogByFlag(0);
                if(submitLog != null)
                {                      
                    //开始拆分任务
                    WebRegClass cd = new WebRegClass();
                    if(submitLog.SubmitType == 1)
                    {                    
                        DivideReqDTO req = JsonConvert.DeserializeObject<DivideReqDTO>(submitLog.SubmitContent);

                        ActionLog step1 = new ActionLog
                        {
                            SubmitId = submitLog.Id,
                            Step = 1,
                            ActionName = "GetPersonInWeb",
                            RequestData = BActionXml.GetInstance().GetPersonWebXml(req.Person),
                            CreateTime = DateTime.Now
                        };

                        
                        LogHelper.WriteDealLogInfo("GetPersonInWeb请求：\r\n" + step1.RequestData);

                        ShowMessage("GetPersonInWeb请求：\r\n" + step1.RequestData);

                        cd.GetPersonInfo_Web(step1.RequestData, out string outXml);

                        LogHelper.WriteDealLogInfo("GetPersonInWeb返回结果：\r\n" + outXml);

                        ShowMessage("GetPersonInWeb返回结果：\r\n" + outXml);

                        ComResultVO<PersonVO> personResult = BActionXml.GetInstance().GetPersonVO(outXml);
                        step1.ResponseData = JsonConvert.SerializeObject(personResult);

                        //保存操作数据                        
                        BActionLog.GetInstance().Insert(step1);
                   
                        if (personResult.State.Success.Equals("true", StringComparison.OrdinalIgnoreCase))
                        {
                            //费用分解
                            ActionLog step2 = new ActionLog
                            {
                                SubmitId = submitLog.Id,
                                Step = 2,
                                ActionName = "DivideInWeb",
                                RequestData = BActionXml.GetInstance().GetDivideFeeXml(req),
                                CreateTime = DateTime.Now
                            };

                            LogHelper.WriteDealLogInfo("DivideInWeb请求：\r\n" + step2.RequestData);
                            ShowMessage("DivideInWeb请求：\r\n" + step2.RequestData);

                            cd.Divide_Web(step2.RequestData, out outXml);

                            LogHelper.WriteDealLogInfo("DivideInWeb返回结果：\r\n" + outXml);
                            ShowMessage("DivideInWeb返回结果：\r\n" + outXml);

                            ComResultVO<TradeDivideVO> tradeDivideResult = BActionXml.GetInstance().GetTradeVO(outXml);
                            step2.ResponseData = JsonConvert.SerializeObject(tradeDivideResult);
                            //保存操作数据                                                        
                            BActionLog.GetInstance().Insert(step2);
                                                       
                            if (tradeDivideResult.State.Success.Equals("true", StringComparison.OrdinalIgnoreCase))
                            {                   
                                //请求医保接口
                                cd.Trade_Web(out outXml);

                                LogHelper.WriteDealLogInfo("TradeInWeb返回结果：\r\n" + outXml);
                                ShowMessage("TradeInWeb返回结果：\r\n" + outXml);

                                ComResultVO<TradeResultVO> tradeResult = BActionXml.GetInstance().GetTradeResultVO(outXml);

                                //交易确认
                                ActionLog step3 = new ActionLog
                                {
                                    SubmitId = submitLog.Id,
                                    Step = 3,
                                    ActionName = "TradeInWeb",
                                    RequestData = "",
                                    ResponseData = JsonConvert.SerializeObject(tradeResult),
                                    CreateTime = DateTime.Now
                                };                                
                                BActionLog.GetInstance().Insert(step3);

                                if(tradeResult.State.Success.Equals("true", StringComparison.OrdinalIgnoreCase))
                                {
                                    //更新返回结果
                                    BSubmitLog.GetInstance().Update(step2.ResponseData, 1, submitLog.Id);
                                    return;
                                }

                                //更新返回结果
                                if (tradeResult.State.Error != null)
                                {
                                    BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(tradeResult.State.Error), 2, submitLog.Id);
                                    return;
                                }
                            }
                            //更新返回结果
                            if (tradeDivideResult.State.Error != null)
                            {
                                BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(tradeDivideResult.State.Error), 2, submitLog.Id);
                                return;
                            }
                        }
                        //更新返回结果
                        if (personResult.State.Error != null)
                        {
                            BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(personResult.State.Error), 2, submitLog.Id);
                            return;
                        }
                    }
                    else if(submitLog.SubmitType == 2)
                    {
                        RefundmentReqDTO req = JsonConvert.DeserializeObject<RefundmentReqDTO>(submitLog.SubmitContent);

                        ActionLog step1 = new ActionLog
                        {
                            SubmitId = submitLog.Id,
                            Step = 1,
                            ActionName = "GetPersonInWeb",
                            RequestData = BActionXml.GetInstance().GetPersonWebXml(req.Person),
                            CreateTime = DateTime.Now
                        };

                        LogHelper.WriteDealLogInfo("GetPersonInWeb请求：\r\n" + step1.RequestData);
                        ShowMessage("GetPersonInWeb请求：\r\n" + step1.RequestData);

                        cd.GetPersonInfo_Web(step1.RequestData, out string outXml);

                        LogHelper.WriteDealLogInfo("GetPersonInWeb返回结果：\r\n" + outXml);
                        ShowMessage("GetPersonInWeb返回结果：\r\n" + outXml);


                        ComResultVO<PersonVO> personResult = BActionXml.GetInstance().GetPersonVO(outXml);
                        step1.ResponseData = JsonConvert.SerializeObject(personResult);

                        //入库
                        BActionLog.GetInstance().Insert(step1);

                        if (personResult.State.Success.Equals("true", StringComparison.OrdinalIgnoreCase))
                        {
                            //费用分解
                            ActionLog step2 = new ActionLog
                            {
                                SubmitId = submitLog.Id,
                                Step = 2,
                                ActionName = "RefundmentInWeb",
                                RequestData = BActionXml.GetInstance().GetRefundmentXml(req),
                                CreateTime = DateTime.Now
                            };

                            LogHelper.WriteDealLogInfo("RefundmentInWeb请求：\r\n" + step2.RequestData);
                            ShowMessage("RefundmentInWeb请求：\r\n" + step2.RequestData);

                            cd.Refundment_Web(step2.RequestData, out outXml);

                            LogHelper.WriteDealLogInfo("RefundmentInWeb返回结果：\r\n" + outXml);

                            ShowMessage("RefundmentInWeb返回结果：\r\n" + outXml);

                            ComResultVO<RefundTradeResultVO> refundResult = BActionXml.GetInstance().GetRefundTradeVO(outXml);

                            step2.ResponseData = JsonConvert.SerializeObject(refundResult);
                            
                            BActionLog.GetInstance().Insert(step2);

                                                      
                            if (refundResult.State.Success.Equals("true", StringComparison.OrdinalIgnoreCase))
                            {
                                //交易确认
                                ActionLog step3 = new ActionLog
                                {
                                    SubmitId = submitLog.Id,
                                    Step = 3,
                                    ActionName = "TradeInWeb",
                                    RequestData = "",
                                    CreateTime = DateTime.Now
                                };


                                cd.Trade_Web(out outXml);
                                LogHelper.WriteDealLogInfo("TradeInWeb返回结果：\r\n" + outXml);
                                ShowMessage("TradeInWeb返回结果：\r\n" + outXml);


                                step3.ResponseData = outXml;
                                BActionLog.GetInstance().Insert(step3);

                                BSubmitLog.GetInstance().Update(step2.ResponseData, 1, submitLog.Id);
                            }
                        }
                    }
                    else
                    {
                        ActionLog step1 = new ActionLog
                        {
                            SubmitId = submitLog.Id,
                            Step = 1,
                            ActionName = "GetTradeStateInWeb",
                            RequestData = submitLog.SubmitContent,
                            CreateTime = DateTime.Now
                        };

                        LogHelper.WriteDealLogInfo("GetTradeStateInWeb查询订单号：\r\n" + submitLog.SubmitContent);
                        ShowMessage("GetTradeStateInWeb查询订单号：\r\n" + submitLog.SubmitContent);

                        cd.CommitTradeState_Web(step1.RequestData, out string outXml);

                        LogHelper.WriteDealLogInfo("GetTradeStateInWeb返回结果：\r\n" + outXml);
                        ShowMessage("GetTradeStateInWeb返回结果：\r\n" + outXml);

                        ComResultVO<TradeStateVO> tradeStateResult = BActionXml.GetInstance().GetTradeStateVO(outXml);
                        step1.ResponseData = JsonConvert.SerializeObject(tradeStateResult);
                        BActionLog.GetInstance().Insert(step1);

                        BSubmitLog.GetInstance().Update(step1.ResponseData, 1, submitLog.Id);
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
