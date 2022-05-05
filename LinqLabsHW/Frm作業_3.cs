using LinqLabsHW;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
        }
        NorthwindEntities dbContext = new NorthwindEntities();
        private void button6_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();
            this.dataGridView1.DataSource = files.ToList();
            var q = from f in files
                    orderby f.CreationTime.Year descending
                    group f by f.CreationTime.Year into g
                    select new { Key = g.Key, Count = g.Count(),Group=g };
            this.dataGridView2.DataSource = q.ToList();

            foreach(var group in q)
            {
                string s = $"{group.Key}({group.Count})";
                TreeNode node = this.treeView1.Nodes.Add(group.Key.ToString(),s);
                foreach(var item in group.Group)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
                   
        }

        private string MyGroup(long n)
        {
            if (n < 1000)
                return "Small File";
            else if (n > 1000 && n < 5000)
                return "Medium File";
            else
                return "Large File";
        }

        private void button38_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();
            this.dataGridView1.DataSource = files.ToList();
            var q = from f in files
                    orderby f.Length descending
                    group f by MyGroup(f.Length) into g
                    select new { Key = g.Key, Count = g.Count(), Group = g };
            this.dataGridView2.DataSource = q.ToList();

            foreach (var group in q)
            {
                string s = $"{group.Key}({group.Count})";
                TreeNode node = this.treeView1.Nodes.Add(group.Key.ToString(), s);
                foreach (var item in group.Group)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            int count = 1;
            foreach(int n in nums)
            {
                TreeNode node = null;
                if (this.treeView1.Nodes[MyKey(n)] == null)
                {
                    node = this.treeView1.Nodes.Add(MyKey(n), MyKey(n));
                    node.Nodes.Add(n.ToString());
                    count = 1;
                }
                else
                {
                    treeView1.Nodes[MyKey(n)].Nodes.Add(n.ToString());
                    count += 1;
                    treeView1.Nodes[MyKey(n)].Text = $"{MyKey(n)}({count})";
                }
            }
            //int s=0, m=0, l=0;
            //foreach(int n in nums)
            //{
            //    if (n < 5)
            //    {
            //        s += 1;
            //    }
            //    if(n>4 && n < 9)
            //    {
            //        m += 1;
            //    }
            //    if (n > 8)
            //    {
            //        l += 1;
            //    }
            //}
            //TreeNode node = this.treeView1.Nodes.Add("small","small("+s.ToString()+")");
            //foreach (int n in nums)
            //{
            //    if (n < 5)
            //    {
            //        node.Nodes.Add(n.ToString());
            //    }
            //}
            //node = this.treeView1.Nodes.Add("medium", "medium(" + m.ToString() + ")");
            //foreach (int n in nums)
            //{
            //    if (n>4 && n < 9)
            //    {
            //        node.Nodes.Add(n.ToString());
            //    }
            //}
            //node = this.treeView1.Nodes.Add("large", "large(" + l.ToString() + ")");
            //foreach (int n in nums)
            //{
            //    if (n > 8)
            //    {
            //        node.Nodes.Add(n.ToString());
            //    }
            //}
        }

        private string MyKey(int n)
        {
            if (n < 5)
                return "small";
            else if (n < 9)
                return "medium";
            else
                return "large";
        }
        private string MyPrice(decimal n)
        {
            if (n < 31)
                return "Low";
            else if (n < 51)
                return "Medium";
            else
                return "High";
        }
        private void button8_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
            this.treeView1.Nodes.Clear();
            var q = from p in this.dbContext.Products
                    select p;
            this.dataGridView1.DataSource = q.ToList();
            var q1 = from p in this.dbContext.Products.ToList()
                     orderby p.UnitPrice descending
                    group p by MyPrice(p.UnitPrice.Value) into g
                    select new { Key=g.Key, Count = g.Count(), Group = g };
            this.dataGridView2.DataSource = q1.ToList();
            foreach (var group in q1)
            {
                string s = $"{group.Key}({group.Count})";
                TreeNode node = treeView1.Nodes.Add(group.Key.ToString(), s);
                foreach (var item in group.Group)
                {
                    node.Nodes.Add(item.UnitPrice.ToString());
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
            this.treeView1.Nodes.Clear();
            var q = from p in this.dbContext.Orders
                    select p;
            this.dataGridView1.DataSource = q.ToList();
            var q1 = from o in this.dbContext.Orders
                     orderby o.OrderDate descending
                     group o by o.OrderDate.Value.Year into g
                     select new { Key = g.Key, Count = g.Count(), Group = g };
            this.dataGridView2.DataSource = q1.ToList();
            foreach (var group in q1)
            {
                string s = $"{group.Key}({group.Count})";
                TreeNode node = treeView1.Nodes.Add(group.Key.ToString(), s);
                foreach (var item in group.Group)
                {
                    node.Nodes.Add(item.OrderDate.ToString());
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
            this.treeView1.Nodes.Clear();
            var q = from p in this.dbContext.Orders
                    select p;
            this.dataGridView1.DataSource = q.ToList();
            var q2 = from o in this.dbContext.Orders
                     group o by new { o.OrderDate.Value.Year, o.OrderDate.Value.Month } into g
                     orderby g.Key.Year,g.Key.Month
                     select new { Year = g.Key, Count = g.Count() , Group = g };
            this.dataGridView2.DataSource = q2.ToList();
            foreach (var group in q2)
            {
                string s = $"{group.Year}({group.Count})";
                TreeNode node = treeView1.Nodes.Add(group.Year.ToString(), s);
                foreach (var item in group.Group)
                {
                    node.Nodes.Add(item.OrderDate.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            var q = from c in this.dbContext.Categories
                    from p in c.Products
                    from o in p.Order_Details
                    orderby p.ProductName
                    select new { ProductName=p.ProductName,Price = Math.Round(o.Quantity * (1 - o.Discount) * (int)o.UnitPrice, 2)  };
            this.dataGridView2.DataSource = q.ToList();
            var q1 = from t in q
                     select new { Total = Math.Round(q.Sum(n => n.Price), 2) };
            this.dataGridView1.DataSource = q1.Take(1).ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            var q = from c in this.dbContext.Categories
                     from p in c.Products
                     orderby p.UnitPrice descending
                     select new { ProductName = p.ProductName, UnitPrice = p.UnitPrice, CategoryName = c.CategoryName };
            this.dataGridView2.DataSource = q.ToList();
            var q1 =from c in this.dbContext.Categories
                    from p in c.Products
                    orderby p.UnitPrice descending
                    select new { ProductName = p.ProductName, UnitPrice = p.UnitPrice, CategoryName = c.CategoryName };
            this.dataGridView1.DataSource = q1.Take(5).ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            var q1 = from c in this.dbContext.Categories
                     from p in c.Products
                     orderby p.UnitPrice descending
                     select p.UnitPrice;
            bool result =q1.Any(n=>n>300);
            string s = "";
         
            switch (result)
            {
                case true:
                    s = "有";
                    break;
                case false:
                    s = "無";
                    break;
            }
            MessageBox.Show("結果為:" + s);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            var q = from em in this.dbContext.Employees.ToList()
                    from o in em.Orders
                    from od in o.Order_Details
                    select new { Name = $"{em.LastName}{em.FirstName}", Price = Math.Round(od.Quantity * (1 - od.Discount) * (int)od.UnitPrice, 2) };
            this.dataGridView2.DataSource = q.ToList();
            var q1 = from s in q.OrderBy(n=>n.Price).GroupBy(n=>n.Name)
                     select new { EmployeeName = s.Key, Total = Math.Round(s.Sum(n => n.Price), 2) };
            var q2 = from sl in q1.OrderByDescending(n => n.Total)
                     select sl;
            this.dataGridView1.DataSource = q2.Take(5).ToList();
        }
    }
}
