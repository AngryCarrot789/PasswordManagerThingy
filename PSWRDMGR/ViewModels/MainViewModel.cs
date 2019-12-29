using PSWRDMGR.Controls;
using PSWRDMGR.Utilities;
using PSWRDMGR.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using static PSWRDMGR.Accounts.Accounts;

namespace PSWRDMGR.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        //Private fields
        private ObservableCollection<AccountListItem> list = new ObservableCollection<AccountListItem>();
        private int selectedIndex;
        private bool enableSaveLoad;
        private string srchAccText;
        //Public Fields
        public bool AccountsArePresent => AccountsList.Count > 0;
        public bool AccountIsSelected { get => SelectedIndex > -1; }
        public ObservableCollection<AccountListItem> AccountsList { get => list; set { list = value; RaisePropertyChanged(); } }
        public int SelectedIndex { get => selectedIndex; set { selectedIndex = value; RaisePropertyChanged(); } }
        public bool EnableSaveAndLoad { get => enableSaveLoad; set { enableSaveLoad = value; RaisePropertyChanged(); } }
        public bool CTRLPressed;
        public bool ALTPressed;
        public string SearchAccountText { get => srchAccText; set { srchAccText = value; RaisePropertyChanged(); } }
        public AccountInformationPresenter AccountPresenter { get; set; }
        public NewAccountWindow NewAccountWndow{ get; set; }
        public EditAccountWindow EditAccountWndow { get; set; }

        public AccountListItem SelectedAccountItem { get { try { return AccountsList[SelectedIndex]; } catch { return null; } } }
        public AccountModel SelectedAccountStructure { get { try { return SelectedAccountItem.DataContext as AccountModel; } catch { return null; } } }
        public Action ScrollIntoView { get; set; }
        //Commands
        public ICommand ShowContentCommand { get; set; }
        public ICommand ShowAddAccountWindowCommand { get; set; }
        public ICommand ShowEditAccountWindowCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }
        public ICommand SaveAccountCommand { get; set; }
        public ICommand LoadAccountCommand { get; set; }
        public ICommand BackupAccountsCommand { get; set; }
        public ICommand SearchAccountCommand { get; set; }
        public ICommand MoveAccountPositionCommand { get; set; }

        public ICommand CreateCustomDirectoryCommand { get; set; }
        public ICommand LoadCustomDirectoryCommand   { get; set; }
        public ICommand SaveCustomDirectoryCommand   { get; set; }

        private void SetupCommandBindings()
        {
            ShowContentCommand = new Command(ShowAccountContent);
            ShowAddAccountWindowCommand = new Command(ShowAddAccountWindow);
            ShowEditAccountWindowCommand = new Command(ShowEditAccountWindow);
            DeleteAccountCommand = new Command(DeleteSelectedAccount);
            SaveAccountCommand = new Command(SaveAccounts);
            LoadAccountCommand = new Command(LoadAccounts);
            BackupAccountsCommand = new Command(BackupAccounts);
            SearchAccountCommand = new Command(SearchAccount);
            MoveAccountPositionCommand = new CommandParam(MoveAccPos);

            CreateCustomDirectoryCommand = new Command(AccountFileCreator.CreateAccountsDirectoryAndFiles);
            LoadCustomDirectoryCommand = new Command(LoadCustomAccounts);
            SaveCustomDirectoryCommand = new Command(SaveCustomAccounts);
        }

        public MainViewModel()
        {
            SetupCommandBindings();
            AccountPresenter = new AccountInformationPresenter();
            NewAccountWndow = new NewAccountWindow();
            EditAccountWndow = new EditAccountWindow();

            NewAccountWndow.AddAccountCallback = this.AddAccount;

            LoadAccounts();
        }

        public void SearchAccount()
        {
            //SearchAccountText
            int index = 0;
            foreach(AccountListItem accItem in AccountsList)
            {
                AccountModel account = accItem.DataContext as AccountModel;
                if (!string.IsNullOrEmpty(SearchAccountText))
                {
                    if (account.AccountName.ToLower().Contains(SearchAccountText.ToLower()))
                    {
                        SelectedIndex = index;
                        ScrollIntoView?.Invoke();
                        break;
                    }
                }
                index++;
            }
        }

        public void BackupAccounts()
        {
            List<AccountModel> oeoe = new List<AccountModel>();
            foreach (AccountListItem item in AccountsList)
            {
                AccountModel m = item.DataContext as AccountModel;
                oeoe.Add(m);
            }
            AccountSaver.SaveBackupFiles(oeoe);
        }

        public void KeyDown(Key key)
        {
            if (key == Key.LeftCtrl)
                CTRLPressed = true;
            else CTRLPressed = false;

            if (key == Key.LeftAlt)
                ALTPressed = true;
            else ALTPressed = false;

            switch (key)
            {
                case Key.Delete: DeleteSelectedAccount(); break;
            }
        }

        public void SaveAccounts()
        {
            List<AccountModel> oeoe = new List<AccountModel>();
            foreach(AccountListItem item in AccountsList)
            {
                AccountModel m = item.DataContext as AccountModel;
                oeoe.Add(m);
            }
            AccountSaver.SaveFiles(oeoe);
        }

        public void SaveCustomAccounts()
        {
            List<AccountModel> oeoe = new List<AccountModel>();
            foreach (AccountListItem item in AccountsList)
            {
                AccountModel m = item.DataContext as AccountModel;
                oeoe.Add(m);
            }
            AccountSaver.SaveCustomFiles(oeoe);
        }

        public void LoadAccounts()
        {
            ClearAccountsList();
            foreach(AccountModel accounts in AccountLoader.LoadFiles())
            {
                AddAccount(accounts);
            }
        }

        public void LoadCustomAccounts()
        {
            ClearAccountsList();
            foreach (AccountModel accounts in AccountLoader.LoadCustomAccounts())
            {
                AddAccount(accounts);
            }
        }

        public void DeleteSelectedAccount()
        {
            if (AccountIsSelected && AccountsArePresent) AccountsList.RemoveAt(SelectedIndex);
        }

        public void ShowAddAccountWindow()
        {
            NewAccountWndow.Show();
        }

        public void ShowEditAccountWindow()
        {
            if (SelectedAccountStructure != null)
            {
                EditAccountWndow.DataContext = SelectedAccountStructure;
                EditAccountWndow.Show();
            }
        }

        public void ShowAccountContent()
        {
            AccountPresenter.DataContext = SelectedAccountStructure;
            AccountPresenter.Show();
        }

        public void ClearAccountsList()
        {
            SelectedIndex = 0;
            AccountsList.Clear();
        }

        public void ShowAccountContent(AccountModel account)
        {
            if (account != null)
            {
                AccountPresenter.DataContext = account;
                AccountPresenter.Show();
            }
        }

        public void MoveAccPos(object upordown)
        {
            switch (int.Parse(upordown.ToString()))
            {
                case 0:
                    if (SelectedIndex > 0)
                    {
                        AccountsList.Move(SelectedIndex, SelectedIndex - 1);
                    }
                    break;
                //Down
                case 1:
                    if (SelectedIndex + 1 < AccountsList.Count())
                    {
                        AccountsList.Move(SelectedIndex, SelectedIndex + 1);
                    }
                    break;
            }
            ScrollIntoView();
        }

        public void AddAccount() { AddAccount(NewAccountWndow.AccountModel); }
        public void AddAccount(AccountModel accountContent)
        {
            AccountListItem ali = new AccountListItem();
            ali.DataContext = accountContent;
            ali.ShowContentCallback = this.ShowAccountContent;

            AccountsList.Add(ali);

            NewAccountWndow.ResetAccountContext();
        }
    }
}
