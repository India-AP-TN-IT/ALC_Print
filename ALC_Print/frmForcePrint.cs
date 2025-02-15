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
    public partial class frmForcePrint : Form
    {
        frmPrint m_ParentFrm = null;

        System.Data.DataTable m_dt = null;
        System.Data.DataTable m_filteredDT = null;
        private int m_BQTY = 0;
        public frmForcePrint(Form parent)
        {
            InitializeComponent();
            if (parent is frmPrint)
            {
                this.m_ParentFrm = (frmPrint)parent;
            }

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Lbl_Customer.Text = m_ParentFrm.GetXML("CUSTCD");
            Lbl_Plant.Text = m_ParentFrm.GetXML("CUST_PLANT");
            Lbl_Line.Text = m_ParentFrm.GetXML("CUST_LINE");
            Lbl_Item.Text = m_ParentFrm.GetXML("ITEM");
            label3.Text = "B/QTY - " + m_ParentFrm.GetXML("BUCKET_COUNT");
            m_BQTY = Convert.ToInt16(m_ParentFrm.GetXML("BUCKET_COUNT"));

            Txt_ITEM_QTY.Text = m_ParentFrm.GetXML("BUCKET_COUNT");
            txtStartSeq.Text = "";
            
            m_dt = m_ParentFrm.GetData();

            if (m_dt.Rows.Count < 1)
            {
                return;
            }

            string strDate = "";

            m_filteredDT = m_dt.Clone();

            //현재 수신된 서열중 Bucket이 발행되지 않은 서열정보 수집
            foreach (System.Data.DataRow dr in m_dt.Rows)
            {
                if (dr["BPRINT_YN"].ToString() == "N" && string.IsNullOrEmpty(dr["ITEM1"].ToString().Trim()) == false)
                {
                    m_filteredDT.ImportRow(dr);

                    strDate = dr["YMD"].ToString();

                    
                }

            }

            if (m_filteredDT.Rows.Count > 0)
            {
                strDate = m_filteredDT.Rows[0]["YMD"].ToString();
                dtStartDate.Value = Convert.ToDateTime(strDate.Substring(0, 4) + "-" + strDate.Substring(4, 2) + "-" + strDate.Substring(6, 2));

                txtStartSeq.Text = m_filteredDT.Rows[0]["SEQNO"].ToString();

                txtEndSeq.Text = (Convert.ToInt32(txtStartSeq.Text) + m_BQTY-1).ToString().PadLeft(4, '0');
            }
        }

        private void btn_ForcePrint_Click(object sender, EventArgs e)
        {
            txtEndSeq.Text = (Convert.ToInt32(txtStartSeq.Text) + m_BQTY - 1).ToString().PadLeft(4, '0');
            if (string.IsNullOrEmpty(txtStartSeq.Text) == true || string.IsNullOrEmpty(txtEndSeq.Text) == true)
            {
                MessageBox.Show("Please check FROM-SEQNO or TO-SEQNO for force printing!!", "Notice", MessageBoxButtons.OK);
                return;
            }



            if (HE_MES.FX.Utils.Glb_FNS.GetO2I(Txt_ITEM_QTY.Text) > m_BQTY)
            {
                MessageBox.Show("It's excess Bucket's Maximum QTY("+ m_BQTY.ToString()+")", "Notice", MessageBoxButtons.OK);
                return;
            }

            if (m_filteredDT.Rows.Count <HE_MES.FX.Utils.Glb_FNS.GetO2I(Txt_ITEM_QTY.Text))
            {
                MessageBox.Show("Your selected data's count is less than the printable bucket count!!", "Notice", MessageBoxButtons.OK);
                return;
            }

            if (MessageBox.Show("Do you print report forced?", "Notice", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                PrintData();
            }

            this.Close();
 
        }

        private void PrintData()
        {
            try
            {

                System.Data.DataTable filteredDT = m_filteredDT.Clone();

                //강제발행을 위해 입력한 서열순번까지 Data 생성
                for (int i = 0; i < HE_MES.FX.Utils.Glb_FNS.GetO2I(Txt_ITEM_QTY.Text);i++)
                {
                    filteredDT.ImportRow(m_filteredDT.Rows[i]);
                }


                //강제발행 대상의 서열테이타 중 사내서열지가 발행되지 않은 데이타가 사내서열지 발행기준수량을 초과 한 경우 사내 서열지 우선 발행
                

                m_ParentFrm.m_bForcePrint = true;

                m_ParentFrm.m_bBucketPrintReady = true;
                m_ParentFrm.PrintBucket(filteredDT);

                //강제발행된TO-SEQNO 이후의 SEQNO번호들에 대한 REPORT출력 초기화 처리

                MessageBox.Show("Success to froce printing!!", "Confirm", MessageBoxButtons.OK);
            }
            catch
            {
            }
            finally
            {
                m_ParentFrm.m_bForcePrint = false;
                m_ParentFrm.ReLoadData();
            }

        }

        private void Txt_ITEM_QTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != 8)
            {


                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (string.IsNullOrEmpty(Txt_ClearSEQ.Text) || string.IsNullOrEmpty(Txt_ClearBSEQ.Text))
                {
                    MessageBox.Show("Please enter the FROM SEQ & BUCKET SEQ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ValidateText(Txt_ClearSEQ.Text) && ValidateText(Txt_ClearBSEQ.Text))
                {
                    m_ParentFrm.SetClear(dtStartDate.Value.ToString("yyyyMMdd"), Txt_ClearSEQ.Text, Txt_ClearBSEQ.Text);
                    m_ParentFrm.ReLoadData();
                }
                else
                {
                    MessageBox.Show("This field accepts only Numbers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private bool ValidateText(string text)
        {
            char[] characters = text.ToCharArray();

            foreach (char c in characters)
            {
                if (!char.IsNumber(c))
                    return false;
            }
            return true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                Txt_ClearSEQ.Enabled = true;
                Txt_ClearBSEQ.Enabled = true;
                button1.Enabled = true;
                dtStartDate.Enabled=true;
            }
            else
            {
                Txt_ClearSEQ.Enabled = false;
                Txt_ClearBSEQ.Enabled = false;
                button1.Enabled = false;
                dtStartDate.Enabled = false;
            }
        }
 
    }
}
