using System;
using System.Text;
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

        private WebRegClass cd = null;

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
            cd = null;
            _running = false;
        }

        #endregion public void Stop()

        #region public void Start()

        /// <summary>
        ///     线程开始工作
        /// </summary>
        public void Start()
        {
            if(cd == null)
            {
               cd = new WebRegClass();
            }
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
                Thread.Sleep(new TimeSpan(0, 0, 0, 2));
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
                    if(submitLog.SubmitType == 1)
                    {                    
                        DivideReqDTO req = JsonConvert.DeserializeObject<DivideReqDTO>(submitLog.SubmitContent);

                        ActionLog step1 = new ActionLog
                        {
                            RequestId = submitLog.RequestId,
                            Step = 1,
                            ActionName = "GetPersonInWeb",
                            RequestData = BActionXml.GetInstance().GetPersonWebXml(req.Person),
                            CreateTime = DateTime.Now
                        };

                        
                        LogHelper.WriteDealLogInfo("GetPersonInWeb请求：\r\n" + step1.RequestData);

                        ShowMessage("GetPersonInWeb请求：\r\n" + step1.RequestData);

                        string outXml;
                        try
                        {
                            cd.GetPersonInfo_Web(step1.RequestData, out outXml);
                        }
                        catch (Exception ex) 
                        {
                            LogHelper.WriteErrorLogInfo("GetPersonInWeb请求", ex.ToString());
                            ApiResultVO errorResult = new ApiResultVO();
                            errorResult.Success = "false";
                            errorResult.ErrorMessage = "首信医保接口无法访问";
                            BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(errorResult), 2, submitLog.RequestId);
                            return;
                        }
                                              

                        LogHelper.WriteDealLogInfo("GetPersonInWeb返回结果：\r\n" + outXml);

                        ShowMessage("GetPersonInWeb返回结果：\r\n" + outXml);

                        ComResultVO<PersonVO> personResult = BActionXml.GetInstance().GetPersonVO(outXml);
                        step1.ResponseData = JsonConvert.SerializeObject(personResult);

                        //保存操作数据                        
                        BActionLog.GetInstance().Insert(step1);

                        if (personResult.State.Success.Equals("false", StringComparison.OrdinalIgnoreCase))
                        {
                            ApiResultVO errorResult = new ApiResultVO();
                            errorResult.Success = "false";
                            errorResult.ErrorMessage = personResult.State.Error.Message;

                            BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(errorResult), 2, submitLog.RequestId);
                            return;
                        }
                        //费用分解
                        ActionLog step2 = new ActionLog
                        {
                            RequestId = submitLog.RequestId,
                            Step = 2,
                            ActionName = "DivideInWeb",
                            RequestData = BActionXml.GetInstance().GetDivideFeeXml(req),
                            CreateTime = DateTime.Now
                        };

                        LogHelper.WriteDealLogInfo("DivideInWeb请求：\r\n" + step2.RequestData);
                        ShowMessage("DivideInWeb请求：\r\n" + step2.RequestData);

                        try
                        {
                            cd.Divide_Web(step2.RequestData, out outXml);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteErrorLogInfo("DivideInWeb请求", ex.ToString());
                            ApiResultVO errorResult = new ApiResultVO();
                            errorResult.Success = "false";
                            errorResult.ErrorMessage = "首信医保接口无法访问";
                            BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(errorResult), 2, submitLog.RequestId);
                            return;
                        }


                        LogHelper.WriteDealLogInfo("DivideInWeb返回结果：\r\n" + outXml);
                        ShowMessage("DivideInWeb返回结果：\r\n" + outXml);

                        ComResultVO<TradeDivideVO> tradeDivideResult = BActionXml.GetInstance().GetTradeVO(outXml);
                        step2.ResponseData = JsonConvert.SerializeObject(tradeDivideResult);
                        //保存操作数据                                                        
                        BActionLog.GetInstance().Insert(step2);

                        if (tradeDivideResult.State.Success.Equals("false", StringComparison.OrdinalIgnoreCase))
                        {
                            ApiResultVO errorResult = new ApiResultVO();
                            errorResult.Success = "false";
                            errorResult.ErrorMessage = tradeDivideResult.State.Error.Message;

                            BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(errorResult), 2, submitLog.RequestId);
                            return;
                        }

                        //确认分解交易
                        try
                        {
                            cd.Trade_Web(out outXml);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteErrorLogInfo("TradeInWeb请求", ex.ToString());
                            ApiResultVO errorResult = new ApiResultVO();
                            errorResult.Success = "false";
                            errorResult.ErrorMessage = "首信医保接口无法访问";
                            BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(errorResult), 2, submitLog.RequestId);
                            return;
                        }
                      

                        LogHelper.WriteDealLogInfo("TradeInWeb返回结果：\r\n" + outXml);
                        ShowMessage("TradeInWeb返回结果：\r\n" + outXml);

                        ComResultVO<TradeResultVO> tradeResult = BActionXml.GetInstance().GetTradeResultVO(outXml);

                        //交易确认
                        ActionLog step3 = new ActionLog
                        {
                            RequestId = submitLog.RequestId,
                            Step = 3,
                            ActionName = "TradeInWeb",
                            RequestData = "",
                            ResponseData = JsonConvert.SerializeObject(tradeResult),
                            CreateTime = DateTime.Now
                        };
                        BActionLog.GetInstance().Insert(step3);

                        if (!tradeResult.State.Success.Equals("true", StringComparison.OrdinalIgnoreCase))
                        {
                            ApiResultVO errorResult = new ApiResultVO();
                            errorResult.Success = "false";
                            errorResult.ErrorMessage = tradeDivideResult.State.Error.Message;

                            BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(errorResult), 2, submitLog.RequestId);
                            return;
                        }
                        //更新返回结果
                        ApiResultVO apiResult = new ApiResultVO
                        {
                            Success = "true",
                            ErrorMessage = "",
                            TradeNumber = tradeDivideResult.Output.Trade.TradeNumber,
                            TotalAmount = tradeDivideResult.Output.SummaryPay.TotalAmount.ToString("#0.####"),
                            InInsuranceAmount = tradeDivideResult.Output.SummaryPay.FundAmount.ToString("#0.####")
                        };

                        StringBuilder sb = new StringBuilder();
                        sb.Append("<![CDATA[");
                        sb.Append(step2.ResponseData);
                        sb.Append("]]>");
                        apiResult.Result = Convert.ToBase64String(Encoding.UTF8.GetBytes(sb.ToString()));

                        BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(apiResult), 1, submitLog.RequestId);
                        return;                       
                    }
                    else if(submitLog.SubmitType == 2)
                    {
                        RefundmentReqDTO req = JsonConvert.DeserializeObject<RefundmentReqDTO>(submitLog.SubmitContent);

                        ActionLog step1 = new ActionLog
                        {
                            RequestId = submitLog.RequestId,
                            Step = 1,
                            ActionName = "GetPersonInWeb",
                            RequestData = BActionXml.GetInstance().GetPersonWebXml(req.Person),
                            CreateTime = DateTime.Now
                        };

                        LogHelper.WriteDealLogInfo("GetPersonInWeb请求：\r\n" + step1.RequestData);
                        ShowMessage("GetPersonInWeb请求：\r\n" + step1.RequestData);

                        string outXml;
                        try
                        {
                            cd.GetPersonInfo_Web(step1.RequestData, out outXml);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteErrorLogInfo("GetPersonInWeb请求", ex.ToString());
                            ApiResultVO errorResult = new ApiResultVO();
                            errorResult.Success = "false";
                            errorResult.ErrorMessage = "首信医保接口无法访问";
                            BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(errorResult), 2, submitLog.RequestId);
                            return;
                        }

                        LogHelper.WriteDealLogInfo("GetPersonInWeb返回结果：\r\n" + outXml);
                        ShowMessage("GetPersonInWeb返回结果：\r\n" + outXml);

                        ComResultVO<PersonVO> personResult = BActionXml.GetInstance().GetPersonVO(outXml);
                        step1.ResponseData = JsonConvert.SerializeObject(personResult);

                        //入库
                        BActionLog.GetInstance().Insert(step1);


                        if (!personResult.State.Success.Equals("true", StringComparison.OrdinalIgnoreCase))
                        {
                            ApiResultVO errorResult = new ApiResultVO();
                            errorResult.Success = "false";
                            errorResult.ErrorMessage = personResult.State.Error.Message;

                            BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(errorResult), 2, submitLog.RequestId);
                            return;
                        }


                        //费用分解
                        ActionLog step2 = new ActionLog
                        {
                            RequestId = submitLog.RequestId,
                            Step = 2,
                            ActionName = "RefundmentInWeb",
                            RequestData = BActionXml.GetInstance().GetRefundmentXml(req),
                            CreateTime = DateTime.Now
                        };

                        LogHelper.WriteDealLogInfo("RefundmentInWeb请求：\r\n" + step2.RequestData);
                        ShowMessage("RefundmentInWeb请求：\r\n" + step2.RequestData);


                        try
                        {
                            cd.Refundment_Web(step2.RequestData, out outXml);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteErrorLogInfo("RefundmentInWeb请求", ex.ToString());
                            ApiResultVO errorResult = new ApiResultVO();
                            errorResult.Success = "false";
                            errorResult.ErrorMessage = "首信医保接口无法访问";
                            BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(errorResult), 2, submitLog.RequestId);
                            return;
                        }


                        LogHelper.WriteDealLogInfo("RefundmentInWeb返回结果：\r\n" + outXml);

                        ShowMessage("RefundmentInWeb返回结果：\r\n" + outXml);

                        ComResultVO<RefundTradeResultVO> refundResult = BActionXml.GetInstance().GetRefundTradeVO(outXml);

                        step2.ResponseData = JsonConvert.SerializeObject(refundResult);

                        BActionLog.GetInstance().Insert(step2);

                        if (!refundResult.State.Success.Equals("true", StringComparison.OrdinalIgnoreCase))
                        {
                            ApiResultVO errorResult = new ApiResultVO
                            {
                                Success = "false",
                                ErrorMessage = refundResult.State.Error.Message
                            };

                            BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(errorResult), 2, submitLog.RequestId);
                            return;
                        }

                        //交易确认
                        ActionLog step3 = new ActionLog
                        {
                            RequestId = submitLog.RequestId,
                            Step = 3,
                            ActionName = "TradeInWeb",
                            RequestData = "",
                            CreateTime = DateTime.Now
                        };

                        //确认分解交易
                        try
                        {
                            cd.Trade_Web(out outXml);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteErrorLogInfo("TradeInWeb请求", ex.ToString());
                            ApiResultVO errorResult = new ApiResultVO();
                            errorResult.Success = "false";
                            errorResult.ErrorMessage = "首信医保接口无法访问";
                            BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(errorResult), 2, submitLog.RequestId);
                            return;
                        }

                        LogHelper.WriteDealLogInfo("TradeInWeb返回结果：\r\n" + outXml);
                        ShowMessage("TradeInWeb返回结果：\r\n" + outXml);

                        step3.ResponseData = outXml;
                        BActionLog.GetInstance().Insert(step3);

                        ApiResultVO apiResult = new ApiResultVO
                        {
                            Success = "true",
                            ErrorMessage = "",
                            TradeNumber = refundResult.Output.Trade.TradeNumber,
                            TotalAmount = refundResult.Output.SummaryPay.TotalAmount.ToString("#0.####"),
                            InInsuranceAmount = refundResult.Output.SummaryPay.FundAmount.ToString("#0.####")
                        };

                        StringBuilder sb = new StringBuilder();
                        sb.Append("<![CDATA[");
                        sb.Append(step2.ResponseData);
                        sb.Append("]]>");
                        apiResult.Result = Convert.ToBase64String(Encoding.UTF8.GetBytes(sb.ToString()));

                        BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(apiResult), 1, submitLog.RequestId);
                    }
                    else
                    {
                        ActionLog step1 = new ActionLog
                        {
                            RequestId = submitLog.RequestId,
                            Step = 1,
                            ActionName = "CommitTradeStateInWeb",
                            RequestData = submitLog.SubmitContent,
                            CreateTime = DateTime.Now
                        };

                        LogHelper.WriteDealLogInfo("CommitTradeStateInWeb查询订单号：\r\n" + submitLog.SubmitContent);
                        ShowMessage("CommitTradeStateInWeb查询订单号：\r\n" + submitLog.SubmitContent);

                        string outXml;

                        try
                        {
                            cd.CommitTradeState_Web(step1.RequestData, out outXml);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteErrorLogInfo("TradeInWeb请求", ex.ToString());
                            ApiResultVO errorResult = new ApiResultVO();
                            errorResult.Success = "false";
                            errorResult.ErrorMessage = "首信医保接口无法访问";
                            BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(errorResult), 2, submitLog.RequestId);
                            return;
                        }                      

                        LogHelper.WriteDealLogInfo("GetTradeStateInWeb返回结果：\r\n" + outXml);
                        ShowMessage("CommitTradeStateInWeb返回结果：\r\n" + outXml);

                        ComResultVO<TradeStateVO> tradeStateResult = BActionXml.GetInstance().GetTradeStateVO(outXml);
                        step1.ResponseData = JsonConvert.SerializeObject(tradeStateResult);
                        BActionLog.GetInstance().Insert(step1);

                        ApiResultVO apiResult = new ApiResultVO
                        {
                            Success = tradeStateResult.State.Success,
                            ErrorMessage = !tradeStateResult.State.Success.Equals("true", StringComparison.OrdinalIgnoreCase) ? tradeStateResult.State.Error.Message : "",
                            TradeNumber = submitLog.SubmitContent,
                            TotalAmount = "",
                            InInsuranceAmount = ""
                        };

                        StringBuilder sb = new StringBuilder();
                        sb.Append("<![CDATA[");
                        sb.Append(step1.ResponseData);
                        sb.Append("]]>");
                        apiResult.Result = Convert.ToBase64String(Encoding.UTF8.GetBytes(sb.ToString()));

                        BSubmitLog.GetInstance().Update(JsonConvert.SerializeObject(apiResult), 1, submitLog.RequestId);
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
