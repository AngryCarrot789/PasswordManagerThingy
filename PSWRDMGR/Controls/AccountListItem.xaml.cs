using PSWRDMGR.AccountStructures;
using PSWRDMGR.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public Action<AccountModel> AutoShowContentCallback { get; set; }
        public Action<AccountModel> EditContentCallback { get; set; }
        private AccountModel AccountContext { get => this.DataContext as AccountModel; }
        public AccountListItem()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (int.Parse(((FrameworkElement)e.Source).Uid))
                {
                    case 1: Clipboard.SetText(AccountContext.Username); break;
                    case 2: Clipboard.SetText(AccountContext.Password); break;
                    case 3: Clipboard.SetText(AccountContext.Email); break;
                    case 4: EditContentCallback?.Invoke(AccountContext); break;
                }
            }
            catch(Exception ed) { MessageBox.Show($"Failed to copy information to clipboard. Error: {ed.Message}", "Err Clipboard Set"); }
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AutoShowContentCallback?.Invoke(AccountContext);
        }

        /// <summary>
        /// WPF doesn't allow the same element to be in 2 different places,
        /// i.e, you cant have the same accountlistitem in 2 different lists (due to
        /// dependency objects and stuff i think, so
        /// you need to duplicate it which only changes the internal 
        /// "class UID" thingy idk, which WPF does allow :)
        /// </summary>
        /// <returns></returns>
        public AccountListItem Duplicate()
        {
            return new AccountListItem()
            {
                AutoShowContentCallback = this.AutoShowContentCallback,
                EditContentCallback = this.EditContentCallback,
                DataContext = this.DataContext
            };
        }
    }
}
