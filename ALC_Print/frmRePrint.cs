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
    public partial class frmRePrint : Form
    {
        frmPrint m_ParentFrm = null;

        string m_strReprintDate = "";

        public frmRePrint(Form parent)
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

            radioButton2.Checked = true;
            Lbl_Customer.Text = m_ParentFrm.GetXML("CUSTCD");
            Lbl_Plant.Text = m_ParentFrm.GetXML("CUST_PLANT");
            Lbl_Line.Text = m_ParentFrm.GetXML("CUST_LINE");
            Lbl_Item.Text = m_ParentFrm.GetXML("ITEM");
            txtPrintSeq.Text = "";
            txtBucketSeq.Text = "";

            button3.Visible = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                pn_OurReport.Visible = true;
                pn_Bucket.Visible = false;
            }
            else
            {
                pn_OurReport.Visible = false;
                pn_Bucket.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBucketSeq.Text) == true && string.IsNullOrEmpty(txtPrintSeq.Text) == true)
            {
                MessageBox.Show("Bucket report or Our report is not exist to re-print!!", "Notice", MessageBoxButtons.OK);
                return;
            }

            PrintData();
        }
        
        private void PrintData()
        {
            DataTable dt = new DataTable();
            //TO DO : MAKE THE QUERY
            if (radioButton2.Checked && string.IsNullOrEmpty(txtBucketSeq.Text) == false)
            {
                //dt = m_ParentFrm.GetData_Reprint(dateTimePicker1.Value.ToString("yyyyMMdd"),txtBucketSeq.Text.PadLeft(3, '0'), txtBucketCount.Text);
                if (string.IsNullOrEmpty(m_strReprintDate) == true)
                {
                    dt = m_ParentFrm.GetData_Reprint(dateTimePicker1.Value.ToString("yyyyMMdd"), txtBucketSeq.Text, txtBucketCount.Text);
                }
                else
                {
                    dt = m_ParentFrm.GetData_Reprint(m_strReprintDate, txtBucketSeq.Text, txtBucketCount.Text);
                }
                if (dt.Rows.Count > 0)
                {
                    string pos = "";
                    if(RD_POS_ALL.Checked)
                    {
                        pos = "ALL";
                    }
                    else if(RD_POS_FA.Checked)
                    {
                        pos = "FA";
                    }
                    else if (RD_POS_RA.Checked)
                    {
                        pos = "RA";
                    }
                    m_ParentFrm.PrintBucket(dt, HE_MES.FX.Utils.Glb_FNS.GetO2I(txtBucketSeq.Text), HE_MES.FX.Utils.Glb_FNS.GetO2I(txtBucketCount.Text), pos);                           
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtSeqno.Text) == true)
            {
                MessageBox.Show("Please, Input SEQNO for Bucket Re-printing!!", "Notice", MessageBoxButtons.OK);
                return;
            }

            DataTable dt = new DataTable();

            if (radioButton1.Checked == true)
            {
                dt = m_ParentFrm.GetData_Reprint_Seqno(dateTimePicker1.Value.ToString("yyyyMMdd"), txtSeqno.Text.PadLeft(4, '0'));

                if (dt.Rows.Count > 0)
                {
                    txtPrintSeq.Text = dt.Rows[0]["OURPRT_SEQ"].ToString();

                    txtBucketSeq.Text = dt.Rows[0]["PRINT_BSEQ"].ToString();

                    if (string.IsNullOrEmpty(txtBucketSeq.Text) == true)
                    {
                        MessageBox.Show("Our report is not exist for SEQNO[" + txtSeqno.Text + "]!!", "Notice", MessageBoxButtons.OK);
                        return;
                    }

                    txtBucketCount.Text = dt.Rows[0]["BSEQCD_RCV_COUNT"].ToString();
                    m_strReprintDate = dt.Rows[0]["YMD"].ToString();
                }
                else
                {
                    MessageBox.Show("Our report is not exist for SEQNO[" + txtSeqno.Text + "]!!", "Notice", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                dt = m_ParentFrm.GetData_Reprint_Seqno(dateTimePicker1.Value.ToString("yyyyMMdd"), txtSeqno.Text.PadLeft(4, '0'));

                if (dt.Rows.Count > 0)
                {
                    txtBucketSeq.Text = dt.Rows[0]["PRINT_BSEQ"].ToString();

                    if (string.IsNullOrEmpty(txtBucketSeq.Text) == true)
                    {
                        MessageBox.Show("Bucket report is not exist for SEQNO[" + txtSeqno.Text + "]!!", "Notice", MessageBoxButtons.OK);
                        return;
                    }

                    txtBucketCount.Text = dt.Rows[0]["BSEQCD_RCV_COUNT"].ToString();
                    m_strReprintDate = dt.Rows[0]["YMD"].ToString();
                }
                else
                {
                    MessageBox.Show("Bucket report is not exist for SEQNO[" + txtSeqno.Text + "]!!", "Notice", MessageBoxButtons.OK);
                    return;
                }
            }

            //if (dt.Rows.Count > 0)
            //{
            //    txtBucketSeq.Text = dt.Rows[0]["PRINT_BSEQ"].ToString();

            //    if (string.IsNullOrEmpty(txtBucketSeq.Text) == true)
            //    {
            //        MessageBox.Show("Bucket report is not exist for SEQNO[" + txtSeqno.Text + "]!!", "Notice", MessageBoxButtons.OK);
            //        return;
            //    }

            //    txtBucketCount.Text = dt.Rows[0]["BSEQCD_RCV_COUNT"].ToString();
            //    m_strReprintDate = dt.Rows[0]["YMD"].ToString();
            //}
            //else
            //{
            //    MessageBox.Show("Bucket report is not exist for SEQNO[" + txtSeqno.Text + "]!!", "Notice", MessageBoxButtons.OK);
            //    return;
            //}

            //GetData_Reprint_Seqno
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton2.Checked == false || string.IsNullOrEmpty(txtBucketSeq.Text) == true)
                {
                    MessageBox.Show("Bucket report is not exist to delete!!", "Notice", MessageBoxButtons.OK);
                    return;
                }

                if (MessageBox.Show("Do you really delete Bucket report forced?", "Notice", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int iResultCnt = 0;
                    if (string.IsNullOrEmpty(m_strReprintDate) == true)
                    {
                        iResultCnt = m_ParentFrm.Delete_Bucket_Report(dateTimePicker1.Value.ToString("yyyyMMdd"), txtBucketSeq.Text, txtBucketCount.Text);
                    }
                    else
                    {
                        iResultCnt = m_ParentFrm.Delete_Bucket_Report(m_strReprintDate, txtBucketSeq.Text, txtBucketCount.Text);
                    }

                    if (iResultCnt < 1)
                    {
                        MessageBox.Show("Bucket report is deleted!!", "Notice", MessageBoxButtons.OK);
                    }

                }

            }
            catch (Exception eLog)
            {
                System.Diagnostics.Debug.WriteLine(eLog.Message);
            }
            finally
            {
                button3.Visible = false;
            }
        }

        private void Lbl_Item_DoubleClick(object sender, EventArgs e)
        {

            //if (radioButton2.Checked == true && button3.Visible == false)
            //{
            //    button3.Visible = true;
            //}
            //else
            //{
            //    button3.Visible = false;
            //}
        }

    }
}
