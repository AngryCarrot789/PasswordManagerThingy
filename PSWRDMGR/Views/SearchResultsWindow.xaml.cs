using PSWRDMGR.AccountStructures;
using PSWRDMGR.Controls;
using PSWRDMGR.Search;
using PSWRDMGR.Utilities;
using PSWRDMGR.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SearchResultsWindow.xaml
    /// </summary>
    public partial class SearchResultsWindow : Window
    {
        public SearchViewModel SearchContext { get => this.DataContext as SearchViewModel; }
        public SearchResultsWindow()
        {
            InitializeComponent();
        }

        public void AddRealAccount(AccountListItem account)
        {
            SearchContext.AddRealAccount(account);
        }

        public void Search()
        {
            SearchContext.Search();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            SearchContext.ClearAccountsList();
            this.Hide();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
            if (e.Key == Key.Enter)
                this.Close();
        }
    }
}
