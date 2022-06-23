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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AIS_Practice.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIS_Practice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private group_4_is_31Context _dbContext = new group_4_is_31Context(); //Контекст базы данных
        private string _currentTableUser; //Переменная для хранения имени текущей таблицы
        private string _currentTableStaff;
        private string _currentTableSupplies;
        private string _currentTableKatalog;

        public MainWindow()
        {
            new Avtorisation().ShowDialog();

            InitializeComponent();

            //Устанавливаем текущую таблицу для пользователей 
            _currentTableUser = "Пользователи";
            //Обновляем текущую таблицу
            RefreshTable(_currentTableUser);

            //Устанавливаем текущую таблицу для персонала
            _currentTableStaff = "Персонал";
            //Обновляем текущую таблицу
            RefreshTable(_currentTableStaff);

            //Устанавливаем текущую таблицу для поставщиков
            _currentTableSupplies = "Поставщики";
            //Обновляем текущую таблицу
            RefreshTable(_currentTableSupplies);

            //Устанавливаем текущую таблицу для поставщиков
            _currentTableKatalog = "Каталог товаров";
            //Обновляем текущую таблицу
            RefreshTable(_currentTableKatalog);

            //Устанавливаем текущую таблицу для ролей
            _currentTableKatalog = "Роли";
            //Обновляем текущую таблицу
            RefreshTable(_currentTableKatalog);
        }

        private void RefreshTable(string tableName) //Метод для обновления данных в таблице
        {
            switch (tableName) //Проверяем пришедшую переменную tableName
            {
                case "Пользователи": //В случае, если имя таблицы - пользователи
                    _dbContext.Users.Load(); //Загружаем данные таблицы из БД
                    /*Устанавливаем загруженные данные таблицы из БД как источник для  
                      * вывода в DataGrid. DataGrid не хранит данные, он их выводит */
                    usersDG.ItemsSource = _dbContext.Users.Local.ToObservableCollection();
                    break;
                case "Персонал":
                    _dbContext.staff.Load(); //Загружаем данные таблицы из БД
                    /*Устанавливаем загруженные данные таблицы из БД как источник для  
                      * вывода в DataGrid. DataGrid не хранит данные, он их выводит */
                    StaffDG.ItemsSource = _dbContext.staff.Local.ToObservableCollection();
                    break;
                case "Поставщики":
                    _dbContext.Supplies.Load(); //Загружаем данные таблицы из БД
                    /*Устанавливаем загруженные данные таблицы из БД как источник для  
                      * вывода в DataGrid. DataGrid не хранит данные, он их выводит */
                    SuppliesDG.ItemsSource = _dbContext.Supplies.Local.ToObservableCollection();
                    break;

                case "Каталог товаров":
                    _dbContext.Katalogs.Load(); //Загружаем данные таблицы из БД
                    /*Устанавливаем загруженные данные таблицы из БД как источник для  
                      * вывода в DataGrid. DataGrid не хранит данные, он их выводит */
                    KatalogDG.ItemsSource = _dbContext.Katalogs.Local.ToObservableCollection();
                    break;

            }
        }

        //В sender будет храниться DataGrid, Во втором - вся инф-ия о столбце и событии в целом
        private void UsersDG_AutoGeneratingColumn(Object sender, DataGridAutoGeneratingColumnEventArgs e) 
        {
            //Получаем имя столбца
            string headerName = e.Column.Header.ToString(); 

            //Проверяем имя столбца
            switch (headerName)
            {
                case "id":
                    //Меняем имя
                    e.Column.Header = "Уникальный идентификатор";
                    break;
                case "Login":
                    //Меняем имя
                    e.Column.Header = "Логин";
                    break;
                case "Role":
                    e.Column.Visibility = Visibility.Collapsed;

                    _dbContext.Roles.Load();

                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("Role");

                    DataGridComboBoxColumn col = new DataGridComboBoxColumn
                    {
                        Header = "Роль",
                        DisplayMemberPath = "Name",
                        SelectedValuePath = "Id",
                        ItemsSource = _dbContext.Roles.ToArray(),
                        SelectedValueBinding = binding
                    };

                    ((DataGrid)sender).Columns.Add(col);
                    break;
                case "Password":
                    e.Column.Header = "Пароль";
                    break;
                case "Address":
                    e.Column.Header = "Адрес";
                    break;
                case "PhoneNumber":
                    e.Column.Header = "Номер телефона";
                    break;
            }
        }

        private void SuppliesDG_AutoGeneratingColumn(Object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //Получаем имя столбца
            string headerName = e.Column.Header.ToString();

            //Проверяем имя столбца
            switch (headerName)
            {
                case "id":
                    //Меняем имя
                    e.Column.Header = "Уникальный идентификатор";
                    break;
                case "Name":
                    //Меняем имя
                    e.Column.Header = "Имя";
                    break;
                case "Address":
                    e.Column.Header = "Адрес";
                    break;
                case "Contact":
                    e.Column.Header = "Номер телефона";
                    break;
            }
        }

        private void StaffDG_AutoGeneratingColumn(Object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //Получаем имя столбца
            string headerName = e.Column.Header.ToString();

            //Проверяем имя столбца
            switch (headerName)
            {
                case "id":
                    //Меняем имя
                    e.Column.Header = "Уникальный идентификатор";
                    break;
                case "Name":
                    //Меняем имя
                    e.Column.Header = "Имя";
                    break;
                case "Surname":
                    //Меняем имя
                    e.Column.Header = "Фамилия";
                    break;
                case "Patronymic":
                    //Меняем имя
                    e.Column.Header = "Отчество";
                    break;
                case "Birthday":
                    //Меняем имя
                    e.Column.Header = "Дата рождения";
                    break;
                case "Contact":
                    //Меняем имя
                    e.Column.Header = "Контактные данные";
                    break;
                case "Address":
                    e.Column.Header = "Адрес";
                    break;
                case "Passport":
                    e.Column.Header = "Паспорт";
                    break;
            }
        }

        private void KatalogDG_AutoGeneratingColumn(Object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //Получаем имя столбца
            string headerName = e.Column.Header.ToString();

            //Проверяем имя столбца
            switch (headerName)
            {
                case "id":
                    //Меняем имя
                    e.Column.Header = "Уникальный идентификатор";
                    break;
                case "Name":
                    //Меняем имя
                    e.Column.Header = "Имя";
                    break;
                case "Type":
                    e.Column.Visibility = Visibility.Collapsed;

                    _dbContext.Types.Load();

                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("Type");

                    DataGridComboBoxColumn col = new DataGridComboBoxColumn
                    {
                        Header = "Тип",
                        DisplayMemberPath = "Name",
                        SelectedValuePath = "Id",
                        ItemsSource = _dbContext.Types.ToArray(),
                        SelectedValueBinding = binding
                    };

                    ((DataGrid)sender).Columns.Add(col);
                    break;
                case "ReleaseDate":
                    //Меняем имя
                    e.Column.Header = "Дата релиза";
                    break;
                case "Company":
                    //Меняем имя
                    e.Column.Header = "Компания";
                    break;
            }
        }

        private void SaveButtonUser_Click(object sender, RoutedEventArgs e)
        {
            _dbContext.SaveChanges();
        }

        private void DeleteButtonUser_Click(object sender, RoutedEventArgs e)
        {
            switch(_currentTableUser)
            {
                case "Пользователи":
                    _dbContext.Users.Local.Remove(usersDG.SelectedItem as User);
                    break;
            }
        }

        private void SaveButtonSupplies_Click(object sender, RoutedEventArgs e)
        {
            _dbContext.SaveChanges();
        }

        private void DeleteButtonSupplies_Click(object sender, RoutedEventArgs e)
        {
            switch (_currentTableSupplies)
            {
                case "Поставщики":
                    _dbContext.Supplies.Local.Remove(SuppliesDG.SelectedItem as Supply);
                    break;
            }
        }

        private void SaveButtonStaff_Click(object sender, RoutedEventArgs e)
        {
            _dbContext.SaveChanges();
        }

        private void DeleteButtonStaff_Click(object sender, RoutedEventArgs e)
        {
            switch (_currentTableStaff)
            {
                case "Персонал":
                    _dbContext.staff.Local.Remove(StaffDG.SelectedItem as staff);
                    break;
            }
        }



        private void SaveButtonKatalog_Click(object sender, RoutedEventArgs e)
        {
            _dbContext.SaveChanges();
        }

        private void DeleteButtonKatalog_Click(object sender, RoutedEventArgs e)
        {
            switch (_currentTableKatalog)
            {
                case "Каталог товаров":
                    _dbContext.Katalogs.Local.Remove(KatalogDG.SelectedItem as Katalog);
                    break;
            }
        }
    }

}
