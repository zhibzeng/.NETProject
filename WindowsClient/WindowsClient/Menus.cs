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
    public partial class Menus : Form
    {
        public Menus()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            new EmployeeUI.EmployeeInfo().ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            new Product.ProductInfo().ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            new Sales.DirectSale().ShowDialog();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            new WareHouse.PurchaseOrder().ShowDialog(); 
        }

        private void label7_Click(object sender, EventArgs e)
        {
            new WareHouse.MonitorWareHouse().ShowDialog();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            new Product.AddProduct().ShowDialog();

        }

        private void label9_Click(object sender, EventArgs e)
        {
            new WareHouse.ProductIn().ShowDialog();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            new WareHouse.ProductOut().ShowDialog();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            new EmployeeUI.AddEmployee().ShowDialog();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            new WareHouse.AddPurchaseOrder().ShowDialog();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            new Sales.AddSaleOrder().ShowDialog();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
