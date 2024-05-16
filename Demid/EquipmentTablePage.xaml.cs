using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Demid
{
    public partial class EquipmentTablePage : Page
    {
        public EquipmentTablePage()
        {
            InitializeComponent();
            LoadEquipmentData(); // Загрузка данных при инициализации страницы
        }

        private void LoadEquipmentData()
        {
            // Загрузка данных из базы данных и привязка их к DataGrid
            using (var context = new supportDBEntities())
            {
                EquipmentDataGrid.ItemsSource = context.Оборудование.ToList();
            }
        }

        private void AddEquipment_Click(object sender, RoutedEventArgs e)
        {
            // Добавление новой записи в базу данных
            using (var context = new supportDBEntities())
            {
                var newEquipment = new Оборудование
                {
                    Название = NameTextBox.Text,
                    Серийный_номер = SerialNumberTextBox.Text,
                    Описание = DescriptionTextBox.Text,
                    Дата_покупки = DateTime.Parse(PurchaseDateTextBox.Text),
                    Гарантийный_срок = DateTime.Parse(WarrantyDateTextBox.Text)
                };

                context.Оборудование.Add(newEquipment);
                context.SaveChanges();
                LoadEquipmentData(); // Обновление данных в DataGrid после добавления
            }
            Clear();
        }

        private void EditEquipment_Click(object sender, RoutedEventArgs e)
        {
            // Изменение выбранной записи в базе данных
            if (EquipmentDataGrid.SelectedItem != null)
            {
                var selectedEquipment = (Оборудование)EquipmentDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var equipmentToUpdate = context.Оборудование.Find(selectedEquipment.Идентификатор);
                    if (equipmentToUpdate != null)
                    {
                        equipmentToUpdate.Название = NameTextBox.Text;
                        equipmentToUpdate.Серийный_номер = SerialNumberTextBox.Text;
                        equipmentToUpdate.Описание = DescriptionTextBox.Text;
                        equipmentToUpdate.Дата_покупки = DateTime.Parse(PurchaseDateTextBox.Text);
                        equipmentToUpdate.Гарантийный_срок = DateTime.Parse(WarrantyDateTextBox.Text);
                        context.SaveChanges();
                        LoadEquipmentData(); // Обновление данных в DataGrid после изменения
                    }
                }
            }
            Clear();
        }

        private void DeleteEquipment_Click(object sender, RoutedEventArgs e)
        {
            // Удаление выбранной записи из базы данных
            if (EquipmentDataGrid.SelectedItem != null)
            {
                var selectedEquipment = (Оборудование)EquipmentDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var equipmentToDelete = context.Оборудование.Find(selectedEquipment.Идентификатор);
                    if (equipmentToDelete != null)
                    {
                        context.Оборудование.Remove(equipmentToDelete);
                        context.SaveChanges();
                        LoadEquipmentData(); // Обновление данных в DataGrid после удаления
                    }
                }
            }
            Clear();
        }

        private void EquipmentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Заполнение полей ввода данными выбранной записи для их последующего изменения
            if (EquipmentDataGrid.SelectedItem != null)
            {
                var selectedEquipment = (Оборудование)EquipmentDataGrid.SelectedItem;
                NameTextBox.Text = selectedEquipment.Название;
                SerialNumberTextBox.Text = selectedEquipment.Серийный_номер;
                DescriptionTextBox.Text = selectedEquipment.Описание;
                PurchaseDateTextBox.Text = selectedEquipment.Дата_покупки.ToString();
                WarrantyDateTextBox.Text = selectedEquipment.Гарантийный_срок.ToString();
            }
        }
        private void Clear()
        {
            NameTextBox.Text = null;
            SerialNumberTextBox.Text = null;
            DescriptionTextBox.Text = null;
      
        }
    }
}
