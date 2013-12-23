using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsClient
{
    public partial class Logon : Form
    {
        public Logon()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox2.Text != ""))
            {
                localhost.Service service = new localhost.Service();
                DataSet dataset = new DataSet();
                dataset = service.QueryEmployeeByName(textBox1.Text.Trim());
                if (dataset.Tables[0].Rows.Count > 0&&textBox2.Text.Trim()=="admin")
                {
                    EmployeeEntity.员工代码 = dataset.Tables[0].Rows[0][0].ToString();
                    EmployeeEntity.姓名 = dataset.Tables[0].Rows[0][1].ToString();
                    EmployeeEntity.性别 = dataset.Tables[0].Rows[0][2].ToString();
                    EmployeeEntity.出生日期 = dataset.Tables[0].Rows[0][3].ToString();
                    EmployeeEntity.所属分公司 = Convert.ToInt32(dataset.Tables[0].Rows[0][4].ToString().Trim()
                        .Substring(1, dataset.Tables[0].Rows[0][4].ToString().Trim().IndexOf("分")-1));
                    EmployeeEntity.照片 = dataset.Tables[0].Rows[0][5].ToString();
                    
                    this.Hide();
                }
                else {
                    MessageBox.Show("用户名或密码不正确！");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Close();

        }
    }
}
