
using System.Windows;
using System.Windows.Controls;
using BankClientsPresenter.Views;
using Prism.Regions;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            regionManager.RegisterViewWithRegion("ClientEditForm", typeof(ClientEditForm));
            regionManager.RegisterViewWithRegion("OperatorSelector", typeof(OperatorSelector));

        }

          }
}