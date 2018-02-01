using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace LvDu.dboUtils
{
    public class DboUtils
    {
        SqlConnection conn = null;

        /// 构造函数,连接数据库，数据库连接字符在web.Config文件的AppSettings下的conStr
        public DboUtils()
        {
            if (conn == null)
            {   
                //判断连接是否为空
                String conString = "server=.;database=test;uid=sa;pwd=sa";
                conn = new SqlConnection(conString);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();//打开数据库连接
                }
            }
        }

        /// 从数据库中查询数据的,返回为DataSet
        public DataSet query(string sql)
        {
            DataSet ds = new DataSet();//DataSet是表的集合
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);//从数据库中查询
            da.Fill(ds);//将数据填充到DataSet
            connClose();//关闭连接
            return ds;//返回结果
        }

        /// 更新数据库
        public int update(string sql)
        {
            SqlCommand oc = new SqlCommand();//表示要对数据源执行的SQL语句或存储过程
            oc.CommandText = sql;//设置命令的文本
            oc.CommandType = CommandType.Text;//设置命令的类型
            oc.Connection = conn;//设置命令的连接
            int x = oc.ExecuteNonQuery();//执行SQL语句
            connClose();//关闭连接
            return x;  //返回一个影响行数
        }
       
        /// 关闭数据库连接
        public void connClose()
        {
            if (conn.State == ConnectionState.Open)
            {
                //判断数据库的连接状态，如果状态是打开的话就将它关闭
                conn.Close();
            }
        }

        /// 向数据库中插入数据
        /// 
        public int insert(String sql)
        {
            int raw = 0;
            SqlCommand com = conn.CreateCommand();
            com.CommandText = sql;
            com.CommandType = CommandType.Text;
            com.ExecuteNonQuery();
            return raw;
        }

    }

}
