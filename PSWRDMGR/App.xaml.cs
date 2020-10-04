using PSWRDMGR.Login;
using System;
using System.Security;
using System.Windows;

namespace PSWRDMGR
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public enum Theme
        {
            Light, ColourfulLight,
            Dark, ColourfulDark
        }

        private void SetThemeDictionary(ResourceDictionary value)
        {
            Resources.MergedDictionaries[0] = value;
        }

        private void ChangeTheme(Uri uri)
        {
            SetThemeDictionary(new ResourceDictionary() { Source = uri });
        }
        public void SetTheme(Theme theme)
        {
            string themeName = null;
            switch (theme)
            {
                case Theme.Dark: themeName = "DarkTheme"; break;
                case Theme.Light: themeName = "LightTheme"; break;
                case Theme.ColourfulDark: themeName = "ColourfulDarkTheme"; break;
                case Theme.ColourfulLight: themeName = "ColourfulLightTheme"; break;
            }

            try
            {
                if (!string.IsNullOrEmpty(themeName))
                    ChangeTheme(new Uri($"Themes/{themeName}.xaml", UriKind.Relative));
            }
            catch { }
        }

        private LoginWindow LoginWindow { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            LoginWindow = new LoginWindow();
            LoginWindow.Login.TryLoginCallback = TryLogin;
            LoginWindow.Show();
        }

        public void TryLogin(SecureString pWrd)
        {
            // for now the password is just your username
            if (pWrd.GetUnsecure() == Environment.UserName)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                MainWindow = mw;
                LoginWindow.Close();
                MainWindow = mw;
            }
            else
            {
                MessageBox.Show("wrong :((");
            }
        }
    }
}
