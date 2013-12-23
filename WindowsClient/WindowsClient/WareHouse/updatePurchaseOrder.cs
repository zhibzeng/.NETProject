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
    public partial class updatePurchaseOrder : Form
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



        public updatePurchaseOrder()
        {
            InitializeComponent();
            textBox1.Text = PurchaseOrder.OrderInfo[0].ToString();
            textBox2.Text = PurchaseOrder.OrderInfo[2].ToString();
            textBox3.Text = PurchaseOrder.OrderInfo[3].ToString();
          
            localhost.Service service = new localhost.Service();
            DataSet pDS = service.QueryGoodsByName(PurchaseOrder.OrderInfo[1].ToString().Trim());
            ArrayList lists = new ArrayList();
            ProductLists p = new ProductLists(pDS.Tables[0].Rows[0][0].ToString(), pDS.Tables[0].Rows[0][1].ToString());
            lists.Add(p);
            DataSet s = service.QueryGoods();
            if (s.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < s.Tables[0].Rows.Count; i++)
                {
                    ProductLists a = new ProductLists(s.Tables[0].Rows[i][0].ToString(), s.Tables[0].Rows[i][1].ToString());
                    lists.Add(p);
                }
            }
            this.comboBox1.DisplayMember = "pName";
            this.comboBox1.ValueMember = "pID";
            this.comboBox1.DataSource = lists;
            this.comboBox1.SelectedIndex = 0;

            //Employee
            DataSet EListDS = service.QueryAllEmployee();
            ArrayList Elists = new ArrayList();
            DataSet eDS = service.QueryEmployeeByName(PurchaseOrder.OrderInfo[6].ToString().Trim());
            ProductLists c = new ProductLists(eDS.Tables[0].Rows[0][0].ToString(), eDS.Tables[0].Rows[0][1].ToString());
            Elists.Add(c);
            if (EListDS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < EListDS.Tables[0].Rows.Count; i++)
                {
                    ProductLists b = new ProductLists(EListDS.Tables[0].Rows[i][0].ToString(), EListDS.Tables[0].Rows[i][1].ToString());
                    Elists.Add(p);
                }
            }
            this.comboBox2.DisplayMember = "pName";
            this.comboBox2.ValueMember = "pID";
            this.comboBox2.DataSource = Elists;
            this.comboBox2.SelectedIndex = 0;
          

            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            dateTimePicker1.Value = Convert.ToDateTime(PurchaseOrder.OrderInfo[5]);
            

        }

        private void updatePurchaseOrder_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {   
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "") {
                Object[] param = new Object[8];
                localhost.Service service = new localhost.Service();
                int result = -1;
                string date = dateTimePicker1.Value.Year.ToString() + "/" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString();
                param[0] = textBox1.Text;
                param[1] = comboBox1.SelectedValue.ToString();
                param[2] = textBox2.Text.Trim();
                param[3] = textBox3.Text.Trim();
                param[4] = comboBox4.SelectedItem.ToString().Trim();
                param[5] = date;
                param[6] = comboBox2.SelectedValue.ToString();
                param[7] = comboBox3.SelectedItem.ToString();
               
                result = service.OperatePurOrder(param, 2);
                if (result > 0)
                {
                    MessageBox.Show("修改成功!");
                }
                else {
                    MessageBox.Show("修改失败!");
                }
                this.Close();
            }
            
        }
    }
}
