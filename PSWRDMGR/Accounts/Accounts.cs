using Microsoft.VisualBasic;
using Ookii.Dialogs.Wpf;
using PSWRDMGR.AccountStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace PSWRDMGR.Accounts
{
    public static class Accounts
    {
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

        public static List<AccountViewModel> AccountFailure
        {
            get
            {
                return new List<AccountViewModel>()
                {
                    new AccountViewModel()
                    {
                        AccountName = "Failed to load accounts from the",
                        Email = "main account directory."
                    }
                };
            }
        }

        public static class AccountFileCreator
        {
            public static void CreateAccountsDirectoryAndFiles()
            {
                VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog
                {
                    RootFolder = Environment.SpecialFolder.MyDocuments
                };

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
            public static List<AccountViewModel> LoadCustomAccounts()
            {
                VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog
                {
                    RootFolder = Environment.SpecialFolder.MyDocuments,
                    UseDescriptionForTitle = true,
                    Description = "Select location of accounts"
                };

                if (fbd.ShowDialog() == true)
                {
                    return LoadFiles(fbd.SelectedPath);
                }

                else
                {
                    return new List<AccountViewModel>();
                }
            }

            public static List<AccountViewModel> LoadFiles()
            {
                if (!Directory.Exists(Properties.Settings.Default.MainAccountPath))
                {
                    // Make sure the account path does exists
                    // but also add a delay just in case the properties haven't loaded
                    Thread.Sleep(100);
                    if (!Directory.Exists(Properties.Settings.Default.MainAccountPath))
                    {
                        VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog
                        {
                            RootFolder = Environment.SpecialFolder.MyDocuments,
                            UseDescriptionForTitle = true,
                            Description = "Select location of accounts"
                        };
                        if (fbd.ShowDialog() == true)
                        {
                            if (Directory.Exists(fbd.SelectedPath))
                            {
                                Properties.Settings.Default.MainAccountPath = fbd.SelectedPath;
                                Properties.Settings.Default.Save();
                                return LoadFiles(fbd.SelectedPath);
                            }
                            else return AccountFailure;
                        }
                        else return AccountFailure;
                    }
                    else return LoadFiles(Properties.Settings.Default.MainAccountPath);
                }
                else return LoadFiles(Properties.Settings.Default.MainAccountPath);
            }

            public static List<AccountViewModel> LoadFiles(string directoryLocation)
            {
                bool dirExists = Directory.Exists(directoryLocation);
                int dirCount = Directory.GetFiles(directoryLocation).Length;
                if (!dirExists)
                {
                    MessageBox.Show(
                        $"{directoryLocation} isn't a valid directory. " +
                        $"You can either create it, or load from another directory",
                        $"Path doesn't exist. Couldn't load accounts");
                    return AccountFailure;
                }

                if (dirCount < 11)
                {
                    List<string> empty = new List<string>();
                    int numberOfListRecreations = 0;
                    void InternalRecreateList(int size)
                    {
                        empty = new List<string>();
                        for(int i = 0; i < size; i++)
                        {
                            empty.Add("[Recreated due to lost file]");
                        }
                        numberOfListRecreations++;
                    }

                    int tempLineCount;
                    if (!File.Exists(Path.Combine(directoryLocation, AccNameName))) File.WriteAllLines(Path.Combine(directoryLocation, AccNameName), empty);
                    else { tempLineCount = File.ReadLines(Path.Combine(directoryLocation, AccNameName)).Count(); InternalRecreateList(tempLineCount); }

                    if (!File.Exists(Path.Combine(directoryLocation, EmailllName))) File.WriteAllLines(Path.Combine(directoryLocation, EmailllName), empty);
                    else { tempLineCount = File.ReadLines(Path.Combine(directoryLocation, EmailllName)).Count(); InternalRecreateList(tempLineCount); }

                    if (!File.Exists(Path.Combine(directoryLocation, UsernamName))) File.WriteAllLines(Path.Combine(directoryLocation, UsernamName), empty);
                    else { tempLineCount = File.ReadLines(Path.Combine(directoryLocation, UsernamName)).Count(); InternalRecreateList(tempLineCount); }
                    
                    if (!File.Exists(Path.Combine(directoryLocation, PasswrdName))) File.WriteAllLines(Path.Combine(directoryLocation, PasswrdName), empty);
                    else { tempLineCount = File.ReadLines(Path.Combine(directoryLocation, PasswrdName)).Count(); InternalRecreateList(tempLineCount); }
                    
                    if (!File.Exists(Path.Combine(directoryLocation, DofBrthName))) File.WriteAllLines(Path.Combine(directoryLocation, DofBrthName), empty);
                    else { tempLineCount = File.ReadLines(Path.Combine(directoryLocation, DofBrthName)).Count(); InternalRecreateList(tempLineCount); }
                    
                    if (!File.Exists(Path.Combine(directoryLocation, ScrtyInName))) File.WriteAllLines(Path.Combine(directoryLocation, ScrtyInName), empty);
                    else { tempLineCount = File.ReadLines(Path.Combine(directoryLocation, ScrtyInName)).Count(); InternalRecreateList(tempLineCount); }
                    
                    if (!File.Exists(Path.Combine(directoryLocation, ExtrIn1Name))) File.WriteAllLines(Path.Combine(directoryLocation, ExtrIn1Name), empty);
                    else { tempLineCount = File.ReadLines(Path.Combine(directoryLocation, ExtrIn1Name)).Count(); InternalRecreateList(tempLineCount); }
                    
                    if (!File.Exists(Path.Combine(directoryLocation, ExtrIn2Name))) File.WriteAllLines(Path.Combine(directoryLocation, ExtrIn2Name), empty);
                    else { tempLineCount = File.ReadLines(Path.Combine(directoryLocation, ExtrIn2Name)).Count(); InternalRecreateList(tempLineCount); }
                    
                    if (!File.Exists(Path.Combine(directoryLocation, ExtrIn3Name))) File.WriteAllLines(Path.Combine(directoryLocation, ExtrIn3Name), empty);
                    else { tempLineCount = File.ReadLines(Path.Combine(directoryLocation, ExtrIn3Name)).Count(); InternalRecreateList(tempLineCount); }
                    
                    if (!File.Exists(Path.Combine(directoryLocation, ExtrIn4Name))) File.WriteAllLines(Path.Combine(directoryLocation, ExtrIn4Name), empty);
                    else { tempLineCount = File.ReadLines(Path.Combine(directoryLocation, ExtrIn4Name)).Count(); InternalRecreateList(tempLineCount); }
                    
                    if (!File.Exists(Path.Combine(directoryLocation, ExtrIn5Name))) File.WriteAllLines(Path.Combine(directoryLocation, ExtrIn5Name), empty);

                    if (numberOfListRecreations > 0)
                        MessageBox.Show($"Had to recreate files because they didn't exist or had the wrong filename. " +
                            $"if you have access to the old files, rename them back or move them to the accounts directory");
                }

                int accountCount = 0;
                int maxCount = 0;

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

                accountCount = accname.Count;
                if (accountCount < 1)
                {
                    if (emailss.Count > accountCount) maxCount = emailss.Count;
                    accountCount = emailss.Count;
                    if (usernam.Count > accountCount) maxCount = usernam.Count;
                    accountCount = usernam.Count;
                    if (passwrd.Count > accountCount) maxCount = passwrd.Count;
                    accountCount = passwrd.Count;
                    if (dofbrth.Count > accountCount) maxCount = dofbrth.Count;
                    accountCount = dofbrth.Count;
                    if (scrtyin.Count > accountCount) maxCount = scrtyin.Count;
                    accountCount = scrtyin.Count;
                    if (extinf1.Count > accountCount) maxCount = extinf1.Count;
                    accountCount = extinf1.Count;
                    if (extinf2.Count > accountCount) maxCount = extinf2.Count;
                    accountCount = extinf2.Count;
                    if (extinf3.Count > accountCount) maxCount = extinf3.Count;
                    accountCount = extinf3.Count;
                    if (extinf4.Count > accountCount) maxCount = extinf4.Count;
                    accountCount = extinf4.Count;
                    if (extinf5.Count > accountCount) maxCount = extinf5.Count;
                    accountCount = extinf5.Count;

                    string userAccountsCount = Interaction.InputBox(
                        $"The AccountNames file was lost, However a maximum of {maxCount} possible " +
                        $"accounts were detected in other files. How many accounts were there?", "How many accounts were there?", maxCount.ToString());

                    if (int.TryParse(userAccountsCount, out int userAccountsOutput))
                    {
                        accountCount = userAccountsOutput;
                    }
                    else
                    {
                        accountCount = maxCount;
                    }
                }

                void EnsureListIsSize(List<string> list, int size)
                {
                    if (list.Count < size)
                    {
                        int sizeDifference = size - list.Count;
                        for (int i = 0; i < sizeDifference; i++)
                        {
                            list.Add("[Info added due to lost file]");
                        }
                    }
                    else
                    {
                        int sizeDifference = list.Count - size;
                        for (int i = 0; i < sizeDifference; i++)
                        {
                            list.RemoveAt(list.Count - 1);
                        }
                    }
                }

                EnsureListIsSize(accname, accountCount);
                EnsureListIsSize(emailss, accountCount);
                EnsureListIsSize(usernam, accountCount);
                EnsureListIsSize(passwrd, accountCount);
                EnsureListIsSize(dofbrth, accountCount);
                EnsureListIsSize(scrtyin, accountCount);
                EnsureListIsSize(extinf1, accountCount);
                EnsureListIsSize(extinf2, accountCount);
                EnsureListIsSize(extinf3, accountCount);
                EnsureListIsSize(extinf4, accountCount);
                EnsureListIsSize(extinf5, accountCount);

                List<AccountViewModel> accounts = new List<AccountViewModel>();


                for (int i = 0; i < accountCount; i++)
                {
                    AccountViewModel am = new AccountViewModel()
                    {
                        AccountName  = accname[i],
                        Email        = emailss[i],
                        Username     = usernam[i],
                        Password     = passwrd[i],
                        DateOfBirth  = dofbrth[i],
                        SecurityInfo = scrtyin[i],
                        ExtraInfo1   = extinf1[i],
                        ExtraInfo2   = extinf2[i],
                        ExtraInfo3   = extinf3[i],
                        ExtraInfo4   = extinf4[i],
                        ExtraInfo5   = extinf5[i]
                    };
                    accounts.Add(am);
                }
                return accounts;
            }
        }

        public static class AccountSaver
        {
            public static void SaveCustomFiles(List<AccountViewModel> accounts)
            {
                VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog
                {
                    RootFolder = Environment.SpecialFolder.MyDocuments,
                    UseDescriptionForTitle = true,
                    Description = "Select location to save accounts"
                };

                if (fbd.ShowDialog() == true)
                {
                    SaveFiles(accounts, fbd.SelectedPath);
                }
            }

            public static void SaveFiles(List<AccountViewModel> accounts)
            {
                if (!Directory.Exists(Properties.Settings.Default.MainAccountPath))
                {
                    VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog
                    {
                        RootFolder = Environment.SpecialFolder.MyDocuments,
                        UseDescriptionForTitle = true,
                        Description = "Select location to save accounts"
                    };
                    if (fbd.ShowDialog() == true)
                    {
                        if (Directory.Exists(fbd.SelectedPath))
                        {
                            Properties.Settings.Default.MainAccountPath = fbd.SelectedPath;
                            Properties.Settings.Default.Save();
                            SaveFiles(accounts, Properties.Settings.Default.MainAccountPath);
                        }
                        else SaveFiles(accounts, Properties.Settings.Default.MainAccountPath);
                    }
                }
                else
                    SaveFiles(accounts, Properties.Settings.Default.MainAccountPath);
            }
            public static void SaveFiles(List<AccountViewModel> accounts, string directoryLocation)
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
        }
    }
}
