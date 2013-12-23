using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace WindowsService.DBOperation
{
    public class DBOperation
    {

        // 数据库连接
        private static string connectionString = "Server=localhost;port=5432;User Id=postgres;Password=7165092054;Database=homework";
        Npgsql.NpgsqlConnection conn;

        /// 打开数据库
        public void open()
        {
            if (conn == null)
            {
                conn = new Npgsql.NpgsqlConnection(connectionString);
            }
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        /// 关闭数据库
        public void close()
        {
            if (conn != null)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// 查询返回多项记录
        public DataSet selectRecords(string CMDString)
        {
            DataSet ds = new DataSet();
            try
            {
                this.open();
                Npgsql.NpgsqlDataAdapter da = new Npgsql.NpgsqlDataAdapter(CMDString, conn);
                da.Fill(ds);
                this.close();
            }
            catch (Exception e)
            {
                //ds.Tables[0].Rows.Add(e.ToString());
            }
            return ds;
        }

   
        /// 修改/插入/删除信息
        public int OperateRecord(string CMDString)
        {
            int i;
            this.open();
            Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(CMDString, conn);
            i = cmd.ExecuteNonQuery();
            this.close();
            return i;
        }
    }
}