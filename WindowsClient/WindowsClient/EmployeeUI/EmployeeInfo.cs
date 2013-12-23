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
    public partial class EmployeeInfo : Form
    {
        public static Object[] staffInfo = new Object[6];
        public EmployeeInfo()
        {
            InitializeComponent();
            bindingData();
            OperateStatus.Hide();

        }

      
        
        private void bindingData() { 
            localhost.Service service = new localhost.Service();
            DataSet dsresult = new DataSet();
            dsresult = service.QueryAllEmployee();
            EmployeeGridView.DataSource = dsresult.Tables[0];
            
          

        }
        //点击数据行
        private void EmployeeGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = EmployeeGridView.CurrentRow.Cells[1].Value.ToString();//姓名
            textBox2.Text = EmployeeGridView.CurrentRow.Cells[0].Value.ToString();//编号
            textBox3.Text = EmployeeGridView.CurrentRow.Cells[2].Value.ToString();//性别
            textBox4.Text = EmployeeGridView.CurrentRow.Cells[3].Value.ToString().Substring(0, 9);//生日
            textBox5.Text = EmployeeGridView.CurrentRow.Cells[4].Value.ToString();//分公司名称
            try
            {
                string pathString = EmployeeGridView.CurrentRow.Cells[5].Value.ToString().Trim();
                pictureBox1.Image = null;
                pictureBox1.Image = Image.FromFile(pathString);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message.ToString());
            }
           
        }

        
       






        private void label4_Click(object sender, EventArgs e)
        {

        }

        //新增员工
        private void button1_Click(object sender, EventArgs e)
        {
            new AddEmployee().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            localhost.Service service = new localhost.Service();
            if (textBox2.Text.Trim() != "")
            {
               
                int status = service.DeleteEmployeeByID(textBox2.Text.Trim());
                if (status != -1)
                {
                    OperateStatus.Text = "执行成功";
                }
                else
                {
                    OperateStatus.Text = "执行出错";
                }
            }
            else {
                OperateStatus.Text = "执行出错";
            }
            OperateStatus.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        //根据姓名查询
        private void button4_Click(object sender, EventArgs e)
        {
            string[] result = new string[6];
            DataSet ds = new localhost.Service().QueryEmployeeByName(searchBox.Text.Trim().ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    result[i] = ds.Tables[0].Rows[0][i].ToString();
                }
                textBox1.Text = result[1];//姓名
                textBox2.Text = result[0];//编号
                textBox3.Text = result[2];//性别
                textBox4.Text = result[3].Substring(0, 9);//生日
                textBox5.Text = result[4];//分公司名称
                try
                {
                    pictureBox1.Image = Image.FromFile(result[5].ToString());
                }
                catch (Exception ex)
                {
                    //
                }
            }
            else
            {
                MessageBox.Show("没有找到数据！");
            }
        }

        //修改员工信息
        private void button2_Click(object sender, EventArgs e)
        {
            staffInfo[0] = textBox2.Text;
            staffInfo[1] = textBox1.Text;
            staffInfo[2] = textBox3.Text;
            staffInfo[3] = textBox4.Text;
            staffInfo[4] = textBox5.Text;
            if (pictureBox1.ImageLocation != null)
            {
                staffInfo[5] = pictureBox1.ImageLocation.ToString();
            }
            new updateEmployee().ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EmployeeGridView.DataSource = new localhost.Service().QueryAllEmployee().Tables[0];
        }

        private void EmployeeInfo_Load(object sender, EventArgs e)
        {

        }

        
    }
}
