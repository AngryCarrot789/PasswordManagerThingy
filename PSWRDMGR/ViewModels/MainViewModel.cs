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
        private ObservableCollection<ListBoxItem> list = new ObservableCollection<ListBoxItem>();
        private int selectedIndex;
        //Public Fields
        public ObservableCollection<ListBoxItem> AccountsList { get => list; set { list = value; RaisePropertyChanged(); } }
        public int SelectedIndex { get => selectedIndex; set { selectedIndex = value; RaisePropertyChanged(); } }
        public AccountInformationPresenter AccountPresenter { get; set; }
        public NewAccountWindow NewAccountWndow{ get; set; }
        public EditAccountWindow EditAccountWndow { get; set; }

        public AccountListItem SelectedAccountItem { get { try { return AccountsList[SelectedIndex].Content as AccountListItem; } catch { return null; } } }
        public AccountModel SelectedAccountStructure { get { try { return SelectedAccountItem.DataContext as AccountModel; } catch { return null; } } }

        //Commands
        public ICommand ShowContentCommand { get; set; }
        public ICommand ShowAddAccountWindowCommand { get; set; }
        public ICommand ShowEditAccountWindowCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }
        public ICommand SaveAccountCommand { get; set; }
        public ICommand LoadAccountCommand { get; set; }

        public MainViewModel()
        {
            ShowContentCommand = new Command(ShowAccountContent);
            ShowAddAccountWindowCommand = new Command(ShowAddAccountWindow);
            ShowEditAccountWindowCommand = new Command(ShowEditAccountWindow);
            DeleteAccountCommand = new Command(DeleteSelectedAccount);
            SaveAccountCommand = new Command(SaveAccounts);
            LoadAccountCommand = new Command(LoadAccounts);
            AccountPresenter = new AccountInformationPresenter();
            NewAccountWndow = new NewAccountWindow();
            EditAccountWndow = new EditAccountWindow();

            NewAccountWndow.AddAccountCallback = this.AddAccount;
            EditAccountWndow.EditAccountCallback = this.EditAccount;
        }

        public void SaveAccounts()
        {
            List<AccountModel> oeoe = new List<AccountModel>();
            foreach(ListBoxItem item in AccountsList)
            {
                AccountModel m = (item.Content as AccountListItem).DataContext as AccountModel;
                oeoe.Add(m);
            }
            AccountSaver.SaveFiles(oeoe);
        }

        public void LoadAccounts()
        {
            ClearAccountsList();
            foreach(AccountModel accounts in AccountLoader.LoadFiles())
            {
                AddAccount(accounts);
            }
        }

        public void DeleteSelectedAccount()
        {
            AccountsList.RemoveAt(SelectedIndex);
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
        public void AddAccount() { AddAccount(NewAccountWndow.AccountModel.Convert()); }
        public void EditAccount() { EditAccount(EditAccountWndow.AccountModel.Convert()); }
        public void AddAccount(AccountModel accountContent)
        {
            AccountListItem ali = new AccountListItem();
            ali.DataContext = accountContent;
            ali.ShowContentCallback = this.ShowAccountContent;

            ListBoxItem lbi = new ListBoxItem() { Content = ali };
            AccountsList.Add(lbi);
        }

        public void EditAccount(AccountModel accountContent)
        {
            if (accountContent != null)
            {
                SelectedAccountItem.DataContext = accountContent;
            }
        }
    }
}
