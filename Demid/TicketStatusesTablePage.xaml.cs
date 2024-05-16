using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Demid
{
    public partial class TicketStatusesTablePage : Page
    {
        public TicketStatusesTablePage()
        {
            InitializeComponent();
            LoadTicketData(); // Загрузка данных при инициализации страницы
        }

        private void LoadTicketData()
        {

             using (var context = new supportDBEntities())
            {
                TicketStatusesDataGrid.ItemsSource = context.Статусы_тикетов.ToList();
                var ticket = context.Тикеты.ToList();
                TicketComboBox.ItemsSource = ticket;
            }
        }

        private void AddTicketStatus_Click(object sender, RoutedEventArgs e)
        {

            using (var context = new supportDBEntities())
            {
                var newTicketStatus = new Статусы_тикетов
                {
                    Идентификатор_тикета = (int)TicketComboBox.SelectedValue,
                    Название = NameTextBox.Text,
                    Описание = DescriptionTextBox.Text
                };
                context.Статусы_тикетов.Add(newTicketStatus);
                context.SaveChanges();
                LoadTicketData();
            }
            Clear();
        }

        private void EditTicketStatus_Click(object sender, RoutedEventArgs e)
        {

            var selectedTicketStatus = (Статусы_тикетов)TicketStatusesDataGrid.SelectedItem;
            using (var context = new supportDBEntities())
            {
                var ticketStatusToUpdate = context.Статусы_тикетов.Find(selectedTicketStatus.Идентификатор);
                if (ticketStatusToUpdate != null)
                {
                    ticketStatusToUpdate.Идентификатор_тикета = (int)TicketComboBox.SelectedValue;
                    ticketStatusToUpdate.Название = NameTextBox.Text;
                    ticketStatusToUpdate.Описание = DescriptionTextBox.Text;
                    context.SaveChanges();
                    LoadTicketData();
                }
            }
            Clear();
        }

        private void DeleteTicketStatus_Click(object sender, RoutedEventArgs e)
        {

            var selectedTicketStatus = (Статусы_тикетов)TicketStatusesDataGrid.SelectedItem;
            using (var context = new supportDBEntities())
            {
                var ticketStatusToDelete = context.Статусы_тикетов.Find(selectedTicketStatus.Идентификатор);
                if (ticketStatusToDelete != null)
                {
                    context.Статусы_тикетов.Remove(ticketStatusToDelete);
                    context.SaveChanges();
                    LoadTicketData();
                    Clear();
                }
            }
        }

        private void TicketStatusesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedTicketStatus = (Статусы_тикетов)TicketStatusesDataGrid.SelectedItem;
            if (selectedTicketStatus != null)
            {
                var selectedSolution = (Статусы_тикетов)TicketStatusesDataGrid.SelectedItem;
                TicketComboBox.SelectedValue = selectedSolution.Идентификатор_тикета;
                NameTextBox.Text = selectedTicketStatus.Название;
                DescriptionTextBox.Text = selectedTicketStatus.Описание;
            }
        }
        private void Clear()
        {
            TicketComboBox.SelectedValue = null;
            NameTextBox.Text = null;
            DescriptionTextBox.Text = null;
        }


    }
}
