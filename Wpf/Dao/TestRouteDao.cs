using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Wpf.Model;

namespace Wpf.Dao
{
    class TestRouteDao
    {
        List<TestRoute> testRouteList = new List<TestRoute>();
        // List<PavementType> pavementTypeList = new List<PavementType>();
        List<TestRouteBase> testRouteBaseList = new List<TestRouteBase>();

        SqliteDbHelper db;//声明连接对象

        //TestRouteBaseInfo建表语句
        private void CreateTableTestRouteBaseInfo()
        {
            db.CreateTable("TestRouteBaseInfo",
                new string[] {
               "Idx" ,"TestRoutes", "LineMileage",
                    "Material", "Time", "Picture"
            },
            new string[] {"INTEGER PRIMARY KEY AUTOINCREMENT","TEXT",
                "TEXT","TEXT","TEXT","TEXT"
            });
        }

        //PavementTypeInfo建表语句
        private void CreateTablePavementTypeInfo()
        {
            db.CreateTable("PavementTypeInfo",
                new string[] {
               "Id","Idx","Name","Length", "Percent","Picture"
            },
            new string[] {"INTEGER PRIMARY KEY AUTOINCREMENT","INTEGER","TEXT","TEXT","TEXT","TEXT"
            });
        }

        //客户端连接串进行连接
        public void CreateClient()
        {
            db = new SqliteDbHelper("Data Source=./sqlite.db;Version=3");
        }

        //构造函数
        public TestRouteDao()
        {
            CreateClient();
            //CreateTableTestRouteBaseInfo();
            //CreateTablePavementTypeInfo();
        }

        //查询Idx对应的路面类型信息
        private List<PavementType> SelectAllPavementTypeInfo(int Index)
        {
            List<PavementType> pavementTypeList = new List<PavementType>();
            //var reader = db.ReadFullTable("PavementTypeInfo");
            //注意语句中的空格
            var reader = db.ExecuteQuery("Select * From" + " " + "PavementTypeInfo" + " " + "where Idx=" + Index.ToString());
            while (reader.Read())
            {
                pavementTypeList.Add(new PavementType
                {
                    Id = Convert.ToInt16(reader["Id"]),
                    Index = Convert.ToInt16(reader["Idx"]),
                    Name = Convert.ToString(reader["Name"]),
                    Length = Convert.ToString(reader["Length"]),
                    Percent = Convert.ToString(reader["Percent"]),
                    Picture = new BitmapImage(new Uri(Convert.ToString(reader["Picture"]), UriKind.RelativeOrAbsolute))
                });
            }
            return pavementTypeList;
        }

        //查询测试路线基础信息
        private void SelectAllRouteBase()
        {
            testRouteBaseList.Clear();
            var reader = db.ReadFullTable("TestRouteBaseInfo");
            while (reader.Read())
            {
                testRouteBaseList.Add(new TestRouteBase
                {
                    Index = Convert.ToInt16(reader["Idx"]),
                    TestRoutes = Convert.ToString(reader["TestRoutes"]),
                    LineMileage = Convert.ToString(reader["LineMileage"]),
                    Material = Convert.ToString(reader["Material"]),
                    Time = Convert.ToString(reader["Time"]),
                    Picture = new BitmapImage(new Uri(Convert.ToString(reader["Picture"]), UriKind.RelativeOrAbsolute))
                });
            }
        }

        //查询所有测试路线信息
        public List<TestRoute> SelectAll()
        {
            //查询所有基础路面信息
            SelectAllRouteBase();

            for (int i = 0; i < testRouteBaseList.Count(); i++)
            {
                testRouteList.Add(new TestRoute { PavementTypeInfo = SelectAllPavementTypeInfo(testRouteBaseList[i].Index), TestRouteBase = testRouteBaseList[i] });
            }
            return testRouteList;
        }

