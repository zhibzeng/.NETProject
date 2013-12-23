using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsClient.Product
{
    public partial class ProductInfo : Form
    {
        public ProductInfo()
        {
            InitializeComponent();
        }

        private void ProductInfo_Load(object sender, EventArgs e)
        {
            localhost.Service service = new localhost.Service();
            DataSet s = service.QueryAllProductInfo();
            if (s.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = s.Tables[0];
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Object[] param = new Object[4];
            param[0] = textBox1.Text.Trim();
            param[1] = textBox2.Text.Trim();
            param[2] = textBox3.Text.Trim();
            param[3] = textBox4.Text.Trim();
            localhost.Service service = new localhost.Service();
            int result = service.OperateProduct(param, 1);
            if (result > 0)
            {
                MessageBox.Show("产品信息更新成功");
                button2.PerformClick();
            }
            else {
                MessageBox.Show("产品信息更新失败");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            localhost.Service service = new localhost.Service();
            DataSet s = service.QueryAllProductInfo();
            if (s.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = s.Tables[0];
            }
        }
    }
}
