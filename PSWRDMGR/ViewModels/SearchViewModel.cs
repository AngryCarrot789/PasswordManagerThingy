using PSWRDMGR.Controls;
using PSWRDMGR.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSWRDMGR.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        private int _selectedIndex;
        public ObservableCollection<AccountListItem> AccountsList { get; set; }

        public int SelectedIndex
        {
            get => _selectedIndex; set => RaisePropertyChanged(ref _selectedIndex, value);
        }

        public SearchViewModel()
        {
            AccountsList = new ObservableCollection<AccountListItem>();
        }

        public void AddAccount(AccountListItem account)
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
    }
}
