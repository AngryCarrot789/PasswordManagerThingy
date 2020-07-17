using PSWRDMGR.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PSWRDMGR.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        // idk how to do security xd
        // this is probably easily hackable
        // probably... idk

        private string _password;

        public string Password
        {
            get => _password;
            set => RaisePropertyChanged(ref _password, value);
        }

        public ICommand LoginCommand { get; set; }

        public Action<string> TryLoginCallback { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(TryLogin);
        }

        public void TryLogin()
        {
            if (!string.IsNullOrEmpty(Password))
                TryLoginCallback?.Invoke(Password);
            else
                MessageBox.Show("Password is empty");
        }
    }
}
