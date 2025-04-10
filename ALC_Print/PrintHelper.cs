using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data;

namespace ALC_Print
{
    public class PrintHelper
    {
        private string m_pkg = "";
        Dictionary<string, int> m_dicBuckQTY = new Dictionary<string, int>();

        
        public struct STOCKINFO
        {
            public string strALC;
            public string strPartno;
            public string strLocation;
            public int iDliveryQty;
        }

        public struct STCUSTINFO
        {
            public string corcd;
            public string bizcd;
            public string custcd;
            public string cust_plant;
            public string cust_line;
            public string item;
            public int BQTY;
        }
        STCUSTINFO m_CustInfo = new STCUSTINFO();




        public struct STSAVEPARAM
        {
            public string corcd;
            public string bizcd;
            public string Start_ymd;
            public string End_ymd;
            public string custcd;
            public string plant;
            public string line;
            public string strItem;
            public string seqnoStart;
            public string seqnoEnd;
            public string strPrintFinish;
            public string printBseq;
            public string Report_Seq_Order;
            public string bseq2D;
        }

        C1.Win.C1Preview.C1PrintPreviewDialog m_prtPreView = new C1.Win.C1Preview.C1PrintPreviewDialog();
 
        C1.C1Report.C1Report m_prtCTL = null;
        HE_MES.DB.ORACLE_DB m_DB = null;
        private string m_strSubSequence = "0";
        

        public PrintHelper(string pkg, string item,STCUSTINFO custInfo, ref HE_MES.DB.ORACLE_DB db)
        {

            if (m_prtCTL == null)
            {
                m_prtCTL = new C1.C1Report.C1Report();
            }
            m_pkg = pkg;
            m_CustInfo = custInfo;
            int bqty = GetBuckQTY(custInfo);
            if (m_CustInfo.custcd == "HMI")
            {
                switch (item)
                {
                    //BUMPER
                    case "Q129":
                    case "Q130":
                        if (bqty == 4)
                        {
                            m_prtCTL.Load(@".\RPTS\BUCKET_RPT_4.xml", "BUCKET_RPT_4");
                        }
                        else
                        {
                            m_prtCTL.Load(@".\RPTS\BUCKET_RPT_8.xml", "BUCKET_RPT_8");
                        }
                        break;

                    //D/TRIM
                    case "Q045":
                    case "Q046":
                    case "Q047":
                    case "Q057":
                        m_prtCTL.Load(@".\RPTS\BUCKET_RPT_32.xml", "BUCKET_RPT_32");
                        break;

                    //SPOILER
                    case "Q117":
                        m_prtCTL.Load(@".\RPTS\BUCKET_RPT_18.xml", "BUCKET_RPT_18");
                        break;
                    //T/Gate
                    case "Q100":
                        m_prtCTL.Load(@".\RPTS\BUCKET_RPT_33.xml", "BUCKET_RPT_33");
                        break;
                    //S/SIDE
                    case "Q109":
                    case "Q008":
                        if (custInfo.cust_plant == "HVF1")
                        {
                            m_prtCTL.Load(@".\RPTS\BUCKET_RPT_12.xml", "BUCKET_RPT_12");
                        }
                        else
                        {
                            m_prtCTL.Load(@".\RPTS\BUCKET_RPT_18.xml", "BUCKET_RPT_18");
                        }
                        break;
                }
            }
            else
            {
                switch (item)
                {
                    //BUMPER
                    case "Q034":
                        m_prtCTL.Load(@".\RPTS_AP\BUCKET_RPT_8.xml", "BUCKET_RPT_8");
                        break;
                    case "Q036":
                        m_prtCTL.Load(@".\RPTS_AP\BUCKET_RPT_6.xml", "BUCKET_RPT_6");
                        break;

                    //D/TRIM
                    case "Q094":
                    case "Q093":
                    case "Q088":
                    case "Q087":
                        m_prtCTL.Load(@".\RPTS_AP\BUCKET_RPT_30.xml", "BUCKET_RPT_30");
                        break;

                    //GARNISH ASSY DR REAR
                    case "Q025":
                    case "Q024":

                    //GARNISH ASSY DR FRONT
                    case "Q027":
                    case "Q026":
                        //m_prtCTL.Load(@".\RPTS\BUCKET_RPT_30.xml", "BUCKET_RPT_30");
                        m_prtCTL.Load(@".\RPTS_AP\BUCKET_RPT_30_DG.xml", "BUCKET_RPT_30_DG");
                        break;

                    //TAIL GATE
                    case "Q030":
                        m_prtCTL.Load(@".\RPTS_AP\BUCKET_RPT_20R.xml", "BUCKET_RPT_20R");
                        break;
                    //SPOILER
                    case "Q033":
                        m_prtCTL.Load(@".\RPTS_AP\BUCKET_RPT_15.xml", "BUCKET_RPT_15");
                        break;
                    //R/RACK LH AND RH   
                    case "Q031":
                    case "Q032":
                        m_prtCTL.Load(@".\RPTS_AP\BUCKET_RPT_28.xml", "BUCKET_RPT_28");
                        break;
                    //R/GRILL UPPER
                    case "P684":
                        m_prtCTL.Load(@".\RPTS_AP\BUCKET_RPT_20R.xml", "BUCKET_RPT_20R");
                        break;
                    //S/SIDE
                    case "P401":
                    case "P402":
                        m_prtCTL.Load(@".\RPTS_AP\BUCKET_RPT_24.xml", "BUCKET_RPT_24");
                        break;
                }
            }
            m_DB = db;
        }

