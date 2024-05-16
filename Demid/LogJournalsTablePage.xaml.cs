using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Demid
{
    public partial class LogJournalsTablePage : Page
    {
        public LogJournalsTablePage()
        {
            InitializeComponent();
            LoadLogJournalsData(); // Загрузка данных при инициализации страницы
            LoadTicket();
        }

        private void LoadLogJournalsData()
        {
            // Загрузка данных из базы данных и привязка их к DataGrid
            using (var context = new supportDBEntities())
            {
                LogJournalsDataGrid.ItemsSource = context.Журналы_логов.ToList();
            }
        }
        private void LoadTicket()
        {
            // Загрузка списка пользователей в комбобокс
            using (var context = new supportDBEntities())
            {
                var ticket = context.Тикеты.ToList();
                TicketComboBox.ItemsSource = ticket;
            }
        }
        private void AddLogJournal_Click(object sender, RoutedEventArgs e)
        {
            // Добавление новой записи в базу данных
            using (var context = new supportDBEntities())
            {
                var newLogJournal = new Журналы_логов
                {
                    Идентификатор_тикета = (int)TicketComboBox.SelectedValue,
                    Содержание_лога = LogContentTextBox.Text,
                    Уровень_важности = ImportanceLevelTextBox.Text,
                    Дата_и_время = DateTime.Parse(DateTimeTextBox.Text)
                };

                context.Журналы_логов.Add(newLogJournal);
                context.SaveChanges();
                LoadLogJournalsData(); // Обновление данных в DataGrid после добавления
            }
            Clear();
        }

        private void EditLogJournal_Click(object sender, RoutedEventArgs e)
        {
            // Изменение выбранной записи в базе данных
            if (LogJournalsDataGrid.SelectedItem != null)
            {
                var selectedLogJournal = (Журналы_логов)LogJournalsDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var logJournalToUpdate = context.Журналы_логов.Find(selectedLogJournal.Идентификатор);
                    if (logJournalToUpdate != null)
                    {
                        logJournalToUpdate.Идентификатор_тикета = (int)TicketComboBox.SelectedValue;
                        logJournalToUpdate.Содержание_лога = LogContentTextBox.Text;
                        logJournalToUpdate.Уровень_важности = ImportanceLevelTextBox.Text;
                        logJournalToUpdate.Дата_и_время = DateTime.Parse(DateTimeTextBox.Text);
                        context.SaveChanges();
                        LoadLogJournalsData(); // Обновление данных в DataGrid после изменения
                    }
                }
            }
            Clear();
        }

        private void DeleteLogJournal_Click(object sender, RoutedEventArgs e)
        {
            // Удаление выбранной записи из базы данных
            if (LogJournalsDataGrid.SelectedItem != null)
            {
                var selectedLogJournal = (Журналы_логов)LogJournalsDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var logJournalToDelete = context.Журналы_логов.Find(selectedLogJournal.Идентификатор);
                    if (logJournalToDelete != null)
                    {
                        context.Журналы_логов.Remove(logJournalToDelete);
                        context.SaveChanges();
                        LoadLogJournalsData(); // Обновление данных в DataGrid после удаления
                    }
                }
            }
            Clear();
        }

        private void LogJournalsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Заполнение полей ввода данными выбранной записи для их последующего изменения
            if (LogJournalsDataGrid.SelectedItem != null)
            {
                var selectedLogJournal = (Журналы_логов)LogJournalsDataGrid.SelectedItem;
                TicketComboBox.SelectedValue = selectedLogJournal.Идентификатор_тикета;
                LogContentTextBox.Text = selectedLogJournal.Содержание_лога;
                ImportanceLevelTextBox.Text = selectedLogJournal.Уровень_важности;
                DateTimeTextBox.Text = selectedLogJournal.Дата_и_время.ToString();
            }
        }
        private void Clear()
        {
            TicketComboBox.SelectedValue = null;
            LogContentTextBox.Text = null;
            ImportanceLevelTextBox.Text=null;
            DateTimeTextBox.Text = null;
        }
    }
}
