using Ookii.Dialogs.Wpf;
using PSWRDMGR.AccountStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PSWRDMGR.Accounts
{
    public static class Accounts
    {
        public static string FolderPath = @"E:\pwrds";

        public static string AccNameName = "accName.txt";
        public static string EmailllName = "email.txt";
        public static string UsernamName = "usrName.txt";
        public static string PasswrdName = "pssWrd.txt";
        public static string DofBrthName = "DtoBrth.txt";
        public static string ScrtyInName = "ScrtyInfo.txt";
        public static string ExtrIn1Name = "ExtInf1.txt";
        public static string ExtrIn2Name = "ExtInf2.txt";
        public static string ExtrIn3Name = "ExtInf3.txt";
        public static string ExtrIn4Name = "ExtInf4.txt";
        public static string ExtrIn5Name = "ExtInf5.txt";

        public static class AccountFileCreator
        {
            public static void CreateAccountsDirectoryAndFiles()
            {
                VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog();
                fbd.RootFolder = Environment.SpecialFolder.MyDocuments;

                if (fbd.ShowDialog() == true)
                {
                    File.Create(Path.Combine(fbd.SelectedPath, AccNameName));
                    File.Create(Path.Combine(fbd.SelectedPath, EmailllName));
                    File.Create(Path.Combine(fbd.SelectedPath, UsernamName));
                    File.Create(Path.Combine(fbd.SelectedPath, PasswrdName));
                    File.Create(Path.Combine(fbd.SelectedPath, DofBrthName));
                    File.Create(Path.Combine(fbd.SelectedPath, ScrtyInName));
                    File.Create(Path.Combine(fbd.SelectedPath, ExtrIn1Name));
                    File.Create(Path.Combine(fbd.SelectedPath, ExtrIn2Name));
                    File.Create(Path.Combine(fbd.SelectedPath, ExtrIn3Name));
                    File.Create(Path.Combine(fbd.SelectedPath, ExtrIn4Name));
                    File.Create(Path.Combine(fbd.SelectedPath, ExtrIn5Name));
                }
            }
        }

        public static class AccountLoader
        {
            public static List<AccountModel> LoadCustomAccounts()
            {
                VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog();
                fbd.RootFolder = Environment.SpecialFolder.MyDocuments;

                if (fbd.ShowDialog() == true)
                {
                    return LoadFiles(fbd.SelectedPath);
                }

                else
                    return new List<AccountModel>()
                    {
                        new AccountModel() { AccountName = "Failed to load accounts from a", Email = "custom directory." }
                    };
            }

            public static List<AccountModel> LoadFiles() { return LoadFiles(FolderPath); }

            public static List<AccountModel> LoadFiles(string directoryLocation)
            {
                if (!Directory.Exists(directoryLocation) || Directory.GetFiles(directoryLocation).Count() != 11)
                {
                    MessageBox.Show(
                        $"{directoryLocation} isn't a valid directory. " +
                        $"You can either create it, or load from another directory",
                        $"Path doesn't exist. Couldn't load accounts");
                    return new List<AccountModel>()
                    {
                        new AccountModel()
                        {
                            AccountName = "Failed to load accounts from the",
                            Email = "main account directory."
                        }
                    };
                }
                List<string> accname = File.ReadAllLines(Path.Combine(directoryLocation, AccNameName)).ToList();
                List<string> emailss = File.ReadAllLines(Path.Combine(directoryLocation, EmailllName)).ToList();
                List<string> usernam = File.ReadAllLines(Path.Combine(directoryLocation, UsernamName)).ToList();
                List<string> passwrd = File.ReadAllLines(Path.Combine(directoryLocation, PasswrdName)).ToList();
                List<string> dofbrth = File.ReadAllLines(Path.Combine(directoryLocation, DofBrthName)).ToList();
                List<string> scrtyin = File.ReadAllLines(Path.Combine(directoryLocation, ScrtyInName)).ToList();
                List<string> extinf1 = File.ReadAllLines(Path.Combine(directoryLocation, ExtrIn1Name)).ToList();
                List<string> extinf2 = File.ReadAllLines(Path.Combine(directoryLocation, ExtrIn2Name)).ToList();
                List<string> extinf3 = File.ReadAllLines(Path.Combine(directoryLocation, ExtrIn3Name)).ToList();
                List<string> extinf4 = File.ReadAllLines(Path.Combine(directoryLocation, ExtrIn4Name)).ToList();
                List<string> extinf5 = File.ReadAllLines(Path.Combine(directoryLocation, ExtrIn5Name)).ToList();

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
            public static void SaveCustomFiles(List<AccountModel> accounts)
            {
                VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog();
                fbd.RootFolder = Environment.SpecialFolder.MyDocuments;

                if (fbd.ShowDialog() == true)
                {
                    SaveFiles(accounts, fbd.SelectedPath);
                }
            }
            public static void SaveFiles(List<AccountModel> accounts) { SaveFiles(accounts, FolderPath); }
            public static void SaveFiles(List<AccountModel> accounts, string directoryLocation)
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

                File.WriteAllLines(Path.Combine(directoryLocation, AccNameName), NEWaccname);
                File.WriteAllLines(Path.Combine(directoryLocation, EmailllName), NEWemailss);
                File.WriteAllLines(Path.Combine(directoryLocation, UsernamName), NEWusernam);
                File.WriteAllLines(Path.Combine(directoryLocation, PasswrdName), NEWpasswrd);
                File.WriteAllLines(Path.Combine(directoryLocation, DofBrthName), NEWdofbrth);
                File.WriteAllLines(Path.Combine(directoryLocation, ScrtyInName), NEWscrtyin);
                File.WriteAllLines(Path.Combine(directoryLocation, ExtrIn1Name), NEWextinf1);
                File.WriteAllLines(Path.Combine(directoryLocation, ExtrIn2Name), NEWextinf2);
                File.WriteAllLines(Path.Combine(directoryLocation, ExtrIn3Name), NEWextinf3);
                File.WriteAllLines(Path.Combine(directoryLocation, ExtrIn4Name), NEWextinf4);
                File.WriteAllLines(Path.Combine(directoryLocation, ExtrIn5Name), NEWextinf5);
            }

            public static void SaveBackupFiles(List<AccountModel> accounts)
            {
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

                File.WriteAllLines(@"E:\stuff\backupthingy\accName.txt", NEWaccname);
                File.WriteAllLines(@"E:\stuff\backupthingy\email.txt", NEWemailss);
                File.WriteAllLines(@"E:\stuff\backupthingy\usrName.txt", NEWusernam);
                File.WriteAllLines(@"E:\stuff\backupthingy\pssWrd.txt", NEWpasswrd);
                File.WriteAllLines(@"E:\stuff\backupthingy\DtoBrth.txt", NEWdofbrth);
                File.WriteAllLines(@"E:\stuff\backupthingy\ScrtyInfo.txt", NEWscrtyin);
                File.WriteAllLines(@"E:\stuff\backupthingy\ExtInf1.txt", NEWextinf1);
                File.WriteAllLines(@"E:\stuff\backupthingy\ExtInf2.txt", NEWextinf2);
                File.WriteAllLines(@"E:\stuff\backupthingy\ExtInf3.txt", NEWextinf3);
                File.WriteAllLines(@"E:\stuff\backupthingy\ExtInf4.txt", NEWextinf4);
                File.WriteAllLines(@"E:\stuff\backupthingy\ExtInf5.txt", NEWextinf5);
            }
        }
    }
}
