using System;
using System.Windows.Forms;
using HC.BLL;
using HC.Model.VO;
using MSXML2;
using Newtonsoft.Json;
using WebRegComLib;

namespace HC.WinService;

public delegate void ShowMessageEventHandler(String message);

public class Form1 : Form
{
    private Panel panel1;
    private TextBox tbResult;
    private Label label1;
    private TextBox tbxTradeNo;
    private Button btnCommitTradeState;
    private Button btnRefundment;
    private Button btnTrade;
    private Button btnDivide;
    private Button btnGetPersonInfo;
    private Button btnStartService;
    private WebRegClass cd;
    private MainJobThread mainJobThread;


    private void InitializeComponent()
    {
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxTradeNo = new System.Windows.Forms.TextBox();
            this.btnCommitTradeState = new System.Windows.Forms.Button();
            this.btnRefundment = new System.Windows.Forms.Button();
            this.btnTrade = new System.Windows.Forms.Button();
            this.btnDivide = new System.Windows.Forms.Button();
            this.btnGetPersonInfo = new System.Windows.Forms.Button();
            this.btnStartService = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.tbResult);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbxTradeNo);
            this.panel1.Controls.Add(this.btnCommitTradeState);
            this.panel1.Controls.Add(this.btnRefundment);
            this.panel1.Controls.Add(this.btnTrade);
            this.panel1.Controls.Add(this.btnDivide);
            this.panel1.Controls.Add(this.btnGetPersonInfo);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(790, 450);
            this.panel1.TabIndex = 9;
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(174, 70);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbResult.Size = new System.Drawing.Size(600, 370);
            this.tbResult.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "交易号";
            // 
            // tbxTradeNo
            // 
            this.tbxTradeNo.Location = new System.Drawing.Point(74, 27);
            this.tbxTradeNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbxTradeNo.Name = "tbxTradeNo";
            this.tbxTradeNo.Size = new System.Drawing.Size(379, 25);
            this.tbxTradeNo.TabIndex = 14;
            // 
            // btnCommitTradeState
            // 
            this.btnCommitTradeState.Location = new System.Drawing.Point(18, 214);
            this.btnCommitTradeState.Margin = new System.Windows.Forms.Padding(4);
            this.btnCommitTradeState.Name = "btnCommitTradeState";
            this.btnCommitTradeState.Size = new System.Drawing.Size(143, 29);
            this.btnCommitTradeState.TabIndex = 13;
            this.btnCommitTradeState.Text = "CommitTradeState";
            this.btnCommitTradeState.UseVisualStyleBackColor = true;
            this.btnCommitTradeState.Click += new System.EventHandler(this.btnCommitTradeState_Click);
            // 
            // btnRefundment
            // 
            this.btnRefundment.Location = new System.Drawing.Point(18, 178);
            this.btnRefundment.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefundment.Name = "btnRefundment";
            this.btnRefundment.Size = new System.Drawing.Size(143, 29);
            this.btnRefundment.TabIndex = 12;
            this.btnRefundment.Text = "Refundment";
            this.btnRefundment.UseVisualStyleBackColor = true;
            this.btnRefundment.Click += new System.EventHandler(this.btnRefundment_Click);
            // 
            // btnTrade
            // 
            this.btnTrade.Location = new System.Drawing.Point(18, 142);
            this.btnTrade.Margin = new System.Windows.Forms.Padding(4);
            this.btnTrade.Name = "btnTrade";
            this.btnTrade.Size = new System.Drawing.Size(143, 29);
            this.btnTrade.TabIndex = 11;
            this.btnTrade.Text = "Trade";
            this.btnTrade.UseVisualStyleBackColor = true;
            this.btnTrade.Click += new System.EventHandler(this.btnTrade_Click);
            // 
            // btnDivide
            // 
            this.btnDivide.Location = new System.Drawing.Point(18, 106);
            this.btnDivide.Margin = new System.Windows.Forms.Padding(4);
            this.btnDivide.Name = "btnDivide";
            this.btnDivide.Size = new System.Drawing.Size(143, 29);
            this.btnDivide.TabIndex = 10;
            this.btnDivide.Text = "Divide";
            this.btnDivide.UseVisualStyleBackColor = true;
            this.btnDivide.Click += new System.EventHandler(this.btnDivide_Click);
            // 
            // btnGetPersonInfo
            // 
            this.btnGetPersonInfo.Location = new System.Drawing.Point(18, 70);
            this.btnGetPersonInfo.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetPersonInfo.Name = "btnGetPersonInfo";
            this.btnGetPersonInfo.Size = new System.Drawing.Size(143, 29);
            this.btnGetPersonInfo.TabIndex = 9;
            this.btnGetPersonInfo.Text = "GetPersonInfo";
            this.btnGetPersonInfo.UseVisualStyleBackColor = true;
            this.btnGetPersonInfo.Click += new System.EventHandler(this.btnGetPersonInfo_Click);
            // 
            // btnStartService
            // 
            this.btnStartService.Location = new System.Drawing.Point(841, 65);
            this.btnStartService.Name = "btnStartService";
            this.btnStartService.Size = new System.Drawing.Size(108, 36);
            this.btnStartService.TabIndex = 10;
            this.btnStartService.Text = "启动服务";
            this.btnStartService.UseVisualStyleBackColor = true;
            this.btnStartService.Click += new System.EventHandler(this.btnStartService_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(971, 475);
            this.Controls.Add(this.btnStartService);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "医保互联医院COM调用端";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

    }

    public Form1()
    {
        InitializeComponent();
        mainJobThread = new MainJobThread();
        mainJobThread.OnShowMessage += AppendMessage;
    }

    private void AppendMessage(String message)
    {
        tbResult.AppendText(message + Environment.NewLine);
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        cd = new WebRegClass();
    }

    private void btnGetPersonInfo_Click(object sender, EventArgs e)
    {
        try
        {
            DOMDocument60Class dOMDocument60Class = new DOMDocument60Class();
            dOMDocument60Class.load("PersonInWeb.xml");
            string xml = dOMDocument60Class.xml;
            string outXml = "";
            cd.GetPersonInfo_Web(xml, out outXml);
            tbResult.Text = outXml;
            MessageBox.Show(outXml, "返回结果", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            throw;
        }
    }

    private void btnDivide_Click(object sender, EventArgs e)
    {
        try
        {
            DOMDocument60Class dOMDocument60Class = new DOMDocument60Class();
            dOMDocument60Class.load("DivideInWeb.xml");
            string xml = dOMDocument60Class.xml;
            string outXml = "";
            cd.Divide_Web(xml, out outXml);
            tbResult.Text = outXml;
            MessageBox.Show(outXml, "返回结果", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            throw;
        }
    }

    private void btnTrade_Click(object sender, EventArgs e)
    {
        try
        {
            string outXml = "";
            cd.Trade_Web(out outXml);
            tbResult.Text = outXml;
            MessageBox.Show(outXml, "返回结果", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            throw;
        }
    }

    private void btnRefundment_Click(object sender, EventArgs e)
    {
        try
        {
            DOMDocument60Class dOMDocument60Class = new DOMDocument60Class();
            dOMDocument60Class.load("RefundmentInWeb.xml");
            string xml = dOMDocument60Class.xml;
            string outXml = "";
            cd.Refundment_Web(xml, out outXml);
            tbResult.Text = outXml;
            MessageBox.Show(outXml, "返回结果", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            throw;
        }
    }

    private void btnCommitTradeState_Click(object sender, EventArgs e)
    {
        try
        {
            string tradeNO = tbxTradeNo.Text;
            string outXml = "";
            cd.CommitTradeState_Web(tradeNO, out outXml);
            tbResult.Text = outXml;
            MessageBox.Show(outXml, "返回结果", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            throw;
        }
    }


    private void btnStartService_Click(object sender, EventArgs e)
    {
        if (btnStartService.Text == "启动服务")
        {
            btnStartService.Text = "停止服务";
            tbResult.Text = "";        
            btnDivide.Enabled = false;
            btnGetPersonInfo.Enabled = false;
            btnTrade.Enabled = false;
            btnRefundment.Enabled = false;
            btnCommitTradeState.Enabled = false;
            mainJobThread.Start();
        }
        else
        {
            btnStartService.Text = "启动服务";         
            btnDivide.Enabled = true;
            btnGetPersonInfo.Enabled = true;
            btnTrade.Enabled = true;
            btnRefundment.Enabled = true;
            btnCommitTradeState.Enabled = true;
            mainJobThread.Stop();
        }

    }
}
