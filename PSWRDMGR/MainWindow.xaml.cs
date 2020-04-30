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
using System.Windows.Media.Animation;
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
        public MainViewModel ViewModel { get; set; }
        public MainWindow(App currAp)
        {
            CurrentApp = currAp;
            InitializeComponent();
            ViewModel = new MainViewModel();
            this.DataContext = ViewModel;
            ViewModel.ScrollIntoView = ScrollIntoViewThingy;
            ViewModel.ShowContentPanel = this.ShowContentPanel;
            ViewModel.HideContentPanel = this.HideContentPanel;

            HideContentPanel();

            LoadSettings();
            //ViewModel.GetWindowVariables = SetViewModelVariables;
        }

        public static void SetTheme(Theme theme)
        {
            CurrentApp.SetTheme(theme);
        }

        public void SetViewModelVariables()
        {
            //ViewModel.WindowHeight = this.ActualHeight;
            //ViewModel.WindowWidth = this.ActualWidth;
            //ViewModel.WindowTop = this.Top;
            //ViewModel.WindowLeft= this.Left;
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

        public void AnimateContentPanelWidth(double oldWidth, double newWidth)
        {
            DoubleAnimation da = new DoubleAnimation(oldWidth, newWidth, TimeSpan.FromSeconds(0.2))
            {
                AccelerationRatio = 0,
                DecelerationRatio = 1,
            };
            AccountPanel.BeginAnimation(WidthProperty, da);
        }

        public void ShowContentPanel()
        {
            AnimateContentPanelWidth(0, 500);
        }

        public void HideContentPanel()
        {
            AnimateContentPanelWidth(500, 0);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //e.Cancel = true;
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
            ViewModel.CloseAllWindows();

            SaveSettings();
            //this.Close();
            //Environment.Exit(0);
        }

        public void SaveSettings()
        {
            if (WindowState == WindowState.Maximized)
            {
                // Use the RestoreBounds as the current values will be 0, 0 and the size of the screen
                Properties.Settings.Default.Top = RestoreBounds.Top;
                Properties.Settings.Default.Left = RestoreBounds.Left;
                Properties.Settings.Default.Height = RestoreBounds.Height;
                Properties.Settings.Default.Width = RestoreBounds.Width;
                Properties.Settings.Default.Maximized = true;
            }
            else
            {
                Properties.Settings.Default.Top = this.Top;
                Properties.Settings.Default.Left = this.Left;
                Properties.Settings.Default.Height = this.Height;
                Properties.Settings.Default.Width = this.Width;
                Properties.Settings.Default.Maximized = false;
            }

            Properties.Settings.Default.IsDarkTheme = ViewModel.DarkThemeEnabled;
            Properties.Settings.Default.Save();
        }

        public void LoadSettings()
        {
            this.Top = Properties.Settings.Default.Top;
            this.Left = Properties.Settings.Default.Left;
            this.Height = Properties.Settings.Default.Height;
            this.Width = Properties.Settings.Default.Width;
            ViewModel.DarkThemeEnabled =  Properties.Settings.Default.IsDarkTheme;
            // Very quick and dirty - but it does the job
            if (Properties.Settings.Default.Maximized)
            {
                WindowState = WindowState.Maximized;
            }
        }
    }
}