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
using System.Windows.Shapes;

namespace PSWRDMGR.Views
{
    /// <summary>
    /// Interaction logic for AccountInformationPresenter.xaml
    /// </summary>
    public partial class AccountInformationPresenter : Window
    {
        public AccountModel AccountModel = new AccountModel();
        public AccountInformationPresenter()
        {
            InitializeComponent();
            DataContext = AccountModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape: this.Hide(); break;
                case Key.Enter: this.Hide(); break;
            }
        }

        private void CopyTextClick(object sender, RoutedEventArgs e)
        {
            switch (int.Parse(((Button)e.Source).Uid))
            {
                case 1:  Clipboard.SetText(a.Text); break;
                case 2:  Clipboard.SetText(b.Text); break;
                case 3:  Clipboard.SetText(c.Text); break;
                case 4:  Clipboard.SetText(d.Text); break;
                case 5:  Clipboard.SetText(ee.Text); break;
                case 6:  Clipboard.SetText(f.Text); break;
                case 7:  Clipboard.SetText(g.Text); break;
                case 8:  Clipboard.SetText(h.Text); break;
                case 9:  Clipboard.SetText(i.Text); break;
                case 10: Clipboard.SetText(j.Text); break;
            }
        }
    }
}
