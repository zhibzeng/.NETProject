using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace WindowsClient.Sales
{
    public partial class AddSaleOrder : Form
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

        public AddSaleOrder()
        {
            InitializeComponent();
            localhost.Service service = new localhost.Service();
            DataSet PListDS = service.QueryGoods();
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
            textBox3.Text = service.CalNextSaleOrderCode();


        }

        private void AddSaleOrder_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Object[] param = new Object[6];
            param[0] = comboBox1.SelectedValue;
            param[1] = textBox1.Text;
            param[2] = textBox2.Text;
            param[3] = comboBox2.SelectedValue;
            param[4] = textBox3.Text;
            param[5] = textBox4.Text;
            localhost.Service service = new localhost.Service();
            if (service.OperateSaleOrder(param, 2) > 0)
            {
                MessageBox.Show("成功新增销售订单");
                this.Close();
            }
            else {
                MessageBox.Show("新增销售订单失败");
            }
        }
    }
}
