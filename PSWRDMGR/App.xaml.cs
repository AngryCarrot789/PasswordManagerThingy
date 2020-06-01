using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
