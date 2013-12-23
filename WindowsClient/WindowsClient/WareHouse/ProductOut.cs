using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace WindowsClient.WareHouse
{
    public partial class ProductOut : Form
    {

        public class ProductLists
        {
            public string id;
            public string name;
            public ProductLists(string vid, string vname)
            {
                id = vid;
                name = vname;
            }
            public string pID
            {
                get { return id; }
            }
            public string pName
            {
                get { return name; }
            }
        }

        public ProductOut()
        {
            InitializeComponent();
            localhost.Service service = new localhost.Service();
            DataSet PListDS = service.QueryWH();
            ArrayList lists = new ArrayList();
            if (PListDS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < PListDS.Tables[0].Rows.Count; i++)
                {
                    ProductLists p = new ProductLists(PListDS.Tables[0].Rows[i][0].ToString(), PListDS.Tables[0].Rows[i][1].ToString());
                    lists.Add(p);
                }
            }
            this.comboBox1.DisplayMember = "pName";
            this.comboBox1.ValueMember = "pID";
            this.comboBox1.DataSource = lists;
            this.comboBox1.SelectedIndex = 0;
            DataSet GVDS = service.QuerySaleNotOutOrders();
            dataGridView1.DataSource = GVDS.Tables[0];
        }

        private void ProductOut_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "" && textBox4.Text.Trim() != "")
            {
                localhost.Service service = new localhost.Service();
                Object[] param = new Object[6];
                param[0] = textBox2.Text;
                string date = dateTimePicker1.Value.Year.ToString() + "/" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString();
                param[1] = date;
                param[2] = "-"+textBox3.Text;
                param[3] = service.calNextWarehousingCode();
                param[4] = textBox1.Text;
                param[5] = comboBox1.SelectedValue;
                int result1 = -1;
                int result2 = -1;
                result1 = service.operateWarehousingRecord(param, 1);
                Object[] param2 = new Object[2];
                param2[0] = textBox1.Text;
                param2[1] = 1;
                result2 = service.OperateSaleOrder(param2, 3);
                if (result1 > 0 && result2 > 0)
                {
                    MessageBox.Show("出库操作成功");
                }
                else
                {
                    MessageBox.Show("出库操作失败");
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            localhost.Service service = new localhost.Service();
            DataSet GVDS = service.QuerySaleNotOutOrders();
            dataGridView1.DataSource = GVDS.Tables[0];
        }
    }
}
