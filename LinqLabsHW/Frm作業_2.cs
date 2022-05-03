using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            
            InitializeComponent();
            this.productPhotoTableAdapter1.Fill(this.awDataSet11.ProductPhoto);
            var q = from o in this.awDataSet11.ProductPhoto
                    group o by o.ModifiedDate.Year into newGroup
                    orderby newGroup.Key
                    select newGroup;
            foreach (var yearGroup in q)
            {
                comboBox3.Items.Add($"{yearGroup.Key}");
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.awDataSet11.ProductPhoto;
            lblMaster.Text = $"共{awDataSet11.Tables[0].Rows.Count}種產品";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox3.Text))
            {
                MessageBox.Show("請選擇年分");
            }
            else
            {
                //var q = from o in this.awDataSet11.ProductPhoto
                //        where o.ModifiedDate.Year.ToString() == comboBox3.Text
                //        select o;
                var q = this.awDataSet11.ProductPhoto.Where(o => o.ModifiedDate.Year.ToString() == comboBox3.Text);
                this.dataGridView1.DataSource = q.ToList();

                lblMaster.Text = $"{comboBox3.Text}年,共{q.Count()}種產品";
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MemoryStream ms;
            int productphotoID = (int)this.dataGridView1.CurrentRow.Cells["ProductPhotoID"].Value;
            var q = from o in this.awDataSet11.ProductPhoto
                    where o.ProductPhotoID==productphotoID
                    select o.LargePhoto;
            foreach(byte[] b in q)
            {
                ms = new MemoryStream(b);
                pictureBox1.Image = new Bitmap(Image.FromStream(ms));
            }
        }     

        private void button10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("請選擇季別");
            }
            else
            {
                var q = from o in this.awDataSet11.ProductPhoto
                        where Math.Ceiling((double)o.ModifiedDate.Month / 3) == season
                        select o;
                this.dataGridView1.DataSource = q.ToList();

                lblMaster.Text = $"{comboBox2.Text},共{q.Count()}種產品";
            }
           
        }
        double season = 0;
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.Text)
            {
                case "第一季":
                    season = 1;
                    break;
                case "第二季":
                    season = 2;
                    break;
                case "第三季":
                    season = 3;
                    break;
                case "第四季":
                    season = 4;
                    break;
            }
        }
        bool Falg = false;
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Falg = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(!Falg || dateTimePicker1.Value> dateTimePicker2.Value)
            {
                MessageBox.Show("請選擇正確起訖日期");
            }
            else
            {
                var q = from o in this.awDataSet11.ProductPhoto
                        where o.ModifiedDate >= dateTimePicker1.Value && o.ModifiedDate <= dateTimePicker2.Value /*DateTime.Parse(endTime)*/
                        select o;
                this.dataGridView1.DataSource = q.ToList();

                lblMaster.Text = $"{dateTimePicker1.Value.Date.ToString("yyyy-MM-dd")}至{dateTimePicker2.Value.Date.ToString("yyyy-MM-dd")},共{q.Count()}種產品";
            }       
        }
    }
}
