using AndroidVersion.Utilities;
//using Plugin.Permissions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Android.Content.PM;
using PSWRDMGR;
using PSWRDMGR.Accounts;
using static PSWRDMGR.Accounts.Accounts;

namespace AndroidVersion.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        //private ObservableCollection<AccountListViewItem> _accs = new ObservableCollection<AccountListViewItem>();
        //public ObservableCollection<AccountListViewItem> Accounts
        //{
        //    get => _accs;
        //    set => RaisePropertyChanged(ref _accs, value);

        //
        //private async void GetPermissionStuff()
        //{
        //    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
        //
        //    if (status != PermissionStatus.Granted)
        //    {
        //        if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
        //        {
        //            await mainPage.DisplayAlert("Need storage permissions", "Need storage permissions", "OK");
        //        }
        //
        //        var result = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
        //
        //        if (result.ContainsKey(Permission.Storage))
        //        {
        //            status = result[Permission.Storage];
        //        }
        //    }
        //}

        public ICommand ShowContentCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }
        public ICommand SaveAccountCommand { get; set; }
        public ICommand LoadAccountCommand { get; set; }
        public ICommand BackupAccountsCommand { get; set; }

        private ObservableCollection<AccountModel> _accs = new ObservableCollection<AccountModel>();
        public ObservableCollection<AccountModel> AccountsList
        {
            get => _accs;
            set => RaisePropertyChanged(ref _accs, value);
        }

        public MainViewModel()
        {

        }

        #region Saving and loading

        public void SaveAccounts()
        {
            List<AccountModel> oeoe = new List<AccountModel>();
            foreach (AccountModel item in AccountsList)
            {
                oeoe.Add(item);
            }
            AccountSaver.SaveDefaultFiles(oeoe);
        }

        public void BackupAccounts()
        {
            List<AccountModel> oeoe = new List<AccountModel>();
            foreach (AccountModel item in AccountsList)
            {
                oeoe.Add(item);
            }
            AccountSaver.SaveBackupFiles(oeoe);
        }

        public void LoadAccounts()
        {
            ClearAccountsList();
            foreach (AccountModel accounts in AccountLoader.LoadFiles())
            {
                AddAccount(accounts);
            }
        }

        #endregion

        private void AddAccount(AccountModel accounts)
        {
            AccountsList.Add(accounts);
        }

        private void ClearAccountsList()
        {
            AccountsList.Clear();
        }
    }
}
