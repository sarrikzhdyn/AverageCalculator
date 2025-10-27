using System.Windows;
using PasswordManager.ViewModels;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainViewModel();
            this.DataContext = _vm;
        }

        private void MasterPassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
                _vm.MasterPassword = MasterPassBox.Password;
        }
    }
}
