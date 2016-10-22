using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace ClassTable
{
   
    public partial class Form1 : Form
    {
       
        public DataRow Row1;
        private static Random ran = new Random(GetRandomSeed());
    
        public Form1()
        {
            InitializeComponent();
           // cleardata();
          // InitialData();         
            ShowData();

        }

        static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

     

        public  String getRandomPlace()
        {
           
            int RandKey = ran.Next(1,5);
            if (RandKey == 1)
            {
                
                return "仙1-203";
            }
            if (RandKey == 2)
            {

                return "仙2-505";
            }
            if (RandKey == 3)
            {

                return "逸B-202";
            }
            if (RandKey == 4)
            {

                return "教2-102";
            }
            if (RandKey == 5)
            {

                return "院1-205";
            }
            return null;
        }

        public List<course> getList()
        {
            List<course> list = new List<course>();
            for(int i = 1; i <= 5; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    int start=2*j + 1;
                    int end = 2 * j + 2;
                    String time = "周" + i.ToString()+" "+start.ToString() + "-"+end.ToString()+"节";
                    course item = getRandomClass();
                    String place = getRandomPlace();
                    item.setTime(time);
                    item.setPlace(place);
                    list.Add(item);
                }
            }

            return list;
        }

        public course getRandomClass()
        {
            
            course course;
            int RandKey = ran.Next(0,6);
            if (RandKey == 0)
            {
                course = new course("大学数学", "", "", "吴明辉", "5", "必修");
                return course;
            }
            if (RandKey == 1)
            {
                course = new course("C++程序设计", "", "", "谭浩强", "3", "选修");
                return course;
            }
            if (RandKey == 2)
            {
                course = new course("需求工程", "", "", "丁二玉", "3", "选修");
                return course;
            }
            if (RandKey == 3)
            {
                course = new course("大学英语", "", "", "陈伟", "4", "必修");
                return course;
            }
            if (RandKey == 4)
            {
                course = new course("Web工程", "", "", "刘海涛", "3", "选修");
                return course;
            }
            if (RandKey == 5)
            {
                course = new course("马克思主义原理", "", "", "刘明", "3", "必修");
                return course;
            }
            if (RandKey == 6)
            {
                course = new course("数据库设计", "", "", "刘嘉", "3", "选修");
                return course;
            }
            return null;
        }

        public void cleardata()
        {
            SqlConnection conn = new SqlConnection("server=(localdb)\\MSSQLLocalDB;Trusted_Connection=yes;database=test");
            conn.Open();
           string cmd = "delete  from classtable";
            SqlCommand comm = new SqlCommand(cmd, conn);
           comm.ExecuteNonQuery();
            conn.Close();
        }

        private void ShowData()
        {
            SqlConnection conn = new SqlConnection("server=(localdb)\\MSSQLLocalDB;Trusted_Connection=yes;database=test");
            conn.Open();
            SqlDataAdapter mySqlDataAdapter =
 new SqlDataAdapter(
 "select * from classtable", conn);
            DataSet myDataSet = new DataSet();
            List<course> list = new List<course>();
            mySqlDataAdapter.Fill(myDataSet, "classtable");
            foreach (DataRow myDataRow in
   myDataSet.Tables["classtable"].Rows)
            {
                String name = myDataRow["name"].ToString();
                String time = myDataRow["time"].ToString();
                String place = myDataRow["place"].ToString();
                String teacher = myDataRow["teacher"].ToString();
                String points = myDataRow["points"].ToString();
                String type = myDataRow["type"].ToString();
                course single = new course(name, time, place, teacher, points, type);
                list.Add(single);


            }

            int index;
            for (int i = 0; i < list.Count; i++)//在列表中展示数据
            {
                index = this.ShowView.Rows.Add();
                this.ShowView.Rows[index].Cells[0].Value = list[i].getTime();
                this.ShowView.Rows[index].Cells[1].Value = list[i].getName();
                this.ShowView.Rows[index].Cells[2].Value = list[i].getPlace();
                this.ShowView.Rows[index].Cells[3].Value = list[i].getTeacher();
                this.ShowView.Rows[index].Cells[4].Value = list[i].getPoints();
                this.ShowView.Rows[index].Cells[5].Value = list[i].getType();
                this.ShowView.Rows[index].Cells[6].Value = "修改";
                this.ShowView.Rows[index].Cells[7].Value = "删除";
            }
            conn.Close();

        



    }

    private void InitialData()
        {
            SqlConnection conn = new SqlConnection("server=(localdb)\\MSSQLLocalDB;Trusted_Connection=yes;database=test");
            conn.Open();
            SqlDataAdapter mySqlDataAdapter =
 new SqlDataAdapter(
 "select * from classtable", conn);
            DataSet myDataSet = new DataSet();
            mySqlDataAdapter.Fill(myDataSet, "classtable");
            myDataSet.Tables["classtable"].Clear();
            List<course> list = getList();
            SqlCommandBuilder mySqlCommandBuilder = new SqlCommandBuilder(mySqlDataAdapter);
            mySqlDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            for (int i = 0; i < list.Count; i++)
            {
                DataRow Row1 = myDataSet.Tables["classtable"].NewRow();
                Row1["name"] = list[i].getName();
                Row1["time"] = list[i].getTime();
                Row1["place"] = list[i].getPlace();
                Row1["teacher"] = list[i].getTeacher();
                Row1["points"] = list[i].getPoints();
                Row1["type"] = list[i].getType();
                myDataSet.Tables["classtable"].Rows.Add(Row1);
                mySqlDataAdapter.Update(myDataSet, "classtable");

            }
            ShowData();

          
            conn.Close();
            
        }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == ShowView.Columns[6].Index && ShowView.Rows[e.RowIndex].Cells[6].Value.ToString() == "修改")
            {
                SqlConnection conn = new SqlConnection("server=(localdb)\\MSSQLLocalDB;Trusted_Connection=yes;database=test");
                String time= ShowView.Rows[e.RowIndex].Cells[0].Value.ToString();
                String name = ShowView.Rows[e.RowIndex].Cells[1].Value.ToString();
                String place = ShowView.Rows[e.RowIndex].Cells[2].Value.ToString();
                String teacher = ShowView.Rows[e.RowIndex].Cells[3].Value.ToString();
                String points = ShowView.Rows[e.RowIndex].Cells[4].Value.ToString();
                String type = ShowView.Rows[e.RowIndex].Cells[5].Value.ToString();
               
                conn.Open();
                string sql = "update classtable set name =N'" + name + "',place=N'" + place+ "',teacher=N'"+teacher+ "',points=N'"+points+ "',type=N'"+type + " '  where time = N'" + time+"'";
                SqlCommand comm = new SqlCommand(sql, conn);
               int i= comm.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("修改成功!");
                }else
                {
                    MessageBox.Show("修改失败!");
                }
                 conn.Close();
                ShowView.Rows.Clear();
                ShowData();
 }

            if (e.ColumnIndex == ShowView.Columns[7].Index && ShowView.Rows[e.RowIndex].Cells[7].Value.ToString() == "删除")
            {
                SqlConnection conn = new SqlConnection("server=(localdb)\\MSSQLLocalDB;Trusted_Connection=yes;database=test");
                String time = ShowView.Rows[e.RowIndex].Cells[0].Value.ToString();
               conn.Open();
                string sql = "update classtable set name =N'" + "无课" + "',place=N'" + "/" + "',teacher=N'" + "/" + "',points=N'" +0+ "',type=N'" + "/" + " '  where time = N'" + time + "'";
                SqlCommand comm = new SqlCommand(sql, conn);
                int i = comm.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("删除成功!");
                }
                else
                {
                    MessageBox.Show("删除失败!");
                }
                conn.Close();
                ShowView.Rows.Clear();
                ShowData();
            }

        }
    }
}
