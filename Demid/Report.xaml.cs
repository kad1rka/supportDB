using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Demid
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Page
    {
        public Report()
        {
            InitializeComponent();
            LoadTickets(); // Load tickets when the page initializes
        }

        private void LoadTickets()
        {
            using (var context = new supportDBEntities())
            {
                if (StatusComboBox != null && TicketsDataGrid != null)
                {
                    // Get the selected priority and status from the ComboBoxes
                    var selectedPriority = (string)(PriorityComboBox.SelectedItem as ComboBoxItem)?.Content;
                    var selectedStatus = (string)(StatusComboBox.SelectedItem as ComboBoxItem)?.Content;

                    // Query tickets based on selected priority and status
                    var query = context.Тикеты.AsQueryable();

                    if (!string.IsNullOrEmpty(selectedPriority))
                        query = query.Where(t => t.Приоритет == selectedPriority);

                    if (!string.IsNullOrEmpty(selectedStatus))
                        query = query.Where(t => t.Статус == selectedStatus);

                    // Order the tickets by priority and bind to the DataGrid
                    var sortedTickets = query.OrderBy(t => t.Приоритет).ToList();
                    TicketsDataGrid.ItemsSource = sortedTickets;
                }
            }
        }


        // Event handler for priority ComboBox selection change
        private void PriorityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadTickets();
        }

        // Event handler for status ComboBox selection change
        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadTickets();
        }
    }
}
