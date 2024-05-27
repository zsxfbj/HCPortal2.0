using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using HC.Model.DTO;
using HC.Model.VO;
using HC.Utitily;

namespace HC.BLL
{
    /// <summary>
    /// 医保XML和对象转换类
    /// </summary>
    public class BActionXml : Singleton<BActionXml>
    {
        private const string XmlStart = "<?xml version=\"1.0\" encoding=\"UTF-16\" standalone=\"yes\"?>\r\n<root version=\"1.10.0004\">";
        private const string XmlEnd = "</root>";

        #region public string GetPersonWebXml(PersonInfoReqDTO personInfo)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public string GetPersonWebXml(PersonInfoReqDTO personInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(XmlStart);
            sb.AppendLine("<input>");
            sb.AppendLine("<card_no>" + personInfo.CardNumber + "</card_no>");
            sb.AppendLine("<id_no>" + personInfo.IdNumber + "</id_no>");
            sb.AppendLine("<personname>" + personInfo.PersonName + "</personname>");
            sb.AppendLine("<sex>" + personInfo.Sex + "</sex>");
            sb.AppendLine("<birthday>" + personInfo.Birthday + "</birthday>");
            sb.AppendLine("<fundtype>" + personInfo.FundType + "</fundtype>");
            if (personInfo.InHospital.HasValue)
            {
                sb.AppendLine("<hospflag>" + personInfo.InHospital + "</hospflag>");
            }
            else
            {
                sb.AppendLine("<hospflag>0</hospflag>");
            }
            sb.AppendLine("</input>");
            sb.Append(XmlEnd);
            return sb.ToString();
        }
        #endregion public string GetPersonWebXml(PersonInfoReqDTO personInfo)

        #region public ComResultVO<PersonVO> GetPersonVO(string outXml)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outXml"></param>
        /// <returns></returns>
        public ComResultVO<PersonVO> GetPersonVO(string outXml)
        {
            ComResultVO<PersonVO> comResult = new ComResultVO<Model.VO.PersonVO>();
            XmlDocument document = new XmlDocument();
            document.LoadXml(outXml);

            //获取根节点
            XmlNode rootNode = document.SelectSingleNode("root");

            //获取警告或者出错信息
            GetBaseInfo(rootNode, comResult);

            //查询输出部分
            XmlNode outputNode = rootNode.SelectSingleNode("output");
            if (outputNode != null)
            {
                PersonVO personInfo = new PersonVO();

                //ic卡信息
                XmlNode icNode = outputNode.SelectSingleNode("ic");
                if (icNode != null)
                {
                    //社保卡号
                    personInfo.CardNumber = GetSubNodeValue(icNode, "card_no");

                    //身份证号
                    personInfo.IdNumber = GetSubNodeValue(icNode, "id_no");

                    //医保号
                    personInfo.IcNumber = GetSubNodeValue(icNode, "ic_no");

                    //姓名
                    personInfo.PersonName = GetSubNodeValue(icNode, "personname");

                    //性别
                    string sex = GetSubNodeValue(icNode, "sex");
                    int.TryParse(sex, out int value);
                    personInfo.Sex = value;

                    //出生日期
                    personInfo.Birthday = GetSubNodeValue(icNode, "birthday");

                    //转诊医院编码
                    personInfo.FromHospital = GetSubNodeValue(icNode, "fromhosp");

                    //转诊时限
                    personInfo.FromHospitalDate = GetSubNodeValue(icNode, "fromhospdate");

                    //险种类型
                    personInfo.FundType = GetSubNodeValue(icNode, "fundtype");
                }

                XmlNode netNode = outputNode.SelectSingleNode("net");
                if (netNode != null)
                {
                    //参保人员类别
                    personInfo.PersonType = GetSubNodeValue(netNode, "persontype");

                    //是否在红名单
                    personInfo.IsInRedList = GetSubNodeValue(netNode, "isinredlist");

                    //本人定点医院状态
                    personInfo.IsSpecifiedHospital = GetSubNodeValue(netNode, "isspecifiedhosp");

                    //是否本人慢病定点医院
                    personInfo.IsChronicHospital = GetSubNodeValue(netNode, "ischronichosp");

                    //个人帐户余额
                    string personCount = GetSubNodeValue(netNode, "personcount");
                    decimal.TryParse(personCount, out decimal value);
                    personInfo.PersonCount = value;

                    //慢病编码
                    personInfo.ChronicCode = GetSubNodeValue(netNode, "chroniccode");
                }
                comResult.Data = personInfo;
            }

            return comResult;
        }
        #endregion public ComResultVO<PersonVO> GetPersonVO(string outXml)

