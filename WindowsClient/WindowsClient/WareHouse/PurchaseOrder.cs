using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsClient.WareHouse
{
    public partial class PurchaseOrder : Form
    {
        public static Object[] OrderInfo= new Object[8];
        public PurchaseOrder()
        {
            InitializeComponent();
        }

        private void PurchaseOrder_Load(object sender, EventArgs e)
        {
            localhost.Service service = new localhost.Service();
            DataSet s = new DataSet();
            s = service.QueryPurOrder("");
            dataGridView1.DataSource = s.Tables[0];
            comboBox1.Text = "请选择筛选条件";
            comboBox1.Items.Add("所有订单");
            comboBox1.Items.Add("已审");
            comboBox1.Items.Add("未审");
           

            comboBox1.SelectedIndex = 0;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString().Trim() == "已审") {
                localhost.Service service = new localhost.Service();
                DataSet s = new DataSet();
                s = service.QueryPurOrder("已审");
                dataGridView1.DataSource = s.Tables[0];
            }
            else if (comboBox1.SelectedItem.ToString().Trim() == "未审")
            {
                localhost.Service service = new localhost.Service();
                DataSet s = new DataSet();
                s = service.QueryPurOrder("未审");
                dataGridView1.DataSource = s.Tables[0];

            }
            else {
                localhost.Service service = new localhost.Service();
                DataSet s = new DataSet();
                s = service.QueryPurOrder("");
                dataGridView1.DataSource = s.Tables[0];
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();//订单号
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();//产品名称
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();//数量
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();//单价
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString(); //状态
            textBox6.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();//时间
            textBox7.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString(); //员工
            textBox8.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();//备注
            
            
           
           

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox9.Text.Trim() != "") {
                localhost.Service service = new localhost.Service();
                DataSet s = new DataSet();
                s = service.QueryPurOrderByID(textBox9.Text.Trim());
                if (s.Tables[0].Rows.Count > 0) {

                    textBox1.Text = s.Tables[0].Rows[0][0].ToString();//订单号
                    textBox2.Text = s.Tables[0].Rows[0][2].ToString(); ;//产品名称
                    textBox3.Text = s.Tables[0].Rows[0][3].ToString(); ;//数量
                    textBox4.Text = s.Tables[0].Rows[0][4].ToString(); ;//单价
                    textBox5.Text = s.Tables[0].Rows[0][5].ToString(); ; //状态
                    textBox6.Text = s.Tables[0].Rows[0][1].ToString(); ;//时间
                    textBox7.Text = s.Tables[0].Rows[0][6].ToString(); ; //员工
                    textBox8.Text = s.Tables[0].Rows[0][7].ToString(); ;//备注
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AddPurchaseOrder().ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            localhost.Service service = new localhost.Service();
            DataSet s = new DataSet();
            s = service.QueryPurOrder("");
            dataGridView1.DataSource = s.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OrderInfo[0] = textBox1.Text.Trim();
            OrderInfo[1] = textBox2.Text.Trim();
            OrderInfo[2] = textBox3.Text.Trim();
            OrderInfo[3] = textBox4.Text.Trim();
            OrderInfo[4] = textBox5.Text.Trim();
            OrderInfo[5] = textBox6.Text.Trim();
            OrderInfo[6] = textBox7.Text.Trim();
            OrderInfo[7] = textBox8.Text.Trim();
            new updatePurchaseOrder().ShowDialog();

        }




    }
}