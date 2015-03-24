using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TestMailStreamers
    {
    class EMailPrepare
        {

        /// <summary>
        /// Method for creation of Outlook Mail Message.
        /// </summary>
        /// <param name="fromAddress">Represents e-mail address that is used for sending e-mail</param>
        /// <param name="toList">Array of the e-mail adresses that will be used in To: field of the e-mauk message</param>
        /// <param name="ccList">Array of the e-mail adresses that will be used in Cc: field of the e-mauk message</param>
        /// <param name="bccList">Array of the e-mail adresses that will be used in Bcc: field of the e-mauk message</param>
        /// <param name="attachName">This parameter represents list of the attachment names how it will be displayed in e-mail</param>
        /// <param name="attachLocation">This parameter represents location of the attachment</param>
        /// <returns>Outlook._MailItem object</returns>
        /// 
        public static Outlook._MailItem CreateOutlookEmail(String fromAddress, List<String> toList, List<String> ccList, List<String> bccList, List<String> attachments, String attachLocation)
            {
            
            Outlook.Application application = new Outlook.Application();
            Outlook.MailItem mail = application.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;
            
            mail.SendUsingAccount = OutlookManage.GetAccount(application, fromAddress);

            if (toList != null)
                {
                mail.Subject = "Please ignore this message. This is created for E2E test purposes. E-mail to: " + toList[0];
                mail.Body = "This is body of-email from Exchange account to " + toList[0];
                }
            else
                {
                mail.Subject = "Please ignore this message. This is created for E2E test purposes. E-mail to empty To List";
                mail.Body = "This is body of-email from Exchange account to Empty to List";
                }

            if (!attachments.Count.Equals(0))
                {

                foreach (String attachName in attachments)
                    {

                    Console.WriteLine("Add attachment to e-mail");
                    int iPosition = (int)mail.Body.Length + 1;
                    int iAttachType = (int)Outlook.OlAttachmentType.olByValue;

                    mail.Attachments.Add(attachLocation, iAttachType, iPosition, attachName);
                    }
                }
                        
            Outlook.Recipients recipients = null;
            Outlook.Recipient recipientTo = null;
            Outlook.Recipient recipientCC = null;
            Outlook.Recipient recipientBCC = null;
            
            try
            {
                recipients = mail.Recipients;

             while(recipients.Count != 0)
                    {
                        recipients.Remove(1);                    
                    }
             if (toList != null)
                 {
                 foreach (String toAddress in toList)
                     {
                     recipientTo = recipients.Add(toAddress);
                     recipientTo.Type = (int)Outlook.OlMailRecipientType.olTo;
                     }
                 }
           
           if(ccList != null) 
               {
               foreach(String ccAddress in ccList) 
                   {
                    recipientCC = recipients.Add(ccAddress);
                    recipientCC.Type =  (int)Outlook.OlMailRecipientType.olCC;
                    }
                }
            
           if(bccList != null) 
               {
               foreach(String bccAddress in bccList) 
                   {
                    recipientCC = recipients.Add(bccAddress);
                    recipientCC.Type =  (int)Outlook.OlMailRecipientType.olBCC;
                    }
                }

            }
            catch (Exception ex)
                {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            finally
                {
                if (recipientBCC != null) Marshal.ReleaseComObject(recipientBCC);
                if (recipientCC != null) Marshal.ReleaseComObject(recipientCC);
                if (recipientTo != null) Marshal.ReleaseComObject(recipientTo);
                if (recipients != null) Marshal.ReleaseComObject(recipients);
                }
                        
           return mail;
         }

        /// <summary>
        /// Method for creation of Outlook Mail Message with empty subject.
        /// </summary>
        /// <param name="fromAddress">Represents e-mail address that is used for sending e-mail</param>
        /// <param name="toList">Array of the e-mail adresses that will be used in To: field of the e-mauk message</param>
        /// <param name="ccList">Array of the e-mail adresses that will be used in Cc: field of the e-mauk message</param>
        /// <param name="bccList">Array of the e-mail adresses that will be used in Bcc: field of the e-mauk message</param>
        /// <param name="attachName">This parameter represents name of the attachment how it will be displayed in e-mail</param>
        /// <param name="attachLocation">This parameter represents location of the attachment</param>
        /// <returns>Outlook._MailItem object</returns>
        /// 

        public static Outlook._MailItem CreateOutlookEmailWithEmptySubject(String fromAddress, List<String> toList, List<String> ccList, List<String> bccList, List<String> attachments, String attachLocation)
            {

            Outlook.Application application = new Outlook.Application();
            Outlook.MailItem mail = application.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;

            mail.SendUsingAccount = OutlookManage.GetAccount(application, fromAddress);

           
            mail.Subject = null;
            mail.Body = "Body";
           
            Outlook.Recipients recipients = null;
            Outlook.Recipient recipientTo = null;
            Outlook.Recipient recipientCC = null;
            Outlook.Recipient recipientBCC = null;

            try
                {
                recipients = mail.Recipients;

                while (recipients.Count != 0)
                    {
                    recipients.Remove(1);
                    }
                if (toList != null)
                    {
                    foreach (String toAddress in toList)
                        {
                        recipientTo = recipients.Add(toAddress);
                        recipientTo.Type = (int)Outlook.OlMailRecipientType.olTo;
                        }
                    }

                if (ccList != null)
                    {
                    foreach (String ccAddress in ccList)
                        {
                        recipientCC = recipients.Add(ccAddress);
                        recipientCC.Type = (int)Outlook.OlMailRecipientType.olCC;
                        }
                    }

                if (bccList != null)
                    {
                    foreach (String bccAddress in bccList)
                        {
                        recipientCC = recipients.Add(bccAddress);
                        recipientCC.Type = (int)Outlook.OlMailRecipientType.olBCC;
                        }
                    }

                }
            catch (Exception ex)
                {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            finally
                {
                if (recipientBCC != null) Marshal.ReleaseComObject(recipientBCC);
                if (recipientCC != null) Marshal.ReleaseComObject(recipientCC);
                if (recipientTo != null) Marshal.ReleaseComObject(recipientTo);
                if (recipients != null) Marshal.ReleaseComObject(recipients);
                }

            if (!attachments.Count.Equals(0))
                {

                foreach (String attachName in attachments)
                    {
                    String sSource = attachLocation;
                    String sDisplayName = attachName;

                    int iPosition = (int)mail.Body.Length + 1;
                    int iAttachType = (int)Outlook.OlAttachmentType.olByValue;

                    mail.Attachments.Add(sSource, iAttachType, iPosition, sDisplayName);
                    }
                }

            return mail;
            }

        /// <summary>
        /// Method for creation and Sending e-mail message from Gmail account.
        /// </summary>
        /// <param name="from">Represents e-mail address that is used for sending e-mail</param>
        /// <param name="toList">Array of the e-mail adresses that will be used in To: field of the e-mauk message</param>
        /// <param name="ccList">Array of the e-mail adresses that will be used in Cc: field of the e-mauk message</param>
        /// <param name="bccList">Array of the e-mail adresses that will be used in Bcc: field of the e-mauk message</param>
        /// <param name="attachNames">This parameter represents name of the attachment how it will be displayed in e-mail</param>
        /// <param name="subject">This parameter represents subject of the e-mail</param>
        /// <param name="body">This parameter represents body of the e-mail</param>
        /// <returns>void</returns>
        /// 

        public static void SendGmailMessage(String from, List<String> toList, List<String> ccList, List<String> bccList, List<String> attachNames, String subject, String body)
            {
                        
            var fromAddress = new MailAddress(from, "From Name");
            String fromPassword = "Enkata#10";

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(from, fromPassword);
          
            MailMessage mail = new MailMessage();

            mail.From = fromAddress;

            if (toList != null)
                {
                Console.WriteLine("toList.Count = " + toList.Count);
                foreach (String toAddress in toList)
                    {
                    Console.WriteLine("Add " + toAddress + " to recipients list");
                    mail.To.Add(toAddress);

                    }
                }
            if (ccList != null)
                {
                Console.WriteLine("ccList.Count = " + ccList.Count);
                foreach (String ccAddress in ccList)
                    {
                    Console.WriteLine("Add " + ccAddress + " to CC list");
                    mail.CC.Add(ccAddress);
            
                    }
                }
            if (bccList != null)
                {
                Console.WriteLine("bccList.Count = " + bccList.Count);
                foreach (String bccAddress in bccList)
                    {
                    Console.WriteLine("Add " + bccAddress + " to BCC list");
                    mail.Bcc.Add(bccAddress);
            
                    }
                }
            if (attachNames != null)
                {
                foreach (String attach in attachNames)
                    {
                    Attachment data = new Attachment(attach, MediaTypeNames.Application.Octet);
                    mail.Attachments.Add(data);
                    }
                }

            mail.Subject = subject;
            mail.Body = body;

            smtp.Send(mail);
            
            }
        
        }
    }
