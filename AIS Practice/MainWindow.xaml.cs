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
using Microsoft.Win32;
using System.IO;
using System.Collections.ObjectModel;

namespace AIS_Practice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private group_4_is_31Context _dbContext = new group_4_is_31Context(); //Контекст базы данных
        private string _currentTable; //Объявление переменной для текущей таблицы

        //Разграничение пользователей
        private void Checkuser()
        {
            switch(Avtorisation.CurrentUser.Role)
            {
                case 1: //Если администратор
                    break;
                case 2: //Если кассир (cashier)
                    //Отключаем видимость для вкладок - Пользователи; Поставщики; Работники; 
                    usersTab.Visibility = Visibility.Collapsed;
                    suppliesTab.Visibility = Visibility.Collapsed;
                    staffTab.Visibility = Visibility.Collapsed;
                    break;
                case 3: //Если бухгалтер (Accountant)
                    usersTab.Visibility = Visibility.Collapsed;
                    break;
                case 4: //Если руководитель (Supervisor)
                    break;
            }
        }
        public MainWindow()
        {
            InitializeComponent();

            new Avtorisation().ShowDialog(); //Открытие диалога для авторизации

            Checkuser(); //Вызов метода для разграничения ролей

            //Устанавливаем текущую таблицу для пользователей 
            _currentTable = "Пользователи";
            //Обновляем текущую таблицу
            RefreshTable(_currentTable);
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
                case "Продажи":
                    _dbContext.Sales.Load(); //Загружаем данные таблицы из БД
                    /*Устанавливаем загруженные данные таблицы из БД как источник для  
                      * вывода в DataGrid. DataGrid не хранит данные, он их выводит */
                    saleDG.ItemsSource = _dbContext.Sales.Local.ToObservableCollection();
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
                    //Скрываем столбец
                    e.Column.Visibility = Visibility.Collapsed;

                    _dbContext.Roles.Load();

                    Binding binding = new Binding(); //Создаем новый биндинг для подвязки роли
                    binding.Path = new PropertyPath("Role"); //В путь подвязки указываем получение Role

                    /*Создаем новый столбец типа ComboBox для
                     * возможности выбора роли и настраимваем его */
                    DataGridComboBoxColumn col = new DataGridComboBoxColumn
                    {
                        Header = "Роль", //Название столбца
                        DisplayMemberPath = "Name", //Отображаем именно поле Name, а не ID
                        SelectedValuePath = "Id", //А выбираем ID
                        ItemsSource = _dbContext.Roles.ToArray(), //Подвязываем эти данные в выпадающий список выбора
                        SelectedValueBinding = binding //Устанавливаем созданный раннее биндинг к столбцу
                    };

                    //Добавляем созданный столбец в DataGrid
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
                case "staff":
                    e.Column.Visibility = Visibility.Collapsed;

                    _dbContext.Roles.Load();

                    Binding binding2 = new Binding();
                    binding2.Path = new PropertyPath("Staff");

                    DataGridComboBoxColumn col2 = new DataGridComboBoxColumn
                    {
                        Header = "Персонал",
                        DisplayMemberPath = "Name",
                        SelectedValuePath = "Id",
                        ItemsSource = _dbContext.staff.ToArray(),
                        SelectedValueBinding = binding2
                    };

                    ((DataGrid)sender).Columns.Add(col2);
                    break;
                case "RoleNavigation":
                    //Скрываем имя
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "StaffNavigation":
                    e.Column.Visibility = Visibility.Collapsed;
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
                case "Sales":
                    //Скрываем имя
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "Users":
                    e.Column.Visibility = Visibility.Collapsed;
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

                case "staff":
                    e.Column.Visibility = Visibility.Collapsed;

                    _dbContext.Roles.Load();

                    Binding binding2 = new Binding();
                    binding2.Path = new PropertyPath("Staff");

                    DataGridComboBoxColumn col2 = new DataGridComboBoxColumn
                    {
                        Header = "Персонал",
                        DisplayMemberPath = "Name",
                        SelectedValuePath = "Id",
                        ItemsSource = _dbContext.staff.ToArray(),
                        SelectedValueBinding = binding2
                    };

                    ((DataGrid)sender).Columns.Add(col2);
                    break;

                case "ReleaseDate":
                    //Меняем имя
                    e.Column.Header = "Дата релиза";
                    break;
                case "Company":
                    //Меняем имя
                    e.Column.Header = "Компания";
                    break;
                case "Price":
                    //Меняем имя
                    e.Column.Header = "Цена";
                break;
                case "TypeNavigation":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "PriceNavigation":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "StaffNavigation":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "Sales":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
            }
            
        }

        private void saleDG_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
                case "Staff":
                    e.Column.Visibility = Visibility.Collapsed;

                    _dbContext.staff.Load();

                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("Staff");

                    DataGridComboBoxColumn col = new DataGridComboBoxColumn
                    {
                        Header = "Персонал",
                        DisplayMemberPath = "Name",
                        SelectedValuePath = "Id",
                        ItemsSource = _dbContext.staff.ToArray(),
                        SelectedValueBinding = binding
                    };

                    ((DataGrid)sender).Columns.Add(col);
                    break;
                case "Product":
                    e.Column.Visibility = Visibility.Collapsed;

                    _dbContext.Sales.Load();

                    Binding binding2 = new Binding();
                    binding2.Path = new PropertyPath("Product");

                    DataGridComboBoxColumn col2 = new DataGridComboBoxColumn
                    {
                        Header = "Продукт",
                        DisplayMemberPath = "Name",
                        SelectedValuePath = "Id",
                        ItemsSource = _dbContext.Katalogs.ToArray(),
                        SelectedValueBinding = binding2
                    };

                    ((DataGrid)sender).Columns.Add(col2);
                    break;
                case "ProductNavigation":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "StaffNavigation":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "Katalogs":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _dbContext.SaveChanges();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            switch(_currentTable)
            {
                case "Пользователи":
                    _dbContext.Users.Local.Remove(usersDG.SelectedItem as User);
                    break;
                case "Поставщики":
                    _dbContext.Supplies.Local.Remove(SuppliesDG.SelectedItem as Supply);
                    break;
                case "Персонал":
                    _dbContext.staff.Local.Remove(StaffDG.SelectedItem as staff);
                    break;
                case "Каталог товаров":
                    _dbContext.Katalogs.Local.Remove(KatalogDG.SelectedItem as Katalog);
                    break;
            }
        }

        //Меняем название текущей таблицы на Header, которая вызвала событие sender, и обновляем таблицу 
        private void usersTab_GotFocus(object sender, RoutedEventArgs e)
        {
            _currentTable = ((TabItem)sender).Header.ToString();
            RefreshTable(_currentTable);
        }

        private void suppliesTab_GotFocus(object sender, RoutedEventArgs e)
        {
            _currentTable = ((TabItem)sender).Header.ToString();
            RefreshTable(_currentTable);
        }

        private void staffTab_GotFocus(object sender, RoutedEventArgs e)
        {
            _currentTable = ((TabItem)sender).Header.ToString();
            RefreshTable(_currentTable);
        }

        private void katalogTab_GotFocus(object sender, RoutedEventArgs e)
        {
            _currentTable = ((TabItem)sender).Header.ToString();
            RefreshTable(_currentTable);
        }

        private void saleTab_GotFocus(object sender, RoutedEventArgs e)
        {
            _currentTable = ((TabItem)sender).Header.ToString();
            RefreshTable(_currentTable);
        }

        private string GetUserFile()
        {
            //Создаем OpenFileDialog для выбора файла
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Filter = "TXT Файлы | *.txt"; //фильтр на CSV файлы
            ofd.Title = "Выберите файл для экспорта";

            //Открываем его ,и если выбрали файл , то возвращаем путь до него 
            if(ofd.ShowDialog() == true)
            {
                return ofd.FileName;
            }
            return null;//Иначе вернется null 
        }
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            //Получаем путь к файлу для экспорта
            string filePath = GetUserFile();

            //Если вернулся null (ничего не выбрали) , выходим из метода 
            if (filePath == null)
                return;

            // Открывам поток на запись 
            StreamWriter file = new StreamWriter(filePath, false);

            // проверяем какую таблицу будем экспортировать 
            switch(_currentTable)
            {
                case "Пользователи":
                    // Сохраняем таблицу в коллекцию table для удобства
                    ObservableCollection<User> table = _dbContext.Users.Local.ToObservableCollection();

                    file.WriteLine($"ID;Логин;Роль;Пароль;Адрес;Номер телефона"); //Записываем заголовки
                    // проходим по всем элементм таблицы 
                    foreach(User elem in table)
                    {
                        //Записываем каждое поле элемента в файл
                        file.WriteLine($"{elem.Id};{elem.Login};{elem.Role};{elem.Password};{elem.Address};{elem.PhoneNumber}");
                    }
                    break;
                case "Поставщики":
                    ObservableCollection<Supply> table2 = _dbContext.Supplies.Local.ToObservableCollection();
                    
                    file.WriteLine($"ID;Имя;Адрес;Номер телефона");

                    foreach (Supply elem in table2)
                    {
                        file.WriteLine($"{elem.Id};{elem.Name};{elem.Address};{elem.Contact}");
                    }
                    break;
                case "Персонал":
                    ObservableCollection<staff> table3 = _dbContext.staff.Local.ToObservableCollection();

                    file.WriteLine($"ID;Имя;Фамилия;Отчество;Дата рождения;Контактные данные;Адрес;Паспорт");

                    foreach (staff elem in table3)
                    {
                        file.WriteLine($"{elem.Id};{elem.Name};{elem.Surname};{elem.Patronymic};{elem.Birthday};{elem.Contact};{elem.Address};{elem.Passport}");
                    }
                    break;
                case "Каталог товаров":
                    ObservableCollection<Katalog> table4 = _dbContext.Katalogs.Local.ToObservableCollection();

                    file.WriteLine($"ID;Название;Тип;Дата выхода;Компания;");

                    foreach (Katalog elem in table4)
                    {
                        file.WriteLine($"{elem.Id};{elem.Name};{elem.Type};{elem.ReleaseDate};{elem.Company};");
                    }
                    break;
                case "Продажи":
                    ObservableCollection<Sale> table5 = _dbContext.Sales.Local.ToObservableCollection();

                    file.WriteLine($"ID;Персонал;Продукт;Дата Продажи;");

                    foreach (Sale elem in table5)
                    {
                        file.WriteLine($"{elem.Id};{elem.Staff};{elem.Product};{elem.Date};");
                    }
                    break;
            }
            file.Close(); //Закрываем файл
            MessageBox.Show("Экспорт успешно завершен!", "УСПЕШНО!!!"); //Вывод успешного экспорта
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReportSalesButton_Click(object sender, RoutedEventArgs e)
        {
            string reportName = ((Button)sender).Content.ToString();
            switch (reportName)
            {

                case "Продажи":
                    ObservableCollection<Sale> salesCurMonth = new ObservableCollection<Sale>();
                    _dbContext.Sales.Load();
                    foreach (Sale sale in _dbContext.Sales.Local.ToObservableCollection())
                    {
                        if (sale.Date.Month == DateTime.Now.Month)
                            salesCurMonth.Add(sale);
                    }
                    reportDG.ItemsSource = salesCurMonth;

                    break;
            }
        }

        private void reportDG_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //Имя столбца
            string headerName = e.Column.Header.ToString();

            //Проверяем имя столбца
            switch (headerName)
            {
                case "User":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "Product":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "Date":
                    e.Column.Header = "Дата";
                    break;
                case "Staff":
                    e.Column.Visibility = Visibility.Collapsed;

                    _dbContext.staff.Load();

                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("Staff");

                    DataGridComboBoxColumn col = new DataGridComboBoxColumn
                    {
                        Header = "Персонал",
                        DisplayMemberPath = "Name",
                        SelectedValuePath = "Id",
                        ItemsSource = _dbContext.staff.ToArray(),
                        SelectedValueBinding = binding
                    };
                    ((DataGrid)sender).Columns.Add(col);
                    break;
                case "UserNavigation":
                    e.Column.Visibility = Visibility.Collapsed;

                    _dbContext.Users.Load(); //Подгружаем данные из таблицы roles

                    Binding binding1 = new Binding(); //Создаем новый биндинг для подвязки роли
                    binding1.Path = new PropertyPath("User"); //В путь подвязки указываем поле RoleId

                    //Создаем новый столбец типа ComboBox
                    //для возможности выбора роли и настраиваем его
                    DataGridComboBoxColumn col1 = new DataGridComboBoxColumn()
                    {
                        Header = "Пользователь", //Название столбца
                        DisplayMemberPath = "Логин", //Отображаем именно поле Name
                        SelectedValuePath = "Id", //А ищем по Id
                        ItemsSource = _dbContext.Users.ToArray(), //Подвязываем данные в список выбора
                        SelectedValueBinding = binding1, //Устанавливаем созданный раннее биндинг к столбцу
                        IsReadOnly = true
                    };

                    ((DataGrid)sender).Columns.Add(col1); //Добавляем созданный столбец в DataGrid
                    break;
                case "StaffNavigation":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "ProductNavigation":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;

                case "Katalogs":
                    e.Column.Visibility = Visibility.Collapsed;

                    _dbContext.Katalogs.Load();

                    Binding binding3 = new Binding();
                    binding3.Path = new PropertyPath("katalog");

                    DataGridComboBoxColumn col3 = new DataGridComboBoxColumn
                    {
                        Header = "Каталог",
                        DisplayMemberPath = "Name",
                        SelectedValuePath = "Id",
                        ItemsSource = _dbContext.Katalogs.ToArray(),
                        SelectedValueBinding = binding3,
                        IsReadOnly = true
                    };

                    ((DataGrid)sender).Columns.Add(col3);
                    break;
            }
        }
    }

}
