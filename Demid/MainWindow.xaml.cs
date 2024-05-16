using System;
using System.Windows;

namespace Demid
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenUsersTable_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new UsersTablePage());
        }

        private void OpenTicketsTable_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new TicketsTablePage());
        }

        private void OpenProblemCategoriesTable_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new ProblemCategoriesTablePage());
        }

        private void OpenActionHistoryTable_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new ActionHistoryTablePage());
        }

        private void OpenResponsibleTable_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new ResponsibleTablePage());
        }

        private void OpenRoomsAndEquipmentTable_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new RoomsAndEquipmentTablePage());
        }

        private void OpenLogJournalsTable_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new LogJournalsTablePage());
        }

        private void OpenSolutionsTable_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new SolutionsTablePage());
        }

        private void OpenEquipmentTable_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new EquipmentTablePage());
        }

        private void OpenTicketStatusesTable_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new TicketStatusesTablePage());
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new Report());
        }
    }
}
