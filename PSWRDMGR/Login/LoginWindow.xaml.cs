using System.Security;
using System.Windows;

namespace PSWRDMGR.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, IPassword
    {
        public SecureString Password => PasswordInput.SecurePassword;

        public LoginViewModel Login
        {
            get => this.DataContext as LoginViewModel;
            set => this.DataContext = value;
        }

        public LoginWindow()
        {
            InitializeComponent();
            Login = new LoginViewModel();
        }
    }
}
