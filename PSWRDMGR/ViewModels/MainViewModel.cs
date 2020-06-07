using PSWRDMGR.AccountStructures;
using PSWRDMGR.Controls;
using PSWRDMGR.Utilities;
using PSWRDMGR.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using static PSWRDMGR.Accounts.Accounts;

namespace PSWRDMGR.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        //Private fields
        private int selectedIndex;
        private bool enableSaveLoad;
        private bool autoSave;
        private bool contentPanelShowing;
        private AccountModel _selectedAccStr = new AccountModel();

        public ObservableCollection<AccountListItem> AccountsList { get; set; }
        public bool AutosaveWhenClosing
        {
            get => autoSave;
            set => RaisePropertyChanged(ref autoSave, value);
        }
        public int SelectedIndex
        {
            get => selectedIndex;
            set { RaisePropertyChanged(ref selectedIndex, value, UpdateSelectedItem); }
        }
        public bool EnableSaveAndLoad
        {
            get => enableSaveLoad;
            set => RaisePropertyChanged(ref enableSaveLoad, value);
        }
        public bool ContentPanelShowing
        {
            get => contentPanelShowing;
            set => RaisePropertyChanged(ref contentPanelShowing, value);
        }
        private void UpdateSelectedItem()
        {
            if (SelectedAccountItem != null && SelectedAccountItem.DataContext != null)
            {
                SelectedAccountStructure = SelectedAccountItem.DataContext as AccountModel;
            }
        }
        public AccountListItem SelectedAccountItem
        {
            get => AccountsList != null && SelectedIndex >= 0 && AccountsList.Count > 0? AccountsList[SelectedIndex] : null;
        }
        public AccountModel SelectedAccountStructure
        {
            get => _selectedAccStr;
            set => RaisePropertyChanged(ref _selectedAccStr, value);
        }

        public ICommand ShowAddAccountWindowCommand { get; set; }
        public ICommand ShowEditAccountWindowCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }
        public ICommand SaveAccountCommand { get; set; }
        public ICommand LoadAccountCommand { get; set; }
        public ICommand SearchAccountCommand { get; set; }
        public ICommand MoveAccountPositionCommand { get; set; }
        public ICommand ShowHelpWindowCommand { get; set; }
        public ICommand AutoShowContentPanelCommand { get; set; }
        public ICommand CopyDetailsCommand { get; set; }

        public ICommand CreateCustomDirectoryCommand { get; set; }
        public ICommand LoadCustomDirectoryCommand { get; set; }
        public ICommand SaveCustomDirectoryCommand { get; set; }

        //Some helpers
        public bool[] KeysDown = new bool[200];
        public bool AccountsArePresent => AccountsList.Count > 0;
        public bool AccountIsSelected { get => SelectedIndex > -1; }

        //this isnt technically very mvmv-ey... but eh
        public NewAccountWindow NewAccountWndow { get; set; }
        public ControlsWindow ControlsWndow { get; set; }
        public SearchResultsWindow SearchWindow { get; set; }

        public Action ShowContentPanel { get; set; }
        public Action HideContentPanel { get; set; }
        public Action ScrollIntoView { get; set; }

        public MainViewModel()
        {
            AccountsList = new ObservableCollection<AccountListItem>();
            NewAccountWndow = new NewAccountWindow();
            ControlsWndow = new ControlsWindow();
            SearchWindow = new SearchResultsWindow();
            SearchWindow.SearchContext.GetAccountItems = SetSearchAccountItems;
            SetupCommandBindings();
            AutosaveWhenClosing = true;
            NewAccountWndow.AddAccountCallback = this.AddAccount;
            LoadAccounts();
            UpdateSelectedItem();
        }

        private void SetupCommandBindings()
        {
            ShowAddAccountWindowCommand = new Command(ShowAddAccountWindow);
            ShowEditAccountWindowCommand = new Command(ShowEditAccountWindow);
            DeleteAccountCommand = new Command(DeleteSelectedAccount);
            SaveAccountCommand = new Command(SaveAccounts);
            LoadAccountCommand = new Command(LoadAccounts);
            SearchAccountCommand = new Command(SearchAccount);
            MoveAccountPositionCommand = new CommandParam(MoveAccPos);
            ShowHelpWindowCommand = new Command(ShowHelpWindow);
            AutoShowContentPanelCommand = new Command(SetContentPanelVisibility);
            CopyDetailsCommand = new CommandParam(CopyDetailsToClipboard);

            CreateCustomDirectoryCommand = new Command(AccountFileCreator.CreateAccountsDirectoryAndFiles);
            LoadCustomDirectoryCommand = new Command(LoadCustomAccounts);
            SaveCustomDirectoryCommand = new Command(SaveCustomAccounts);
        }

        private void SetSearchAccountItems()
        {
            foreach (AccountListItem accItem in AccountsList)
            {
                SearchWindow.AddRealAccount(accItem.Duplicate());
            }
        }

        public void SearchAccount()
        {
            ShowSearchWindow();
        }

        //helper. converts the "index" of a Key to an int. e.g, A = 1, c = 3.
        private int KeyInt(Key key) => (int)key;

        public void KeyDown(Key key, bool keyIsDown)
        {
            KeysDown[KeyInt(key)] = keyIsDown;
            ProcessKeyInputs();
        }

        public void ProcessKeyInputs()
        {
            if (KeysDown[KeyInt(Key.LeftCtrl)])
            {
                //CTRL Pressed
                if (KeysDown[KeyInt(Key.A)]) ShowAddAccountWindow();
                if (KeysDown[KeyInt(Key.S)] && EnableSaveAndLoad) SaveAccounts();
                if (KeysDown[KeyInt(Key.E)]) ShowEditAccountWindow();
                if (KeysDown[KeyInt(Key.L)] && EnableSaveAndLoad) LoadAccounts();
                if (KeysDown[KeyInt(Key.K)]) SetContentPanelVisibility();
            }
            else
            {
                //CTRL Released
                if (KeysDown[KeyInt(Key.Delete)]) DeleteSelectedAccount();
                if (KeysDown[KeyInt(Key.Left)]) SetContentPanelVisibility();
            }
        }

        #region Saving, Loading

        public void SaveAccounts()
        {
            List<AccountModel> oeoe = new List<AccountModel>();
            foreach (AccountListItem item in AccountsList)
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
            foreach (AccountModel accounts in AccountLoader.LoadFiles())
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

        #endregion

        #region Adding, Editing and Deleting Accounts

        public void AddAccount() { AddAccount(NewAccountWndow.AccountModel); }
        public void AddAccount(AccountModel accountContent)
        {
            //e
            AccountListItem ali = CreateAccountItem(accountContent);

            AccountsList.Add(ali);
            NewAccountWndow.ResetAccountContext();
        }

        public AccountListItem CreateAccountItem(AccountModel model)
        {
            return new AccountListItem
            {
                DataContext = model,
                AutoShowContentCallback = this.ShowAccountContent,
                EditContentCallback = this.ShowEditAccountWindow
            };
        }

        public void DeleteSelectedAccount()
        {
            if (AccountIsSelected && AccountsArePresent) AccountsList.RemoveAt(SelectedIndex);
        }

        public void ShowAddAccountWindow()
        {
            NewAccountWndow.Show();
            NewAccountWndow.Focus();
        }

        public void ShowEditAccountWindow(AccountModel account)
        {
            if (account != null)
            {
                SelectedAccountStructure = account;
            }
        }

        public void ShowEditAccountWindow()
        {
            ShowAccountContent(SelectedAccountStructure);
            UpdateSelectedItem();
        }

        public void ShowAccountContent(AccountModel account)
        {
            if (account != null)
            {
                if (!ContentPanelShowing) ShowContentPanelFunc();
                SelectedAccountStructure = account;
            }
        }

        public void ShowHelpWindow()
        {
            ControlsWndow.Show();
            ControlsWndow.Focus();
        }

        public void ShowSearchWindow()
        {
            SearchWindow.Show();
            SearchWindow.Focus();
        }

        #endregion


        public void ClearAccountsList()
        {
            SelectedIndex = 0;
            AccountsList.Clear();
        }

        public void MoveAccPos(object upordown)
        {
            switch (int.Parse(upordown.ToString()))
            {
                //UP
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

        public void CloseAllWindows()
        {
            NewAccountWndow.Close();
            ControlsWndow.Close();
            SearchWindow.Close();
        }

        public void SetContentPanelVisibility()
        {
            if (ContentPanelShowing)
                HideContentPanelFunc();
            else
                ShowContentPanelFunc();
        }

        public void ShowContentPanelFunc()
        {
            if (!ContentPanelShowing)
            {
                ShowContentPanel?.Invoke();
                ContentPanelShowing = true;
            }
        }

        public void HideContentPanelFunc()
        {
            if (ContentPanelShowing)
            {
                HideContentPanel?.Invoke();
                ContentPanelShowing = false;
            }
        }

        public void CopyDetailsToClipboard(object detailsIndex)
        {
            AccountModel currSelectedAcc = SelectedAccountStructure;
            switch (int.Parse(detailsIndex.ToString()))
            {
                case 0: Clipboard.SetText(currSelectedAcc.Email); break;
                case 1: Clipboard.SetText(currSelectedAcc.Username); break;
                case 2: Clipboard.SetText(currSelectedAcc.Password); break;
                case 3: Clipboard.SetText(currSelectedAcc.DateOfBirth); break;
                case 4: Clipboard.SetText(currSelectedAcc.SecurityInfo); break;
                case 5: Clipboard.SetText(currSelectedAcc.ExtraInfo1); break;
                case 6: Clipboard.SetText(currSelectedAcc.ExtraInfo2); break;
                case 7: Clipboard.SetText(currSelectedAcc.ExtraInfo3); break;
                case 8: Clipboard.SetText(currSelectedAcc.ExtraInfo4); break;
                case 9: Clipboard.SetText(currSelectedAcc.ExtraInfo5); break;
            }
        }
    }
}
