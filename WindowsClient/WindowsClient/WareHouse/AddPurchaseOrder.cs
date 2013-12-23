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
    public partial class AddPurchaseOrder : Form
    {
        public class ProductLists {
            public string id;
            public string name;
            public ProductLists(string vid,string vname) {
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


        public AddPurchaseOrder()
        {
            InitializeComponent();
            localhost.Service service = new localhost.Service();
            textBox1.Text = service.CalNextPurchaseCode();


            DataSet PListDS = service.QueryGoods();
            ArrayList lists = new ArrayList();
            if (PListDS.Tables[0].Rows.Count > 0) {
                for (int i = 0; i < PListDS.Tables[0].Rows.Count; i++) {
                    ProductLists p = new ProductLists(PListDS.Tables[0].Rows[i][0].ToString(),PListDS.Tables[0].Rows[i][1].ToString());
                    lists.Add(p);
                }
            }
            this.comboBox1.DisplayMember = "pName";
            this.comboBox1.ValueMember = "pID";
            this.comboBox1.DataSource = lists;
            this.comboBox1.SelectedIndex = 0;


            DataSet EListDS = service.QueryAllEmployee();
            ArrayList Elists = new ArrayList();
            if (EListDS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < EListDS.Tables[0].Rows.Count; i++)
                {
                    ProductLists p = new ProductLists(EListDS.Tables[0].Rows[i][0].ToString(), EListDS.Tables[0].Rows[i][1].ToString());
                    Elists.Add(p);
                }
            }
            this.comboBox2.DisplayMember = "pName";
            this.comboBox2.ValueMember = "pID";
            this.comboBox2.DataSource = Elists;
            this.comboBox2.SelectedIndex = 0;

            
   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Object[] param = new Object[8];
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "") {
                string date = dateTimePicker1.Value.Year.ToString() + "/" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString();
                param[0] = textBox1.Text;
                param[1] = comboBox1.SelectedValue.ToString();
                param[2] = textBox2.Text.Trim();
                param[3] = textBox3.Text.Trim();
                param[4] = "未审";
                param[5] = date;
                param[6] = comboBox2.SelectedValue.ToString();
                param[7] = comboBox3.SelectedItem.ToString();
                localhost.Service service = new localhost.Service();
                int status = -1;
                status = service.OperatePurOrder(param, 1);
                if (status > 0)
                {
                    MessageBox.Show("添加成功");
                }
                else {
                    MessageBox.Show("添加出错");
                }
            }
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddPurchaseOrder_Load(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
