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
            AccountModel.CopyToClipboardCommand = new CommandParam(pasteClipbrd);
            DataContext = AccountModel;
        }

        private void pasteClipbrd(object index)
        {
            string text = "";
            switch (int.Parse(index.ToString()))
            {
                case 1: text = AccountModel.AccountName ; break;
                case 2: text = AccountModel.Email       ; break;
                case 3: text = AccountModel.Username    ; break;
                case 4: text = AccountModel.Password    ; break;
                case 5: text = AccountModel.DateOfBirth ; break;
                case 6: text = AccountModel.SecurityInfo; break;
                case 7: text = AccountModel.ExtraInfo1  ; break;
                case 8: text = AccountModel.ExtraInfo2  ; break;
                case 9: text = AccountModel.ExtraInfo3  ; break;
                case 10: text = AccountModel.ExtraInfo4 ; break;
                case 11: text = AccountModel.ExtraInfo5 ; break;
            }
            if (!string.IsNullOrEmpty(text))
                Clipboard.SetText(text);
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
    }
}
