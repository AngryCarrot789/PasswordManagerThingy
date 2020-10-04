using System.Windows.Controls;
using System.Windows.Input;

namespace PSWRDMGR.Controls
{
    /// <summary>
    /// Interaction logic for AccountListItem.xaml
    /// </summary>
    public partial class AccountControl : UserControl
    {
        public AccountControlViewModel Account
        {
            get => this.DataContext as AccountControlViewModel;
            set => this.DataContext = value;
        }

        public AccountControl()
        {
            InitializeComponent();
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Account.ShowContentsPanel();
        }
    }
}
