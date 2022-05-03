using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            this.ordersTableAdapter1.Fill(this.nwDataSet11.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet11.Order_Details);
            this.productsTableAdapter1.Fill(this.nwDataSet11.Products);

            var q =from o in this.nwDataSet11.Orders
            group o by o.OrderDate.Year into newGroup
            orderby newGroup.Key
            select newGroup;

            foreach (var yearGroup in q)
            {
                comboBox1.Items.Add($"{yearGroup.Key}");
            }


        }
        bool Flag = true;
        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
        int num;
        int count = 0;
        int end = 0;
        private void button13_Click(object sender, EventArgs e)
        {
            bool isNum = int.TryParse(textBox1.Text, out num);
            if (Flag && count == 0) 
            {
                dataGridView2.DataSource = null;
                var q = from p in this.nwDataSet11.Products.Take(num)
                        select p;
                this.dataGridView1.DataSource = q.ToList();

                var q1 = from p in this.nwDataSet11.Products
                        select p;
                end = q1.Count();
              
                count += 1;
                Flag = false;
            }
            else if(!isNum)
            {
                MessageBox.Show("請輸入數值。");
            }
            else
            {
                if(count * num > end)
                {
                    MessageBox.Show("已在資料表末端。");
                }
                else
                {
                    var q = from p in this.nwDataSet11.Products.Skip(count * num).Take(num)
                            select p;
                    this.dataGridView1.DataSource = q.ToList();
                    count += 1;
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files =  dir.GetFiles();

            
            var q = from f in files
                    where f.Extension == ".log"
                    select f;
            this.dataGridView1.DataSource = q.ToList();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            var q = from o in this.nwDataSet11.Orders
                    where !o.IsShippedDateNull()
                    select o;
            this.dataGridView1.DataSource = q.ToList();
            Flag = true;
            count = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var q = from o in this.nwDataSet11.Orders
                    where o.OrderDate.Year.ToString()==comboBox1.Text && !o.IsShippedDateNull()
                    select o;
            this.dataGridView1.DataSource = q.ToList();


            var q1 = from o in this.nwDataSet11.Orders
                     join od in this.nwDataSet11.Order_Details on o.OrderID equals od.OrderID
                     where o.OrderDate.Year.ToString() == comboBox1.Text
                     select new { o.OrderID, od.ProductID, od.UnitPrice, od.Discount };
            this.dataGridView2.DataSource = q1.ToList();
            Flag = true;
            count = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            var q = from f in files
                    where f.CreationTime.Year == 2020
                    select f;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            var q = from f in files
                    where f.Length>65000
                    select f;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(Flag && count == 0)
            {
                int orderID = (int)this.dataGridView1.CurrentRow.Cells["OrderID"].Value;
                var q = from o in this.nwDataSet11.Order_Details
                        where o.OrderID == orderID
                        select o;
                this.dataGridView2.DataSource = q.ToList();
            }
            else
            {
                int productID = (int)this.dataGridView1.CurrentRow.Cells["ProductID"].Value;
                var q = from o in this.nwDataSet11.Order_Details
                        where o.ProductID == productID
                        select o;
                this.dataGridView2.DataSource = q.ToList();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            var q = from p in this.nwDataSet11.Products
                    select p;
            this.dataGridView1.DataSource = q.ToList();
            end = q.Count();
            count = 0;
            Flag = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            bool isNum = int.TryParse(textBox1.Text, out num);
            if (count == 0 || count == 1)
            {
                MessageBox.Show("已在資料表首端");
            }
            else if (!isNum)
            {
                MessageBox.Show("請輸入數值。");
            }
            else
            {
                count -= 2;
                var q = from p in this.nwDataSet11.Products.Skip(count * num).Take(num)
                        select p;
                this.dataGridView1.DataSource = q.ToList();
                count += 1;
            }
        }

   
    }
}