        private void WritePRT(string ctlNM, Bitmap img)
        {
            m_prtCTL.Fields[ctlNM].PictureScale = C1.C1Report.PictureScaleEnum.Stretch;
            m_prtCTL.Fields[ctlNM].Picture = img;
        }
        private void WritePRT(string ctlNM, string val, string format="")
        {
            if (m_prtCTL.Fields.Contains(ctlNM))
            {
                m_prtCTL.Fields[ctlNM].Text = val;

                if (string.IsNullOrEmpty(format) == false)
                {
                    switch (format)
                    {
                        case "YMD":
                            if (val.Length == 8)
                            {
                                m_prtCTL.Fields[ctlNM].Text = val.Substring(0, 4) + "-" + val.Substring(4, 2) + "-" + val.Substring(6, 2);
                            }
                            break;
                        case "TIME":
                            if (val.Length == 6)
                            {
                                m_prtCTL.Fields[ctlNM].Text = val.Substring(0, 2) + ":" + val.Substring(2, 2);
                            }
                            break;
                    }
                }
            }
        }

        private int GetIDX_ITEM(string pos)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("IN_CORD", m_CustInfo.corcd);
            param.Add("IN_BIZCD", m_CustInfo.bizcd);
            param.Add("IN_ITEM", m_CustInfo.item);
            param.Add("IN_POS", pos);
            DataTable dt = m_DB.ExcuteQuery(m_pkg + ".GET_ITEM_IDX_BY_POS", param);
            if(dt.Rows.Count>0)
            {
                return Convert.ToInt32(dt.Rows[0]["IDX"]);
            }
            return -1;
        }
        public DataTable GetData_Prv_Diff(string ymd, string seqno)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("IN_CORD", m_CustInfo.corcd);
            param.Add("IN_BIZCD", m_CustInfo.bizcd);
            param.Add("IN_YMD", ymd);
            //param.Add("IN_YMD", "20190515");
            param.Add("IN_CUSTCD", m_CustInfo.custcd);
            param.Add("IN_CUST_PLANT", m_CustInfo.cust_plant);
            param.Add("IN_CUST_LINE", m_CustInfo.cust_line);
            param.Add("IN_ITEM", m_CustInfo.item);
            param.Add("IN_SEQNO", seqno);
            DataTable dt = m_DB.ExcuteQuery(m_pkg + ".GET_PRV_SEQ_DIFF", param);
            return dt;
        }
        private DataTable RemoveUselessRow(DataTable dt)
        {
            dt.DefaultView.Sort = "YMD ASC, SEQNO ASC";
            if(dt.Rows.Count > m_CustInfo.BQTY)
            {
                DataTable copyDT = dt.Clone();
                int row = 0;
                foreach(DataRow dr in dt.Rows)
                {
                    if(row<m_CustInfo.BQTY)
                    {
                        copyDT.ImportRow(dr);
                    }
                    else
                    {
                        return copyDT;
                    }
                    row++;
                }
                return dt;
            }
            else
            {
                return dt;
            }   
        }
        
        private DataTable SkipDataProcess( int itemCNT, DataTable dt, bool bMerge = false)
        {
            if ((m_CustInfo.cust_plant == "HVF2") && (m_CustInfo.item == "Q109" || m_CustInfo.item == "Q008"))
            {
                DataTable copyDT = dt.Clone();
                foreach (DataRow dr in dt.Select("ITEM1 <>'' AND ITEM2 <>'' ", "YMD ASC, SEQNO ASC"))
                {
                    copyDT.ImportRow(dr);
                }
                return copyDT;
            }
            else if ((m_CustInfo.cust_plant == "HVF1") && (m_CustInfo.item == "Q117"))
            {
                DataTable copyDT = dt.Clone();
                foreach (DataRow dr in dt.Select("ITEM1 <>''", "YMD ASC, SEQNO ASC"))
                {
                    copyDT.ImportRow(dr);
                }
                return copyDT;
            }
            else if (m_CustInfo.item == "Q100")
            {
                DataTable copyDT = dt.Clone();
                foreach (DataRow dr in dt.Select("ITEM1 <>''", "YMD ASC, SEQNO ASC"))
                {
                    copyDT.ImportRow(dr);
                }
                return copyDT;
            }
            else
            {
                DataTable copyDT = dt.Clone();
                dt = RemoveUselessRow(dt);
                Dictionary<string, string> param = new Dictionary<string, string>();

                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    string ymd = dt.Rows[row]["YMD"].ToString();
                    string seqno = dt.Rows[row]["SEQNO"].ToString();
                    DataTable diffDT = GetData_Prv_Diff(ymd, seqno);
                    if (diffDT.Rows.Count > 0)
                    {
                        int diff = 0;
                        if (diffDT.Rows[0]["DIFF"] != DBNull.Value)
                        {
                            diff = Convert.ToInt32(diffDT.Rows[0]["DIFF"]);
                        }
                        if (diff != 0)
                        {
                            for (int i = 0; i < diff; i++)
                            {

                                DataRow dr = dt.Copy().Rows[row];
                                int nSEQNO = Convert.ToInt32(seqno) - (diff - i);
                                dr["SEQNO"] = nSEQNO.ToString().PadLeft(4, '0');
                                dr["BODY"] = "-";
                                for (int item = 0; item < itemCNT; item++)
                                {
                                    dr["ITEM" + (item + 1).ToString()] = "-";
                                }
                                dr["TIME"] = "SKIP";
                                dr["BODY"] = "X";
                                dr["VINCD"] = "-";
                                copyDT.ImportRow(dr);
                                if (copyDT.Rows.Count >= dt.Rows.Count)
                                {

                                    return copyDT;
                                }
                            }
                        }

                        copyDT.ImportRow(dt.Rows[row]);
                        if (copyDT.Rows.Count >= dt.Rows.Count)
                        {

                            return copyDT;
                        }
                    }


                }
            }
            return dt;
        }
        private string GetNextBSEQ(string ymd, string item)
        {
            
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("IN_CORD", m_CustInfo.corcd);
            param.Add("IN_BIZCD", m_CustInfo.bizcd);
            param.Add("IN_YMD", ymd);
            param.Add("IN_CUSTCD", m_CustInfo.custcd);
            param.Add("IN_CUST_PLANT", m_CustInfo.cust_plant);
            param.Add("IN_CUST_LINE", m_CustInfo.cust_line);
            param.Add("IN_ITEM", item);

            DataTable dt = m_DB.ExcuteQuery(m_pkg + ".GET_NEXT_BSEQ", param);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["BSEQ"].ToString();
            }
            return "000";
        }
        public struct SUMM_ST
        {
            public string loc;
            public int cnt;
        }
        private void CalcSUMM(string alc, string loc, ref Dictionary<string, SUMM_ST> summ)
        {
            SUMM_ST data;
            if(summ.Keys.Contains(alc))
            {
                data = summ[alc];
                data.cnt += 1;
                data.loc +=","+ loc;
                summ[alc] = data;
                
            }
            else
            {
                data = new SUMM_ST();
                data.cnt = 1;
                data.loc = loc;
                summ.Add(alc, data);
            }
        }
        private string  PrintSUMM(Dictionary<string, SUMM_ST> summ)
        {
            string ret = "";
            if (summ != null && summ.Count > 0)
            {
                
                foreach (KeyValuePair<string, SUMM_ST> pair in summ)
                {
                    ret += "  "+ pair.Key + "    " + pair.Value.cnt.ToString() + "   " + pair.Value.loc + Environment.NewLine;
                }
            }
            return ret;
        }
        private void PrintBucketRPT(string strXmlItem, int maxCnt, int itemCnt, System.Data.DataTable dt, bool bReprint = false, string installPOS = "ALL")
        {   /*TODO : Make Bucket Format*/
            try
            {
                bool bMerge = false;
                if (strXmlItem == "Q045" || strXmlItem == "Q046" || strXmlItem == "Q047" || strXmlItem == "Q057")
                {
                    bMerge = true;
                }
                dt = SkipDataProcess(itemCnt, dt, bMerge);
                Dictionary<string, SUMM_ST> dicSUMM = new Dictionary<string, SUMM_ST>();
                Dictionary<string, SUMM_ST> dicSUMM1 = new Dictionary<string, SUMM_ST>();
                Dictionary<int, STSAVEPARAM> dicPrtParam = new Dictionary<int, STSAVEPARAM>();
                for (int i = 0; i < itemCnt; i++)
                {
                    STSAVEPARAM prtParam = new STSAVEPARAM();
                    dicSUMM = new Dictionary<string, SUMM_ST>();
                    dicSUMM1 = new Dictionary<string, SUMM_ST>();

                    int itemIdx = i + 1;
                    string strVID = "";
                    string strStart_YMD = "";
                    string strWKDATE = dt.Rows[dt.Rows.Count - 1]["WKDATE"].ToString();
                    string strEnd_YMD = "";
                    string strALC = "";
                    string strCUST = dt.Rows[0]["CUSTCD"].ToString();
                    string strPLANT = dt.Rows[0]["PLANT"].ToString();
                    string strLINE = dt.Rows[0]["LINE"].ToString();
                    string strSEQNO = "";
                    string strBSEQ = "";
                    string strCORCD = "";
                    string strBIZCD = "";
                    string strSEQ_START = "";
                    string strSEQ_END = "";
                    string strTIME = "";
                    string strITEM = "";
                    string strBSEQCD_RCV_COUNT = "";

                    string strALC_M = "";

                    string strORDYMD = "ORDER DATE(YMD) : " + dt.Rows[0]["YMD"].ToString();

                    int iClearMaxCnt = GetBuckQTY(m_CustInfo);



                    //Init Value
                    for (int inx = 1; inx <= iClearMaxCnt; inx++)
                    {
                        WritePRT("fBUCKET_SEQENCE", "");

                        WritePRT("fRECTIME" + inx.ToString(), "");


                        WritePRT("fDATE", "");
                        WritePRT("fTIME", "");
                        WritePRT("fITEMDESC", "");
                        WritePRT("fPGN", "");
                        WritePRT("fBUCKET_HEADER", "");

                        WritePRT("fLINE", "");
                        WritePRT("fORD_DATE", "");
                        WritePRT("fSUM", "");

                        //WritePRT("fBAR" + inx.ToString(), "");
                        WritePRT("fVID" + inx.ToString(), "");
                        WritePRT("fSEQ" + inx.ToString(), "");
                        WritePRT("fPAC" + inx.ToString(), "");
                        WritePRT("fNUM" + inx.ToString(), "");
                        WritePRT("fEXCOLO" + inx.ToString(), "");
                        WritePRT("fBUCKET" + inx.ToString(), "");
                        
                        
                        if (bMerge)
                        {
                            WritePRT("fSUM1", "");
                            

                            //WritePRT("fBAR" + inx.ToString(), "");
                            WritePRT("fRECTIMEB" + (inx).ToString(), "");
                            WritePRT("fVIDB" + (inx).ToString(), "");
                            WritePRT("fSEQB" + (inx).ToString(), "");
                            WritePRT("fPACB" + (inx).ToString(), "");
                            WritePRT("fNUMB" + (inx).ToString(), "");
                            WritePRT("fEXCOLOB" + (inx).ToString(), "");
                            
                        }
                        
                        

                    }
                    if (bReprint)
                    {
                        if (dt.Rows.Count < maxCnt)
                        {
                            maxCnt = dt.Rows.Count;
                        }
                        strBSEQ = dt.Rows[0]["BITEM" + itemIdx.ToString()].ToString();
                    }
                    for (int row = 0; row < maxCnt; row++)
                    {
                        if (m_CustInfo.custcd == "HMI")
                        {
                            switch (strXmlItem)
                            {
                                //D/Trim
                                case "Q045": //FL
                                case "Q046":
                                case "Q047":
                                case "Q057":
                                    if (itemIdx == 1)
                                    {
                                        strITEM = "Q045";
                                        WritePRT("fITEMDESC", "D/TRIM LH");
                                        WritePRT("fPOSLH", "F" + Environment.NewLine + "/" + Environment.NewLine + "L" + Environment.NewLine + "H");
                                        WritePRT("fPOSRH", "R" + Environment.NewLine + "/" + Environment.NewLine + "L" + Environment.NewLine + "H");
                                    }
                                    else if (itemIdx == 2)
                                    {
                                        strITEM = "Q046";
                                        WritePRT("fITEMDESC", "D/TRIM RH");
                                        WritePRT("fPOSLH", "F" + Environment.NewLine + "/" + Environment.NewLine + "R" + Environment.NewLine + "H");
                                        WritePRT("fPOSRH", "R" + Environment.NewLine + "/" + Environment.NewLine + "R" + Environment.NewLine + "H");
                                    }
                                    break;
                                //BUMPER
                                case "Q129": //FRONT
                                case "Q130":
                                    if (itemIdx == 1)
                                    {
                                        strITEM = "Q129";
                                        WritePRT("fITEMDESC", "BUMPER FRONT");
                                    }
                                    else if (itemIdx == 2)
                                    {
                                        strITEM = "Q130";
                                        WritePRT("fITEMDESC", "BUMPER REAR");
                                    }
                                    break;

                                //SPOILER
                                case "Q117":
                                    strITEM = "Q117";
                                    WritePRT("fITEMDESC", "SPOILER");
                                    break;
                                //SPOILER
                                case "Q100":
                                    strITEM = "Q100";
                                    WritePRT("fITEMDESC", "T/Gate");
                                    break;

                                //S/SIDE
                                case "Q109":
                                case "Q008": //LH
                                    if (itemIdx == 1)
                                    {
                                        strITEM = "Q008";
                                        WritePRT("fITEMDESC", "Luggage SIDE LH");
                                    }
                                    else if (itemIdx == 2)
                                    {
                                        strITEM = "Q109";
                                        WritePRT("fITEMDESC", "Luggage SIDE RH");
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (strXmlItem)
                            {
                                //D/Trim
                                case "Q094": //FL
                                case "Q093": //FR
                                case "Q088": //RL
                                case "Q087": //RR
                                    if (itemIdx == 1)
                                    {
                                        strITEM = "Q094";
                                        WritePRT("fITEMDESC", "D/TRIM F/LH");
                                    }
                                    else if (itemIdx == 2)
                                    {
                                        strITEM = "Q093";
                                        WritePRT("fITEMDESC", "D/TRIM F/RH");
                                    }
                                    else if (itemIdx == 3)
                                    {
                                        strITEM = "Q088";
                                        WritePRT("fITEMDESC", "D/TRIM R/LH");
                                    }
                                    else if (itemIdx == 4)
                                    {
                                        strITEM = "Q087";
                                        WritePRT("fITEMDESC", "D/TRIM R/RH");
                                    }
                                    else if (itemIdx == 5)
                                    {
                                        strITEM = "Q030";
                                        WritePRT("fITEMDESC", "TAIL GATE");
                                    }
                                    break;

                                //GARNISH ASSY DR
                                case "Q027": //FL
                                case "Q026": //FR
                                case "Q025": //RL
                                case "Q024": //RR
                                    if (itemIdx == 1)
                                    {
                                        strITEM = "Q027";
                                        WritePRT("fITEMDESC", "GARNISH ASSY DR F/LH");
                                    }
                                    else if (itemIdx == 2)
                                    {
                                        strITEM = "Q026";
                                        WritePRT("fITEMDESC", "GARNISH ASSY DR F/RH");
                                    }
                                    else if (itemIdx == 3)
                                    {
                                        strITEM = "Q025";
                                        WritePRT("fITEMDESC", "GARNISH ASSY DR R/LH");
                                    }
                                    else if (itemIdx == 4)
                                    {
                                        strITEM = "Q024";
                                        WritePRT("fITEMDESC", "GARNISH ASSY DR R/RH");
                                    }
                                    break;

                                //BUMPER
                                case "Q034": //REAR
                                    if (itemIdx == 1)
                                    {
                                        strITEM = "Q034";
                                        WritePRT("fITEMDESC", "BUMPER REAR");
                                    }
                                    break;
                                case "Q036": //FRONT
                                    if (itemIdx == 1)
                                    {
                                        strITEM = "Q036";
                                        WritePRT("fITEMDESC", "BUMPER FRONT");
                                    }
                                    break;

                                //T/GATE
                                case "Q030":
                                    if (itemIdx == 1)
                                    {
                                        strITEM = "Q030";
                                        WritePRT("fITEMDESC", "TAIL GATE");
                                    }
                                    break;
                                case "Q033":
                                    if (itemIdx == 1)
                                    {
                                        strITEM = "Q033";
                                        WritePRT("fITEMDESC", "SPOILER");
                                    }
                                    break;

                                //S/SIDE
                                case "P401": //LH
                                case "P402": //RH
                                    if (itemIdx == 1)
                                    {
                                        strITEM = "P401";
                                        WritePRT("fITEMDESC", "MOLD ASSY S/SIDE LH");
                                    }
                                    else if (itemIdx == 2)
                                    {
                                        strITEM = "P402";
                                        WritePRT("fITEMDESC", "MOLD ASSY S/SIDE RH");
                                    }
                                    break;
                                //R/RACK
                                case "Q031": //LH
                                case "Q032": //RH
                                    if (itemIdx == 1)
                                    {
                                        strITEM = "Q031";
                                        WritePRT("fITEMDESC", "ROOF RACK LH");
                                    }
                                    else if (itemIdx == 2)
                                    {
                                        strITEM = "Q032";
                                        WritePRT("fITEMDESC", "ROOF RACK RH");
                                    }
                                    break;
                                case "P684":
                                    if (itemIdx == 1)
                                    {
                                        strITEM = "P684";
                                        WritePRT("fITEMDESC", "RAD/GRILL UPR");
                                    }
                                    break;
                            }
                        }
                        if (bReprint == false)
                        {
                            strBSEQ = GetNextBSEQ(dt.Rows[0]["YMD"].ToString(), strITEM);
                        }
                        strVID = dt.Rows[row]["BODY"].ToString();
                        strSEQNO = dt.Rows[row]["SEQNO"].ToString();
                        strCORCD = dt.Rows[row]["CORCD"].ToString();
                        strBIZCD = dt.Rows[row]["BIZCD"].ToString();
                        strALC = dt.Rows[row]["ITEM" + itemIdx.ToString()].ToString();
                        if (dt.Rows[row]["TIME"].ToString().Contains("SKIP"))
                        {
                            strTIME = "[SKIP]";
                        }
                        else
                        {
                            strTIME = dt.Rows[row]["TIME"].ToString().Substring(0, 2) + ":" + dt.Rows[row]["TIME"].ToString().Substring(2, 2);
                        }
                        //strBSEQCD_RCV_COUNT = dt.Rows[row]["ITEM" + itemIdx.ToString() + "_BSEQCD_RCV_COUNT"].ToString();
                        if(bMerge)
                        {
                            strALC_M = dt.Rows[row]["ITEM" + (itemIdx+2).ToString()].ToString();
                        }
                        if (row == 0)
                        {

                            strStart_YMD = dt.Rows[row]["YMD"].ToString();
                            strSEQ_START = dt.Rows[row]["SEQNO"].ToString();

                            strEnd_YMD = dt.Rows[row]["YMD"].ToString();
                            strSEQ_END = dt.Rows[row]["SEQNO"].ToString();

                            strBSEQCD_RCV_COUNT = dt.Rows[row]["ITEM" + itemIdx.ToString() + "_BSEQCD_RCV_COUNT"].ToString();
                        }
                        else
                        {
                            strEnd_YMD = dt.Rows[row]["YMD"].ToString();
                            strSEQ_END = dt.Rows[row]["SEQNO"].ToString();
                        }
                        WritePRT("fLINE", strPLANT + " / " + strLINE);
                        WritePRT("fORD_DATE", strORDYMD);

                        WritePRT("fBUCKET_SEQENCE", strBSEQ.Length >= 3 ? strBSEQ.Substring(0, 3) : "");
                        WritePRT("fBUCKET_HEADER", strBSEQ.Length >= 3 ? strBSEQ.Substring(0, 3) : "");
                        

                        WritePRT("fVID" + (row + 1), dt.Rows.Count > row ? strVID : "");
                        WritePRT("fSEQ" + (row + 1), dt.Rows.Count > row ? strSEQNO : "");
                        WritePRT("fPAC" + (row + 1), dt.Rows.Count > row ? strALC : "");
                        WritePRT("fNUM" + (row + 1), (row + 1).ToString());
                        WritePRT("fRECTIME" + (row + 1), strTIME);
                        WritePRT("fDATE", dt.Rows[row]["PRINT_DATE"].ToString());
                        WritePRT("fTIME", dt.Rows[row]["PRINT_TIME"].ToString());

                        WritePRT("fBUCKET" + (row + 1), strBSEQ);
                        if (m_CustInfo.custcd == "HMI")
                        {
                            WritePRT("fEXCOLO" + (row + 1), dt.Rows[row]["EXCOLORCD"].ToString());
                        }
                        CalcSUMM(strALC, (row + 1).ToString(), ref dicSUMM);
                        if(bMerge)
                        {
                            WritePRT("fNUMB" + (row  + 1), (row + maxCnt + 1).ToString());
                            WritePRT("fPACB" + (row + 1), dt.Rows.Count > row ? strALC_M : "");
                            WritePRT("fVIDB" + (row + 1), dt.Rows.Count > row ? strVID : "");
                            WritePRT("fSEQB" + (row + 1), dt.Rows.Count > row ? strSEQNO : "");
                            if (m_CustInfo.custcd == "HMI")
                            {
                                WritePRT("fEXCOLOB" + (row + 1), dt.Rows[row]["EXCOLORCD"].ToString());
                            }
                            WritePRT("fRECTIMEB" + (row+ 1), strTIME);

                            CalcSUMM(strALC_M, (row + maxCnt + 1).ToString(), ref dicSUMM1);
                        }


                    }
                    WritePRT("fBSEQ_FIRST1", strSEQ_START.Length >= 5 ? strSEQ_START.Substring(strSEQ_START.Length - 4) : strSEQ_START);
                    WritePRT("fBSEQ_FIRST", strSEQ_START.Length >= 5 ? strSEQ_START.Substring(strSEQ_START.Length - 4) : strSEQ_START);
                    WritePRT("fBSEQ_LAST1", strSEQ_END.Length >= 5 ? strSEQ_END.Substring(strSEQ_END.Length - 4) : strSEQ_END);
                    WritePRT("fBSEQ_LAST", strSEQ_END.Length >= 5 ? strSEQ_END.Substring(strSEQ_END.Length - 4) : strSEQ_END);
                    

                    WritePRT("fPGN", strITEM);

                    

                    string stroldBarcode = "O-" + strStart_YMD + "@" + Convert.ToInt32(strSEQ_START).ToString() + "@" + Convert.ToInt32(strSEQ_END).ToString() + "@" + strPLANT + "@" + GetOldPOS(strITEM) + "@" + strLINE + "@" + strITEM + "@" + strEnd_YMD + "@";
                    string strBSEQ_matixCODE = Make2DCode(strCORCD, strBIZCD, strCUST, strPLANT, strLINE, strStart_YMD, strITEM, strBSEQ.Length >= 3 ? strBSEQ.Substring(0, 3) : "", strBSEQCD_RCV_COUNT, false);

                    if(m_CustInfo.custcd =="KMI")
                    {
                        stroldBarcode = strBSEQ_matixCODE;
                        WritePRT("fBSEQ_DATE", strWKDATE.Substring(6, 2));

                        string strBucketBarcode = strWKDATE.Substring(6, 2) + strWKDATE.Substring(4, 2) + strWKDATE.Substring(2, 2) + strITEM + (strBSEQ.Length >= 3 ? strBSEQ.Substring(0, 3) : "") + m_strSubSequence;
                        WritePRT("fBARCODE", strBucketBarcode);
                        WritePRT("fBarcodeText", strBucketBarcode);
                    }
                    WritePRT("fSeoyonBarCode", new DataMatrix.net.DmtxImageEncoder().EncodeImage(stroldBarcode));

                    string summPRT = PrintSUMM(dicSUMM);
                    string summPRT1 = PrintSUMM(dicSUMM1);
                    WritePRT("fSUM", summPRT);
                    if(bMerge)
                    {
                        WritePRT("fSUM1", summPRT1);
                    }
                    if (frmPrint.m_strPreviewPrint == "Y")
                    {
                        m_prtPreView.Text = m_prtCTL.ReportName;
                        m_prtPreView.PrintPreviewControl.Document = m_prtCTL;
                        m_prtPreView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        m_prtPreView.ShowDialog();
                    }
                    else
                    {

                        m_prtCTL.Document.Print();
                    }
                    if (bReprint == false)
                    {

                        //SetPrintYN(string corcd, string bizcd, string Start_ymd, string End_ymd, string custcd, string plant, string line, string strItem
                        //, string seqnoStart, string seqnoEnd, 
                         //string strPrintFinish, string printBseq="", string Report_Seq_Order ="", string bseq2D = "")
                        prtParam.corcd = strCORCD;
                        prtParam.bizcd = strBIZCD;
                        prtParam.Start_ymd = strStart_YMD;
                        prtParam.End_ymd = strEnd_YMD;
                        prtParam.custcd = strCUST;
                        prtParam.plant = strPLANT;
                        prtParam.line = strLINE;
                        prtParam.strItem = strITEM;
                        prtParam.seqnoStart = strSEQ_START;
                        prtParam.seqnoEnd = strSEQ_END;
                        prtParam.strPrintFinish = "";
                        prtParam.printBseq = Convert.ToInt16(strBSEQ).ToString();
                        prtParam.Report_Seq_Order = "";
                        prtParam.bseq2D = stroldBarcode;

                        dicPrtParam.Add(i, prtParam);
                        
                    }

                }

                if (bReprint == false && dicPrtParam.Count > 0)
                {
                    SetPrintYN(dicPrtParam);
                }

            }
            catch (Exception eLog)
            {
                System.Diagnostics.Debug.WriteLine(eLog.Message);
            }
        }
        private string GetOldPOS(string item)
        {
            if(item == "Q129")
            {
                return "FRT";
            }
            else if(item == "Q130")
            {
                return "RR";
            }
            else if (item == "Q008")
            {
                return "LH";
            }
            else if (item == "Q109")
            {
                return "RH";
            }
            else if(item == "Q045")
            {
                return "FLH/RLH";
            }
            else if(item == "Q046")
            {
                return "FRH/RRH";
            }
            else if(item == "Q047")
            {
                return "FLH/RLH";
            }
            else if (item == "Q057")
            {
                return "FRH/RRH";
            }
            else if (item == "Q117")
            {
                return "RR";
            }
            else if (item == "Q100")
            {
                return "RR";
            }

                return "";
        }


        private string Make2DCode(string corcd, string bizcd ,string custcd, string plant, string line, string ymd, string item, string bSEQ, string bSEQ_rcv_count, bool bReprint)
        {
            string catSTR = "@";
            string strRET = "<[BK]>";
            strRET += catSTR + "C" + corcd;
            strRET += catSTR + "B" + bizcd;
            strRET += catSTR + "U" + custcd;
            strRET += catSTR + "P" + plant;
            strRET += catSTR + "L" + line;
            strRET += catSTR + "Y" + ymd;
            strRET += catSTR + "I" + item;
            strRET += catSTR + "S" + HE_MES.FX.Utils.Glb_FNS.GetO2I(bSEQ).ToString();
            //strRET += catSTR + "N" + HE_MES.FX.Utils.Glb_FNS.GetO2I(bSEQ_rcv_count).ToString();

            //신규발행일 경우에만 중복여부 체크
            if (bReprint == false)
            {
                string strMaxBSEQ2D = "";

                //BSEQ 2D 값 중복 여부 체크
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("IN_CORD", corcd);
                param.Add("IN_BIZCD", bizcd);
                param.Add("IN_BUCKET_NO", strRET);

                DataTable dt = m_DB.ExcuteQuery(m_pkg + ".GET_MAX_BSEQ_2DCODE", param);
                if(dt.Rows.Count<=0)
                {
                    strMaxBSEQ2D = "NONE";
                }
                else
                {
                    strMaxBSEQ2D = dt.Rows[0]["MAX_BSEQ_2D"].ToString();
                }
               

                if (strMaxBSEQ2D == "NONE")
                {
                    strRET += catSTR + "N" + HE_MES.FX.Utils.Glb_FNS.GetO2I(bSEQ_rcv_count).ToString();
                }
                else
                {
                    int iBSeqRcvCount = Convert.ToInt16(strMaxBSEQ2D.Substring(strMaxBSEQ2D.IndexOf("@N") + 2)) + 1;

                    strRET += catSTR + "N" + (iBSeqRcvCount).ToString();
                }
            }
            else
            {
                strRET += catSTR + "N" + HE_MES.FX.Utils.Glb_FNS.GetO2I(bSEQ_rcv_count).ToString();
            }

            return strRET;
        }
        public void Print(string item, int maxCnt, int maxBucketCnt, int itemCnt, System.Data.DataTable dt, int iOurPrintMaxCount, string strOurPrintYN, ref bool bBucketPrintReady
                , int rePrintSEQ = 0, int rePrintRcvCount = 1, bool bForcePrint = false
                , string installPOS = "ALL")
        {
            System.Data.DataTable filteredDT = dt.Clone();
            string filterStr = "";
            string filterStr1 = "";
            string strLastReportFinish = "Y";
            int iLastReport_Seq_Order = 0;


            //string strBSEQ_matixCODE = Make2DCode("5200", "5210", "KMI", "1", "1", "20190715", "Q030", "001".Length >= 3 ? "001".Substring(0, 3) : "", "3", false);

            if (rePrintSEQ == 0)
            {   //New Printing
                filterStr = "BPRINT_YN";
            }
            else
            {   //Reprinting
                filterStr = "BITEM1";
                filterStr1 = "ITEM1_BSEQCD_RCV_COUNT";
            }

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                if (rePrintSEQ == 0)
                {   //New Printing
                    if (dr[filterStr].ToString() == "N")
                    {
                        filteredDT.ImportRow(dr);
                    }
                    else
                    {
                        strLastReportFinish = dr["PRINT_FINISH"].ToString();
                        iLastReport_Seq_Order = HE_MES.FX.Utils.Glb_FNS.GetO2I(dr["PRINT_SEQ_ORDER"]);
                        //iLastReport_Seq_Order = iLastReport_Seq_Order + 1;
                    }
                }
                else
                {   //Reprinting
                    //if (dr[filterStr].ToString() == rePrintSEQ.ToString().PadLeft(3, '0')  && dr[filterStr1].ToString() == rePrintRcvCount.ToString())
                    //{
                         filteredDT.ImportRow(dr);
                    //}
                }
            }

            //재발행
            if (rePrintSEQ > 0)
            {
                PrintBucketRPT(item, maxCnt, itemCnt, filteredDT, true, installPOS);  
            }
            //신규발행
            else
            {
                //강제발행 여부
                if (bForcePrint == true)
                {
                    if (filteredDT.Rows.Count <= maxCnt)
                    {
                        maxCnt = filteredDT.Rows.Count;

                        if (strLastReportFinish == "Y")
                        {
                            maxBucketCnt = filteredDT.Rows.Count;
                        }
                    }
                }

                if (maxCnt <= filteredDT.Rows.Count)
                {
                    PrintBucketRPT(item, maxCnt, itemCnt, filteredDT);

                    frmPrint.m_bPrinted = true;
                }
                else
                {
                    frmPrint.m_bPrinted = false;
                }
            }

        }

        private void SetPrintYN(Dictionary<int, STSAVEPARAM> dicParam)
        {

            try
            {
                foreach (KeyValuePair<int, STSAVEPARAM> pair in dicParam)
                {
                    Dictionary<string, string> param = new Dictionary<string, string>();
                    param.Add("IN_CORCD", pair.Value.corcd);
                    param.Add("IN_BIZCD", pair.Value.bizcd);
                    param.Add("IN_START_YMD", pair.Value.Start_ymd);
                    param.Add("IN_END_YMD", pair.Value.End_ymd);
                    param.Add("IN_CUSTCD", pair.Value.custcd);
                    param.Add("IN_CUST_PLANT", pair.Value.plant);
                    param.Add("IN_CUST_LINE", pair.Value.line);
                    param.Add("IN_ITEM", pair.Value.strItem);
                    param.Add("IN_SEQNO_START", pair.Value.seqnoStart);
                    param.Add("IN_SEQNO_END", pair.Value.seqnoEnd);
                    param.Add("IN_PRINT_BSEQ", pair.Value.printBseq);
                    param.Add("IN_BSEQ_2D", pair.Value.bseq2D);
                    m_DB.ExcuteNonQuery(m_pkg + ".SET_BPRINT_YN", param);
                }
            }
            catch (Exception eLog)
            {
                System.Diagnostics.Debug.WriteLine(eLog.Message);
            }

        }

        private void GetStokedStatusByLocation(string strCORCD, string strBIZCD, string stOurReportUseBSEQ, ref STOCKINFO oSumStock)
        {
            try
            {
                string strSumLocation = "";
                string strLocation = "";
                string strIN_LOCATION = "";
                string strALC = "";
                int iStockedQty = 0;
                int iStockedSumQty = 0;
                int iDeliveryQty = 0;

                strALC = oSumStock.strALC;
                iDeliveryQty = oSumStock.iDliveryQty; 

                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("IN_CORD", strCORCD);
                param.Add("IN_BIZCD", strBIZCD);
                param.Add("IN_PARTNO", oSumStock.strPartno);
                param.Add("IN_LOCATION", "");
                param.Add("IN_FIRST", "Y");
                DataTable dt = m_DB.ExcuteQuery(m_pkg + ".GET_STOCK_STATUS_BY_LOCATION", param);

                iStockedQty = HE_MES.FX.Utils.Glb_FNS.GetO2I(dt.Rows[0]["STOCK_QTY"]);
                iStockedSumQty = iStockedQty;

                strLocation = dt.Rows[0]["LOCATION_NO"].ToString();

                dt.Dispose();

                //첫번째 검색된(FIFO기준) Location에 해당 제품에 대한 적재 수량이 출고수량 보다 많은 경우
                if (strLocation != "NO-LOC" && iStockedQty >= iDeliveryQty)
                {
                    strSumLocation = strLocation + "(" + iDeliveryQty.ToString() + ")";
                    oSumStock.strLocation = strSumLocation;

                    //MES2011의 해당 제품을 Report발행에 사용한것으로 UPDATE 처리 함
                    param.Clear();
                    param.Add("IN_CORD", strCORCD);
                    param.Add("IN_BIZCD", strBIZCD);
                    param.Add("IN_PARTNO", oSumStock.strPartno);
                    param.Add("IN_LOCATION", strLocation);
                    param.Add("IN_STOCK_QTY", iDeliveryQty.ToString());
                    param.Add("IN_BUCKET_NO", stOurReportUseBSEQ);
                    m_DB.ExcuteNonQuery(m_pkg + ".SET_BUCKET_MES2011_BY_REPORT", param);

                    return;
                }
                //해당 제품에 대한 재고가 없는 경우
                else if (strLocation == "NO-LOC" || iStockedQty == 0)
                {
                    return;
                }

                strIN_LOCATION = strLocation;
                strSumLocation = strLocation + "(" + iStockedQty.ToString() + ")";

                //출고수량을 초과 할 때까지 해당 제품이 적재된 다른 Location 검색(FIFO기준)
                do
                {
                    param.Clear();
 
                    param.Add("IN_CORD", strCORCD);
                    param.Add("IN_BIZCD", strBIZCD);
                    param.Add("IN_PARTNO", oSumStock.strPartno);
                    param.Add("IN_LOCATION", strIN_LOCATION);
                    param.Add("IN_FIRST", "N");
                    dt = m_DB.ExcuteQuery(m_pkg + ".GET_STOCK_STATUS_BY_LOCATION", param);

                    iStockedQty = HE_MES.FX.Utils.Glb_FNS.GetO2I(dt.Rows[0]["STOCK_QTY"]);
                    strLocation = dt.Rows[0]["LOCATION_NO"].ToString();
      
                    if (strLocation != "NO-LOC" && iStockedSumQty + iStockedQty >= iDeliveryQty)
                    {
                        oSumStock.strLocation = strSumLocation + ", " + strLocation + "(" + (iDeliveryQty - iStockedSumQty).ToString() + ")";

                        //MES2011의 해당 제품을 Report발행에 사용한것으로 UPDATE 처리 함
                        param.Clear();
                        param.Add("IN_CORD", strCORCD);
                        param.Add("IN_BIZCD", strBIZCD);
                        param.Add("IN_PARTNO", oSumStock.strPartno);
                        param.Add("IN_LOCATION", strLocation);
                        param.Add("IN_STOCK_QTY", (iDeliveryQty - iStockedSumQty).ToString());
                        param.Add("IN_STOCK_QTY", stOurReportUseBSEQ);
                        m_DB.ExcuteNonQuery(m_pkg + ".SET_BUCKET_MES2011_BY_REPORT", param);

                        return;
                    }
                    //해당 제품에 대한 재고가 없는 경우
                    else if (strLocation == "NO-LOC" || iStockedQty == 0)
                    {
                        oSumStock.strLocation = strSumLocation;
                        return;
                    }

                    strIN_LOCATION = strIN_LOCATION + "," + strLocation;
                    strSumLocation = strSumLocation + "," + strLocation + "(" + (iDeliveryQty - iStockedSumQty).ToString() + ")";
                    iStockedSumQty = iStockedSumQty + iStockedQty;

                    //MES2011의 해당 제품을 Report발행에 사용한것으로 UPDATE 처리 함
                    param.Clear();
                    param.Add("IN_CORD", strCORCD);
                    param.Add("IN_BIZCD", strBIZCD);
                    param.Add("IN_PARTNO", oSumStock.strPartno);
                    param.Add("IN_LOCATION", strLocation);
                    param.Add("IN_STOCK_QTY", (iDeliveryQty - iStockedSumQty).ToString());
                    param.Add("IN_STOCK_QTY", stOurReportUseBSEQ);
                    m_DB.ExcuteNonQuery(m_pkg + ".SET_BUCKET_MES2011_BY_REPORT", param);

                } while (iDeliveryQty < iStockedSumQty);

            }
            catch (Exception eLog)
            {
                System.Diagnostics.Debug.WriteLine(eLog.Message);
            }
            finally
            {
                
            }

        }


        private string GetLastPrint(string corcd, string bizcd ,string ymd, string custcd, string plant, string line, string item)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("IN_CORD", corcd);
            param.Add("IN_BIZCD", bizcd);
            param.Add("IN_YMD", ymd);
            param.Add("IN_CUSTCD", custcd);
            param.Add("IN_CUST_PLANT", plant);
            param.Add("IN_CUST_LINE", line);
            param.Add("IN_ITEM", item);
            DataTable dt = m_DB.ExcuteQuery(m_pkg + ".GET_LAST_PRINT_SEQ", param);

            if (dt.Rows.Count < 1)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["LAST_PRINT_SEQ"].ToString();
            }
        }

        private int GetBuckQTY(STCUSTINFO custInfo)
        {
            return custInfo.BQTY;
        }
        
    }

}
