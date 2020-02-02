using PSWRDMGR.Controls;
using PSWRDMGR.Utilities;
using PSWRDMGR.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private bool autoSave;
        private string themeName;
        private bool darkThemeEnabled;
        //Public Fields
        public AccountInformationPresenter AccountPresenter { get; set; }
        public NewAccountWindow NewAccountWndow { get; set; }
        public EditAccountWindow EditAccountWndow { get; set; }
        public ControlsWindow ControlsWndow { get; set; }
        public SearchResultsWindow SearchWindow { get; set; }

        //Some helpers
        public bool[] KeysDown = new bool[200];
        public bool AccountsArePresent => AccountsList.Count > 0;
        public bool AccountIsSelected { get => SelectedIndex > -1; }

        //public Action GetWindowVariables { get; set; }
        //public double WindowTop { get; set; }
        //public double WindowLeft { get; set; }
        //public double WindowWidth { get; set; }
        //public double WindowHeight { get; set; }

        public ObservableCollection<AccountListItem> AccountsList
        {
            get => list;
            set => RaisePropertyChanged(ref list, value);
        }
        public bool AutosaveWhenClosing
        {
            get => autoSave;
            set => RaisePropertyChanged(ref autoSave, value);
        }
        public int SelectedIndex
        {
            get => selectedIndex;
            set => RaisePropertyChanged(ref selectedIndex, value);
        }
        public bool EnableSaveAndLoad
        {
            get => enableSaveLoad;
            set => RaisePropertyChanged(ref enableSaveLoad, value);
        }
        public string SearchAccountText
        {
            get => srchAccText;
            set => RaisePropertyChanged(ref srchAccText, value);
        }
        public string ThemeName
        {
            get => themeName;
            set => RaisePropertyChanged(ref themeName, value);
        }
        public bool DarkThemeEnabled
        {
            get => darkThemeEnabled;
            set
            {
                RaisePropertyChanged(ref darkThemeEnabled, value);
                MainWindow.SetTheme(value ? App.Theme.Dark : App.Theme.Light);
                if (value) ThemeName = "Dark"; else ThemeName = "Light";
            }
        }

        public AccountListItem SelectedAccountItem { get { try { return AccountsList[SelectedIndex]; } catch { return null; } } }
        public AccountModel SelectedAccountStructure { get { try { return SelectedAccountItem.DataContext as AccountModel; } catch { return null; } } }

        public Action ScrollIntoView { get; set; }
        public Action<bool> SetThemeDark { get; set; }
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
        public ICommand ShowHelpWindowCommand { get; set; }

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
            ShowHelpWindowCommand = new Command(ShowHelpWindow);

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
            ControlsWndow = new ControlsWindow();
            SearchWindow = new SearchResultsWindow();
            AutosaveWhenClosing = true;
            DarkThemeEnabled = true;

            NewAccountWndow.AddAccountCallback = this.AddAccount;

            LoadAccounts();
        }

        public void SearchAccount()
        {
            SearchWindow.SearchContext.ClearAccountsList();
            ShowSearchWindow();
            //SearchAccountText
            int index = 0;
            foreach(AccountListItem accItem in AccountsList)
            {
                AccountModel account = accItem.DataContext as AccountModel;
                if (!string.IsNullOrEmpty(SearchAccountText))
                {
                    if (account.AccountName.ToLower().Contains(SearchAccountText.ToLower()))
                    {
                        SearchWindow.AddAccount(accItem);
                        //SelectedIndex = index;
                        //ScrollIntoView?.Invoke();
                    }
                }
                index++;
            }
        }

        //helper. converts the "index" of a Key to an int. e.g, A = 1, c = 3.
        private int KeyInt(Key key) => Convert.ToInt32(key);

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
                if (KeysDown[KeyInt(Key.K)]) ShowAccountContent();
            }
            else
            {
                //CTRL Released
                if (KeysDown[KeyInt(Key.Delete)]) DeleteSelectedAccount();
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
            AccountListItem ali = new AccountListItem();
            ali.DataContext = accountContent;
            ali.ShowContentCallback = this.ShowAccountContent;

            AccountsList.Add(ali);
            NewAccountWndow.ResetAccountContext();
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

        public void ShowEditAccountWindow()
        {
            if (SelectedAccountStructure != null)
            {
                EditAccountWndow.DataContext = SelectedAccountStructure;
                EditAccountWndow.Show();
                EditAccountWndow.Focus();
            }
        }

        public void ShowAccountContent()
        {
            AccountPresenter.DataContext = SelectedAccountStructure;
            AccountPresenter.Show();
            AccountPresenter.Focus();
        }

        public void ShowAccountContent(AccountModel account)
        {
            if (account != null)
            {
                AccountPresenter.DataContext = account;
                AccountPresenter.Show();
                AccountPresenter.Focus();
            }
        }

        public void ShowHelpWindow()
        {
            ControlsWndow.Show();
            ControlsWndow.Focus();
        }

        /// <summary>
        /// //////////////////////
        /// </summary>
        public void ShowSearchWindow()
        {
            SearchWindow.Show();
            SearchWindow.Focus();

            //double DesktopScreenWidth =  SystemParameters.PrimaryScreenWidth;
            //double DesktopScreenHeight = SystemParameters.PrimaryScreenHeight;
            //
            //double WindowFromLeft = WindowLeft;
            //double WindowFromTop = WindowTop;
            //double WindowFromRight = DesktopScreenWidth - WindowFromLeft;
            //double WindowFromBottom = DesktopScreenHeight - WindowFromTop;
            //
            //double leftOffset = 0;
            //double topOffset = 26;
            //26 is the height of the title bar for the mainwindow.
            //trying to make the searchwindow contents in the direct centre... well
            //the searchwindow and mainwindow title bar heights cancel eachother out
            //so the content is in the centre (if you include title bars) without an offset but eh.

            //double SearchWindowLeft = WindowLeft + ((WindowWidth - SearchWindow.Width) / 2) + leftOffset;
            //double SearchWindowTop = WindowTop + ((WindowHeight - SearchWindow.Height) / 2) + topOffset;

            //double windowCentreLeft = WindowWidth / 2;
            //double windowCentreTop = WindowHeight / 2;

            //double width = (screenWidth / 2) - (SearchWindowLeft / 2);
            //double height = (screenHeight / 2) - (SearchWindowTop / 2);

            //SearchWindow.Left = SearchWindowLeft;
            //SearchWindow.Top = SearchWindowTop;
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

        //public double GetCentralisedWindowTop(double windowheight) => WindowTop + ((WindowHeight - windowheight) / 2);
        //public double GetCentralisedWindowLeft(double windowwidth) => WindowLeft + ((WindowWidth - windowwidth) / 2);
    }
}
