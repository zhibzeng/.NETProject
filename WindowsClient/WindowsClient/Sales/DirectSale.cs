using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsClient.Sales
{
    public partial class DirectSale : Form
    {
        public DirectSale()
        {
            InitializeComponent();
            localhost.Service service = new localhost.Service();
            DataSet SalesDS = new DataSet();
            SalesDS = service.QuerySaleOrders("");
            dataGridView1.DataSource = SalesDS.Tables[0];
            comboBox1.SelectedIndex = 0;

        }

        private void DirectSale_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            localhost.Service service = new localhost.Service();
            DataSet SalesDS = service.QuerySaleOrders("");
            dataGridView1.DataSource = SalesDS.Tables[0];
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            localhost.Service service = new localhost.Service();
            DataSet SalesDS;
            if (comboBox1.SelectedItem.ToString().Trim() == "所有")
            {
                 SalesDS = service.QuerySaleOrders("");
            }
            else {
                 SalesDS = service.QuerySaleOrders(comboBox1.SelectedItem.ToString().Trim());
            }
         
            dataGridView1.DataSource = SalesDS.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            localhost.Service service = new localhost.Service();
            Object[] param = new Object[2];
            param[1] = "已审";
            param[0] = textBox5.Text.Trim();
            int result = -1;
            result = service.OperateSaleOrder(param,1);
            if (result > 0)
            {
                MessageBox.Show("完成订单审核");
                localhost.Service service2 = new localhost.Service();
                DataSet SalesDS = new DataSet();
                SalesDS = service2.QuerySaleOrders("");
                dataGridView1.DataSource = SalesDS.Tables[0];

            }
            else {
                MessageBox.Show("审核订单出错");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //string str = System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.IndexOf("WindowsClient"));
            new AddSaleOrder().ShowDialog();
        }
    }
}
