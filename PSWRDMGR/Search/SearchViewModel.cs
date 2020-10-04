using PSWRDMGR.AccountStructures;
using PSWRDMGR.Controls;
using PSWRDMGR.Search;
using PSWRDMGR.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PSWRDMGR.Search
{
    public class SearchViewModel : BaseViewModel
    {
        private SearchFindTypes _filter;
        private string _searchFor;
        private int _selectedIndex;

        public SearchFindTypes Filter
        {
            get => _filter;
            set => RaisePropertyChanged(ref _filter, value);
        }

        public string SearchFor
        {
            get => _searchFor;
            set => RaisePropertyChanged(ref _searchFor, value);
        }

        public int SelectedIndex
        {
            get => _selectedIndex; set => RaisePropertyChanged(ref _selectedIndex, value);
        }

        public List<AccountControlViewModel> TempItems { get; set; }
        public ObservableCollection<AccountControlViewModel> AccountsList { get; set; }

        public ICommand SearchCommand { get; private set; }

        // i know this is a really bad way to do it
        // but i programmed this late at night...
        public Action GetAccountItems { get; set; }

        public SearchViewModel()
        {
            AccountsList = new ObservableCollection<AccountControlViewModel>();
            TempItems = new List<AccountControlViewModel>();
            SearchCommand = new Command(Search);
            Filter = SearchFindTypes.AccountName;
        }

        public void AddTempItem(AccountControlViewModel account)
        {
            TempItems.Add(account);
        }

        public void AddAccount(AccountControlViewModel account)
        {
            AccountsList.Add(account);
        }

        public void RemoveSelectedAccount()
        {
            AccountsList.RemoveAt(SelectedIndex);
        }

        public void ClearAccountsList()
        {
            SelectedIndex = 0;
            AccountsList.Clear();
        }

        public void Search()
        {
            ClearAccountsList();
            GetAccountItems?.Invoke();
            List<AccountControlViewModel> items = new List<AccountControlViewModel>();
            SearchFor.Trim();
            string searchFor = SearchFor.ToLower();
            foreach (AccountControlViewModel accountItm in TempItems)
            {
                if (accountItm?.Account != null && !string.IsNullOrEmpty(searchFor))
                {
                    AccountViewModel account = accountItm.Account;
                    switch (Filter)
                    {
                        case SearchFindTypes.AccountName:
                            if (account.AccountName.ToLower().Contains(searchFor))
                                items.Add(accountItm);
                            break;
                        case SearchFindTypes.Email:
                            if (account.Email.ToLower().Contains(searchFor))
                                items.Add(accountItm);
                            break;
                        case SearchFindTypes.Username:
                            if (account.Username.ToLower().Contains(searchFor))
                                items.Add(accountItm);
                            break;
                        case SearchFindTypes.Password:
                            if (account.Password.ToLower().Contains(searchFor))
                                items.Add(accountItm);
                            break;
                        case SearchFindTypes.DateOfBirth:
                            if (account.DateOfBirth.ToLower().Contains(searchFor))
                                items.Add(accountItm);
                            break;
                        case SearchFindTypes.SecurityInfo:
                            if (account.SecurityInfo.ToLower().Contains(searchFor))
                                items.Add(accountItm);
                            break;
                        case SearchFindTypes.ExtraInfo1:
                            if (account.ExtraInfo1.ToLower().Contains(searchFor))
                                items.Add(accountItm);
                            break;
                        case SearchFindTypes.ExtraInfo2:
                            if (account.ExtraInfo2.ToLower().Contains(searchFor))
                                items.Add(accountItm);
                            break;
                        case SearchFindTypes.ExtraInfo3:
                            if (account.ExtraInfo3.ToLower().Contains(searchFor))
                                items.Add(accountItm);
                            break;
                        case SearchFindTypes.ExtraInfo4:
                            if (account.ExtraInfo4.ToLower().Contains(searchFor))
                                items.Add(accountItm);
                            break;
                        case SearchFindTypes.ExtraInfo5:
                            if (account.ExtraInfo5.ToLower().Contains(searchFor))
                                items.Add(accountItm);
                            break;
                    }
                    AccountsList.Clear();
                    foreach(AccountControlViewModel accountItem in items)
                    {
                        AddAccount(accountItem);
                    }
                }
            }
        }
    }
}
