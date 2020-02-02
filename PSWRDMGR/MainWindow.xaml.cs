using PSWRDMGR.ViewModels;
using PSWRDMGR.Views;
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
using static PSWRDMGR.App;

namespace PSWRDMGR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static App CurrentApp;
        public MainViewModel ViewModel { get => DataContext as MainViewModel; }
        public MainWindow(App currAp)
        {
            CurrentApp = currAp;
            InitializeComponent();
            ViewModel.ScrollIntoView = ScrollIntoViewThingy;
            //ViewModel.GetWindowVariables = SetViewModelVariables;
        }

        public static void SetTheme(Theme theme)
        {
            CurrentApp.SetTheme(theme);
        }

        //public void SetViewModelVariables()
        //{
        //    ViewModel.WindowHeight = this.ActualHeight;
        //    ViewModel.WindowWidth = this.ActualWidth;
        //    ViewModel.WindowTop = this.Top;
        //    ViewModel.WindowLeft= this.Left;
        //}

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