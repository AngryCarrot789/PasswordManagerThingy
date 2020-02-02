using AndroidPSWRDMGR.Controls;
using AndroidVersion.Utilities;
using Plugin.Permissions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Android.Content.PM;

namespace AndroidVersion.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        //private ObservableCollection<AccountListViewItem> _accs = new ObservableCollection<AccountListViewItem>();
        //public ObservableCollection<AccountListViewItem> Accounts
        //{
        //    get => _accs;
        //    set => RaisePropertyChanged(ref _accs, value);
        //}

        public ICommand AddItem { get; set; }

        private ObservableCollection<string> _accs = new ObservableCollection<string>();
        public ObservableCollection<string> Accounts
        {
            get => _accs;
            set => RaisePropertyChanged(ref _accs, value);
        }

        private async void GetPermissionStuff()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                {
                    await mainPage.DisplayAlert("Need storage permissions", "Need storage permissions", "OK");
                }

                var result = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);

                if (result.ContainsKey(Permission.Storage))
                {
                    status = result[Permission.Storage];
                }
            }
        }

        public MainViewModel()
        {

            AddItem = new Command(_AddItem);
            Accounts.Add("init test");
            GetPermissionStuff();

            string tt = $"{Xamarin.Essentials.FileSystem.AppDataDirectory}";
            Accounts.Add(File.Exists(Xamarin.Essentials.FileSystem.AppDataDirectory) ? "Exists" : "Doesnt exist");
            //Accounts.Add(File.Exists("/system") ? "system Exists" : "system doesn't exist");

            //foreach(string file in Directory.GetFiles(@"/system"))
            //{
            //    Accounts.Add(file);
            //}

            var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "count.txt");
            //File.WriteAllText(backingFile, "jkiwejkiwejkjkewijkiwejk");

            Accounts.Add(File.ReadAllText(backingFile));
        }

        private void _AddItem()
        {
            Accounts.Add(Accounts.Count.ToString());
        }
    }
}
