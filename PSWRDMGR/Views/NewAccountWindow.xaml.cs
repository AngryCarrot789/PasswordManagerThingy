using PSWRDMGR.AccountStructures;
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
    /// Interaction logic for NewAccountWindow.xaml
    /// </summary>
    public partial class NewAccountWindow : Window
    {
        public AddAccountModel AccountModel = new AddAccountModel();
        public Action AddAccountCallback { get; set; }
        public NewAccountWindow()
        {
            InitializeComponent();
            AccountModel.AddAccountCommand = new Command(addAccCallback);
            AccountModel.PasteFromClipboard = new CommandParam(pasteClipbrd);
            DataContext = AccountModel;
        }
        private void pasteClipbrd(object index)
        {
            switch (int.Parse(index.ToString()))
            {
                case 1: AccountModel.AccountName = Clipboard.GetText(); break;
                case 2: AccountModel.Email = Clipboard.GetText(); break;
                case 3: AccountModel.Username = Clipboard.GetText(); break;
                case 4: AccountModel.Password = Clipboard.GetText(); break;
                case 5: AccountModel.DateOfBirth = Clipboard.GetText(); break;
                case 6: AccountModel.SecurityInfo = Clipboard.GetText(); break;
                case 7: AccountModel.ExtraInfo1 = Clipboard.GetText(); break;
                case 8: AccountModel.ExtraInfo2 = Clipboard.GetText(); break;
                case 9: AccountModel.ExtraInfo3 = Clipboard.GetText(); break;
                case 10: AccountModel.ExtraInfo4 = Clipboard.GetText(); break;
                case 11: AccountModel.ExtraInfo5 = Clipboard.GetText(); break;
            }
        }
        private void addAccCallback() { AddAccountCallback?.Invoke(); }
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
                case Key.Enter: addAccCallback(); this.Hide(); break;
            }
        }

        private void PasteTextToBox(object sender, RoutedEventArgs e)
        {
            switch (int.Parse(((Button)e.Source).Uid))
            {
                case 1: a.Text = Clipboard.GetText(); break;
                case 2: b.Text = Clipboard.GetText(); break;
                case 3: c.Text = Clipboard.GetText(); break;
                case 4: d.Text = Clipboard.GetText(); break;
                case 5: ee.Text = Clipboard.GetText(); break;
                case 6: f.Text = Clipboard.GetText(); break;
                case 7: g.Text = Clipboard.GetText(); break;
                case 8: h.Text = Clipboard.GetText(); break;
                case 9: i.Text = Clipboard.GetText(); break;
                case 10: j.Text = Clipboard.GetText(); break;
                case 11: k.Text = Clipboard.GetText(); break;
            }
        }

        private void ClearTextClick(object sender, RoutedEventArgs e)
        {
            switch (int.Parse(((Button)e.Source).Uid))
            {
                case 1: a.Text = ""; break;
                case 2: b.Text = ""; break;
                case 3: c.Text = ""; break;
                case 4: d.Text = ""; break;
                case 5: ee.Text = ""; break;
                case 6: f.Text = ""; break;
                case 7: g.Text = ""; break;
                case 8: h.Text = ""; break;
                case 9: i.Text = ""; break;
                case 10: j.Text = ""; break;
                case 11: k.Text = ""; break;
            }
        }
    }
}
