using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSWRDMGR.Accounts
{
    public static class Accounts
    {
        public static string FolderPath = @"E:\pwrds";

        public static string AccNameLocation = @"E:\pwrds\accName.txt";
        public static string EmailllLocation = @"E:\pwrds\email.txt";
        public static string UsernamLocation = @"E:\pwrds\usrName.txt";
        public static string PasswrdLocation = @"E:\pwrds\pssWrd.txt";
        public static string DofBrthLocation = @"E:\pwrds\DtoBrth.txt";
        public static string ScrtyInLocation = @"E:\pwrds\ScrtyInfo.txt";
        public static string ExtrIn1Location = @"E:\pwrds\ExtInf1.txt";
        public static string ExtrIn2Location = @"E:\pwrds\ExtInf2.txt";
        public static string ExtrIn3Location = @"E:\pwrds\ExtInf3.txt";
        public static string ExtrIn4Location = @"E:\pwrds\ExtInf4.txt";
        public static string ExtrIn5Location = @"E:\pwrds\ExtInf5.txt";
        public static class AccountLoader
        {
            public static List<AccountModel> LoadFiles()
            {
                List<string> accname = File.ReadAllLines(AccNameLocation).ToList();
                List<string> emailss = File.ReadAllLines(EmailllLocation).ToList();
                List<string> usernam = File.ReadAllLines(UsernamLocation).ToList();
                List<string> passwrd = File.ReadAllLines(PasswrdLocation).ToList();
                List<string> dofbrth = File.ReadAllLines(DofBrthLocation).ToList();
                List<string> scrtyin = File.ReadAllLines(ScrtyInLocation).ToList();
                List<string> extinf1 = File.ReadAllLines(ExtrIn1Location).ToList();
                List<string> extinf2 = File.ReadAllLines(ExtrIn2Location).ToList();
                List<string> extinf3 = File.ReadAllLines(ExtrIn3Location).ToList();
                List<string> extinf4 = File.ReadAllLines(ExtrIn4Location).ToList();
                List<string> extinf5 = File.ReadAllLines(ExtrIn5Location).ToList();

                List<AccountModel> accounts = new List<AccountModel>();

                for (int i = 0; i < accname.Count; i++)
                {
                    AccountModel am = new AccountModel()
                    {
                        AccountName = accname[i],
                        Email = emailss[i],
                        Username = usernam[i],
                        Password = passwrd[i],
                        DateOfBirth = dofbrth[i],
                        SecurityInfo = scrtyin[i],
                        ExtraInfo1 = extinf1[i],
                        ExtraInfo2 = extinf2[i],
                        ExtraInfo3 = extinf3[i],
                        ExtraInfo4 = extinf4[i],
                        ExtraInfo5 = extinf5[i]
                    };
                    accounts.Add(am);
                }
                return accounts;
            }
        }

        public static class AccountSaver
        {
            public static void SaveFiles(List<AccountModel> accounts)
            {
                //List<string> accname = File.ReadAllLines(AccNameLocation).ToList();
                //List<string> emailss = File.ReadAllLines(EmailllLocation).ToList();
                //List<string> usernam = File.ReadAllLines(UsernamLocation).ToList();
                //List<string> passwrd = File.ReadAllLines(PasswrdLocation).ToList();
                //List<string> dofbrth = File.ReadAllLines(DofBrthLocation).ToList();
                //List<string> scrtyin = File.ReadAllLines(ScrtyInLocation).ToList();
                //List<string> extinf1 = File.ReadAllLines(ExtrIn1Location).ToList();
                //List<string> extinf2 = File.ReadAllLines(ExtrIn2Location).ToList();
                //List<string> extinf3 = File.ReadAllLines(ExtrIn3Location).ToList();
                //List<string> extinf4 = File.ReadAllLines(ExtrIn4Location).ToList();
                //List<string> extinf5 = File.ReadAllLines(ExtrIn5Location).ToList();

                List<string> NEWaccname = new List<string>();
                List<string> NEWemailss = new List<string>();
                List<string> NEWusernam = new List<string>();
                List<string> NEWpasswrd = new List<string>();
                List<string> NEWdofbrth = new List<string>();
                List<string> NEWscrtyin = new List<string>();
                List<string> NEWextinf1 = new List<string>();
                List<string> NEWextinf2 = new List<string>();
                List<string> NEWextinf3 = new List<string>();
                List<string> NEWextinf4 = new List<string>();
                List<string> NEWextinf5 = new List<string>();

                for (int i = 0; i < accounts.Count; i++)
                {
                    NEWaccname.Add(accounts[i].AccountName);
                    NEWemailss.Add(accounts[i].Email);
                    NEWusernam.Add(accounts[i].Username);
                    NEWpasswrd.Add(accounts[i].Password);
                    NEWdofbrth.Add(accounts[i].DateOfBirth);
                    NEWscrtyin.Add(accounts[i].SecurityInfo);
                    NEWextinf1.Add(accounts[i].ExtraInfo1);
                    NEWextinf2.Add(accounts[i].ExtraInfo2);
                    NEWextinf3.Add(accounts[i].ExtraInfo3);
                    NEWextinf4.Add(accounts[i].ExtraInfo4);
                    NEWextinf5.Add(accounts[i].ExtraInfo5);
                }

                File.WriteAllLines(AccNameLocation, NEWaccname);
                File.WriteAllLines(EmailllLocation, NEWemailss);
                File.WriteAllLines(UsernamLocation, NEWusernam);
                File.WriteAllLines(PasswrdLocation, NEWpasswrd);
                File.WriteAllLines(DofBrthLocation, NEWdofbrth);
                File.WriteAllLines(ScrtyInLocation, NEWscrtyin);
                File.WriteAllLines(ExtrIn1Location, NEWextinf1);
                File.WriteAllLines(ExtrIn2Location, NEWextinf2);
                File.WriteAllLines(ExtrIn3Location, NEWextinf3);
                File.WriteAllLines(ExtrIn4Location, NEWextinf4);
                File.WriteAllLines(ExtrIn5Location, NEWextinf5);
            }
        }
    }
}
