using PSWRDMGR.Utilities;

namespace PSWRDMGR.AccountStructures
{
    public class AccountViewModel : BaseViewModel
    {
        private string _accountName;
        private string _email;
        private string _username;
        private string _password;
        private string _dateOfBirth;
        private string _securityInfo;
        private string _extraInfo1;
        private string _extraInfo2;
        private string _extraInfo3;
        private string _extraInfo4;
        private string _extraInfo5;

        public string AccountName { get => _accountName; set => RaisePropertyChanged(ref _accountName, value); }
        public string Email { get => _email; set => RaisePropertyChanged(ref _email, value); }
        public string Username { get => _username; set => RaisePropertyChanged(ref _username, value); }
        public string Password { get => _password; set => RaisePropertyChanged(ref _password, value); }
        public string DateOfBirth { get => _dateOfBirth; set => RaisePropertyChanged(ref _dateOfBirth, value); }
        public string SecurityInfo { get => _securityInfo; set => RaisePropertyChanged(ref _securityInfo, value); }
        public string ExtraInfo1 { get => _extraInfo1; set => RaisePropertyChanged(ref _extraInfo1, value); }
        public string ExtraInfo2 { get => _extraInfo2; set => RaisePropertyChanged(ref _extraInfo2, value); }
        public string ExtraInfo3 { get => _extraInfo3; set => RaisePropertyChanged(ref _extraInfo3, value); }
        public string ExtraInfo4 { get => _extraInfo4; set => RaisePropertyChanged(ref _extraInfo4, value); }
        public string ExtraInfo5 { get => _extraInfo5; set => RaisePropertyChanged(ref _extraInfo5, value); }
    }
}
