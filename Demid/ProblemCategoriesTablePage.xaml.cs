using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Demid
{
    public partial class ProblemCategoriesTablePage : Page
    {
        public ProblemCategoriesTablePage()
        {
            InitializeComponent();
            LoadCategoriesData(); // Загрузка данных при инициализации страницы
        }

        private void LoadCategoriesData()
        {
            // Загрузка данных из базы данных и привязка их к DataGrid
            using (var context = new supportDBEntities())
            {
                CategoriesDataGrid.ItemsSource = context.Категории_проблем.ToList();
            }
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            // Добавление новой записи в базу данных
            using (var context = new supportDBEntities())
            {
                var newCategory = new Категории_проблем
                {
                    Название = NameTextBox.Text,
                    Описание = DescriptionTextBox.Text
                };

                context.Категории_проблем.Add(newCategory);
                context.SaveChanges();
                LoadCategoriesData(); // Обновление данных в DataGrid после добавления
            }
            Clear();
        }

        private void EditCategory_Click(object sender, RoutedEventArgs e)
        {
            // Изменение выбранной записи в базе данных
            if (CategoriesDataGrid.SelectedItem != null)
            {
                var selectedCategory = (Категории_проблем)CategoriesDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var categoryToUpdate = context.Категории_проблем.Find(selectedCategory.Идентификатор);
                    if (categoryToUpdate != null)
                    {
                        categoryToUpdate.Название = NameTextBox.Text;
                        categoryToUpdate.Описание = DescriptionTextBox.Text;
                        context.SaveChanges();
                        LoadCategoriesData(); // Обновление данных в DataGrid после изменения
                    }
                }
            }
            Clear();
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            // Удаление выбранной записи из базы данных
            if (CategoriesDataGrid.SelectedItem != null)
            {
                var selectedCategory = (Категории_проблем)CategoriesDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var categoryToDelete = context.Категории_проблем.Find(selectedCategory.Идентификатор);
                    if (categoryToDelete != null)
                    {
                        context.Категории_проблем.Remove(categoryToDelete);
                        context.SaveChanges();
                        LoadCategoriesData(); // Обновление данных в DataGrid после удаления
                    }
                }
            }
            Clear();
        }

        private void CategoriesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, что выбрана хотя бы одна строка
            if (CategoriesDataGrid.SelectedItem != null)
            {
                // Получаем выбранный элемент из DataGrid
                var selectedCategory = (Категории_проблем)CategoriesDataGrid.SelectedItem;

                // Заполняем текстовые поля данными выбранной категории
                NameTextBox.Text = selectedCategory.Название;
                DescriptionTextBox.Text = selectedCategory.Описание;
            }
        }
         private void Clear()
        {
            NameTextBox.Text = null;
            DescriptionTextBox.Text = null;
        }
    }
}
