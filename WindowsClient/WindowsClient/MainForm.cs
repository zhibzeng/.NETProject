using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsClient.WareHouse;

namespace WindowsClient
{
    public partial class MainForm : Form
    {
        


        public MainForm()
        {
            InitializeComponent();
            this.label1.Hide();
           
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 员工信息浏览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new EmployeeUI.EmployeeInfo().Show();
        }

        private void 系统设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
            if (EmployeeEntity.员工代码 == "")
            {
                new Logon().ShowDialog();
            }
            else
            {
                this.label1.Text = "操作员：" + EmployeeEntity.姓名;
                this.label1.Show();

            }

        }

        private void 产品采购ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PurchaseOrder().Show();
        }

        private void 产品入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ProductIn().Show();
        }

        private void 直接销售ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Sales.DirectSale().Show();
        }

        private void 产品出库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ProductOut().Show();
        }

        private void 库存监控ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new MonitorWareHouse().Show();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void 产品信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Product.ProductInfo().ShowDialog();
        }

        private void 新增产品ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Product.AddProduct().ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            new Menus().ShowDialog();
        }
       
    }
}
