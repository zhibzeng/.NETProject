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
    public partial class MonitorWareHouse : Form
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

        public class WHLists
        {
            public string id;
            public string name;
            public WHLists(string vid, string vname)
            {
                id = vid;
                name = vname;
            }
            public string wID
            {
                get { return id; }
            }
            public string wName
            {
                get { return name; }
            }
        }

        public MonitorWareHouse()
        {
            InitializeComponent();
            localhost.Service service = new localhost.Service();

            //仓库选择
            DataSet WHDS = service.QueryWH();
            ArrayList WHlists = new ArrayList();
            if (WHDS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < WHDS.Tables[0].Rows.Count; i++)
                {
                    WHLists p = new WHLists(WHDS.Tables[0].Rows[i][0].ToString(), WHDS.Tables[0].Rows[i][1].ToString());
                    WHlists.Add(p);
                }
            }
            this.comboBox2.DisplayMember = "wName";
            this.comboBox2.ValueMember = "wID";
            this.comboBox2.DataSource = WHlists;
            this.comboBox2.SelectedIndex = 0;


            //产品选择
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


            

            Object[] param = new Object[2];
            param[0] = comboBox1.SelectedValue.ToString().Trim();
            param[1] = comboBox2.SelectedValue.ToString().Trim();
            DataSet gvds = service.QueryWarehousing(param, 1);
            dataGridView1.DataSource = gvds.Tables[0];
            CalculateWHInfo(gvds);

        }

        private void CalculateWHInfo(DataSet ds) {
            int sum = 0;
            //遍历一个表多行多列
            foreach (DataRow mDr in ds.Tables[0].Rows)
            {
                sum = sum + Convert.ToInt32(mDr[2].ToString().Trim());
            }
            if (sum > 0)
            {
                textBox1.Text = "该仓库内该产品库存充足,库存数量为：" + sum;
                textBox1.ForeColor = Color.Black;
            }
            else {
                textBox1.Text = "该仓库内该产品库存不足，短缺数量为:" + sum; //sum.ToString().Substring(1,sum.ToString().Length-1);
                textBox1.ForeColor = Color.Red;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void MonitorWareHouse_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            localhost.Service service = new localhost.Service();
            Object[] param = new Object[2];
            param[0] = comboBox1.SelectedValue.ToString().Trim();
            param[1] = comboBox2.SelectedValue.ToString().Trim();
            DataSet gvds = service.QueryWarehousing(param, 1);
            dataGridView1.DataSource = gvds.Tables[0];
            CalculateWHInfo(gvds);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            localhost.Service service = new localhost.Service();
            Object[] param = new Object[2];
            param[0] = comboBox1.SelectedValue.ToString().Trim();
            param[1] = comboBox2.SelectedValue.ToString().Trim();
            DataSet gvds = service.QueryWarehousing(param, 1);
            dataGridView1.DataSource = gvds.Tables[0];
            CalculateWHInfo(gvds);
        }
    }
}
