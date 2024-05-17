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
            //先读取是否成功
            XmlNode stateNode = rootNode.SelectSingleNode("state");
            if(stateNode != null)
            {
                comResult.State = stateNode.Attributes["success"].Value;

                XmlNodeList warnings = stateNode.SelectNodes("warning");
                if(warnings != null && warnings.Count > 0)
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
                if(errors != null && errors.Count > 0)
                {
                    comResult.Errors = new List<ErrorMessageVO>();
                    foreach(XmlNode error in errors)
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

            //查询输出部分
            XmlNode outputNode = rootNode.SelectSingleNode("output");
            if(outputNode != null)
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