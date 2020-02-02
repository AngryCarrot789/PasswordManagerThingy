using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Button = Xamarin.Forms.Button;

namespace AndroidPSWRDMGR.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountListViewItem : ContentPage
	{
        //public Action<AccountModel> ShowContentCallback { get; set; }
        //public Action FocusCallback { get; set; }
        //private AccountModel AccountContext { get => this.DataContext as AccountModel; }
        public AccountListViewItem ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            //Application.Current.MainPage.DisplayAlert("Alert", "", "Ok");
            try
            {

                switch (int.Parse(((Button)sender).Id.ToString()))
                {
                    case 1: base.DisplayAlert("Alert", "Username", "Ok"); break; //Clipboard.SetText(AccountContext.Username); break;
                    case 2: base.DisplayAlert("Alert", "Password", "Ok"); break; //Clipboard.SetText(AccountContext.Password); break;
                    case 3: base.DisplayAlert("Alert", "Email", "Ok"); break; //Clipboard.SetText(AccountContext.Email); break;
                }
            }
            catch (Exception ed) { base.DisplayAlert("Alert", "eeeeeeeeeeeee", "Ok"); } 
            //{ MessageBox.Show($"Failed to copy information to clipboard. Error: {ed.Message}", "Err Clipboard Set"); }
        }

        //private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    ShowContentCallback?.Invoke(AccountContext);
        //    FocusCallback?.Invoke();
        //}
    }
}