using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Demid
{
    public partial class SolutionsTablePage : Page
    {
        public SolutionsTablePage()
        {
            InitializeComponent();
            LoadSolutionsData(); // Загрузка данных при инициализации страницы
        }

        private void LoadSolutionsData()
        {
            // Загрузка данных из базы данных и привязка их к DataGrid
            using (var context = new supportDBEntities())
            {
                SolutionsDataGrid.ItemsSource = context.Решения.ToList();
                var ticket = context.Тикеты.ToList();
                TicketComboBox.ItemsSource = ticket;
            }
        }

        private void AddSolution_Click(object sender, RoutedEventArgs e)
        {
            // Добавление новой записи в базу данных
            using (var context = new supportDBEntities())
            {
                var newSolution = new Решения
                {
                    Идентификатор_тикета = (int)TicketComboBox.SelectedValue,
                    Описание_решения = SolutionDescriptionTextBox.Text,
                    Дата_создания = DateTime.Parse(CreationDateTextBox.Text)
                };

                context.Решения.Add(newSolution);
                context.SaveChanges();
                LoadSolutionsData(); // Обновление данных в DataGrid после добавления
            }
            Clear();
        }

        private void EditSolution_Click(object sender, RoutedEventArgs e)
        {
            // Изменение выбранной записи в базе данных
            if (SolutionsDataGrid.SelectedItem != null)
            {
                var selectedSolution = (Решения)SolutionsDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var solutionToUpdate = context.Решения.Find(selectedSolution.Идентификатор);
                    if (solutionToUpdate != null)
                    {
                        solutionToUpdate.Идентификатор_тикета = (int)TicketComboBox.SelectedValue;
                        solutionToUpdate.Описание_решения = SolutionDescriptionTextBox.Text;
                        solutionToUpdate.Дата_создания = DateTime.Parse(CreationDateTextBox.Text);
                        context.SaveChanges();
                        LoadSolutionsData(); // Обновление данных в DataGrid после изменения
                    }
                }
            }
            Clear();
        }

        private void DeleteSolution_Click(object sender, RoutedEventArgs e)
        {
            // Удаление выбранной записи из базы данных
            if (SolutionsDataGrid.SelectedItem != null)
            {
                var selectedSolution = (Решения)SolutionsDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var solutionToDelete = context.Решения.Find(selectedSolution.Идентификатор);
                    if (solutionToDelete != null)
                    {
                        context.Решения.Remove(solutionToDelete);
                        context.SaveChanges();
                        LoadSolutionsData(); // Обновление данных в DataGrid после удаления
                    }
                }
            }
            Clear();
        }

        private void SolutionsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Заполнение полей ввода данными выбранной записи для их последующего изменения
            if (SolutionsDataGrid.SelectedItem != null)
            {
                var selectedSolution = (Решения)SolutionsDataGrid.SelectedItem;
                TicketComboBox.SelectedValue = selectedSolution.Идентификатор_тикета;
                SolutionDescriptionTextBox.Text = selectedSolution.Описание_решения;
                CreationDateTextBox.Text = selectedSolution.Дата_создания.ToString();
            }
        }
        private void Clear()
        {
            TicketComboBox.SelectedValue = null;
            SolutionDescriptionTextBox.Text = null;
            CreationDateTextBox.Text = null;    
        }
    }
}
