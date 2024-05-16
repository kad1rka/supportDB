using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Demid
{
    public partial class ActionHistoryTablePage : Page
    {
        public ActionHistoryTablePage()
        {
            InitializeComponent();
            LoadActionHistoryData(); // Загрузка данных при инициализации страницы
            LoadTickets(); // Загрузка списка тикетов в комбобокс
            LoadUsers(); // Загрузка списка пользователей в комбобокс
        }

        private void LoadActionHistoryData()
        {
            // Загрузка данных из базы данных и привязка их к DataGrid
            using (var context = new supportDBEntities())
            {
                ActionHistoryDataGrid.ItemsSource = context.История_действий.ToList();
            }
        }

        private void LoadTickets()
        {
            // Загрузка списка тикетов в комбобокс
            using (var context = new supportDBEntities())
            {
                TicketComboBox.ItemsSource = context.Тикеты.ToList();
            }
        }

        private void LoadUsers()
        {
            // Загрузка списка пользователей в комбобокс
            using (var context = new supportDBEntities())
            {
                // Создание нового объекта с полем FullName, объединяющим имя и фамилию пользователя
                UserComboBox.ItemsSource = context.Пользователи.Select(u => new { FullName = u.Имя + " " + u.Фамилия, u.Идентификатор }).ToList();
            }
        }

        private void AddAction_Click(object sender, RoutedEventArgs e)
        {
            // Добавление новой записи в базу данных
            using (var context = new supportDBEntities())
            {
                var newAction = new История_действий
                {
                    Идентификатор_тикета = (int)TicketComboBox.SelectedValue,
                    Действие = ActionTextBox.Text,
                    Идентификатор_пользователя = (int)UserComboBox.SelectedValue,
                    Дата_и_время = DateTime.Now
                };

                context.История_действий.Add(newAction);
                context.SaveChanges();
                LoadActionHistoryData(); // Обновление данных в DataGrid после добавления
            }
            Clear();
        }

        private void EditAction_Click(object sender, RoutedEventArgs e)
        {
            // Изменение выбранной записи в базе данных
            if (ActionHistoryDataGrid.SelectedItem != null)
            {
                var selectedAction = (История_действий)ActionHistoryDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var actionToUpdate = context.История_действий.Find(selectedAction.Идентификатор);
                    if (actionToUpdate != null)
                    {
                        actionToUpdate.Идентификатор_тикета = (int)TicketComboBox.SelectedValue;
                        actionToUpdate.Действие = ActionTextBox.Text;
                        actionToUpdate.Идентификатор_пользователя = (int)UserComboBox.SelectedValue;
                        actionToUpdate.Дата_и_время = DateTime.Now;
                        context.SaveChanges();
                        LoadActionHistoryData(); // Обновление данных в DataGrid после изменения
                    }
                }
            }
            Clear();
        }

        private void DeleteAction_Click(object sender, RoutedEventArgs e)
        {
            // Удаление выбранной записи из базы данных
            if (ActionHistoryDataGrid.SelectedItem != null)
            {
                var selectedAction = (История_действий)ActionHistoryDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var actionToDelete = context.История_действий.Find(selectedAction.Идентификатор);
                    if (actionToDelete != null)
                    {
                        context.История_действий.Remove(actionToDelete);
                        context.SaveChanges();
                        LoadActionHistoryData(); // Обновление данных в DataGrid после удаления
                    }
                }
            }
            Clear();
        }

        private void ActionHistoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, что выбрана хотя бы одна строка
            if (ActionHistoryDataGrid.SelectedItem != null)
            {
                // Получаем выбранный элемент из DataGrid
                var selectedAction = (История_действий)ActionHistoryDataGrid.SelectedItem;

                // Заполняем текстовые поля данными выбранного действия
                ActionTextBox.Text = selectedAction.Действие;

                // Устанавливаем выбранные значения в комбобоксы
                TicketComboBox.SelectedValue = selectedAction.Идентификатор_тикета;
                UserComboBox.SelectedValue = selectedAction.Идентификатор_пользователя;
            }
        }
        private void Clear()
        {
            ActionTextBox.Text = null;
                TicketComboBox.SelectedValue = null;
            UserComboBox.SelectedValue = null;
        }
    }
}
