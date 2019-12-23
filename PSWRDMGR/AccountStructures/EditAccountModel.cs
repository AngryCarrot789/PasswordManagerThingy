using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PSWRDMGR.AccountStructures
{
    public class EditAccountModel
    {
        public string AccountName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DateOfBirth { get; set; }
        public string SecurityInfo { get; set; }
        public string ExtraInfo1 { get; set; }
        public string ExtraInfo2 { get; set; }
        public string ExtraInfo3 { get; set; }
        public string ExtraInfo4 { get; set; }
        public string ExtraInfo5 { get; set; }

        public ICommand ShowContentCommand { get; set; }
        public ICommand PasteFromClipboard { get; set; }
        public ICommand EditAccountCommand { get; set; }

        public AccountModel Convert()
        {
            AccountModel am = new AccountModel()
            {
                AccountName = this.AccountName,
                Email = this.Email,
                Username = this.Username,
                Password = this.Password,
                DateOfBirth = this.DateOfBirth,
                SecurityInfo = this.SecurityInfo,
                ExtraInfo1 = this.ExtraInfo1,
                ExtraInfo2 = this.ExtraInfo2,
                ExtraInfo3 = this.ExtraInfo3,
                ExtraInfo4 = this.ExtraInfo4,
                ExtraInfo5 = this.ExtraInfo5
            };
            return am;
        }
    }
}
