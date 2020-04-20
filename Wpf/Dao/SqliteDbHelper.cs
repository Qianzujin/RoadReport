using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Dao
{
    class SqliteDbHelper
    {
        /// <summary>
        /// 声明一个连接对象
        /// </summary>
        private SQLiteConnection dbConnection;
        /// <summary>
        /// 声明一个操作数据库命令
        /// </summary>
        private SQLiteCommand dbCommand;
        /// <summary>
        /// 声明一个读取结果集的一个或多个结果流
        /// </summary>
        private SQLiteDataReader reader;
        /// <summary>
        /// 数据库的连接字符串，用于建立与特定数据源的连接
        /// </summary>
        /// <param name="connectionString">数据库的连接字符串，用于建立与特定数据源的连接</param>
        public SqliteDbHelper(string connectionString)
        {
            OpenDB(connectionString);
            Console.WriteLine(connectionString);
        }

        public void OpenDB(string connectionString)
        {
            try
            {
                dbConnection = new SQLiteConnection(connectionString);
                dbConnection.Open();
                Console.WriteLine("Connected to db");
            }
            catch (Exception e)
            {
                string temp1 = e.ToString();
                Console.WriteLine(temp1);
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void CloseSqlConnection()
        {
            if (dbCommand != null)
            {
                dbCommand.Dispose();
            }
            dbCommand = null;
            if (reader != null)
            {
                reader.Dispose();
            }
            reader = null;
            if (dbConnection != null)
            {
                dbConnection.Close();
            }
            dbConnection = null;
            Console.WriteLine("Disconnected from db.");
        }

        /// <summary>
        /// 执行查询sqlite语句操作
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public SQLiteDataReader ExecuteQuery(string sqlQuery)
        {
            dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            reader = dbCommand.ExecuteReader();
            return reader;
        }

        public string ExecuteQueryLastIdx()  
        {
            string sqlQuery;
            sqlQuery = "select last_insert_rowid() newid";
            dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            var i = dbCommand.ExecuteScalar();
            return i.ToString();
        }

        /// <summary>
        /// 查询该表所有数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public SQLiteDataReader ReadFullTable(string tableName)
        {
            string query = "SELECT * FROM " + tableName;
            return ExecuteQuery(query);
        }

        /// <summary>
        /// 动态添加表字段到指定表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="values">字段集合</param>
        /// <returns></returns>
        public SQLiteDataReader InsertInto(string tableName, string[] col,string[] values)
        {
            string query = "INSERT INTO " + tableName+" (" + col[0];
            for (int i = 1; i < col.Length; ++i)
            {
                query += ", " + col[i];
            }
            query += ")"

            +" VALUES ('" + values[0]+"'";
            for (int i = 1; i < values.Length; ++i)
            {
                query += ", '" + values[i]+"'";
            }
            query += ")";
            return ExecuteQuery(query);
           
        }     

        /// <summary>
        /// 动态更新表结构
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="cols">字段集</param>
        /// <param name="colsvalues">对于集合值</param>
        /// <param name="selectkey">要查询的字段</param>
        /// <param name="selectvalue">要查询的字段值</param>
        /// <returns></returns>
        public SQLiteDataReader UpdateInto(string tableName, string[] cols,
          string[] colsvalues, string selectkey, string selectvalue)
        {
            string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];
            for (int i = 1; i < colsvalues.Length; ++i)
            {
                query += ", " + cols[i] + " =" + "'" + colsvalues[i] + "'";
            }
            query += " WHERE " + selectkey + " = " + selectvalue + " ";
            return ExecuteQuery(query);
        }

        /// <summary>
        /// 动态更新表结构
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="cols">字段集</param>
        /// <param name="colsvalues">对于集合值</param>
        /// <param name="selectkey">要查询的字段</param>
        /// <param name="selectvalue">要查询的字段值</param>
        /// <returns></returns>
        public SQLiteDataReader Update(string tableName, string[] cols, 
          string[] colsvalues, string[] selectkey, string[] selectvalue)
        {
            string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];
            for (int i = 1; i < colsvalues.Length; ++i)
            {
                query += ", " + cols[i] + " =" + "'" + colsvalues[i] + "'";
            }
            query += " WHERE " + selectkey[0] + " = " + selectvalue[0] + " ";

            for (int i = 1; i < selectkey.Length; ++i)
            {
                query += " and " + selectkey[i] + " = " + selectvalue[i];
            }
            return ExecuteQuery(query);
        }

        /// <summary>
        /// 动态删除指定表字段数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="cols">字段</param>
        /// <param name="colsvalues">字段值</param>
        /// <returns></returns>
        public SQLiteDataReader Delete(string tableName, string[] cols, string[] colsvalues)
        {
            string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];
            for (int i = 1; i < colsvalues.Length; ++i)
            {
                query += " or " + cols + " = " + colsvalues;
            }
            Console.WriteLine(query);
            return ExecuteQuery(query);
        }

        /// <summary>
        /// 动态添加数据到指定表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="cols">字段</param>
        /// <param name="values">值</param>
        /// <returns></returns>
        public SQLiteDataReader InsertIntoSpecific(string tableName, string[] cols,
         string[] values)
        {
            if (cols.Length != values.Length)
            {
                throw new SQLiteException("columns.Length != values.Length");
            }
            string query = "INSERT INTO " + tableName + "(" + cols[0];
            for (int i = 1; i < cols.Length; ++i)
            {
                query += ", " + cols;
            }
            query += ") VALUES (" + values[0];
            for (int i = 1; i < values.Length; ++i)
            {
                query += ", " + values;
            }
            query += ")";
            return ExecuteQuery(query);
        }
        /// <summary>
        /// 动态删除表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public SQLiteDataReader DeleteContents(string tableName)
        {
            string query = "DELETE FROM " + tableName;
            return ExecuteQuery(query);
        }

        /// <summary>
        /// 动态创建表
        /// </summary>
        /// <param name="name">表名</param>
        /// <param name="col">字段</param>
        /// <param name="colType">类型</param>
        /// <returns></returns>
        public SQLiteDataReader CreateTable(string name, string[] col, string[] colType)
        {
            if (col.Length != colType.Length)
            {
                throw new SQLiteException("columns.Length != colType.Length");
            }
            string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0];
            for (int i = 1; i < col.Length; ++i)
            {
                query += ", " + col[i] + " " + colType[i];
            }
            query += ")";
            Console.WriteLine(query);
            return ExecuteQuery(query);
        }

        /// <summary>
        /// 根据查询条件 动态查询数据信息
        /// </summary>
        /// <param name="tableName">表</param>
        /// <param name="items">查询数据集合</param>
        /// <param name="col">字段</param>
        /// <param name="operation">操作</param>
        /// <param name="values">值</param>
        /// <returns></returns>
        public SQLiteDataReader SelectWhere(string tableName, string[] items, string[] col, string[] operation, string[] values)
        {
            if (col.Length != operation.Length || operation.Length != values.Length)
            {
                throw new SQLiteException("col.Length != operation.Length != values.Length");
            }
            string query = "SELECT " + items[0];
            for (int i = 1; i < items.Length; ++i)
            {
                query += ", " + items;
            }
            query += " FROM " + tableName + " WHERE " + col[0] + operation[0] + "'" + values[0] + "' ";
            for (int i = 1; i < col.Length; ++i)
            {
                query += " AND " + col + operation + "'" + values[0] + "' ";
            }
            return ExecuteQuery(query);
        }
    }
}
