using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConnectMysql
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Enter the student ID");
            //string stu_no = Console.ReadLine();
            //Console.WriteLine();
            //Console.ReadLine();
            String connetStr = "server=169.254.251.139;port=3306;user=root;password=AIHUO0926xz; database=world;";
            // server=127.0.0.1/localhost 代表本机，端口号port默认是3306可以不写
            MySqlConnection conn = new MySqlConnection(connetStr);
            conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
            Console.WriteLine("已经建立连接");

            string sql = "SELECT * FROM `country` WHERE Continent='Asia'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象

            while (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
            {

                Console.WriteLine(reader.GetString("Name"));//"userid"是数据库对应的列名，推荐这种方式
            }

            //string sql = "select count(*) from user";
            //MySqlCommand cmd = new MySqlCommand(sql, conn);
            //Object result = cmd.ExecuteScalar();//执行查询，并返回查询结果集中第一行的第一列。所有其他的列和行将被忽略。select语句无记录返回时，ExecuteScalar()返回NULL值
            //if (result != null)
            //{
            //    int count = int.Parse(result.ToString());
            //}


            Console.ReadLine();
            //try
            //{
            //    conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
            //    Console.WriteLine("已经建立连接");
            //    //在这里使用代码对数据库进行增删查改
            //}
            //catch (MySqlException ex)
            //{
            //    switch (ex.Number)
            //    {
            //        case 0:
            //            Console.WriteLine("Cannot connect to server.  Contact administrator");
            //            break;
            //        case 1045:
            //            Console.WriteLine("Invalid username/password, please try again");
            //            break;
            //    }
            //}
            //finally
            //{
            //    conn.Close();
            //}

        }
    }
}
