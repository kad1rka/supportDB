using System;
using System.Windows;
using System.Linq;
using System.Windows.Controls;

namespace Demid
{
    public partial class UsersTablePage : Page
    {
        public UsersTablePage()
        {
            InitializeComponent();
            LoadUsersData(); // Загрузка данных при инициализации страницы
        }

        private void LoadUsersData()
        {
            // Загрузка данных из базы данных и привязка их к DataGrid
            using (var context = new supportDBEntities())
            {
                UsersDataGrid.ItemsSource = context.Пользователи.ToList();
            }
        }

        private void UsersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Обработка события выбора элемента в DataGrid
            if (UsersDataGrid.SelectedItem != null)
            {
                var selectedUser = (Пользователи)UsersDataGrid.SelectedItem;
                // Заполнение полей формы данными выбранного пользователя
                NameTextBox.Text = selectedUser.Имя;
                SurnameTextBox.Text = selectedUser.Фамилия;
                PositionTextBox.Text = selectedUser.Должность;
                ContactTextBox.Text = selectedUser.Контактная_информация;
            }
        }


        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            // Добавление новой записи в базу данных
            using (var context = new supportDBEntities())
            {
                var newUser = new Пользователи
                {
                    Имя = NameTextBox.Text,
                    Фамилия = SurnameTextBox.Text,
                    Должность = PositionTextBox.Text,
                    Контактная_информация = ContactTextBox.Text
                };

                context.Пользователи.Add(newUser);
                context.SaveChanges();
                MessageBox.Show( "Пользователь добавлен!", "Успешно!");
                LoadUsersData(); // Обновление данных в DataGrid после добавления
            }
            Clear();
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            // Изменение выбранной записи в базе данных
            if (UsersDataGrid.SelectedItem != null)
            {
                var selectedUser = (Пользователи)UsersDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var userToUpdate = context.Пользователи.Find(selectedUser.Идентификатор);
                    if (userToUpdate != null)
                    {
                        userToUpdate.Имя = NameTextBox.Text;
                        userToUpdate.Фамилия = SurnameTextBox.Text;
                        userToUpdate.Должность = PositionTextBox.Text;
                        userToUpdate.Контактная_информация = ContactTextBox.Text;
                        context.SaveChanges();
                        MessageBox.Show("Данные изменены!", "Успешно!");
                        LoadUsersData(); // Обновление данных в DataGrid после изменения
                    }
                }
            }
            Clear();
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            // Удаление выбранной записи из базы данных
            if (UsersDataGrid.SelectedItem != null)
            {
                var selectedUser = (Пользователи)UsersDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var userToDelete = context.Пользователи.Find(selectedUser.Идентификатор);
                    if (userToDelete != null)
                    {
                        context.Пользователи.Remove(userToDelete);
                        context.SaveChanges();
                        MessageBox.Show("Данные удалены!", "Успешно!");
                        LoadUsersData(); // Обновление данных в DataGrid после удаления
                    }
                }
            }
            Clear();
        }
        private void Clear()
        {
            NameTextBox.Text = null;
            SurnameTextBox.Text = null;
            PositionTextBox.Text = null;
            ContactTextBox.Text = null;
        }
    }
}
