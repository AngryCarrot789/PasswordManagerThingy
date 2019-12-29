using PSWRDMGR.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PSWRDMGR.Controls
{
    /// <summary>
    /// Interaction logic for AccountListItem.xaml
    /// </summary>
    public partial class AccountListItem : UserControl
    {
        public Action<AccountModel> ShowContentCallback { get; set; }
        private AccountModel AccountContext { get => this.DataContext as AccountModel; }
        public AccountListItem()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (int.Parse(((FrameworkElement)e.Source).Uid))
            {
                case 1: Clipboard.SetText(AccountContext.Username); break;
                case 2: Clipboard.SetText(AccountContext.Password); break;
                case 3: Clipboard.SetText(AccountContext.Email); break;
            }
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowContentCallback?.Invoke(AccountContext);
        }
    }
}