        //删除测试路线信息
        public void Delete(int Index)
        {
            //删除测试路线基础信息
            db.Delete("TestRouteBaseInfo", new string[] { "Idx" }, new string[] { Index.ToString() });
            //删除测试路线基础信息对应的测试路线路面类型信息
            db.Delete("PavementTypeInfo", new string[] { "Idx" }, new string[] { Index.ToString() });
        }

        //修改测试路线信息
        public void Update(TestRoute tr)
        {
            //先修改测试路线基础信息
            db.UpdateInto("TestRouteBaseInfo", new string[] {
               "Idx" ,"TestRoutes", "LineMileage",
                    "Material", "Time", "Picture"
            }, new string[] {
                    tr.TestRouteBase.Index.ToString(),
                    tr.TestRouteBase.TestRoutes.ToString(),
                    tr.TestRouteBase.LineMileage.ToString(),
                    tr.TestRouteBase.Material.ToString(),
                    tr.TestRouteBase.Time.ToString(),
                    tr.TestRouteBase.Picture.ToString(),
                }, "Idx", tr.TestRouteBase.Index.ToString()
                );
            //再修改测试路线对应的所有路面类型信息
            for (int i = 0; i < tr.PavementTypeInfo.Count(); i++)
            {
                db.Update("PavementTypeInfo", new string[]
                { "Idx", "Id", "Name", "Length", "Percent", "Picture" },
                    new string[] {
                        tr.PavementTypeInfo[i].Index.ToString(),
                        tr.PavementTypeInfo[i].Id.ToString(),
                        tr.PavementTypeInfo[i].Name.ToString(),
                        tr.PavementTypeInfo[i].Length.ToString(),
                        tr.PavementTypeInfo[i].Percent.ToString(),
                        tr.PavementTypeInfo[i].Picture.ToString()
                    },
                    new string[] { "Idx", "Id" },
                    new string[] {tr.TestRouteBase.Index.ToString(),
                    tr.PavementTypeInfo[i].Id.ToString()
                    });
                //如果路面类型的ID和IDX都是0说明该路面类型不存在，则是新增路面类型，进行添加
                if (tr.PavementTypeInfo[i].Index == tr.PavementTypeInfo[i].Id && tr.PavementTypeInfo[i].Id == 0)
                {
                    db.InsertInto("PavementTypeInfo", new string[]
              {  "Idx", "Name", "Length", "Percent", "Picture" },
              new string[] {
                         tr.TestRouteBase.Index.ToString(),
                        tr.PavementTypeInfo[i].Name.ToString(),
                        tr.PavementTypeInfo[i].Length.ToString(),
                        tr.PavementTypeInfo[i].Percent.ToString(),
                        tr.PavementTypeInfo[i].Picture.ToString()
              });
                }
            }
        }

        //增加测试路线信息
        public void Insert(TestRoute tr)
        {
            //增加测试路线基础信息Idx自增
            db.InsertInto("TestRouteBaseInfo", new string[]
                { "TestRoutes", "LineMileage",
                    "Material", "Time", "Picture"},
                new string[] {
                    tr.TestRouteBase.TestRoutes.ToString(),
                    tr.TestRouteBase.LineMileage.ToString(),
                    tr.TestRouteBase.Material.ToString(),
                    tr.TestRouteBase.Time.ToString(),
                    tr.TestRouteBase.Picture.ToString(),
                });

            string lastIdx = db.ExecuteQueryLastIdx();

            //增加测试路线基础信息对应的路面类型信息,Id自增
            for (int i = 0; i < tr.PavementTypeInfo.Count(); i++)
            {
                db.InsertInto("PavementTypeInfo", new string[]
                {  "Idx", "Name", "Length", "Percent", "Picture" }
                , new string[] {
                        lastIdx,
                        tr.PavementTypeInfo[i].Name.ToString(),
                        tr.PavementTypeInfo[i].Length.ToString(),
                        tr.PavementTypeInfo[i].Percent.ToString(),
                        tr.PavementTypeInfo[i].Picture.ToString()
                });
            }
        }



    }
}
