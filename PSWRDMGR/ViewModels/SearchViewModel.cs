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
        private ObservableCollection<AccountListItem> list = new ObservableCollection<AccountListItem>();
        private int _selectedIndex;

        public ObservableCollection<AccountListItem> AccountsList
        {
            get => list; set => RaisePropertyChanged(ref list, value);
        }

        public int SelectedIndex
        {
            get => _selectedIndex; set => RaisePropertyChanged(ref _selectedIndex, value);
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
