
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BankClients_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var g = sender as DataGrid;
            g.ScrollIntoView(g.SelectedItem);
        }
    }
}