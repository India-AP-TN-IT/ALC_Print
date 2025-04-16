using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ALC_Print
{
    public partial class frmPrint : Form
    {
        
        PrintHelper m_BUCKET_PRT = null;
        
        private const string CN_CONFIG_XML_PATH = @".\HE_MES_Config.xml";
        
        private DataTable m_dtXML = null;
        private HE_MES.DB.ORACLE_DB m_DB = null;
        public HE_MES.DB.ORACLE_DB DB
        {
            get { return m_DB; }
        }
        int m_ReloadTimeSec = 0;
        int m_ReloadTimeSec_Bak = 0;
        DateTime m_QDate = new DateTime();

        public bool m_bBucketPrintReady = true;
        public int m_iOurPrintMaxCount = 1;
        public int m_iReport_Seq_Order = 0;

        public static bool m_bPrinted = false;

        public static bool m_bChangeDate = false;

        //public static bool m_bForcePrint = false;
        public bool m_bForcePrint = false;

        public static string m_strPreviewPrint = "N";

        public static string m_strPrintStartDate = "";

        public static bool m_bPGStart = true;
        public string GetPackageName()
        {
            if (GetXML("CUSTCD") == "HMI")
            {
                return "APG_ALC_PRINT";
            }
            else
            {
                return "APG_ALC_PRINT_V2";
            }
        }
        public frmPrint()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            m_DB = new HE_MES.DB.ORACLE_DB();
            
            m_DB.Open(CN_CONFIG_XML_PATH);
            m_dtXML = OpenXML(CN_CONFIG_XML_PATH);
            m_ReloadTimeSec = HE_MES.FX.Utils.Glb_FNS.GetO2I(GetXML("RELOAD_TICK_SEC"));
            m_ReloadTimeSec_Bak = m_ReloadTimeSec;
            SetTitle();
            Lbl_ReadInterval.BackColor = Color.LightSkyBlue;
            TmrMain.Start();

            m_QDate = DateTime.Now.AddSeconds(m_ReloadTimeSec * -1);
            dataGridView1.AutoGenerateColumns = false;
            SetGrid();

            Lbl_Customer.Text = GetXML("CUSTCD");
            Lbl_Plant.Text = GetXML("CUST_PLANT");
            Lbl_Line.Text = GetXML("CUST_LINE");
            Lbl_Item.Text = Lbl_Item.Text + "(" + GetXML("ITEM") + ")";

            m_strPreviewPrint = GetXML("PREVIEW_PRINT_YN");

            
            PrintHelper.STCUSTINFO custInfo = new PrintHelper.STCUSTINFO();
            custInfo.corcd = GetXML("CORCD");
            custInfo.bizcd = GetXML("BIZCD");
            custInfo.cust_plant = GetXML("CUST_PLANT");
            custInfo.custcd = GetXML("CUSTCD");
            custInfo.cust_line = GetXML("CUST_LINE");
            custInfo.item = GetXML("ITEM");
            custInfo.BQTY = Convert.ToInt32(GetXML("BUCKET_COUNT"));
            m_BUCKET_PRT = new PrintHelper(GetPackageName(), GetXML("ITEM"), custInfo, ref m_DB);

        }
        
        private void SetTitle()
        {
            string strItem = "";

            dataGridView1.Columns["ITEM1"].Visible = false;
            dataGridView1.Columns["ITEM2"].Visible = false;
            dataGridView1.Columns["ITEM3"].Visible = false;
            dataGridView1.Columns["ITEM4"].Visible = false;
            if (GetXML("CUSTCD") == "HMI")
            {
                switch (GetXML("ITEM"))
                {
                    //BUMPER
                    case "Q129":
                    case "Q130":
                        strItem = "[BUMPER]";
                        Lbl_Item.Text = "BUMPER";
                        break;
                    //DT
                    case "Q045":
                    case "Q046":
                    case "Q047":
                    case "Q057":
                        strItem = "[DOOR TRIM]";
                        Lbl_Item.Text = "D/TRIM";
                        break;

                    //SPOILER
                    case "Q117":
                        strItem = "[SPOILER]";
                        Lbl_Item.Text = "SPOILER";
                        break;
                    //T/GATE
                    case "Q100":
                        strItem = "[T/GATE]";
                        Lbl_Item.Text = "T/GATE";
                        break;
                    // LISDE
                    case "Q008":
                    case "Q109":
                        strItem = "[L/SIDE]";
                        Lbl_Item.Text = "L/SIDE";
                        break;

                }
            }
            else
            {
                switch (GetXML("ITEM"))
                {
                    //BUMPER
                    case "Q034":
                        strItem = "[BUMPER]";
                        Lbl_Item.Text = "BUMPER-RR";
                        break;
                    case "Q036":
                        strItem = "[BUMPER]";
                        Lbl_Item.Text = "BUMPER-FRT";
                        break;
                    //DT
                    case "Q094":
                    case "Q093":
                    case "Q088":
                    case "Q087":
                        strItem = "[DOOR TRIM]";
                        Lbl_Item.Text = "D/TRIM";
                        break;

                    //TAIL GATE
                    case "Q023":
                    case "Q030":
                        strItem = "[TAIL GATE]";
                        Lbl_Item.Text = "T/GATE";
                        break;

                    //SPOILER
                    case "Q033":
                        strItem = "[SPOILER]";
                        Lbl_Item.Text = "SPOILER";
                        break;

                    //GARNISH ASSY DR FRONT SIDE 
                    case "Q027":
                    case "Q026":
                    case "Q025":
                    case "Q024":
                        strItem = "[GARNISH ASSY DR SIDE]";
                        Lbl_Item.Text = "G/ASSY DR SIDE";
                        break;

                    //MOLD S/SIDE 
                    case "P401":
                    case "P402":
                        strItem = "[MOLD S/SIDE ASSY]";
                        Lbl_Item.Text = "S/SIDE";
                        break;
                    //R/RACK
                    case "Q031":
                    case "Q032":
                        strItem = "[R/RACK LH AND RH]";
                        Lbl_Item.Text = "R/RACK";
                        break;
                    // R/GRILL
                    case "P684":
                        strItem = "[R/GRILL]";
                        Lbl_Item.Text = "R/GRILL";
                        break;

                }
            }

            this.Text = strItem + " " + this.Text; 
        }

        private void SetGrid()
        {
            dataGridView1.Columns["ITEM1"].Visible = false;
            dataGridView1.Columns["ITEM2"].Visible = false;
            dataGridView1.Columns["ITEM3"].Visible = false;
            dataGridView1.Columns["ITEM4"].Visible = false;
            dataGridView1.Columns["ITEM5"].Visible = false;
            if (GetXML("CUSTCD") == "HMI")
            {
                switch (GetXML("ITEM"))
                {
                    //BUMPER
                    case "Q129":
                    case "Q130":
                        dataGridView1.Columns["ITEM1"].Visible = true;
                        dataGridView1.Columns["ITEM2"].Visible = true;

                        dataGridView1.Columns["ITEM1"].HeaderText = "FRT";
                        dataGridView1.Columns["ITEM2"].HeaderText = "REAR";
                        break;

                    //DT
                    case "Q045":
                    case "Q046":
                    case "Q047":
                    case "Q057":
                        dataGridView1.Columns["ITEM1"].Visible = true;
                        dataGridView1.Columns["ITEM2"].Visible = true;
                        dataGridView1.Columns["ITEM3"].Visible = true;
                        dataGridView1.Columns["ITEM4"].Visible = true;



                        dataGridView1.Columns["ITEM1"].HeaderText = "F/LH";
                        dataGridView1.Columns["ITEM2"].HeaderText = "F/RH";
                        dataGridView1.Columns["ITEM3"].HeaderText = "R/LH";
                        dataGridView1.Columns["ITEM4"].HeaderText = "R/RH";

                        break;

                    //TAIL GATE
                    case "Q023":
                    case "Q030":
                        dataGridView1.Columns["ITEM1"].Visible = true;

                        dataGridView1.Columns["ITEM1"].HeaderText = "T/GATE";
                        break;
                    //SPOILER
                    case "Q117":
                        dataGridView1.Columns["ITEM1"].Visible = true;

                        dataGridView1.Columns["ITEM1"].HeaderText = "SPOILER";
                        break;

                    //GARNISH ASSY DR SIDE 
                    case "Q027":
                    case "Q026":
                    case "Q025":
                    case "Q024":
                        dataGridView1.Columns["ITEM1"].Visible = true;
                        dataGridView1.Columns["ITEM2"].Visible = true;
                        dataGridView1.Columns["ITEM3"].Visible = true;
                        dataGridView1.Columns["ITEM4"].Visible = true;

                        dataGridView1.Columns["ITEM1"].HeaderText = "F/LH";
                        dataGridView1.Columns["ITEM2"].HeaderText = "F/RH";
                        dataGridView1.Columns["ITEM3"].HeaderText = "R/LH";
                        dataGridView1.Columns["ITEM4"].HeaderText = "R/RH";
                        break;

                    //L/SDIE
                    case "Q008":
                    case "Q109":
                        dataGridView1.Columns["ITEM1"].Visible = true;
                        dataGridView1.Columns["ITEM2"].Visible = true;

                        dataGridView1.Columns["ITEM1"].HeaderText = "LH";
                        dataGridView1.Columns["ITEM2"].HeaderText = "RH";
                        break;

                    //T/GATE
                    case "Q100":
                        dataGridView1.Columns["ITEM1"].Visible = true;

                        dataGridView1.Columns["ITEM1"].HeaderText = "T/GATE";
                        break;

                }
            }
            else
            {
                switch (GetXML("ITEM"))
                {
                    //BUMPER
                    case "Q034":
                        dataGridView1.Columns["ITEM1"].Visible = true;

                        dataGridView1.Columns["ITEM1"].HeaderText = "REAR";
                        break;
                    case "Q036":
                        dataGridView1.Columns["ITEM1"].Visible = true;

                        dataGridView1.Columns["ITEM1"].HeaderText = "FRT";
                        break;

                    //DT
                    case "Q094":
                    case "Q093":
                    case "Q088":
                    case "Q087":
                        dataGridView1.Columns["ITEM1"].Visible = true;
                        dataGridView1.Columns["ITEM2"].Visible = true;
                        dataGridView1.Columns["ITEM3"].Visible = true;
                        dataGridView1.Columns["ITEM4"].Visible = true;
                        dataGridView1.Columns["ITEM5"].Visible = false;



                        dataGridView1.Columns["ITEM1"].HeaderText = "F/LH";
                        dataGridView1.Columns["ITEM2"].HeaderText = "F/RH";
                        dataGridView1.Columns["ITEM3"].HeaderText = "R/LH";
                        dataGridView1.Columns["ITEM4"].HeaderText = "R/RH";
                        dataGridView1.Columns["ITEM5"].HeaderText = "T/GATE";

                        break;

                    //TAIL GATE
                    case "Q023":
                    case "Q030":
                        dataGridView1.Columns["ITEM1"].Visible = true;

                        dataGridView1.Columns["ITEM1"].HeaderText = "T/GATE";
                        break;
                    //SPOILER
                    case "Q033":
                        dataGridView1.Columns["ITEM1"].Visible = true;

                        dataGridView1.Columns["ITEM1"].HeaderText = "SPOILER";
                        break;

                    //GARNISH ASSY DR SIDE 
                    case "Q027":
                    case "Q026":
                    case "Q025":
                    case "Q024":
                        dataGridView1.Columns["ITEM1"].Visible = true;
                        dataGridView1.Columns["ITEM2"].Visible = true;
                        dataGridView1.Columns["ITEM3"].Visible = true;
                        dataGridView1.Columns["ITEM4"].Visible = true;

                        dataGridView1.Columns["ITEM1"].HeaderText = "F/LH";
                        dataGridView1.Columns["ITEM2"].HeaderText = "F/RH";
                        dataGridView1.Columns["ITEM3"].HeaderText = "R/LH";
                        dataGridView1.Columns["ITEM4"].HeaderText = "R/RH";
                        break;

                    //MOLD S/SIDE 
                    case "P401":
                    case "P402":
                        dataGridView1.Columns["ITEM1"].Visible = true;
                        dataGridView1.Columns["ITEM2"].Visible = true;

                        dataGridView1.Columns["ITEM1"].HeaderText = "LH";
                        dataGridView1.Columns["ITEM2"].HeaderText = "RH";
                        break;
                    /// R/RACK
                    case "Q032":
                    case "Q031":
                        dataGridView1.Columns["ITEM1"].Visible = true;
                        dataGridView1.Columns["ITEM2"].Visible = true;

                        dataGridView1.Columns["ITEM1"].HeaderText = "LH";
                        dataGridView1.Columns["ITEM2"].HeaderText = "RH";
                        break;
                    // R/GRILL UPPER
                    case "P684":
                        dataGridView1.Columns["ITEM1"].Visible = true;

                        dataGridView1.Columns["ITEM1"].HeaderText = "R/GRILL";
                        break;

                }
            }
        }

        public string GetXML(string element)
        {
            if (m_dtXML.Rows.Count > 0 && m_dtXML.Columns.Contains(element))
            {
                return m_dtXML.Rows[0][element].ToString();
            }
            return "";
        }
        public DataTable GetData(string ymd = "")
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("IN_CORD", GetXML("CORCD"));
            param.Add("IN_BIZCD", GetXML("BIZCD"));
            param.Add("IN_YMD", ymd);
            //param.Add("IN_YMD", "20190708");
            param.Add("IN_CUSTCD", GetXML("CUSTCD"));
            param.Add("IN_CUST_PLANT", GetXML("CUST_PLANT"));
            param.Add("IN_CUST_LINE", GetXML("CUST_LINE"));
            param.Add("IN_ITEM", GetXML("ITEM"));
            DataTable dt = m_DB.ExcuteQuery(GetPackageName() +".GET_DATA", param);
            return dt;
        }

        public DataTable GetData_Reprint(string ymd, string bseqcd, string bseq_rcv_count )
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("IN_CORD", GetXML("CORCD"));
            param.Add("IN_BIZCD", GetXML("BIZCD"));
            param.Add("IN_YMD", ymd);
            //param.Add("IN_YMD", "20190515");
            param.Add("IN_CUSTCD", GetXML("CUSTCD"));
            param.Add("IN_CUST_PLANT", GetXML("CUST_PLANT"));
            param.Add("IN_CUST_LINE", GetXML("CUST_LINE"));
            param.Add("IN_ITEM", GetXML("ITEM"));
            param.Add("IN_BSEQ", bseqcd);
            param.Add("IN_RCV_COUNT", bseq_rcv_count);
            DataTable dt = m_DB.ExcuteQuery(GetPackageName() + ".GET_REPRINT_BSEQ2D", param);
            return dt;
        }

        public DataTable GetData_Reprint_Seqno(string ymd, string seqno)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("IN_CORD", GetXML("CORCD"));
            param.Add("IN_BIZCD", GetXML("BIZCD"));
            param.Add("IN_YMD", ymd);
            //param.Add("IN_YMD", "20190515");
            param.Add("IN_CUSTCD", GetXML("CUSTCD"));
            param.Add("IN_CUST_PLANT", GetXML("CUST_PLANT"));
            param.Add("IN_CUST_LINE", GetXML("CUST_LINE"));
            param.Add("IN_ITEM", GetXML("ITEM"));
            param.Add("IN_SEQNO", seqno);
            DataTable dt = m_DB.ExcuteQuery(GetPackageName() + ".GET_REPRINT_SEQNO", param);
            return dt;
        }

        

        public DataTable GetData_Reprint_OurRPT_Seqno(string ymd, string seqno)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("IN_CORD", GetXML("CORCD"));
            param.Add("IN_BIZCD", GetXML("BIZCD"));
            param.Add("IN_YMD", ymd);
            //param.Add("IN_YMD", "20190515");
            param.Add("IN_CUSTCD", GetXML("CUSTCD"));
            param.Add("IN_CUST_PLANT", GetXML("CUST_PLANT"));
            param.Add("IN_CUST_LINE", GetXML("CUST_LINE"));
            param.Add("IN_ITEM", GetXML("ITEM"));
            param.Add("IN_SEQNO", seqno);
            DataTable dt = m_DB.ExcuteQuery(GetPackageName() + ".GET_REPRINT_OURRPT_SEQNO", param);
            return dt;
        }

        public int Delete_Bucket_Report(string ymd, string bseqcd, string bseq_rcv_count)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("IN_CORD", GetXML("CORCD"));
            param.Add("IN_BIZCD", GetXML("BIZCD"));
            param.Add("IN_YMD", ymd);
            param.Add("IN_CUSTCD", GetXML("CUSTCD"));
            param.Add("IN_CUST_PLANT", GetXML("CUST_PLANT"));
            param.Add("IN_CUST_LINE", GetXML("CUST_LINE"));
            param.Add("IN_ITEM", GetXML("ITEM"));
            param.Add("IN_BSEQ", bseqcd);
            param.Add("IN_RCV_COUNT", bseq_rcv_count);

            int iResultCnt = 0;
            iResultCnt = m_DB.ExcuteNonQuery(GetPackageName() + ".SET_DELETE_BUCKET_REPORT", param);

            return iResultCnt;
        }

        public int init_ALC_Report(string ymd, string seqno)
        {
            //강제발행된TO-SEQNO 이후의 SEQNO번호에 대하여 REPORT출력이 이루어진 경우 REPORT 출력 상태 초기화 함
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("IN_CORD", GetXML("CORCD"));
            param.Add("IN_BIZCD", GetXML("BIZCD"));
            param.Add("IN_YMD", ymd);
            param.Add("IN_CUSTCD", GetXML("CUSTCD"));
            param.Add("IN_CUST_PLANT", GetXML("CUST_PLANT"));
            param.Add("IN_CUST_LINE", GetXML("CUST_LINE"));
            param.Add("IN_ITEM", GetXML("ITEM"));
            param.Add("IN_SEQNO", seqno);

            int iResultCnt = -1;

            iResultCnt = m_DB.ExcuteNonQuery(GetPackageName() + ".SET_INIT_ALC_REPORT", param);

            return iResultCnt;
        }

        private DataTable OpenXML(string path = "")
        {

            try
            {

                if (string.IsNullOrEmpty(path))
                {
                    path = CN_CONFIG_XML_PATH;
                }
                if (System.IO.File.Exists(path))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(path);
                    if (ds.Tables.Count > 0)
                    {
                        for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
                        {
                            for (int col = 0; col < ds.Tables[0].Columns.Count; col++)
                            {
                                ds.Tables[0].Rows[row][col] = ds.Tables[0].Rows[row][col].ToString().Trim();
                            }
                        }
                    }
                    return ds.Tables[0];
                }
                return null;
            }
            catch(Exception eLog)
            {
                System.Diagnostics.Debug.WriteLine(eLog.Message);
                return null;

            }
        }

        public void PrintBucket(DataTable dt, int rePrintSeq = 0, int rePrintRcvCount = 0, string installPOS = "ALL")
        {
            if (rePrintSeq == 0 && m_bBucketPrintReady == false)
            {
                return;
            }
            
            m_BUCKET_PRT.Print(GetXML("ITEM")
                    , HE_MES.FX.Utils.Glb_FNS.GetO2I(GetXML("BUCKET_COUNT"))
                    , HE_MES.FX.Utils.Glb_FNS.GetO2I(GetXML("BUCKET_COUNT"))
                    //, 0
                    , HE_MES.FX.Utils.Glb_FNS.GetO2I(GetXML("ITEM_COUNT"))
                    , dt
                    , m_iOurPrintMaxCount
                    , GetXML("OUR_PAPER_PRINT_YN")
                    , ref m_bBucketPrintReady
                    , rePrintSeq
                    , rePrintRcvCount
                    , m_bForcePrint
                    , installPOS);

        }
        public void SetClear(string ymd, string seq,string bseq)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("IN_CORD", GetXML("CORCD"));
            param.Add("IN_BIZCD", GetXML("BIZCD"));
            param.Add("IN_YMD", ymd);
            param.Add("IN_CUSTCD", GetXML("CUSTCD"));
            param.Add("IN_CUST_PLANT", GetXML("CUST_PLANT"));
            param.Add("IN_CUST_LINE", GetXML("CUST_LINE"));
            param.Add("IN_ITEM", GetXML("ITEM"));
            param.Add("IN_SEQNO", seq);
            param.Add("IN_BSEQ", bseq);
            m_DB.ExcuteNonQuery(GetPackageName()+".SET_INIT_ALC_REPORT", param);
        }
        public DataRow GetLastData()
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("IN_CORD", GetXML("CORCD"));
            param.Add("IN_BIZCD", GetXML("BIZCD"));
            param.Add("IN_CUSTCD", GetXML("CUSTCD"));
            param.Add("IN_CUST_PLANT", GetXML("CUST_PLANT"));
            param.Add("IN_CUST_LINE", GetXML("CUST_LINE"));
            param.Add("IN_ITEM", GetXML("ITEM"));
            DataTable dt = m_DB.ExcuteQuery(GetPackageName()+".GET_CUR_SEQ_INFO", param);
            if(dt.Rows.Count>0)
            {
                return dt.Rows[0];
            }
            return null;
        }
        private void DispCurSeqInfo()
        {
            DataRow dr = GetLastData();

            if (dr!=null)
            {
                Lbl_YMD.Text = dr["ED_YMD"].ToString();
                Lbl_SEQ.Text = dr["ED_SEQNO"].ToString();
            }

        }

        public  void ReLoadData()
        {
            Lbl_ReadInterval.BackColor = Color.Lime;

            DataTable dt = GetData();
            dataGridView1.DataSource = dt;

            if (dt.Rows.Count < 1)
            {
                m_QDate = DateTime.Now;
                return;
            }

            //프로그램 최초 시작인 경우 마지막 출력된 Bucket에 대한 시작일자와 종료일자 변경여부 확인
            //(사내 서열지 출력 시 Report번호 생성을 위해 참조)
            if (m_bPGStart == true)
            {
                m_bPGStart = false;



                m_strPrintStartDate = "";

                int iLastRow = 0;

                for (int inx = 0; inx <= dataGridView1.Rows.Count - 1; inx++)
                {
                    if (dataGridView1.Rows[inx].Cells["BUCKET_PRINT_YN"].Value.ToString() == "Y")
                    {
                        iLastRow = inx;
                    }
                    else
                    {
                        m_strPrintStartDate = dataGridView1.Rows[inx].Cells["YMD"].Value.ToString();
                        break;
                    }
                }

                string strYMD = dt.Rows[iLastRow]["YMD"].ToString();
                string PRINT_BSEQ = dt.Rows[iLastRow]["PRINT_BSEQ"].ToString();
                string BSEQCD_RCV_COUNT = dt.Rows[iLastRow]["ITEM1_BSEQCD_RCV_COUNT"].ToString();


                Lbl_YMD.Text = "O/DATE(YMD) : " + dt.Rows[iLastRow]["YMD"].ToString();
                Lbl_SEQ.Text = "S/NO : " + dt.Rows[iLastRow]["SEQNO"].ToString();


                for (int inx = iLastRow; inx >= 0; inx--)
                {
                    if (PRINT_BSEQ == dt.Rows[inx]["PRINT_BSEQ"].ToString() && BSEQCD_RCV_COUNT == dt.Rows[inx]["ITEM1_BSEQCD_RCV_COUNT"].ToString())
                    {
                        if (strYMD != dt.Rows[inx]["YMD"].ToString())
                        {
                            m_bChangeDate = true;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

            }

            dataGridView1.FirstDisplayedCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
            if (checkBox1.Checked)
            {

                PrintBucket(dt);

                if (m_bPrinted == true)
                {
                    dt = GetData();
                    dataGridView1.DataSource = dt;
                    dataGridView1.FirstDisplayedCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
                }

                ////강제발행 Reset
                //if (m_bForcePrint == true)
                //{
                //    SetForcePrint(false);
                //}
            }

            //강제발행 Reset
            if (m_bForcePrint == true)
            {
                SetForcePrint(false);
            }

            //서열지 출력이된 서열에 대한 화면 Refresh
            string strPrintYN = "";
            string strBPrintYN = "";

            for (int inx = 0; inx <= dataGridView1.Rows.Count - 1; inx++)
            {
                strPrintYN = dataGridView1.Rows[inx].Cells["OUR_PRINT_YN"].Value.ToString();
                strBPrintYN = dataGridView1.Rows[inx].Cells["BUCKET_PRINT_YN"].Value.ToString();

                if (strPrintYN == "Y" && strBPrintYN == "Y")
                {
                    this.dataGridView1.Rows[inx].DefaultCellStyle.BackColor = Color.LightGreen;

                }
                else if (strPrintYN == "Y" && strBPrintYN == "N") //(strPrintYN == "Y" || strBPrintYN == "N")
                {
                    this.dataGridView1.Rows[inx].DefaultCellStyle.BackColor = Color.LightYellow;
                }

            }
            DispCurSeqInfo();
            
        }
        private void TmrMain_Tick(object sender, EventArgs e)
        {
            try
            {
                if (checkBox2.Checked == true)
                {
                    return;
                }
                TmrMain.Enabled = false;
                double span = (DateTime.Now - m_QDate).TotalSeconds; 
                Lbl_ReadInterval.Text = string.Format("{0:N0}/{1:N0}", span, m_ReloadTimeSec);
                Lbl_ReadInterval.BackColor = Color.LightSkyBlue;
                if (span >= m_ReloadTimeSec)
                {
                    ReLoadData();
                    m_QDate = DateTime.Now;
                }

            }
            catch(Exception eLog)
            {
                System.Diagnostics.Debug.WriteLine(eLog.Message);
            }
            finally
            {
                TmrMain.Enabled = true;
            }
            
        }


        private void btn_Reprint_Click(object sender, EventArgs e)
        {
            try
            {
                TmrMain.Enabled = false;
                frmRePrint prt = new frmRePrint(this);
                prt.ShowDialog(this);
            }
            catch (Exception eLog)
            {
                System.Diagnostics.Debug.WriteLine(eLog.Message);
            }
            finally
            {
                m_QDate = DateTime.Now;
                TmrMain.Enabled = true;
            }
        }


        private void btn_ForcePrint_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Do you print report forced?", "Notice", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    SetForcePrint(true);
            //}
            //else
            //{
            //    SetForcePrint(false);
            //}

            //Maked new process for force printing(2019-08-30 KIM.JB)
            try
            {

                TmrMain.Enabled = false;
                frmForcePrint prt = new frmForcePrint(this);
                prt.ShowDialog(this);

                
            }
            catch (Exception eLog)
            {
                System.Diagnostics.Debug.WriteLine(eLog.Message);
            }
            finally
            {
                m_QDate = DateTime.Now;

                TmrMain.Enabled = true;

                m_bForcePrint = false;

            }

        }

        private void SetForcePrint(bool bSet)
        {
            if (bSet == false)
            {
                m_bForcePrint = false;
                btn_ForcePrint.BackColor = Color.Gainsboro;
                btn_ForcePrint.Visible = false;
                m_ReloadTimeSec = m_ReloadTimeSec_Bak;
            }
            else
            {
                m_bForcePrint = true;
                btn_ForcePrint.BackColor = Color.Orange;
                btn_ForcePrint.Visible = true;
                m_ReloadTimeSec_Bak = m_ReloadTimeSec;
                m_ReloadTimeSec = 1;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TmrMain.Enabled == false)
            {

                m_QDate = DateTime.Now;
                TmrMain.Enabled = true;
            }
            else
            {
                TmrMain.Enabled = false;
            }

        }


        private void Lbl_Customer_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                TmrMain.Enabled = false;
                frmForcePrint prt = new frmForcePrint(this);
                prt.ShowDialog(this);

                //TmrMain.Enabled = true;
            }
            catch (Exception eLog)
            {
                System.Diagnostics.Debug.WriteLine(eLog.Message);
            }
            finally
            {
                m_QDate = DateTime.Now;

                TmrMain.Enabled = true;

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked)
            {
                Lbl_ReadInterval.BackColor = Color.Yellow;
            }
            else
            {
                Lbl_ReadInterval.BackColor = Color.LightSkyBlue;
            }
        }


    }
}
