using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Outlook;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TestMailStreamers
    {
    static class OutlookManage
        {

        /// <summary>
        /// Method for instantiating Outlook Account that will be used in particular test.
        /// </summary>
        /// <param name="application"></param>
        /// <param name="smtpAddress"></param>
        /// <returns></returns>
        /// 

        public static Outlook.Account GetAccount(Outlook.Application application, string smtpAddress)
            {

            // Loop over the Accounts collection of the current Outlook session.
            Outlook.Accounts accounts = application.Session.Accounts;
            foreach (Outlook.Account account in accounts)
                {
                //Console.WriteLine("Account: " + account.SmtpAddress);
                //Console.WriteLine("Account StoreID: " + account.DeliveryStore.StoreID);
                // When the e-mail address matches, return the account.
                if (account.SmtpAddress == smtpAddress)
                    {
                    return account;
                    }
                }
            throw new System.Exception(string.Format("No Account with SmtpAddress: {0} exists!", smtpAddress));
            }

        
        /// <summary>
        /// Method for Creating new folders and subfolders inside Inbox for each Account
        /// </summary>
        /// <param name="foldersToCreate">List of Folders that should be created inside Inbox. Each created folder will be created with one default Subfolder</param>
        /// <returns></returns>
        /// 
        public static void CreateFolders(List<String> foldersToCreate, string account)
            {
            
            Outlook.Application application = new Outlook.Application();
            Outlook.Accounts accounts = application.Session.Accounts;
                       
            foreach (Outlook.Account acc in accounts)
                {
                
                Console.WriteLine("Account Display name: " + acc.DisplayName);
                if (acc.DisplayName.Equals(account))
                    {
                                       
                    NameSpace oNs = application.GetNamespace("MAPI");
                    Recipient oRep = oNs.CreateRecipient(acc.DisplayName);
                   
                    Outlook.Folder folder = application.Session.GetSharedDefaultFolder(oRep, OlDefaultFolders.olFolderInbox) as Outlook.Folder;

                    Outlook.Folders folders = folder.Folders;

                    foreach (String fold in foldersToCreate)
                        {
                        try
                            {
                            Outlook.Folder newFolder = folders.Add(
                                fold, Type.Missing)
                                as Outlook.Folder;

                            Outlook.Folder newSubfolder = folders.Add("Subfolder", Type.Missing)
                                as Outlook.Folder;

                            newSubfolder.MoveTo(newFolder);
                            
                            }
                        catch
                            {
                            //
                            }
                        }
                    }
                }
            }
        }
    }
