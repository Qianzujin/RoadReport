using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Wpf.Model;

namespace Wpf.Dao
{
    public class UserDao
    {
        List<User> userList = new List<User>();
        SqliteDbHelper db;//声明连接对象

        /// <summary>
        /// 建表语句
        /// </summary>
        private void CreateTableUserInfo()
        {
            db.CreateTable("UserInfo",
                new string[] {
               "Idx" ,"UserName", "PassWord",
                    "Type", "Picture"
            },
            new string[] {"INTEGER PRIMARY KEY AUTOINCREMENT","TEXT",
                "TEXT","TEXT","TEXT"
            });
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserDao()
        {
            CreateClient();
            //CreateTableUserInfo();
        }

        /// <summary>
        /// 客户端连接串进行连接
        /// </summary>
        public void CreateClient()
        {
            db = new SqliteDbHelper("Data Source=./sqlite.db;Version=3");
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<User> SelectAll()
        {
            userList.Clear();
            var reader = db.ReadFullTable("UserInfo");
            while (reader.Read())
            {
                userList.Add(new User
                {
                    Index = Convert.ToInt16(reader["Idx"]),
                    UserName = Convert.ToString(reader["UserName"]),
                    PassWord = Convert.ToString(reader["PassWord"]),
                    Type = Convert.ToString(reader["Type"]),
                    Picture = new BitmapImage(new Uri(Convert.ToString(reader["Picture"]), UriKind.RelativeOrAbsolute))
                });
            }
            return userList;
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="Index"></param>
        public void Delete(int Index)
        {
            //删除用户信息
            db.Delete("UserInfo", new string[] { "Idx" }, new string[] { Index.ToString() });
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="tr"></param>
        public void Update(User user)
        {
            //先修改测试路线基础信息
            db.UpdateInto("UserInfo", new string[] {
               "Idx" ,"UserName", "PassWord",
                    "Type","Picture"
            }, new string[] {
                    user.Index.ToString(),
                 user.UserName.ToString(),
                    user.PassWord.ToString(),
                    user.Type.ToString(),
                    user.Picture.ToString(),

                }, "Idx", user.Index.ToString()
                );

        }

        /// <summary>
        /// 增加用户信息
        /// </summary>
        /// <param name="tr"></param>
        public void Insert(User user)
        {
            //增加用户信息
            db.InsertInto("UserInfo", new string[]
                { "UserName", "PassWord",
                    "Type", "Picture"},
                new string[] {
                user.UserName.ToString(),
            user.PassWord.ToString(),
            user.Type.ToString(),
            user.Picture.ToString(),
                });
        }

        public bool VertifyLogin(string userName,string passWord) 
        {
            string str = "select count(*) from UserInfo where UserName = '" + userName + "' and PassWord =" + passWord;

            if (db.ExecuteScalar(str) != "0")
            {
                return true;
            }
            else {
                return false;
            }
           
        }

    }
}
