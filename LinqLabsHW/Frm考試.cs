using LinqLabsHW;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs
{
    public partial class Frm考試 : Form
    {
        public Frm考試()
        {
            InitializeComponent();

            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },

                                          };
        }
        NorthwindEntities dbContext = new NorthwindEntities();
        List<Student> students_scores;
        List<int> Student100 = new List<int>();
        public class Student
        {
            public string Name { get; set; }
            public string Class { get;  set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get;  set; }
            public string Gender { get; set; }
        }
        public class Scores
        {
            public string Subject { get; set; }
            public int Max { get; set; }
            public int Min { get; set; }
            public int Sum { get; set; }
            public decimal Avg { get; set; }
        }
        private void button36_Click(object sender, EventArgs e)
        {
            #region 搜尋 班級學生成績

            // 
            // 共幾個 學員成績 ?						
            int c = students_scores.Count;
          
            label1.Text = $"共{c}個學員成績";
            // 找出 前面三個 的學員所有科目成績	
            var q = (from f in students_scores
                     select new { Name =f.Name, Chi = f.Chi, Eng = f.Eng, Math = f.Math }).Take(3);
            this.dataGridView1.DataSource = q.ToList();
            label2.Text = "前面三個的學員所有科目成績";
            // 找出 後面兩個 的學員所有科目成績					
            var q1 = (from f in students_scores
                      select new { Name = f.Name, Chi = f.Chi, Eng = f.Eng, Math = f.Math }).Skip(students_scores.Count - 2); ;
            this.dataGridView2.DataSource = q1.ToList();
            label3.Text = "後面兩個的學員所有科目成績";
            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績						
            var q2 = from abc in students_scores
                     where abc.Name == "aaa" || abc.Name == "bbb" || abc.Name == "ccc"
                     select new { Name = abc.Name, Chi = abc.Chi, Eng = abc.Eng };
            this.dataGridView3.DataSource = q2.ToList();
            label4.Text = "'aaa','bbb','ccc'的學員國文英文科目成績";

            #endregion

        }
  
        private void button37_Click(object sender, EventArgs e)
        {
            this.dataGridView3.DataSource = null;
          
            //個人 sum, min, max, avg
            var q = from c in students_scores
                    select new
                    {
                        Name = c.Name,
                        Class =c.Class,
                        Sum = new int[] { c.Chi, c.Eng, c.Math }.Sum(),
                        Avg = Math.Round(new int[] { c.Chi, c.Eng, c.Math }.Average(),2),
                        Max=new int[] {c.Chi,c.Eng,c.Math }.Max(),
                        Min=new int[] { c.Chi, c.Eng, c.Math }.Min(),
                        Gender =c.Gender
                    };
            this.dataGridView1.DataSource = q.ToList();
            //各科 sum, min, max, avg
            int csum = students_scores.Sum(n => n.Chi);
            decimal cavg = Math.Round((decimal)students_scores.Average(n => n.Chi),2);
            int cmax = students_scores.Max(n => n.Chi);
            int cmin = students_scores.Min(n => n.Chi);
            int esum = students_scores.Sum(n => n.Eng);
            decimal eavg = Math.Round((decimal)students_scores.Average(n => n.Eng),2);
            int emax = students_scores.Max(n => n.Eng);
            int emin = students_scores.Min(n => n.Eng);
            int msum = students_scores.Sum(n => n.Math);
            decimal mavg = Math.Round((decimal)students_scores.Average(n => n.Math),2);
            int mmax = students_scores.Max(n => n.Math);
            int mmin = students_scores.Min(n => n.Math);
            List<Scores> scores;
            scores = new List<Scores>() 
            { 
                new Scores{Subject="Chi",Sum=csum,Avg=cavg,Max=cmax,Min=cmin },
                new Scores{Subject="Eng",Sum=esum,Avg=eavg,Max=emax,Min=emin },
                new Scores{Subject="Math",Sum=msum,Avg=mavg,Max=mmax,Min=mmin }
            };
            this.dataGridView2.DataSource = scores;
        }
        private void button33_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            Student100.Clear();
            this.label2.Text = "";
            this.label3.Text = "";
            this.label4.Text = "";
            Random ra = new Random();
            for (int i = 0; i < 100; i++)
            {
                int s = ra.Next(0, 100);
                Student100.Add(s);
            }
            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 
            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
            var q = from p in Student100
                    orderby p descending
                    select new { socres=p};
            this.dataGridView1.DataSource = q.ToList();
            var q1 = from p in Student100
                     orderby p descending
                     group p by MyScores(p) into g
                     select new { Key = g.Key, Count = g.Count(), Group = g };
            this.dataGridView2.DataSource = q1.ToList();
            foreach (var group in q1)
            {
                string s = $"{group.Key}({group.Count})";
                TreeNode node = treeView1.Nodes.Add(group.Key.ToString(), s);
                foreach (var item in group.Group)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
            this.button35.Enabled = true;
        }
        private string MyScores(int n)
        {
            if (n < 70)
                return "待加強";
            else if (n < 90)
                return "佳";
            else
                return "優良";
        }
        private void button35_Click(object sender, EventArgs e)
        {
            this.label2.Text = "";
            this.label3.Text = "";
            this.label4.Text = "";
            // 統計 :　所有隨機分數出現的次數/比率; sort ascending or descending
            // 63     7.00%
            // 100    6.00%
            // 78     6.00%
            // 89     5.00%
            // 83     5.00%
            // 61     4.00%
            // 64     4.00%
            // 91     4.00%
            // 79     4.00%
            // 84     3.00%
            // 62     3.00%
            // 73     3.00%
            // 74     3.00%
            // 75     3.00%
            var q1 = from p in Student100
                     orderby p descending
                     group p by p into g
                     select new { Scores = g.Key, Count = g.Count(), Rate = ((decimal)g.Count()/100).ToString("P") };
            this.dataGridView3.DataSource = q1.ToList();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            this.label2.Text = "年總銷售統計";
            this.label3.Text = "月總銷售統計";
            this.label4.Text = "年度銷售金額統計";
            // 年度最高銷售金額 年度最低銷售金額
            var q4 = from o in this.dbContext.Orders
                     from od in o.Order_Details
                     join p in this.dbContext.Products on od.ProductID equals p.ProductID
                     join c in this.dbContext.Categories on p.CategoryID equals c.CategoryID
                     select new { Year=o.OrderDate.Value.Year,Category = c.CategoryName, Price = Math.Round(od.Quantity * (1 - od.Discount) * (int)od.UnitPrice, 2) };
            var q5 = (from t in q4.GroupBy(n =>new { n.Year,n.Category })
                      select new { Year=t.Key.Year,Category = t.Key.Category, Total = Math.Round(t.Sum(n => n.Price), 2) }).OrderByDescending(n => new { n.Year,n.Total });
            this.dataGridView3.DataSource=q5.ToList();
            // 那一年總銷售最好 ? 那一年總銷售最不好 ? 
            var q = from o in this.dbContext.Orders
                     from od in o.Order_Details
                     select new { Year = o.OrderDate.Value.Year, Price = Math.Round(od.Quantity * (1 - od.Discount) * (int)od.UnitPrice, 2) };
            var q1=(from t in q.GroupBy(n=>n.Year)
                   select new { Year = t.Key, Total = Math.Round(t.Sum(n => n.Price), 2) }).OrderByDescending(n=>n.Year);
            this.dataGridView1.DataSource = q1.ToList();
            this.chart1.DataSource = q1.ToList();
            this.chart1.Series[0].XValueMember = "Year";
            this.chart1.Series[0].YValueMembers = "Total";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            // 那一個月總銷售最好 ? 那一個月總銷售最不好 ?
            var q2 = from o in this.dbContext.Orders
                    from od in o.Order_Details
                    select new { Month = o.OrderDate.Value.Month, Price = Math.Round(od.Quantity * (1 - od.Discount) * (int)od.UnitPrice, 2) };
            var q3 = (from t in q2.GroupBy(n => n.Month)
                     select new { Month = t.Key, Total = Math.Round(t.Sum(n => n.Price), 2) }).OrderBy(n=>n.Month);
            this.dataGridView2.DataSource = q3.ToList();
            this.chart2.DataSource = q3.ToList();
            this.chart2.Series[0].XValueMember = "Month";
            this.chart2.Series[0].YValueMembers = "Total";
            this.chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            // 每年 總銷售分析 圖
            // 每月 總銷售分析 圖
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 找出學員 'bbb' 的成績	                          
            var q3 = from bbb in students_scores
                     where bbb.Name == "bbb"
                     select bbb;
            this.dataGridView1.DataSource = q3.ToList();
            label2.Text = "'bbb'的成績";
            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	
            var q4 = from nb in students_scores
                     where nb.Name != "bbb"
                     select nb;
            this.dataGridView2.DataSource = q4.ToList();
            label3.Text = "除了'bbb'學員的學員的所有成績";
            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績  
            var q5 = from abc2 in students_scores
                     where abc2.Name == "aaa" || abc2.Name == "bbb" || abc2.Name == "ccc"
                     select new { Name = abc2.Name, Chi = abc2.Chi, Math = abc2.Math };
            this.dataGridView3.DataSource = q5.ToList();
            label4.Text = "'aaa', 'bbb' 'ccc'學員國文數學兩科科目成績";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.label3.Text = "";
            this.label4.Text = "";
            this.dataGridView2.DataSource = null;
            this.dataGridView3.DataSource = null;
            // 數學不及格 ... 是誰 
            var q6 = from f in students_scores
                     where f.Math < 60
                     select f;
            this.dataGridView1.DataSource = q6.ToList();
            label2.Text = "數學不及格";
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
