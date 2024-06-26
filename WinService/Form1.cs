using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HC.WinService;

public delegate void ShowMessageEventHandler(String message);

public class Form1 : Form
{
    private Button btnStartService;
    private CheckBox chkIsTest;
    private MainJobThread mainJobThread;
    private TextBox tbResult;
    private TestJobThread testJobThread;

    private void InitializeComponent()
    {
        this.btnStartService = new System.Windows.Forms.Button();
        this.chkIsTest = new System.Windows.Forms.CheckBox();
        this.tbResult = new System.Windows.Forms.TextBox();
        this.SuspendLayout();
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
        // chkIsTest
        // 
        this.chkIsTest.AutoSize = true;
        this.chkIsTest.Location = new System.Drawing.Point(848, 25);
        this.chkIsTest.Name = "chkIsTest";
        this.chkIsTest.Size = new System.Drawing.Size(89, 19);
        this.chkIsTest.TabIndex = 11;
        this.chkIsTest.Text = "模拟模式";
        this.chkIsTest.UseVisualStyleBackColor = true;
        // 
        // tbResult
        // 
        this.tbResult.Location = new System.Drawing.Point(2, 2);
        this.tbResult.Multiline = true;
        this.tbResult.Name = "tbResult";
        this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.tbResult.Size = new System.Drawing.Size(833, 461);
        this.tbResult.TabIndex = 18;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        this.ClientSize = new System.Drawing.Size(971, 475);
        this.Controls.Add(this.tbResult);
        this.Controls.Add(this.chkIsTest);
        this.Controls.Add(this.btnStartService);
        this.Margin = new System.Windows.Forms.Padding(4);
        this.Name = "Form1";
        this.Text = "医保互联医院COM调用端";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    public Form1()
    {
        InitializeComponent();
        mainJobThread = new MainJobThread();
        mainJobThread.OnShowMessage += AppendMessage;

        testJobThread = new TestJobThread();
        testJobThread.OnShowMessage += AppendMessage;

    }

    private void AppendMessage(String message)
    {
        tbResult.AppendText(message + Environment.NewLine);
    }


    private void btnStartService_Click(object sender, EventArgs e)
    {
        if (btnStartService.Text == "启动服务")
        {
            btnStartService.Text = "停止服务";
            chkIsTest.Enabled = false;
            tbResult.Text = "";
            if (chkIsTest.Checked)
            {
                testJobThread.Start();
            }
            else
            {
                mainJobThread.Start();
            }

        }
        else
        {
            btnStartService.Text = "启动服务";
            if (chkIsTest.Checked)
            {
                testJobThread.Stop();
            }
            else
            {
                mainJobThread.Stop();
            }
            Thread.Sleep(2000);
            chkIsTest.Enabled = true;
        }

    }
}
