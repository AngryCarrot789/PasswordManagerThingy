using PSWRDMGR.AccountStructures;
using PSWRDMGR.Utilities;
using System;
using System.Windows;
using System.Windows.Input;

namespace PSWRDMGR.Controls
{
    public class AccountControlViewModel : BaseViewModel
    {
        private AccountViewModel _account;
        public AccountViewModel Account
        {
            get => _account;
            set => RaisePropertyChanged(ref _account, value);
        }

        public ICommand SetClipboardCommand { get; }

        public Action<AccountControlViewModel> AutoShowContentCallback { get; set; }

        public AccountControlViewModel()
        {
            SetClipboardCommand = new CommandParam<int>(SetClipboard);
        }

        public void SetClipboard(int accountInfoUid)
        {
            switch (accountInfoUid)
            {
                case 1: Clipboard.SetText(Account.Username); break;
                case 2: Clipboard.SetText(Account.Password); break;
                case 3: Clipboard.SetText(Account.Email); break;
            }
        }

        public void ShowContentsPanel()
        {
            AutoShowContentCallback?.Invoke(this);
        }
    }
}
