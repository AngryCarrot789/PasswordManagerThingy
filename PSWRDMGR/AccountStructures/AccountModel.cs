namespace PSWRDMGR.AccountStructures
{
    public class AccountModel
    {
        public string AccountName  { get; set; }
        public string Email        { get; set; }
        public string Username     { get; set; }
        public string Password     { get; set; }
        public string DateOfBirth  { get; set; }
        public string SecurityInfo { get; set; }
        public string ExtraInfo1  { get; set; }
        public string ExtraInfo2  { get; set; }
        public string ExtraInfo3  { get; set; }
        public string ExtraInfo4  { get; set; }
        public string ExtraInfo5  { get; set; }

        public static AccountModel Duplicate(AccountModel original)
        {
            return new AccountModel()
            {
                AccountName = original.AccountName,
                Email = original.Email,
                Username = original.Username,
                Password = original.Password,
                DateOfBirth = original.DateOfBirth,
                SecurityInfo = original.SecurityInfo,
                ExtraInfo1 = original.ExtraInfo1,
                ExtraInfo2 = original.ExtraInfo2,
                ExtraInfo3 = original.ExtraInfo3,
                ExtraInfo4 = original.ExtraInfo4,
                ExtraInfo5 = original.ExtraInfo5
            };
        }
    }
}
