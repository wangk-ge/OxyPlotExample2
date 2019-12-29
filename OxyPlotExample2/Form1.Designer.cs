namespace OxyPlotExample2
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonClean = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxCmd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonStatistical = new System.Windows.Forms.Button();
            this.numericUpDownCursor2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCursor1 = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonResetScale = new System.Windows.Forms.Button();
            this.textBoxFilterAvg = new System.Windows.Forms.TextBox();
            this.textBoxAvg = new System.Windows.Forms.TextBox();
            this.textBoxFilterNum = new System.Windows.Forms.TextBox();
            this.textBoxNum = new System.Windows.Forms.TextBox();
            this.textBoxFilterSum = new System.Windows.Forms.TextBox();
            this.textBoxSum = new System.Windows.Forms.TextBox();
            this.textBoxFilterRange = new System.Windows.Forms.TextBox();
            this.textBoxRange = new System.Windows.Forms.TextBox();
            this.textBoxFilterMin = new System.Windows.Forms.TextBox();
            this.textBoxMin = new System.Windows.Forms.TextBox();
            this.textBoxFilterMax = new System.Windows.Forms.TextBox();
            this.textBoxMax = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxAutoCursor = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoScroll = new System.Windows.Forms.CheckBox();
            this.buttonFilterApply = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownR = new System.Windows.Forms.NumericUpDown();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBoxCom = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonZero = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLoad = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonClear = new System.Windows.Forms.ToolStripButton();
            this.plotView1 = new OxyPlot.WindowsForms.PlotView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCursor2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCursor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownR)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonClean);
            this.panel1.Controls.Add(this.buttonSend);
            this.panel1.Controls.Add(this.textBoxCmd);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 160);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1334, 56);
            this.panel1.TabIndex = 0;
            // 
            // buttonClean
            // 
            this.buttonClean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClean.Location = new System.Drawing.Point(1254, 15);
            this.buttonClean.Name = "buttonClean";
            this.buttonClean.Size = new System.Drawing.Size(68, 29);
            this.buttonClean.TabIndex = 6;
            this.buttonClean.Text = "清空";
            this.buttonClean.UseVisualStyleBackColor = true;
            this.buttonClean.Click += new System.EventHandler(this.buttonClean_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Location = new System.Drawing.Point(1177, 15);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(66, 29);
            this.buttonSend.TabIndex = 5;
            this.buttonSend.Text = "发送";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxCmd
            // 
            this.textBoxCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCmd.Location = new System.Drawing.Point(59, 20);
            this.textBoxCmd.Name = "textBoxCmd";
            this.textBoxCmd.Size = new System.Drawing.Size(1112, 21);
            this.textBoxCmd.TabIndex = 3;
            this.textBoxCmd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxCmd_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "命令：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBoxInfo);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 273);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1334, 216);
            this.panel2.TabIndex = 2;
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxInfo.Location = new System.Drawing.Point(0, 78);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.ReadOnly = true;
            this.textBoxInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInfo.Size = new System.Drawing.Size(1334, 82);
            this.textBoxInfo.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonStatistical);
            this.panel3.Controls.Add(this.numericUpDownCursor2);
            this.panel3.Controls.Add(this.numericUpDownCursor1);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.buttonResetScale);
            this.panel3.Controls.Add(this.textBoxFilterAvg);
            this.panel3.Controls.Add(this.textBoxAvg);
            this.panel3.Controls.Add(this.textBoxFilterNum);
            this.panel3.Controls.Add(this.textBoxNum);
            this.panel3.Controls.Add(this.textBoxFilterSum);
            this.panel3.Controls.Add(this.textBoxSum);
            this.panel3.Controls.Add(this.textBoxFilterRange);
            this.panel3.Controls.Add(this.textBoxRange);
            this.panel3.Controls.Add(this.textBoxFilterMin);
            this.panel3.Controls.Add(this.textBoxMin);
            this.panel3.Controls.Add(this.textBoxFilterMax);
            this.panel3.Controls.Add(this.textBoxMax);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.checkBoxAutoCursor);
            this.panel3.Controls.Add(this.checkBoxAutoScroll);
            this.panel3.Controls.Add(this.buttonFilterApply);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.numericUpDownR);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1334, 78);
            this.panel3.TabIndex = 2;
            // 
            // buttonStatistical
            // 
            this.buttonStatistical.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStatistical.Enabled = false;
            this.buttonStatistical.Location = new System.Drawing.Point(1088, 49);
            this.buttonStatistical.Name = "buttonStatistical";
            this.buttonStatistical.Size = new System.Drawing.Size(130, 23);
            this.buttonStatistical.TabIndex = 15;
            this.buttonStatistical.Text = "统计";
            this.buttonStatistical.UseVisualStyleBackColor = true;
            this.buttonStatistical.Click += new System.EventHandler(this.buttonStatistical_Click);
            // 
            // numericUpDownCursor2
            // 
            this.numericUpDownCursor2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownCursor2.Enabled = false;
            this.numericUpDownCursor2.Location = new System.Drawing.Point(1161, 26);
            this.numericUpDownCursor2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownCursor2.Name = "numericUpDownCursor2";
            this.numericUpDownCursor2.Size = new System.Drawing.Size(57, 21);
            this.numericUpDownCursor2.TabIndex = 14;
            this.numericUpDownCursor2.ValueChanged += new System.EventHandler(this.numericUpDownCursor2_ValueChanged);
            // 
            // numericUpDownCursor1
            // 
            this.numericUpDownCursor1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownCursor1.Enabled = false;
            this.numericUpDownCursor1.Location = new System.Drawing.Point(1088, 26);
            this.numericUpDownCursor1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownCursor1.Name = "numericUpDownCursor1";
            this.numericUpDownCursor1.Size = new System.Drawing.Size(57, 21);
            this.numericUpDownCursor1.TabIndex = 14;
            this.numericUpDownCursor1.ValueChanged += new System.EventHandler(this.numericUpDownCursor1_ValueChanged);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1159, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 13;
            this.label12.Text = "游标2位置";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1086, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 13;
            this.label11.Text = "游标1位置";
            // 
            // buttonResetScale
            // 
            this.buttonResetScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonResetScale.Location = new System.Drawing.Point(1259, 47);
            this.buttonResetScale.Name = "buttonResetScale";
            this.buttonResetScale.Size = new System.Drawing.Size(72, 23);
            this.buttonResetScale.TabIndex = 12;
            this.buttonResetScale.Text = "缩放还原";
            this.buttonResetScale.UseVisualStyleBackColor = true;
            this.buttonResetScale.Click += new System.EventHandler(this.buttonResetScale_Click);
            // 
            // textBoxFilterAvg
            // 
            this.textBoxFilterAvg.Location = new System.Drawing.Point(926, 46);
            this.textBoxFilterAvg.Name = "textBoxFilterAvg";
            this.textBoxFilterAvg.ReadOnly = true;
            this.textBoxFilterAvg.Size = new System.Drawing.Size(135, 21);
            this.textBoxFilterAvg.TabIndex = 11;
            // 
            // textBoxAvg
            // 
            this.textBoxAvg.Location = new System.Drawing.Point(926, 25);
            this.textBoxAvg.Name = "textBoxAvg";
            this.textBoxAvg.ReadOnly = true;
            this.textBoxAvg.Size = new System.Drawing.Size(135, 21);
            this.textBoxAvg.TabIndex = 11;
            // 
            // textBoxFilterNum
            // 
            this.textBoxFilterNum.Location = new System.Drawing.Point(791, 46);
            this.textBoxFilterNum.Name = "textBoxFilterNum";
            this.textBoxFilterNum.ReadOnly = true;
            this.textBoxFilterNum.Size = new System.Drawing.Size(135, 21);
            this.textBoxFilterNum.TabIndex = 11;
            // 
            // textBoxNum
            // 
            this.textBoxNum.Location = new System.Drawing.Point(791, 25);
            this.textBoxNum.Name = "textBoxNum";
            this.textBoxNum.ReadOnly = true;
            this.textBoxNum.Size = new System.Drawing.Size(135, 21);
            this.textBoxNum.TabIndex = 11;
            // 
            // textBoxFilterSum
            // 
            this.textBoxFilterSum.Location = new System.Drawing.Point(656, 46);
            this.textBoxFilterSum.Name = "textBoxFilterSum";
            this.textBoxFilterSum.ReadOnly = true;
            this.textBoxFilterSum.Size = new System.Drawing.Size(135, 21);
            this.textBoxFilterSum.TabIndex = 11;
            // 
            // textBoxSum
            // 
            this.textBoxSum.Location = new System.Drawing.Point(656, 25);
            this.textBoxSum.Name = "textBoxSum";
            this.textBoxSum.ReadOnly = true;
            this.textBoxSum.Size = new System.Drawing.Size(135, 21);
            this.textBoxSum.TabIndex = 11;
            // 
            // textBoxFilterRange
            // 
            this.textBoxFilterRange.Location = new System.Drawing.Point(521, 46);
            this.textBoxFilterRange.Name = "textBoxFilterRange";
            this.textBoxFilterRange.ReadOnly = true;
            this.textBoxFilterRange.Size = new System.Drawing.Size(135, 21);
            this.textBoxFilterRange.TabIndex = 11;
            // 
            // textBoxRange
            // 
            this.textBoxRange.Location = new System.Drawing.Point(521, 25);
            this.textBoxRange.Name = "textBoxRange";
            this.textBoxRange.ReadOnly = true;
            this.textBoxRange.Size = new System.Drawing.Size(135, 21);
            this.textBoxRange.TabIndex = 11;
            // 
            // textBoxFilterMin
            // 
            this.textBoxFilterMin.Location = new System.Drawing.Point(386, 46);
            this.textBoxFilterMin.Name = "textBoxFilterMin";
            this.textBoxFilterMin.ReadOnly = true;
            this.textBoxFilterMin.Size = new System.Drawing.Size(135, 21);
            this.textBoxFilterMin.TabIndex = 11;
            // 
            // textBoxMin
            // 
            this.textBoxMin.Location = new System.Drawing.Point(386, 25);
            this.textBoxMin.Name = "textBoxMin";
            this.textBoxMin.ReadOnly = true;
            this.textBoxMin.Size = new System.Drawing.Size(135, 21);
            this.textBoxMin.TabIndex = 11;
            // 
            // textBoxFilterMax
            // 
            this.textBoxFilterMax.Location = new System.Drawing.Point(251, 46);
            this.textBoxFilterMax.Name = "textBoxFilterMax";
            this.textBoxFilterMax.ReadOnly = true;
            this.textBoxFilterMax.Size = new System.Drawing.Size(135, 21);
            this.textBoxFilterMax.TabIndex = 10;
            // 
            // textBoxMax
            // 
            this.textBoxMax.Location = new System.Drawing.Point(251, 25);
            this.textBoxMax.Name = "textBoxMax";
            this.textBoxMax.ReadOnly = true;
            this.textBoxMax.Size = new System.Drawing.Size(135, 21);
            this.textBoxMax.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(837, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "样本数";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(702, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "累积和";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(974, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "平均值";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(558, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "波动范围";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(193, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 5;
            this.label10.Text = "滤波数据";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(193, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "原始数据";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(303, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "最大值";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(434, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "最小值";
            // 
            // checkBoxAutoCursor
            // 
            this.checkBoxAutoCursor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAutoCursor.AutoSize = true;
            this.checkBoxAutoCursor.Checked = true;
            this.checkBoxAutoCursor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoCursor.Location = new System.Drawing.Point(1259, 26);
            this.checkBoxAutoCursor.Name = "checkBoxAutoCursor";
            this.checkBoxAutoCursor.Size = new System.Drawing.Size(72, 16);
            this.checkBoxAutoCursor.TabIndex = 3;
            this.checkBoxAutoCursor.Text = "自动游标";
            this.checkBoxAutoCursor.UseVisualStyleBackColor = true;
            this.checkBoxAutoCursor.CheckedChanged += new System.EventHandler(this.checkBoxAutoCursor_CheckedChanged);
            // 
            // checkBoxAutoScroll
            // 
            this.checkBoxAutoScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAutoScroll.AutoSize = true;
            this.checkBoxAutoScroll.Checked = true;
            this.checkBoxAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoScroll.Location = new System.Drawing.Point(1259, 9);
            this.checkBoxAutoScroll.Name = "checkBoxAutoScroll";
            this.checkBoxAutoScroll.Size = new System.Drawing.Size(72, 16);
            this.checkBoxAutoScroll.TabIndex = 3;
            this.checkBoxAutoScroll.Text = "自动滚屏";
            this.checkBoxAutoScroll.UseVisualStyleBackColor = true;
            // 
            // buttonFilterApply
            // 
            this.buttonFilterApply.Location = new System.Drawing.Point(10, 45);
            this.buttonFilterApply.Name = "buttonFilterApply";
            this.buttonFilterApply.Size = new System.Drawing.Size(129, 27);
            this.buttonFilterApply.TabIndex = 2;
            this.buttonFilterApply.Text = "应用";
            this.buttonFilterApply.UseVisualStyleBackColor = true;
            this.buttonFilterApply.Click += new System.EventHandler(this.buttonFilterApply_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "滤波参数R：";
            // 
            // numericUpDownR
            // 
            this.numericUpDownR.DecimalPlaces = 2;
            this.numericUpDownR.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownR.Location = new System.Drawing.Point(10, 22);
            this.numericUpDownR.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownR.Name = "numericUpDownR";
            this.numericUpDownR.Size = new System.Drawing.Size(129, 21);
            this.numericUpDownR.TabIndex = 0;
            this.numericUpDownR.Value = new decimal(new int[] {
            100,
            0,
            0,
            65536});
            this.numericUpDownR.ValueChanged += new System.EventHandler(this.numericUpDownR_ValueChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxCom,
            this.toolStripButtonOpen,
            this.toolStripButtonRefresh,
            this.toolStripSeparator1,
            this.toolStripButtonStart,
            this.toolStripButtonZero,
            this.toolStripSeparator2,
            this.toolStripButtonSave,
            this.toolStripButtonLoad,
            this.toolStripButtonClear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1334, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripComboBoxCom
            // 
            this.toolStripComboBoxCom.MaxDropDownItems = 20;
            this.toolStripComboBoxCom.Name = "toolStripComboBoxCom";
            this.toolStripComboBoxCom.Size = new System.Drawing.Size(75, 25);
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpen.Image")));
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonOpen.Text = "连接";
            this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonRefresh.Text = "刷新";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonStart
            // 
            this.toolStripButtonStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonStart.Enabled = false;
            this.toolStripButtonStart.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStart.Image")));
            this.toolStripButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStart.Name = "toolStripButtonStart";
            this.toolStripButtonStart.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonStart.Text = "开始";
            this.toolStripButtonStart.Click += new System.EventHandler(this.toolStripButtonStart_Click);
            // 
            // toolStripButtonZero
            // 
            this.toolStripButtonZero.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonZero.Enabled = false;
            this.toolStripButtonZero.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonZero.Image")));
            this.toolStripButtonZero.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZero.Name = "toolStripButtonZero";
            this.toolStripButtonZero.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonZero.Text = "归零";
            this.toolStripButtonZero.Click += new System.EventHandler(this.toolStripButtonZero_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonSave.Text = "保存";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripButtonLoad
            // 
            this.toolStripButtonLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonLoad.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLoad.Image")));
            this.toolStripButtonLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoad.Name = "toolStripButtonLoad";
            this.toolStripButtonLoad.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonLoad.Text = "加载";
            this.toolStripButtonLoad.Click += new System.EventHandler(this.toolStripButtonLoad_Click);
            // 
            // toolStripButtonClear
            // 
            this.toolStripButtonClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonClear.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonClear.Image")));
            this.toolStripButtonClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClear.Name = "toolStripButtonClear";
            this.toolStripButtonClear.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonClear.Text = "清除";
            this.toolStripButtonClear.Click += new System.EventHandler(this.toolStripButtonClear_Click);
            // 
            // plotView1
            // 
            this.plotView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotView1.Location = new System.Drawing.Point(0, 25);
            this.plotView1.Name = "plotView1";
            this.plotView1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView1.Size = new System.Drawing.Size(1334, 248);
            this.plotView1.TabIndex = 5;
            this.plotView1.Text = "plotView1";
            this.plotView1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 489);
            this.Controls.Add(this.plotView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCursor2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCursor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownR)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxCmd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonStart;
        private System.Windows.Forms.ToolStripButton toolStripButtonZero;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoad;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxCom;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripButton toolStripButtonClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownR;
        private System.Windows.Forms.Button buttonFilterApply;
        private System.Windows.Forms.CheckBox checkBoxAutoScroll;
        private System.Windows.Forms.Button buttonClean;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxFilterAvg;
        private System.Windows.Forms.TextBox textBoxAvg;
        private System.Windows.Forms.TextBox textBoxFilterNum;
        private System.Windows.Forms.TextBox textBoxNum;
        private System.Windows.Forms.TextBox textBoxFilterSum;
        private System.Windows.Forms.TextBox textBoxSum;
        private System.Windows.Forms.TextBox textBoxFilterRange;
        private System.Windows.Forms.TextBox textBoxRange;
        private System.Windows.Forms.TextBox textBoxFilterMin;
        private System.Windows.Forms.TextBox textBoxMin;
        private System.Windows.Forms.TextBox textBoxFilterMax;
        private System.Windows.Forms.TextBox textBoxMax;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonResetScale;
        private System.Windows.Forms.NumericUpDown numericUpDownCursor2;
        private System.Windows.Forms.NumericUpDown numericUpDownCursor1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonStatistical;
        private System.Windows.Forms.CheckBox checkBoxAutoCursor;
    }
}

