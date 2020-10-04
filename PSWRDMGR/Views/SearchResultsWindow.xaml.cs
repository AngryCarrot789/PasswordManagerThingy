using PSWRDMGR.Controls;
using PSWRDMGR.Search;
using System.Windows;
using System.Windows.Input;

namespace PSWRDMGR.Views
{
    /// <summary>
    /// Interaction logic for SearchResultsWindow.xaml
    /// </summary>
    public partial class SearchResultsWindow : Window
    {
        public SearchViewModel SearchContext
        {
            get => this.DataContext as SearchViewModel;
            set => this.DataContext = value;
        }
        public SearchResultsWindow()
        {
            InitializeComponent();
        }

        public void AddRealAccount(AccountControlViewModel account)
        {
            SearchContext.AddTempItem(account);
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
