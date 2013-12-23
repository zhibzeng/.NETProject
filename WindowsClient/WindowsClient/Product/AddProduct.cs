using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace WindowsClient.Product
{
    public partial class AddProduct : Form
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



        public AddProduct()
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
            this.comboBox2.DisplayMember = "pName";
            this.comboBox2.ValueMember = "pID";
            this.comboBox2.DataSource = lists;
            this.comboBox2.SelectedIndex = 0;
            textBox1.Text = service.CalNextProductCode();
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Object[] param = new Object[5];
            param[0] = textBox1.Text;
            param[1] = textBox2.Text;
            param[2] = textBox4.Text.ToString().Trim();
            param[3] = textBox3.Text.ToString().Trim();
            param[4] = comboBox2.SelectedValue; 
            localhost.Service service = new localhost.Service();
            if (service.OperateProduct(param, 2) > 0)
            {
                MessageBox.Show("成功新增产品");
                this.Close();
            }
            else
            {
                MessageBox.Show("新增产品失败");
            }
        }
    }
}
