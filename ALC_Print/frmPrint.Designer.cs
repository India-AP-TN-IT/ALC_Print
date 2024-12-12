namespace ALC_Print
{
    partial class frmPrint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TmrMain = new System.Windows.Forms.Timer(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ROW_SEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YMD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BODYNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VINCD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BSEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SEQNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRINT_SEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM1_PARTNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM2_PARTNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM3_PARTNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM4_PARTNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM5_PARTNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OUR_PRINT_YN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BUCKET_PRINT_YN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BSEQCD_RCV_COUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lbl_ReadInterval = new System.Windows.Forms.Label();
            this.Lbl_Customer = new System.Windows.Forms.Label();
            this.Lbl_Plant = new System.Windows.Forms.Label();
            this.Lbl_Line = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Lbl_Item = new System.Windows.Forms.Label();
            this.btn_Reprint = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btn_ForcePrint = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Lbl_SEQ = new System.Windows.Forms.Label();
            this.Lbl_YMD = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TmrMain
            // 
            this.TmrMain.Tick += new System.EventHandler(this.TmrMain_Tick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ROW_SEQ,
            this.YMD,
            this.TIME,
            this.VID,
            this.BODYNO,
            this.VINCD,
            this.BSEQ,
            this.SEQNO,
            this.ITEM1,
            this.ITEM2,
            this.ITEM3,
            this.ITEM4,
            this.ITEM5,
            this.PRINT_SEQ,
            this.ITEM1_PARTNO,
            this.ITEM2_PARTNO,
            this.ITEM3_PARTNO,
            this.ITEM4_PARTNO,
            this.ITEM5_PARTNO,
            this.OUR_PRINT_YN,
            this.BUCKET_PRINT_YN,
            this.BSEQCD_RCV_COUNT});
            this.dataGridView1.Location = new System.Drawing.Point(13, 104);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1058, 639);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // ROW_SEQ
            // 
            this.ROW_SEQ.DataPropertyName = "ROW_SEQ";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            this.ROW_SEQ.DefaultCellStyle = dataGridViewCellStyle1;
            this.ROW_SEQ.HeaderText = "No.";
            this.ROW_SEQ.Name = "ROW_SEQ";
            this.ROW_SEQ.ReadOnly = true;
            this.ROW_SEQ.Width = 50;
            // 
            // YMD
            // 
            this.YMD.DataPropertyName = "YMD";
            this.YMD.HeaderText = "YMD";
            this.YMD.Name = "YMD";
            this.YMD.ReadOnly = true;
            this.YMD.Width = 80;
            // 
            // TIME
            // 
            this.TIME.DataPropertyName = "TIME";
            this.TIME.HeaderText = "TIME";
            this.TIME.Name = "TIME";
            this.TIME.ReadOnly = true;
            this.TIME.Width = 50;
            // 
            // VID
            // 
            this.VID.DataPropertyName = "VID";
            this.VID.HeaderText = "V/ID";
            this.VID.Name = "VID";
            this.VID.ReadOnly = true;
            this.VID.Visible = false;
            this.VID.Width = 170;
            // 
            // BODYNO
            // 
            this.BODYNO.DataPropertyName = "BODY";
            this.BODYNO.FillWeight = 160F;
            this.BODYNO.HeaderText = "BODY NO";
            this.BODYNO.Name = "BODYNO";
            this.BODYNO.ReadOnly = true;
            this.BODYNO.Width = 160;
            // 
            // VINCD
            // 
            this.VINCD.DataPropertyName = "VINCD";
            this.VINCD.HeaderText = "CAR";
            this.VINCD.Name = "VINCD";
            this.VINCD.ReadOnly = true;
            this.VINCD.Width = 60;
            // 
            // BSEQ
            // 
            this.BSEQ.DataPropertyName = "BITEM1";
            this.BSEQ.HeaderText = "BSEQ";
            this.BSEQ.Name = "BSEQ";
            this.BSEQ.ReadOnly = true;
            this.BSEQ.Width = 55;
            // 
            // SEQNO
            // 
            this.SEQNO.DataPropertyName = "SEQNO";
            this.SEQNO.HeaderText = "S/NO";
            this.SEQNO.Name = "SEQNO";
            this.SEQNO.ReadOnly = true;
            this.SEQNO.Width = 55;
            // 
            // ITEM1
            // 
            this.ITEM1.DataPropertyName = "ITEM1";
            this.ITEM1.HeaderText = "ITEM1";
            this.ITEM1.Name = "ITEM1";
            this.ITEM1.ReadOnly = true;
            this.ITEM1.Width = 65;
            // 
            // ITEM2
            // 
            this.ITEM2.DataPropertyName = "ITEM2";
            this.ITEM2.HeaderText = "ITEM2";
            this.ITEM2.Name = "ITEM2";
            this.ITEM2.ReadOnly = true;
            this.ITEM2.Width = 65;
            // 
            // ITEM3
            // 
            this.ITEM3.DataPropertyName = "ITEM3";
            this.ITEM3.HeaderText = "ITEM3";
            this.ITEM3.Name = "ITEM3";
            this.ITEM3.ReadOnly = true;
            this.ITEM3.Width = 65;
            // 
            // ITEM4
            // 
            this.ITEM4.DataPropertyName = "ITEM4";
            this.ITEM4.HeaderText = "ITEM4";
            this.ITEM4.Name = "ITEM4";
            this.ITEM4.ReadOnly = true;
            this.ITEM4.Width = 65;
            // 
            // ITEM5
            // 
            this.ITEM5.DataPropertyName = "ITEM5";
            this.ITEM5.HeaderText = "ITEM5";
            this.ITEM5.Name = "ITEM5";
            this.ITEM5.ReadOnly = true;
            this.ITEM5.Width = 65;
            // 
            // PRINT_SEQ
            // 
            this.PRINT_SEQ.DataPropertyName = "RPT_PRINT_SEQ";
            this.PRINT_SEQ.HeaderText = "RPT-SEQ";
            this.PRINT_SEQ.Name = "PRINT_SEQ";
            this.PRINT_SEQ.ReadOnly = true;
            this.PRINT_SEQ.Visible = false;
            this.PRINT_SEQ.Width = 70;
            // 
            // ITEM1_PARTNO
            // 
            this.ITEM1_PARTNO.DataPropertyName = "ITEM1_PARTNO";
            this.ITEM1_PARTNO.HeaderText = "PART1";
            this.ITEM1_PARTNO.Name = "ITEM1_PARTNO";
            this.ITEM1_PARTNO.ReadOnly = true;
            this.ITEM1_PARTNO.Visible = false;
            // 
            // ITEM2_PARTNO
            // 
            this.ITEM2_PARTNO.DataPropertyName = "ITEM2_PARTNO";
            this.ITEM2_PARTNO.HeaderText = "PART2";
            this.ITEM2_PARTNO.Name = "ITEM2_PARTNO";
            this.ITEM2_PARTNO.ReadOnly = true;
            this.ITEM2_PARTNO.Visible = false;
            // 
            // ITEM3_PARTNO
            // 
            this.ITEM3_PARTNO.DataPropertyName = "ITEM3_PARTNO";
            this.ITEM3_PARTNO.HeaderText = "PART3";
            this.ITEM3_PARTNO.Name = "ITEM3_PARTNO";
            this.ITEM3_PARTNO.ReadOnly = true;
            this.ITEM3_PARTNO.Visible = false;
            // 
            // ITEM4_PARTNO
            // 
            this.ITEM4_PARTNO.DataPropertyName = "ITEM4_PARTNO";
            this.ITEM4_PARTNO.HeaderText = "PART4";
            this.ITEM4_PARTNO.Name = "ITEM4_PARTNO";
            this.ITEM4_PARTNO.ReadOnly = true;
            this.ITEM4_PARTNO.Visible = false;
            // 
            // ITEM5_PARTNO
            // 
            this.ITEM5_PARTNO.DataPropertyName = "ITEM5_PARTNO";
            this.ITEM5_PARTNO.HeaderText = "PART5";
            this.ITEM5_PARTNO.Name = "ITEM5_PARTNO";
            this.ITEM5_PARTNO.ReadOnly = true;
            this.ITEM5_PARTNO.Visible = false;
            // 
            // OUR_PRINT_YN
            // 
            this.OUR_PRINT_YN.DataPropertyName = "PRINT_YN";
            this.OUR_PRINT_YN.HeaderText = "REPORT YN";
            this.OUR_PRINT_YN.Name = "OUR_PRINT_YN";
            this.OUR_PRINT_YN.ReadOnly = true;
            this.OUR_PRINT_YN.Visible = false;
            // 
            // BUCKET_PRINT_YN
            // 
            this.BUCKET_PRINT_YN.DataPropertyName = "BPRINT_YN";
            this.BUCKET_PRINT_YN.HeaderText = "BUCKET YN";
            this.BUCKET_PRINT_YN.Name = "BUCKET_PRINT_YN";
            this.BUCKET_PRINT_YN.ReadOnly = true;
            this.BUCKET_PRINT_YN.Visible = false;
            // 
            // BSEQCD_RCV_COUNT
            // 
            this.BSEQCD_RCV_COUNT.DataPropertyName = "ITEM1_BSEQCD_RCV_COUNT";
            this.BSEQCD_RCV_COUNT.HeaderText = "BSEQ-CNT";
            this.BSEQCD_RCV_COUNT.Name = "BSEQCD_RCV_COUNT";
            this.BSEQCD_RCV_COUNT.ReadOnly = true;
            this.BSEQCD_RCV_COUNT.Visible = false;
            this.BSEQCD_RCV_COUNT.Width = 80;
            // 
            // Lbl_ReadInterval
            // 
            this.Lbl_ReadInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_ReadInterval.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Lbl_ReadInterval.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ReadInterval.Location = new System.Drawing.Point(226, 5);
            this.Lbl_ReadInterval.Name = "Lbl_ReadInterval";
            this.Lbl_ReadInterval.Size = new System.Drawing.Size(68, 39);
            this.Lbl_ReadInterval.TabIndex = 2;
            this.Lbl_ReadInterval.Text = "99/99";
            this.Lbl_ReadInterval.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Customer
            // 
            this.Lbl_Customer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Lbl_Customer.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Customer.Location = new System.Drawing.Point(80, 9);
            this.Lbl_Customer.Name = "Lbl_Customer";
            this.Lbl_Customer.Size = new System.Drawing.Size(66, 28);
            this.Lbl_Customer.TabIndex = 4;
            this.Lbl_Customer.Text = "1";
            this.Lbl_Customer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_Customer.DoubleClick += new System.EventHandler(this.Lbl_Customer_DoubleClick);
            // 
            // Lbl_Plant
            // 
            this.Lbl_Plant.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Lbl_Plant.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Plant.Location = new System.Drawing.Point(206, 9);
            this.Lbl_Plant.Name = "Lbl_Plant";
            this.Lbl_Plant.Size = new System.Drawing.Size(77, 28);
            this.Lbl_Plant.TabIndex = 5;
            this.Lbl_Plant.Text = "1";
            this.Lbl_Plant.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Line
            // 
            this.Lbl_Line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Lbl_Line.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Line.Location = new System.Drawing.Point(371, 9);
            this.Lbl_Line.Name = "Lbl_Line";
            this.Lbl_Line.Size = new System.Drawing.Size(59, 28);
            this.Lbl_Line.TabIndex = 6;
            this.Lbl_Line.Text = "1";
            this.Lbl_Line.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "Customer";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(165, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "Plant";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(337, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 16);
            this.label7.TabIndex = 9;
            this.label7.Text = "Line";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(458, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 16);
            this.label8.TabIndex = 11;
            this.label8.Text = "Item";
            // 
            // Lbl_Item
            // 
            this.Lbl_Item.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Lbl_Item.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Item.Location = new System.Drawing.Point(501, 9);
            this.Lbl_Item.Name = "Lbl_Item";
            this.Lbl_Item.Size = new System.Drawing.Size(182, 28);
            this.Lbl_Item.TabIndex = 10;
            this.Lbl_Item.Text = "1";
            this.Lbl_Item.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Reprint
            // 
            this.btn_Reprint.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_Reprint.Location = new System.Drawing.Point(9, 6);
            this.btn_Reprint.Name = "btn_Reprint";
            this.btn_Reprint.Size = new System.Drawing.Size(92, 35);
            this.btn_Reprint.TabIndex = 12;
            this.btn_Reprint.Text = "RePrint";
            this.btn_Reprint.UseVisualStyleBackColor = false;
            this.btn_Reprint.Click += new System.EventHandler(this.btn_Reprint_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(116, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(76, 36);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Auto\r\nPrinting";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btn_ForcePrint
            // 
            this.btn_ForcePrint.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_ForcePrint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ForcePrint.ForeColor = System.Drawing.Color.Red;
            this.btn_ForcePrint.Location = new System.Drawing.Point(810, 12);
            this.btn_ForcePrint.Name = "btn_ForcePrint";
            this.btn_ForcePrint.Size = new System.Drawing.Size(121, 35);
            this.btn_ForcePrint.TabIndex = 14;
            this.btn_ForcePrint.Text = "Force Print";
            this.btn_ForcePrint.UseVisualStyleBackColor = false;
            this.btn_ForcePrint.Click += new System.EventHandler(this.btn_ForcePrint_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.Lbl_ReadInterval);
            this.panel1.Controls.Add(this.btn_Reprint);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Location = new System.Drawing.Point(452, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(479, 48);
            this.panel1.TabIndex = 16;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(382, 25);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(94, 20);
            this.checkBox2.TabIndex = 14;
            this.checkBox2.Text = "Time Stop";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Lbl_SEQ);
            this.panel2.Controls.Add(this.Lbl_YMD);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(12, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(434, 48);
            this.panel2.TabIndex = 17;
            // 
            // Lbl_SEQ
            // 
            this.Lbl_SEQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_SEQ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Lbl_SEQ.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_SEQ.ForeColor = System.Drawing.Color.Blue;
            this.Lbl_SEQ.Location = new System.Drawing.Point(324, 4);
            this.Lbl_SEQ.Name = "Lbl_SEQ";
            this.Lbl_SEQ.Size = new System.Drawing.Size(107, 37);
            this.Lbl_SEQ.TabIndex = 19;
            this.Lbl_SEQ.Text = "S/NO : 0031";
            this.Lbl_SEQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_YMD
            // 
            this.Lbl_YMD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_YMD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Lbl_YMD.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_YMD.Location = new System.Drawing.Point(95, 5);
            this.Lbl_YMD.Name = "Lbl_YMD";
            this.Lbl_YMD.Size = new System.Drawing.Size(223, 37);
            this.Lbl_YMD.TabIndex = 18;
            this.Lbl_YMD.Text = "O/DDATE(YMD) : 20241011";
            this.Lbl_YMD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Last Print Order";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmPrint
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1076, 749);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_ForcePrint);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Lbl_Item);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Lbl_Line);
            this.Controls.Add(this.Lbl_Plant);
            this.Controls.Add(this.Lbl_Customer);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmPrint";
            this.Text = "ALC Print";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer TmrMain;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label Lbl_ReadInterval;
        private System.Windows.Forms.Label Lbl_Customer;
        private System.Windows.Forms.Label Lbl_Plant;
        private System.Windows.Forms.Label Lbl_Line;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label Lbl_Item;
        private System.Windows.Forms.Button btn_Reprint;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btn_ForcePrint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label Lbl_SEQ;
        private System.Windows.Forms.Label Lbl_YMD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ROW_SEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn YMD;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn VID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BODYNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VINCD;
        private System.Windows.Forms.DataGridViewTextBoxColumn BSEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn SEQNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM4;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM5;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRINT_SEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM1_PARTNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM2_PARTNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM3_PARTNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM4_PARTNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM5_PARTNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn OUR_PRINT_YN;
        private System.Windows.Forms.DataGridViewTextBoxColumn BUCKET_PRINT_YN;
        private System.Windows.Forms.DataGridViewTextBoxColumn BSEQCD_RCV_COUNT;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}

