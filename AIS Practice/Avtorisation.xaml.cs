using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AIS_Practice.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIS_Practice
{
    /// <summary>
    /// Interaction logic for Avtorisation.xaml
    /// </summary>
    public partial class Avtorisation : Window
    {
        private group_4_is_31Context _dbContext = new group_4_is_31Context(); //Контекст БД
        private bool _isLogin = false; //Залогинились ли
        public static User CurrentUser; //Текущий пользователь

        public Avtorisation()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try //Попытка
            {
                User user = _dbContext.Users.Where(
                    (usr) => usr.Login == loginTextBox.Text && usr.Password == passwordTextBox.Text
                    ).Single();
                MessageBox.Show($"Привет, {user.Login}!", "Успешно!");

                _isLogin = true;
                CurrentUser = user;
                Close(); //Закрываем окно
            }
            catch //Обработка исключения
            {
                MessageBox.Show("Ошибка!", "Неверный логин или пароль!");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (!_isLogin)
                App.Current.Shutdown(); //Завершение работы приложения
        }

        
    }
}
