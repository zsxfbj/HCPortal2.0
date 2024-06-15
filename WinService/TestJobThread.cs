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
                            State = new StateVO { Success = "true" },
                            Output = BActionXml.GetInstance().GetPersonVO(req.Person)
                        };

                        step1.ResponseData = JsonConvert.SerializeObject(personResult);

                        ShowMessage("GetPersonInWeb模拟请求返回结果：" + step1.ResponseData);

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
                                },
                                Output = new TradeDivideVO
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
                                        InInsuranceAmount = req.Fee == 50 ? 40 : 0,
                                        OutInsuranceAmount = req.Fee == 50 ? 10 : req.Fee,
                                        OutOfBigPayAmount = 0
                                    },
                                    MedicineCatalog = new MedicineCatalogVO
                                    {
                                        BloodTransfusionFee = 0,
                                        ChineseHerbalDrinkFee = 0,
                                        ChineseMedicineFee = 0,
                                        CtFee = 0,
                                        ExamineFee = 0,
                                        ForensicExpertiseFee = 0,
                                        LabExamFee = 0,
                                        MaterialFee = 0,
                                        MedicineFee = 0,
                                        MriFee = 0,
                                        OperationFee = 0,
                                        OrthodonticsFee = 0,
                                        OtherFee = 0,
                                        OxygenFee = 0,
                                        ProsthesisFee = 0,
                                        TreatmentFee = 0,
                                        UltrasonicFee = 0,
                                        XRayFee = 0
                                    },
                                    MedicineCatalog2 = new MedicineCatalog2VO
                                    {
                                        ExamineFee = 0,
                                        DiagnosisFee = 0,
                                        CommonServiceFee = 0,
                                        ChineseHerbalDrinkFee = 0,
                                        ChineseMedicineFee = 0,
                                        TreatmentFee = 0,
                                        LabExamFee = 0,
                                        MaterialFee = 0,
                                        MedicineFee = 0,
                                        MedicalServiceFee = 0,
                                        OperationFee = 0,
                                        OtherOperationFee = 0,
                                        RegistFee = 0
                                    },
                                    FeeItems = new FeeItemsVO
                                    {
                                        FeeItem = new FeeItemVO
                                        {
                                            Count = 1,
                                            Fee = req.Fee,
                                            FeeType = "0601",
                                            HisCode = req.HisCode,
                                            InInsuranceFee = req.Fee == 50 ? 40 : 0,
                                            ItemCode = "w0101020010",
                                            ItemName = "医事服务费【三级医院】【普通门诊】",
                                            ItemNumber = 1,
                                            ItemType = 2,
                                            RecipeNumber = "1",
                                            OutInsuranceFee = req.Fee == 50 ? 10 : req.Fee,
                                            PreferentialFee = 0,
                                            PreferentialScale = 0,
                                            SelfPayFee = req.Fee == 50 ? 10 : req.Fee,
                                            State = req.Fee == 50 ? 0 : 6,
                                            UnitPrice = req.Fee
                                        }
                                    }
                                }
                            };


                            step2.ResponseData = JsonConvert.SerializeObject(tradeResult);
                            ShowMessage("DivideInWeb模拟请求返回结果：" + step2.ResponseData);                            

                            BActionLog.GetInstance().Insert(step2);

                            BSubmitLog.GetInstance().Update(step2.ResponseData, 1, submitLog.Id);

                            if (tradeResult.State.Equals("true"))
                            {
                                //交易确认
                                ActionLog step3 = new ActionLog
                                {
                                    SubmitId = submitLog.Id,
                                    Step = 3,
                                    ActionName = "TradeInWeb"
                                };

                                ComResultVO<TradeResultVO> comResultVO = new ComResultVO<TradeResultVO>
                                {
                                    State = new StateVO
                                    {
                                        Success = "true"
                                    },

                                    Output = new TradeResultVO
                                    {                                       
                                        PersonAccountAfterSubtractAmount = 0,
                                        Sign = "441C11F6CF574627BF6A7DF4B83FF539",
                                        CertId = "Algorithm"
                                    }
                                };

                                step3.ResponseData = JsonConvert.SerializeObject(comResultVO);
                                ShowMessage("TradeInWeb模拟请求返回结果：" + step3.ResponseData);
                                                                    
                                BActionLog.GetInstance().Insert(step3);
                            }
                        }  
                       
                    }
                    else if (submitLog.SubmitType == 2) //退费请求
                    {
                        RefundmentReqDTO req = JsonConvert.DeserializeObject<RefundmentReqDTO>(submitLog.SubmitContent);

                        ActionLog step1 = new ActionLog
                        {
                            SubmitId = submitLog.Id,
                            Step = 1,
                            ActionName = "GetPersonInWeb",
                            RequestData = BActionXml.GetInstance().GetPersonWebXml(req.Person)
                        };
                        ComResultVO<PersonVO> personResult = new ComResultVO<PersonVO>
                        {
                            State = new StateVO { Success = "true" },
                            Output = BActionXml.GetInstance().GetPersonVO(req.Person)
                        };

                        step1.ResponseData = JsonConvert.SerializeObject(personResult);

                        ShowMessage("GetPersonInWeb模拟请求返回结果：" + step1.ResponseData);

                        BActionLog.GetInstance().Insert(step1);
                                                                     
                        if (personResult.State.Equals("true"))
                        {
                            //费用分解
                            ActionLog step2 = new ActionLog
                            {
                                SubmitId = submitLog.Id,
                                Step = 2,
                                ActionName = "RefundmentInWeb",
                                RequestData = BActionXml.GetInstance().GetRefundmentXml(req)
                            };

                            ComResultVO<RefundTradeResultVO> refundResult = new ComResultVO<RefundTradeResultVO>()
                            {
                                State = new StateVO
                                {
                                    Success = "true"
                                },
                                Output = new RefundTradeResultVO
                                {
                                    Trade = new RefundTradeVO
                                    {
                                        TradeNumber = IdHelper.IdWorker.NextId().ToString(),
                                        TradeDate = DateTime.Now.ToString("yyyyMMddHHmmss"),
                                         CureType = "17",
                                         IcNumber = "13021369000S",
                                         IllType = "0"
                                    },

                                    SummaryPay = new SummaryPayVO
                                    {
                                        TotalAmount = -50,
                                        PersonAccountAmount = 0,
                                        CashAmount = -10,
                                        FundAmount = -40
                                    },
                                    Payment = new PaymentVO
                                    {
                                        SelfPayAmount = -10,
                                        SupplementaryPayAmount = 0,
                                        BigPayAmount = -40,
                                        FirstPayAmount = 0,
                                        TotalAmount = -50,
                                        MilitaryPayAmount = 0,
                                        SelfBigPayAmount = 0,
                                        InInsuranceAmount = -40,
                                        OutInsuranceAmount = -10,
                                        OutOfBigPayAmount = 0
                                    },
                                    
                                    FeeItems = new FeeItemsVO
                                    {
                                        FeeItem = new FeeItemVO
                                        {
                                            Count = 1,
                                            Fee = -50,
                                            FeeType = "0601",
                                            HisCode = "w0101020010",
                                            InInsuranceFee = -40,
                                            ItemCode = "w0101020010",
                                            ItemName = "医事服务费【三级医院】【普通门诊】",
                                            ItemNumber = 1,
                                            ItemType = 2,
                                            RecipeNumber = "1",
                                            OutInsuranceFee = -10,
                                            PreferentialFee = 0,
                                            PreferentialScale = 0,
                                            SelfPayFee = -10,
                                            State = 0,
                                            UnitPrice = -50
                                        }
                                    }
                                }
                            };

                            step2.ResponseData = JsonConvert.SerializeObject(refundResult);
                            ShowMessage("RefundmentInWeb模拟请求返回结果：" + step2.ResponseData);

                            BActionLog.GetInstance().Insert(step2);

                            BSubmitLog.GetInstance().Update(step2.ResponseData, 1, submitLog.Id);

                            if (refundResult.State.Equals("true"))
                            {
                                //交易确认
                                ActionLog step3 = new ActionLog
                                {
                                    SubmitId = submitLog.Id,
                                    Step = 3,
                                    ActionName = "TradeInWeb"
                                };

                                ComResultVO<TradeResultVO> comResultVO = new ComResultVO<TradeResultVO>
                                {
                                    State = new StateVO
                                    {
                                        Success = "true"
                                    },

                                    Output = new TradeResultVO
                                    {                                       
                                        PersonAccountAfterSubtractAmount = 0,
                                        Sign = "441C11F6CF574627BF6A7DF4B83FF539",
                                        CertId = "Algorithm"
                                    }
                                };

                                step3.ResponseData = JsonConvert.SerializeObject(comResultVO);
                                ShowMessage("TradeInWeb模拟请求返回结果：" + step3.ResponseData);

                                BActionLog.GetInstance().Insert(step3);
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
                            RequestData = submitLog.SubmitContent
                        };

                        ComResultVO<TradeStateVO> comResult = new ComResultVO<TradeStateVO>();
                        comResult.State.Success = "true";

                        comResult.Output = new TradeStateVO();
                        comResult.Output.State = "ok";
                        comResult.Output.StateName = "交易成功";


                        step1.ResponseData = JsonConvert.SerializeObject(comResult);

                        BActionLog.GetInstance().Insert(step1);

                        ShowMessage("GetTradeStateInWeb模拟请求返回结果：" + step1.ResponseData);

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
