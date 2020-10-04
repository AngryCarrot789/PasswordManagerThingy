using PSWRDMGR.AccountStructures;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PSWRDMGR.Views
{
    /// <summary>
    /// Interaction logic for NewAccountWindow.xaml
    /// </summary>
    public partial class NewAccountWindow : Window
    {
        public AccountViewModel AccountModel = new AccountViewModel();
        public Action AddAccountCallback { get; set; }
        public NewAccountWindow()
        {
            InitializeComponent();
            DataContext = AccountModel;
        }

        public void ResetAccountContext()
        {
            AccountModel = new AccountViewModel();
            this.DataContext = AccountModel;
        }

        private void AddAccountCallbackFunc() { AddAccountCallback?.Invoke(); }
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
                case Key.Enter: AddAccountCallbackFunc(); this.Hide(); break;
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

        private void AddAccountClick(object sender, RoutedEventArgs e)
        {
            AddAccountCallbackFunc();
            this.Hide();
        }
    }
}
