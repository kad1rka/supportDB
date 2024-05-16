using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Demid
{
    public partial class ResponsibleTablePage : Page
    {
        public ResponsibleTablePage()
        {
            InitializeComponent();
            LoadResponsibleData(); // Загрузка данных при инициализации страницы
        }

        private void LoadResponsibleData()
        {
            // Загрузка данных из базы данных и привязка их к DataGrid
            using (var context = new supportDBEntities())
            {
                ResponsibleDataGrid.ItemsSource = context.Ответственные.ToList();
            }
        }

        private void AddResponsible_Click(object sender, RoutedEventArgs e)
        {
            // Добавление новой записи в базу данных
            using (var context = new supportDBEntities())
            {
                var newResponsible = new Ответственные
                {
                    Имя = NameTextBox.Text,
                    Фамилия = SurnameTextBox.Text,
                    Должность = PositionTextBox.Text,
                    Контактная_информация = ContactTextBox.Text
                };

                context.Ответственные.Add(newResponsible);
                context.SaveChanges();
                LoadResponsibleData(); // Обновление данных в DataGrid после добавления
            }
            Clear();
        }

        private void EditResponsible_Click(object sender, RoutedEventArgs e)
        {
            // Изменение выбранной записи в базе данных
            if (ResponsibleDataGrid.SelectedItem != null)
            {
                var selectedResponsible = (Ответственные)ResponsibleDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var responsibleToUpdate = context.Ответственные.Find(selectedResponsible.Идентификатор);
                    if (responsibleToUpdate != null)
                    {
                        responsibleToUpdate.Имя = NameTextBox.Text;
                        responsibleToUpdate.Фамилия = SurnameTextBox.Text;
                        responsibleToUpdate.Должность = PositionTextBox.Text;
                        responsibleToUpdate.Контактная_информация = ContactTextBox.Text;
                        context.SaveChanges();
                        LoadResponsibleData(); // Обновление данных в DataGrid после изменения
                    }
                }
            }
            Clear();
        }

        private void DeleteResponsible_Click(object sender, RoutedEventArgs e)
        {
            // Удаление выбранной записи из базы данных
            if (ResponsibleDataGrid.SelectedItem != null)
            {
                var selectedResponsible = (Ответственные)ResponsibleDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var responsibleToDelete = context.Ответственные.Find(selectedResponsible.Идентификатор);
                    if (responsibleToDelete != null)
                    {
                        context.Ответственные.Remove(responsibleToDelete);
                        context.SaveChanges();
                        LoadResponsibleData(); // Обновление данных в DataGrid после удаления
                    }
                }
            }
            Clear(); 
        }

        private void ResponsibleDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResponsibleDataGrid.SelectedItem != null)
            {
                var selectedResponsible = (Ответственные)ResponsibleDataGrid.SelectedItem;
                NameTextBox.Text = selectedResponsible.Имя;
                SurnameTextBox.Text = selectedResponsible.Фамилия;
                PositionTextBox.Text = selectedResponsible.Должность;
                ContactTextBox.Text = selectedResponsible.Контактная_информация;
            }
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