        #region public string GetDivideFeeXml(DivideReqDTO req)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string GetDivideFeeXml(DivideReqDTO req)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(XmlStart);
            sb.AppendLine("<input>");

            //交易信息
            sb.AppendLine("<tradeinfo>");
            sb.AppendLine("<curetype>17</curetype><illtype>0</illtype>");
            sb.AppendLine("<feeno>" + req.FeeNumber + "</feeno>");
            sb.AppendLine("<Histradeno>" + req.FeeNumber + "</Histradeno>");
            sb.AppendLine("<operator>" + (string.IsNullOrEmpty(req.Operator) ? "" : req.Operator.Trim()) + "</operator>");
            sb.AppendLine("</tradeinfo>");

            //单据信息
            sb.AppendLine("<recipearray>");
            sb.AppendLine("<recipe>");
            sb.AppendLine("<diagnoseno>1</diagnoseno>");
            sb.AppendLine("<recipeno>1</recipeno>");
            sb.AppendLine("<recipedate>" + req.RecipeDate + "</recipedate>");
            sb.AppendLine("<diagnosecode></diagnosecode>");
            sb.AppendLine("<diagnosename></diagnosename>");
            sb.AppendLine("<medicalrecord></medicalrecord>");
            sb.AppendLine("<sectioncode>" + req.SectionCode.Trim() + "</sectioncode>");
            sb.AppendLine("<sectionname>" + req.SectionName.Trim() + "</sectionname>");
            sb.AppendLine("<hissectionname></hissectionname>");
            sb.AppendLine("<drid>" + (string.IsNullOrEmpty(req.DoctorId) ? "" : req.DoctorId.Trim()) + "</drid>");
            sb.AppendLine("<drname>" + (string.IsNullOrEmpty(req.DoctorName) ? "" : req.DoctorName.Trim()) + "</drname>");
            sb.AppendLine(" <recipetype>1</recipetype>");
            sb.AppendLine("<remark></remark>");
            sb.AppendLine("<registertradeno></registertradeno>");
            sb.AppendLine("<billstype>1</billstype>");
            sb.AppendLine("<DoctorLevel>01</DoctorLevel>");
            sb.AppendLine("</recipe>");
            sb.AppendLine("</recipearray>");

            //费用清单
            sb.AppendLine("<feeitemarray>");
            sb.Append("<feeitem itemno=\"1\" recipeno=\"1\" hiscode=\"" + req.HisCode + "\" ");
            sb.Append("itemname=\"" + req.ItemName.Trim());
            sb.Append("\" itemtype=\"1\" ");
            sb.Append("unitprice=\" " + req.UnitPrice.ToString("#0.####") + " \" ");
            sb.Append("count=\"1\" ");
            sb.Append("fee=\"" + req.Fee.ToString("#0.####") + "\" ");
            sb.AppendLine("dose=\"\" specification=\"\" unit=\"\" howtouse=\"\" dosage=\"\" packaging=\"\" minpackage=\"\" conversion=\"\" days=\"\" babyflag=\"0\"></feeitem>");
            sb.AppendLine("</feeitemarray>");

            //输入结束标记
            sb.AppendLine("</input>");

