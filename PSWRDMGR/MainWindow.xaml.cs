using PSWRDMGR.ViewModels;
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

namespace PSWRDMGR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get => DataContext as MainViewModel; }
        public MainWindow()
        {
            InitializeComponent();
            ViewModel.ScrollIntoView = ScrollIntoViewThingy;
        }

        public void ScrollIntoViewThingy()
        {
            lBox.ScrollIntoView(lBox.SelectedItem);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            ViewModel.KeyDown(e.Key, true);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            ViewModel.KeyDown(e.Key, false);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            if (ViewModel.AutosaveWhenClosing)
                ViewModel.SaveAccounts();
            else
            {
                if (MessageBox.Show("Do you want to save all accounts in the accounts list, to the default storage location? " +
                    "(if you're debugging, this could replace all real accounts with a list of test accounts which would be bad)",
                    "Save accounts to default/main accounts storage location?",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    ViewModel.SaveAccounts();
                }
            }
            Environment.Exit(0);
        }
    }
}