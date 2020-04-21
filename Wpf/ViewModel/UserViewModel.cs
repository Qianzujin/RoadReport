using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Dao;
using Wpf.Model;

namespace Wpf.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        private ObservableCollection<User> userView;
        UserDao userDao;

        public ObservableCollection<User> UserView
        {
            get { return userView; }
            set { value = userView; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 构造函数 查询所有用户信息
        /// </summary>
        public UserViewModel()
        {
            userView = new ObservableCollection<User>();
            userDao = new UserDao();

            DeleteCommand = new RelayCommand<int>(t => Delete(t));
            UpdateCommand = new RelayCommand<User>(t => Update(t));
            InsertCommand = new RelayCommand<User>(t => Insert(t));

            SelectAll();
        }

        #region  
        // 操作命令  
        public RelayCommand<int> DeleteCommand { get; set; }
        public RelayCommand<User> UpdateCommand { get; set; }
        public RelayCommand<User> InsertCommand { get; set; }
        #endregion

        /// <summary>
        /// 查询所有用户信息
        /// </summary>
        public void SelectAll()
        {
            userView.Clear();
            foreach (var item in userDao.SelectAll())
            {
                userView.Add(item);
            }
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="index"></param>
        public void Delete(int index)
        {
            userDao.Delete(index);
            SelectAll();
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        public void Update(User user)
        {
            userDao.Update(user);
            SelectAll();
        }

        /// <summary>
        /// 插入用户信息
        /// </summary>
        /// <param name="user"></param>
        public void Insert(User user)
        {
            userDao.Insert(user);
            SelectAll();
        }


    }
}
