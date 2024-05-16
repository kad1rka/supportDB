using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Demid
{
    public partial class RoomsAndEquipmentTablePage : Page
    {
        public RoomsAndEquipmentTablePage()
        {
            InitializeComponent();
            LoadRoomsAndEquipmentData(); // Загрузка данных при инициализации страницы
        }

        private void LoadRoomsAndEquipmentData()
        {
            // Загрузка данных из базы данных и привязка их к DataGrid
            using (var context = new supportDBEntities())
            {
                RoomsAndEquipmentDataGrid.ItemsSource = context.Помещения_и_оборудование.ToList();
                var obor = context.Оборудование.ToList();
                EquipmentTypeComboBox.ItemsSource = obor;
            }
        }

        private void AddRoomAndEquipment_Click(object sender, RoutedEventArgs e)
        {
            // Добавление новой записи в базу данных
            using (var context = new supportDBEntities())
            {
                var newRoomAndEquipment = new Помещения_и_оборудование
                {
                    Номер_помещения = RoomNumberTextBox.Text,
                    Идентификатор_оборудования = (int)EquipmentTypeComboBox.SelectedValue,
                    Состояние = StateTextBox.Text,
                    Дата_установки = DateTime.Parse(InstallationDateTextBox.Text),
                    Описание = DescriptionTextBox.Text
                };

                context.Помещения_и_оборудование.Add(newRoomAndEquipment);
                context.SaveChanges();
                LoadRoomsAndEquipmentData(); // Обновление данных в DataGrid после добавления
            }
            Clear();
        }

        private void EditRoomAndEquipment_Click(object sender, RoutedEventArgs e)
        {
            // Изменение выбранной записи в базе данных
            if (RoomsAndEquipmentDataGrid.SelectedItem != null)
            {
                var selectedRoomAndEquipment = (Помещения_и_оборудование)RoomsAndEquipmentDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var roomAndEquipmentToUpdate = context.Помещения_и_оборудование.Find(selectedRoomAndEquipment.Идентификатор);
                    if (roomAndEquipmentToUpdate != null)
                    {
                        roomAndEquipmentToUpdate.Номер_помещения = RoomNumberTextBox.Text;
                        roomAndEquipmentToUpdate.Идентификатор_оборудования = (int)EquipmentTypeComboBox.SelectedValue;
                        roomAndEquipmentToUpdate.Состояние = StateTextBox.Text;
                        roomAndEquipmentToUpdate.Дата_установки = DateTime.Parse(InstallationDateTextBox.Text);
                        roomAndEquipmentToUpdate.Описание = DescriptionTextBox.Text;
                        context.SaveChanges();
                        LoadRoomsAndEquipmentData(); // Обновление данных в DataGrid после изменения
                    }
                }
            }
            Clear();
        }

        private void DeleteRoomAndEquipment_Click(object sender, RoutedEventArgs e)
        {
            // Удаление выбранной записи из базы данных
            if (RoomsAndEquipmentDataGrid.SelectedItem != null)
            {
                var selectedRoomAndEquipment = (Помещения_и_оборудование)RoomsAndEquipmentDataGrid.SelectedItem;
                using (var context = new supportDBEntities())
                {
                    var roomAndEquipmentToDelete = context.Помещения_и_оборудование.Find(selectedRoomAndEquipment.Идентификатор);
                    if (roomAndEquipmentToDelete != null)
                    {
                        context.Помещения_и_оборудование.Remove(roomAndEquipmentToDelete);
                        context.SaveChanges();
                        LoadRoomsAndEquipmentData(); // Обновление данных в DataGrid после удаления
                    }
                }
            }
            Clear();
        }

        private void RoomsAndEquipmentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Заполнение полей ввода данными выбранной записи для их последующего изменения
            if (RoomsAndEquipmentDataGrid.SelectedItem != null)
            {
                var selectedRoomAndEquipment = (Помещения_и_оборудование)RoomsAndEquipmentDataGrid.SelectedItem;
                RoomNumberTextBox.Text = selectedRoomAndEquipment.Номер_помещения;
                EquipmentTypeComboBox.SelectedValue = selectedRoomAndEquipment.Идентификатор_оборудования;
                StateTextBox.Text = selectedRoomAndEquipment.Состояние;
                InstallationDateTextBox.Text = selectedRoomAndEquipment.Дата_установки.ToString();
                DescriptionTextBox.Text = selectedRoomAndEquipment.Описание;
            }
        }
        private void Clear()
        {
            RoomNumberTextBox.Text = null;
            EquipmentTypeComboBox.SelectedValue = null;
            StateTextBox.Text = null;
            InstallationDateTextBox.Text = null;
            DescriptionTextBox.Text = null;

        }
    }
}
