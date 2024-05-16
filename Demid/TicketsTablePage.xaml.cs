using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Demid
{
    public partial class TicketsTablePage : Page
    {
        public TicketsTablePage()
        {
            InitializeComponent();
            LoadTicketsData(); // Загрузка данных при инициализации страницы
            LoadUsers(); // Загрузка списка пользователей в комбобокс
            LoadCombo();

        }

        private void LoadTicketsData()
        {
            // Загрузка данных из базы данных и привязка их к DataGrid
            using (var context = new supportDBEntities())
            {
                TicketsDataGrid.ItemsSource = context.Тикеты.ToList();

                var otv = context.Ответственные.ToList();

                DescriptionComboBox.ItemsSource = otv;
            }
        }
        private void LoadCombo()
        {
            // Загрузка данных из базы данных и привязка их к DataGrid
            using (var context = new supportDBEntities())
            {

                var otv = context.Ответственные.ToList();

                OtvComboBox.ItemsSource = otv;
            }
        }
        private void LoadUsers()
        {
            // Загрузка списка пользователей в комбобокс
            using (var context = new supportDBEntities())
            {
                var users = context.Пользователи.ToList();
                UserComboBox.ItemsSource = users;

                var problems = context.Категории_проблем.ToList();
                DescriptionComboBox.ItemsSource = problems;

            }
        }

        private void TicketsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Обработка события выбора элемента в DataGrid
            if (TicketsDataGrid.SelectedItem != null)
            {
                var selectedTicket = (Тикеты)TicketsDataGrid.SelectedItem;
                // Заполнение полей формы данными выбранного тикета
                ThemeTextBox.Text = selectedTicket.Тема;
                DescriptionComboBox.SelectedValue = selectedTicket.Идентификатор_проблемы;
                var statusItem = StatusComboBox.Items.Cast<ComboBoxItem>()
   .FirstOrDefault(item => item.Content.ToString() == selectedTicket.Статус);
                StatusComboBox.SelectedItem = statusItem;
              
                var priorityItem = PriorityComboBox.Items.Cast<ComboBoxItem>()
    .FirstOrDefault(item => item.Content.ToString() == selectedTicket.Приоритет);
                PriorityComboBox.SelectedItem = priorityItem;
                CreationDatePicker.SelectedDate = selectedTicket.Дата_создания;
                UpdateDatePicker.SelectedDate = selectedTicket.Дата_обновления;
                UserComboBox.SelectedValue = selectedTicket.Идентификатор_пользователя;
                OtvComboBox.SelectedValue = selectedTicket.Идентификатор_ответственного;
            }
        }

        private void AddTicket_Click(object sender, RoutedEventArgs e)
        {
            // Добавление новой записи в базу данных
            using (var context = new supportDBEntities())
            {
                var newTicket = new Тикеты
                {
                    Идентификатор_пользователя = (int)UserComboBox.SelectedValue,
                    Тема = ThemeTextBox.Text,
                    Идентификатор_проблемы = (int)DescriptionComboBox.SelectedValue,
                    Статус = (string)((ComboBoxItem)StatusComboBox.SelectedItem)?.Content,
                    Приоритет = (string)((ComboBoxItem)PriorityComboBox.SelectedItem)?.Content,
                     Дата_создания = CreationDatePicker.SelectedDate,
                    Дата_обновления = UpdateDatePicker.SelectedDate,
                    Идентификатор_ответственного = (int)OtvComboBox.SelectedValue

                };

                context.Тикеты.Add(newTicket);
                context.SaveChanges();
                LoadTicketsData(); // Обновление данных в DataGrid после добавления
                LoadUsers();
            }
            Clear();
        }

        private void EditTicket_Click(object sender, RoutedEventArgs e)
        {
            // Изменение выбранной записи в базе данных
            if (TicketsDataGrid.SelectedItem != null)
            {
                var selectedTicket = (Тикеты)TicketsDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var ticketToUpdate = context.Тикеты.Find(selectedTicket.Идентификатор);
                    if (ticketToUpdate != null)
                    {
                        ticketToUpdate.Тема = ThemeTextBox.Text;
                        ticketToUpdate.Идентификатор_проблемы = (int)DescriptionComboBox.SelectedValue;
                        ticketToUpdate.Статус = (string)((ComboBoxItem)StatusComboBox.SelectedItem)?.Content;
                        ticketToUpdate.Приоритет = (string)((ComboBoxItem)PriorityComboBox.SelectedItem)?.Content;

                        ticketToUpdate.Дата_создания = CreationDatePicker.SelectedDate;
                        ticketToUpdate.Дата_обновления = UpdateDatePicker.SelectedDate;
                        ticketToUpdate.Идентификатор_пользователя = (int)UserComboBox.SelectedValue;
                        ticketToUpdate.Идентификатор_ответственного = (int)OtvComboBox.SelectedValue;
                        context.SaveChanges();
                        LoadTicketsData(); // Обновление данных в DataGrid после изменения
                        LoadUsers();
                    }
                }
            }
            Clear();
        }

        private void DeleteTicket_Click(object sender, RoutedEventArgs e)
        {
            // Удаление выбранной записи из базы данных
            if (TicketsDataGrid.SelectedItem != null)
            {
                var selectedTicket = (Тикеты)TicketsDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var ticketToDelete = context.Тикеты.Find(selectedTicket.Идентификатор);
                    if (ticketToDelete != null)
                    {
                        context.Тикеты.Remove(ticketToDelete);
                        context.SaveChanges();
                        LoadTicketsData(); // Обновление данных в DataGrid после удаления
                        LoadUsers();
                    }
                }
            }
            Clear();
        }
        private void Clear()
        {
           
            ThemeTextBox.Text = null;
            StatusComboBox.SelectedValue = null;
            PriorityComboBox.SelectedValue = null;    
            UserComboBox.SelectedValue = null;
            OtvComboBox.SelectedValue = null;
        }


    }
}
