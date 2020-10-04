using PSWRDMGR.Utilities;
using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace PSWRDMGR.Login
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; set; }

        public Action<SecureString> TryLoginCallback { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new CommandParam<IPassword>(TryLogin);
        }

        public void TryLogin(IPassword password)
        {
            if (password.Password != null)
                TryLoginCallback?.Invoke(password.Password);
            else
                MessageBox.Show("Password is null");
        }
    }
}