            sb.Append(XmlEnd);
            return sb.ToString();
        }
        #endregion public string GetDivideFeeXml(DivideReqDTO req)

        #region public ComResultVO<TradeVO> GetTradeVO(string outXml)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outXml"></param>
        /// <returns></returns>
        public ComResultVO<TradeVO> GetTradeVO(string outXml)
        {
            ComResultVO<TradeVO> comResult = new ComResultVO<TradeVO>();

            XmlDocument document = new XmlDocument();
            document.LoadXml(outXml);
            //获取根节点
            XmlNode rootNode = document.SelectSingleNode("root");
            //获取警告或者出错信息
            GetBaseInfo(rootNode, comResult);

            //查询具体返回数据
            XmlNode outputNode = rootNode.SelectSingleNode("output");
            if (outputNode != null)
            {
                comResult.Data = new TradeVO();
                //交易信息
                XmlNode tradeNode = outputNode.SelectSingleNode("tradeinfo");
                if (tradeNode != null)
                {
                    //交易流水号
                    comResult.Data.TradeNumber = GetSubNodeValue(tradeNode, "tradeno");

                    //收费单据号
                    comResult.Data.FeeNumber = GetSubNodeValue(tradeNode, "feeno");

                    //交易日期
                    comResult.Data.TradeDate = GetSubNodeValue(tradeNode, "tradedate");
                }

                //费用明细
                XmlNode feeItemsNode = outputNode.SelectSingleNode("feeitemarray");
                if (feeItemsNode != null)
                {
                    comResult.Data.FeeItems = new System.Collections.Generic.List<FeeItemVO>();
                    foreach (XmlNode feeItemNode in feeItemsNode.ChildNodes)
                    {
                        //添加到计费明细项
                        comResult.Data.FeeItems.Add(GetFeeItemVO(feeItemNode));
                    }
                }

                //汇总支付信息
                XmlNode summaryPayNode = outputNode.SelectSingleNode("sumpay");
                if (summaryPayNode != null)
                {
                    comResult.Data.SummaryPay = GetSummaryPay(summaryPayNode);
                }

                //支付信息
                XmlNode paymentNode = outputNode.SelectSingleNode("payinfo");
                if (paymentNode != null)
                {
                    comResult.Data.Payment = GetPayment(paymentNode);
                }

                //分类汇总信息
                XmlNode medicineCatalogNode = outputNode.SelectSingleNode("medicatalog");
                if (medicineCatalogNode != null)
                {
                    GetMedicineCatalogVO(comResult.Data, medicineCatalogNode);
                }

                //新单据分类汇总信息
                XmlNode medicineCatalog2Node = outputNode.SelectSingleNode("medicatalog2");
                if (medicineCatalog2Node != null)
                {
                    GetMedicineCatalog2VO(comResult.Data, medicineCatalog2Node);
                }

            }

            return comResult;
        }
        #endregion public ComResultVO<TradeVO> GetTradeVO(string outXml)

        #region public ComResultVO<TradeResultVO> GetTradeResultVO(string outXml)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outXml"></param>
        /// <returns></returns>
        public ComResultVO<TradeResultVO> GetTradeResultVO(string outXml)
        {
            ComResultVO<TradeResultVO> comResult = new ComResultVO<TradeResultVO>();
            XmlDocument document = new XmlDocument();
            document.LoadXml(outXml);
            //获取根节点
            XmlNode rootNode = document.SelectSingleNode("root");
            //获取警告或者出错信息
            GetBaseInfo(rootNode, comResult);

            //查询具体返回数据
            XmlNode outputNode = rootNode.SelectSingleNode("output");
            if (outputNode != null)
            {
                comResult.Data = new TradeResultVO();

                decimal.TryParse(GetSubNodeValue(outputNode, "personcountaftersub"), out decimal amount);
                comResult.Data.PersonAccountAfterSubtractAmount = amount;
                comResult.Data.CertId = GetSubNodeValue(outputNode, "certid");
                comResult.Data.Sign = GetSubNodeValue(outputNode, "sign");
            }
            return comResult;
        }
        #endregion public ComResultVO<TradeResultVO> GetTradeResultVO(string outXml)

        #region public string GetRefundmentXml(RefundmentReqDTO req)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string GetRefundmentXml(RefundmentReqDTO req)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(XmlStart);
            sb.AppendLine("<input>");

            //交易信息
            sb.AppendLine("<tradeinfo>");
            sb.AppendLine("<tradeno>" + req.TradeNumber + "</tradeno>");
            sb.AppendLine("<operator>" + (string.IsNullOrEmpty(req.Operator) ? "" : req.Operator.Trim()) + "</operator>");
            sb.AppendLine("</tradeinfo>");

            //输入结束标记
            sb.AppendLine("</input>");

            sb.Append(XmlEnd);
            return sb.ToString();
        }
        #endregion public string GetRefundmentXml(RefundmentReqDTO req)

        #region public ComResultVO<RefundTradeVO> GetRefundTradeVO(string outXml)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outXml"></param>
        /// <returns></returns>
        public ComResultVO<RefundTradeVO> GetRefundTradeVO(string outXml)
        {
            ComResultVO<RefundTradeVO> comResult = new ComResultVO<RefundTradeVO>();
            XmlDocument document = new XmlDocument();
            document.LoadXml(outXml);
            //获取根节点
            XmlNode rootNode = document.SelectSingleNode("root");
            //获取警告或者出错信息
            GetBaseInfo(rootNode, comResult);

            XmlNode outputNode = rootNode.SelectSingleNode("output");
            if (outputNode != null)
            {
                comResult.Data = new RefundTradeVO();


                XmlNode fullTradeNode = outputNode.SelectSingleNode("fulltrade");
                if (fullTradeNode != null)
                {
                    //交易信息
                    XmlNode tradeNode = outputNode.SelectSingleNode("tradeinfo");
                    if (tradeNode != null)
                    {
                        //交易流水号
                        comResult.Data.TradeNumber = GetSubNodeValue(tradeNode, "tradeno");

                        //交易流水号
                        comResult.Data.IllType = GetSubNodeValue(tradeNode, "illtype");

                        //医保应用号
                        comResult.Data.IcNumber = GetSubNodeValue(tradeNode, "ic_no");

                        //交易流水号
                        comResult.Data.CureType = GetSubNodeValue(tradeNode, "curetype");

                        //交易日期
                        comResult.Data.TradeDate = GetSubNodeValue(tradeNode, "tradedate");
                    }

                    //费用明细
                    XmlNode feeItemsNode = outputNode.SelectSingleNode("feeitemarray");
                    if (feeItemsNode != null)
                    {
                        comResult.Data.FeeItems = new System.Collections.Generic.List<FeeItemVO>();
                        foreach (XmlNode feeItemNode in feeItemsNode.ChildNodes)
                        {
                            //添加到计费明细项
                            comResult.Data.FeeItems.Add(GetFeeItemVO(feeItemNode));
                        }
                    }

                    //汇总支付信息
                    XmlNode summaryPayNode = outputNode.SelectSingleNode("sumpay");
                    if (summaryPayNode != null)
                    {
                        comResult.Data.SummaryPay = GetSummaryPay(summaryPayNode);
                    }

                    //支付信息
                    XmlNode paymentNode = outputNode.SelectSingleNode("payinfo");
                    if (paymentNode != null)
                    {
                        comResult.Data.Payment = GetPayment(paymentNode);
                    }
                }

            }

            return comResult;
        }
        #endregion public ComResultVO<RefundTradeVO> GetRefundTradeVO(string outXml)

        #region public ComResultVO<TradeStateVO> GetTradeStateVO(string outXml)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outXml"></param>
        /// <returns></returns>
        public ComResultVO<TradeStateVO> GetTradeStateVO(string outXml)
        {
            ComResultVO<TradeStateVO> comResult = new ComResultVO<TradeStateVO>();
            XmlDocument document = new XmlDocument();
            document.LoadXml(outXml);
            //获取根节点
            XmlNode rootNode = document.SelectSingleNode("root");
            //获取警告或者出错信息
            GetBaseInfo(rootNode, comResult);

            XmlNode outputNode = rootNode.SelectSingleNode("output");
            if (outputNode != null)
            {
                comResult.Data = new TradeStateVO();
                comResult.Data.State = GetSubNodeValue(outputNode, "tradestate");
                if (comResult.Data.State.Equals("ok", StringComparison.OrdinalIgnoreCase))
                {
                    comResult.Data.StateName = "交易成功";
                }
                else
                {
                    comResult.Data.StateName = "交易撤销";
                }
            }

            return comResult;
        }
        #endregion public ComResultVO<TradeStateVO> GetTradeStateVO(string outXml)



        #region private void GetBaseInfo<T>(XmlNode rootNode, ComResultVO<T> comResult)
        private void GetBaseInfo<T>(XmlNode rootNode, ComResultVO<T> comResult)
        {
            //先读取是否成功
            XmlNode stateNode = rootNode.SelectSingleNode("state");
            if (stateNode != null)
            {
                comResult.State = stateNode.Attributes["success"].Value;

                XmlNodeList warnings = stateNode.SelectNodes("warning");
                if (warnings != null && warnings.Count > 0)
                {
                    comResult.Warnings = new List<ErrorMessageVO>();
                    foreach (XmlNode warning in warnings)
                    {
                        ErrorMessageVO vo = new ErrorMessageVO();
                        if (warning.Attributes["no"] != null)
                        {
                            vo.Number = warning.Attributes["no"].Value;
                        }
                        if (warning.Attributes["info"] != null)
                        {
                            vo.Message = warning.Attributes["info"].Value;
                        }
                        comResult.Warnings.Add(vo);
                    }
                }

                XmlNodeList errors = stateNode.SelectNodes("error");
                if (errors != null && errors.Count > 0)
                {
                    comResult.Errors = new List<ErrorMessageVO>();
                    foreach (XmlNode error in errors)
                    {
                        ErrorMessageVO vo = new ErrorMessageVO();
                        if (error.Attributes["no"] != null)
                        {
                            vo.Number = error.Attributes["no"].Value;
                        }
                        if (error.Attributes["info"] != null)
                        {
                            vo.Message = error.Attributes["info"].Value;
                        }
                        comResult.Errors.Add(vo);
                    }
                }

            }
        }
        #endregion private void GetBaseInfo<T>(XmlNode rootNode, ComResultVO<T> comResult)

        #region private FeeItemVO GetFeeItemVO(XmlNode feeItemNode)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="feeItemNode"></param>
        /// <returns></returns>
        private FeeItemVO GetFeeItemVO(XmlNode feeItemNode)
        {
            FeeItemVO feeItem = new FeeItemVO();

            int.TryParse(feeItemNode.Attributes["itemno"].Value, out int itemNumber);

            feeItem.ItemNumber = itemNumber;
            feeItem.RecipeNumber = feeItemNode.Attributes["recipeno"].Value;
            feeItem.HisCode = feeItemNode.Attributes["hiscode"].Value;
            feeItem.ItemCode = feeItemNode.Attributes["itemcode"].Value;
            feeItem.ItemName = feeItemNode.Attributes["itemname"].Value;

            int.TryParse(feeItemNode.Attributes["itemtype"].Value, out int itemType);
            feeItem.ItemType = itemType;

            decimal.TryParse(feeItemNode.Attributes["unitprice"].Value, out decimal unitPrice);
            feeItem.UnitPrice = unitPrice;

            decimal.TryParse(feeItemNode.Attributes["count"].Value, out decimal count);
            feeItem.Count = count;

            decimal.TryParse(feeItemNode.Attributes["fee"].Value, out decimal fee);
            feeItem.Fee = fee;

            decimal.TryParse(feeItemNode.Attributes["feein"].Value, out decimal inInsuranceFee);
            feeItem.InInsuranceFee = inInsuranceFee;

            decimal.TryParse(feeItemNode.Attributes["feeout"].Value, out decimal outInsuranceFee);
            feeItem.OutInsuranceFee = outInsuranceFee;

            decimal.TryParse(feeItemNode.Attributes["selfpay2"].Value, out decimal selfPayFee);
            feeItem.SelfPayFee = selfPayFee;

            int.TryParse(feeItemNode.Attributes["state"].Value, out int state);
            feeItem.State = state;

            feeItem.FeeType = feeItemNode.Attributes["fee_type"].Value;

            decimal.TryParse(feeItemNode.Attributes["preferentialfee"].Value, out decimal preferentialFee);
            feeItem.PreferentialFee = preferentialFee;

            int.TryParse(feeItemNode.Attributes["preferentialscale"].Value, out int scale);
            feeItem.PreferentialScale = scale;

            return feeItem;
        }
        #endregion private FeeItemVO GetFeeItemVO(XmlNode feeItemNode)

        #region private SummaryPayVO GetSummaryPay(XmlNode summaryPayNode)
        /// <summary>
        /// 
        /// </summary>        
        /// <param name="summaryPayNode"></param>
        private SummaryPayVO GetSummaryPay(XmlNode summaryPayNode)
        {
            SummaryPayVO vo = new SummaryPayVO();

            decimal.TryParse(GetSubNodeValue(summaryPayNode, "feeall"), out decimal totalAmount);
            vo.TotalAmount = totalAmount;

            decimal.TryParse(GetSubNodeValue(summaryPayNode, "fund"), out decimal fundAmount);
            vo.FundAmount = fundAmount;

            decimal.TryParse(GetSubNodeValue(summaryPayNode, "cash"), out decimal cashAmount);
            vo.CashAmount = cashAmount;

            decimal.TryParse(GetSubNodeValue(summaryPayNode, "personcountpay"), out decimal personAccountAmount);
            vo.PersonAccountAmount = personAccountAmount;

            return vo;
        }
        #endregion private SummaryPayVO GetSummaryPay(XmlNode summaryPayNode)

        #region private PaymentVO GetPayment(XmlNode paymentNode)
        /// <summary>
        /// 获取支付信息结果
        /// </summary>
        /// <param name="paymentNode"></param>
        private PaymentVO GetPayment(XmlNode paymentNode)
        {
            PaymentVO vo = new PaymentVO();

            decimal.TryParse(GetSubNodeValue(paymentNode, "mzfee"), out decimal totalAmount);
            vo.TotalAmount = totalAmount;

            decimal.TryParse(GetSubNodeValue(paymentNode, "mzfeein"), out decimal inInsuranceAmount);
            vo.InInsuranceAmount = inInsuranceAmount;

            decimal.TryParse(GetSubNodeValue(paymentNode, "mzpayfirst"), out decimal firstPayAmount);
            vo.FirstPayAmount = firstPayAmount;

            decimal.TryParse(GetSubNodeValue(paymentNode, "mzselfpay2"), out decimal selfPayAmount);
            vo.SelfPayAmount = selfPayAmount;

            decimal.TryParse(GetSubNodeValue(paymentNode, "mzbigpay"), out decimal bigPayAmount);
            vo.BigPayAmount = bigPayAmount;

            decimal.TryParse(GetSubNodeValue(paymentNode, "mzbigselfpay"), out decimal selfBigPayAmount);
            vo.SelfBigPayAmount = selfBigPayAmount;

            decimal.TryParse(GetSubNodeValue(paymentNode, "mzoutofbig"), out decimal outOfBigPayAmount);
            vo.OutOfBigPayAmount = outOfBigPayAmount;

            decimal.TryParse(GetSubNodeValue(paymentNode, "bcpay"), out decimal supplementaryPayAmount);
            vo.SupplementaryPayAmount = supplementaryPayAmount;

            decimal.TryParse(GetSubNodeValue(paymentNode, "jcbz"), out decimal militaryPayAmount);
            vo.MilitaryPayAmount = militaryPayAmount;

            return vo;
        }
        #endregion private PaymentVO GetPayment(XmlNode paymentNode)

        #region private void GetMedicineCatalogVO(TradeVO trade, XmlNode medicineCatalogNode)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trade"></param>
        /// <param name="medicineCatalogNode"></param>
        private void GetMedicineCatalogVO(TradeVO trade, XmlNode medicineCatalogNode)
        {
            trade.MedicineCatalog = new MedicineCatalogVO();

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "medicine"), out decimal medicineFee);
            trade.MedicineCatalog.MedicineFee = medicineFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "tmedicine"), out decimal chineseMedicineFee);
            trade.MedicineCatalog.ChineseMedicineFee = chineseMedicineFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "therb"), out decimal chineseHerbalDrinkFee);
            trade.MedicineCatalog.ChineseHerbalDrinkFee = chineseHerbalDrinkFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "examine"), out decimal examineFee);
            trade.MedicineCatalog.ExamineFee = examineFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "ct"), out decimal ctFee);
            trade.MedicineCatalog.CtFee = ctFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "mri"), out decimal mriFee);
            trade.MedicineCatalog.MriFee = mriFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "ultrasonic"), out decimal ultrasonicFee);
            trade.MedicineCatalog.UltrasonicFee = ultrasonicFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "labexam"), out decimal labExamFee);
            trade.MedicineCatalog.LabExamFee = labExamFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "xray"), out decimal xrayFee);
            trade.MedicineCatalog.XRayFee = xrayFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "treatment"), out decimal treatmentFee);
            trade.MedicineCatalog.TreatmentFee = treatmentFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "material"), out decimal materialFee);
            trade.MedicineCatalog.MaterialFee = materialFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "operation"), out decimal operationFee);
            trade.MedicineCatalog.OperationFee = operationFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "oxygen"), out decimal oxygenFee);
            trade.MedicineCatalog.OxygenFee = oxygenFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "bloodt"), out decimal bloodTransfusionFee);
            trade.MedicineCatalog.BloodTransfusionFee = bloodTransfusionFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "orthodontics"), out decimal orthodonticsFee);
            trade.MedicineCatalog.OrthodonticsFee = orthodonticsFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "prosthesis"), out decimal prosthesisFee);
            trade.MedicineCatalog.ProsthesisFee = prosthesisFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "forensic_expertise"), out decimal forensicExpertiseFee);
            trade.MedicineCatalog.ForensicExpertiseFee = forensicExpertiseFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "other"), out decimal otherFee);
            trade.MedicineCatalog.OtherFee = otherFee;
        }
        #endregion private void GetMedicineCatalogVO(TradeVO trade, XmlNode medicineCatalogNode)

        #region private void GetMedicineCatalog2VO(TradeVO trade, XmlNode medicineCatalog2Node)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trade"></param>
        /// <param name="medicineCatalog2Node"></param>
        private void GetMedicineCatalog2VO(TradeVO trade, XmlNode medicineCatalog2Node)
        {
            trade.MedicineCatalog2 = new MedicineCatalog2VO();

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "diagnosis"), out decimal diagnosisFee);
            trade.MedicineCatalog2.DiagnosisFee = diagnosisFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "examine"), out decimal examineFee);
            trade.MedicineCatalog2.ExamineFee = examineFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "labexam"), out decimal labExamFee);
            trade.MedicineCatalog2.LabExamFee = labExamFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "treatment"), out decimal treatmentFee);
            trade.MedicineCatalog2.TreatmentFee = treatmentFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "material"), out decimal materialFee);
            trade.MedicineCatalog2.MaterialFee = materialFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "operation"), out decimal operationFee);
            trade.MedicineCatalog2.OperationFee = operationFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "medicine"), out decimal medicineFee);
            trade.MedicineCatalog2.MedicineFee = medicineFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "tmedicine"), out decimal chineseMedicineFee);
            trade.MedicineCatalog2.ChineseMedicineFee = chineseMedicineFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "therb"), out decimal chineseHerbalDrinkFee);
            trade.MedicineCatalog2.ChineseHerbalDrinkFee = chineseHerbalDrinkFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "medicalservice"), out decimal medicalServiceFee);
            trade.MedicineCatalog2.MedicalServiceFee = medicalServiceFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "commonservice"), out decimal commonServiceFee);
            trade.MedicineCatalog2.CommonServiceFee = commonServiceFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "registfee"), out decimal registFee);
            trade.MedicineCatalog2.RegistFee = registFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "otheropfee"), out decimal otherOperationFee);
            trade.MedicineCatalog2.OtherOperationFee = otherOperationFee;
        }
        #endregion private void GetMedicineCatalog2VO(TradeVO trade, XmlNode medicineCatalog2Node)

        #region private string GetSubNodeValue(XmlNode parentNode, string nodeName)
        /// <summary>
        /// 查询指定节点下，指定名称节点的值
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="nodeName">子节点名称</param>
        /// <returns></returns>
        private string GetSubNodeValue(XmlNode parentNode, string nodeName)
        {
            XmlNode childNpde = parentNode.SelectSingleNode(nodeName);
            if (childNpde != null)
            {
                return childNpde.InnerText;
            }
            return null;
        }
        #endregion private string GetSubNodeValue(XmlNode parentNode, string nodeName)

    }
}