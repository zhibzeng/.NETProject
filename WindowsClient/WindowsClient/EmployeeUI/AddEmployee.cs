using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsClient.EmployeeUI
{
    public partial class AddEmployee : Form
    {
        public AddEmployee()
        {
            InitializeComponent();
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {
            textBox1.Text = new localhost.Service().CalNextEmployeeCode();
            DataSet ds = new DataSet();
            ds = new localhost.Service().QueryCompanies();
            comboBox2.Items.Add("请选择公司");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboBox2.Items.Add("第"+ds.Tables[0].Rows[i][0].ToString()+"分公司");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "jpg文件(*.jpg)|*.jpg|gif文件(*.gif)|*.gif";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string picPath = openFile.FileName;
                this.pictureBox1.Image = Image.FromFile(picPath);
                textBox3.Text = picPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || comboBox2.SelectedIndex == 0)
            {
                MessageBox.Show("请输入完整信息！");
            }
            else
            {
                string date = dateTimePicker1.Value.Year.ToString() + "/" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString();
                Object[] param = new object[6];
                param[0] = textBox1.Text.Trim();
                param[1] = textBox2.Text.Trim();
                param[2] = comboBox1.SelectedItem.ToString();
                param[3] = date;
                param[4] = comboBox2.SelectedItem.ToString().Substring(1, comboBox2.SelectedItem.ToString().IndexOf("分")-1);
                param[5] = textBox3.Text.Trim();
                try
                {
                    if (new localhost.Service().OperateEmployee(param, 1) > 0)
                    {
                        MessageBox.Show("添加成功!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            this.Close();
        }
    }
}
