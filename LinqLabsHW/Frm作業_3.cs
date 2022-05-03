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
    }
}
