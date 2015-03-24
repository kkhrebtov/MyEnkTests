using System;
using System.Net;
using System.Net.Mail;
using NUnit.Framework;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace TestMailStreamers
    {

    class ExchangeStreamerTest
        {

        private static string sqlConnectionString = "Data Source=rcvmln69;Initial Catalog=ADA_RAW;Integrated Security=True";

        private static String attSource = "d:\\Temp\\performance.log";
        private static List<String> attDisplayNames = new List<String>();
        private static int timeout = 60000;

        private static List<String> toAddresses = new List<String>();
        private static List<String> ccAddresses = new List<String>();
        private static List<String> bccAddresses  = new List<String>();
        private static List<String> requiredAttendies = new List<String>();
        private static List<String> optionalAttendies = new List<String>();
       
        
       
        [TestFixture]
        public class TestOutcomingOutlookMessages
            {

            [SetUp]
            public void Initialize()
                {
                attDisplayNames.Add("Attachment_to.log");

                }

            [TearDown]
            public void Cleanup()
                {

                attDisplayNames.Clear();
                toAddresses.Clear();
                ccAddresses.Clear();
                bccAddresses.Clear();
                requiredAttendies.Clear();
                optionalAttendies.Clear();

                }


            [Test]
            public void SendMailOutlook_SingleRecipentTo(
            [Values("account_3@enkata.com")] String fromAddress,
            [Values("enk.lead1@gmail.com")] String toAddress)
                {

                toAddresses.Add(toAddress);

                Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAddress, toAddresses, null, null, attDisplayNames, attSource);

                String expSubj = mail.Subject;
                String expConvId = (String)mail.ConversationID;

                ((Outlook._MailItem)mail).Send();

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_3@enkata.com", fromAddress, toAddresses, null, null, expSubj, expConvId, attDisplayNames, "Email Sent");

                String[] expectedScrapes = { "To", "Mailbox", "Subject", "ConversationId", "ConversationTopic", "Id" };

                SqlDataValidator.CheckScrapePresence(sqlConnectionString, expectedScrapes);

                }

            [Test]
            public void SendMailOutlook_MultipleRecipentsTo(
            [Values("account_3@enkata.com")] String fromAddress,
            [Values("enk.lead1@gmail.com")] String toAddress)
                {

                toAddresses.Add(toAddress);
                toAddresses.Add("enk.lead2@gmail.com");
               
                Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAddress, toAddresses, null, null, attDisplayNames, attSource);

                String expSubj = mail.Subject;
                String expConvId = (String)mail.ConversationID;

                ((Outlook._MailItem)mail).Send();

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_3@enkata.com", fromAddress, toAddresses, null, null, expSubj, expConvId, attDisplayNames, "Email Sent");

                String[] expectedScrapes = { "To", "Mailbox", "Subject", "ConversationId", "ConversationTopic", "Id" };

                SqlDataValidator.CheckScrapePresence(sqlConnectionString, expectedScrapes);

                }

            [Test]
            public void SendMailOutlook_RecipentsToCc(
            [Values("account_3@enkata.com")] String fromAddress,
            [Values("enk.lead1@gmail.com")] String toAddress,
            [Values("enk.lead2@gmail.com")] String ccAddress)
                {

                toAddresses.Add(toAddress);
                ccAddresses.Add(ccAddress);


                Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAddress, toAddresses, ccAddresses, null, attDisplayNames, attSource);

                String expSubj = mail.Subject;
                String expConvId = (String)mail.ConversationID;

                ((Outlook._MailItem)mail).Send();

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_3@enkata.com", fromAddress, toAddresses, ccAddresses, null, expSubj, expConvId, attDisplayNames, "Email Sent");

                String[] expectedScrapes = { "To", "Mailbox", "Subject", "ConversationId", "ConversationTopic", "Id" };

                SqlDataValidator.CheckScrapePresence(sqlConnectionString, expectedScrapes);

                }

            [Test]
            public void SendMailOutlook_RecipentsToBccCc(
            [Values("account_3@enkata.com")] String fromAddress,
            [Values("enk.lead1@gmail.com")] String toAddress,
            [Values("enk.lead2@gmail.com")] String bccAddress)
                {

                toAddresses.Add(toAddress);
                bccAddresses.Add(bccAddress);

                Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAddress, toAddresses, null, bccAddresses, attDisplayNames, attSource);

                String expSubj = mail.Subject;
                String expConvId = (String)mail.ConversationID;

                ((Outlook._MailItem)mail).Send();

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_3@enkata.com", fromAddress, toAddresses, null, bccAddresses, expSubj, expConvId, attDisplayNames, "Email Sent");

                String[] expectedScrapes = { "To", "Mailbox", "Subject", "ConversationId", "ConversationTopic", "Id" };

                SqlDataValidator.CheckScrapePresence(sqlConnectionString, expectedScrapes);

                }

            [Test]
            public void SendMailOutlook_RecipentsMultipleToCcBcc(
            [Values("account_1@enkata.com")] String fromAddress,
            [Values("enk.lead1@gmail.com")] String toAddress,
            [Values("enk.lead2@gmail.com")] String ccAddress,
            [Values("enk.lead3@gmail.com")] String bccAddress)
                {
                
                toAddresses.Add(toAddress);
                toAddresses.Add("enk.contact1@gmail.com");
                
                ccAddresses.Add(ccAddress);
                ccAddresses.Add("enk.contact2@gmail.com");
                
                bccAddresses.Add(bccAddress);
                bccAddresses.Add("enk.contact3@gmail.com");
                
                Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAddress, toAddresses, ccAddresses, bccAddresses, attDisplayNames, attSource);

                String expSubj = mail.Subject;
                String expConvId = (String)mail.ConversationID;

                ((Outlook._MailItem)mail).Send();

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_1@enkata.com", fromAddress, toAddresses, ccAddresses, bccAddresses, expSubj, expConvId, attDisplayNames, "Email Sent");

                String[] expectedScrapes = { "To", "Mailbox", "Subject", "ConversationId", "ConversationTopic", "Id" };

                SqlDataValidator.CheckScrapePresence(sqlConnectionString, expectedScrapes);

                }

            [Test]
            public void SendMailOutlook_EmptyTo(
            [Values("account_1@enkata.com")] String fromAddress,
            [Values("enk.lead2@gmail.com")] String ccAddress,
            [Values("enk.lead3@gmail.com")] String bccAddress)
                {

                ccAddresses.Add(ccAddress);
                ccAddresses.Add("enk.contact2@gmail.com");

                bccAddresses.Add(bccAddress);
                bccAddresses.Add("enk.contact3@gmail.com");

                Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAddress, null, ccAddresses, bccAddresses, attDisplayNames, attSource);

                String expSubj = mail.Subject;
                String expConvId = (String)mail.ConversationID;

                ((Outlook._MailItem)mail).Send();

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_1@enkata.com", fromAddress, null, ccAddresses, bccAddresses, expSubj, expConvId, attDisplayNames, "Email Sent");

                String[] expectedScrapes = { "To", "Mailbox", "Subject", "ConversationId", "ConversationTopic", "Id" };

                SqlDataValidator.CheckScrapePresence(sqlConnectionString, expectedScrapes);

                }
            
            [Test]
            public void SendMailOutlook_SeveralAttachments(
            [Values("account_1@enkata.com")] String fromAddress,
            [Values("enk.lead2@gmail.com")] String toAddress)
                {
                
                toAddresses.Add(toAddress);
                toAddresses.Add("enk.contact1@gmail.com");
                                
                attDisplayNames.Add("Attachment_to2.log");

                Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAddress, toAddresses, null, null, attDisplayNames, attSource);

                String expSubj = mail.Subject;
                String expConvId = (String)mail.ConversationID;

                ((Outlook._MailItem)mail).Send();

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_1@enkata.com", fromAddress, null, null, null, expSubj, expConvId, attDisplayNames, "Email Sent");

                String[] expectedScrapes = { "To", "Mailbox", "Subject", "ConversationId", "ConversationTopic", "Id" };

                SqlDataValidator.CheckScrapePresence(sqlConnectionString, expectedScrapes);

                }
            
            }
         
        [TestFixture]
        public class TestIncomingOutlookMessages
            {
            
           

            [Test]
            public void SendMailOutlook_MultipleRecipentsTo(
            [Values("account_1@enkata.com")] String toAddress,
            [Values("kkhrebtov@enkata.com")] String fromAddress
            )
                {
                toAddresses.Add(toAddress);
                toAddresses.Add("enk.lead1@gmail.com");

                Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAddress, toAddresses, null, null, attDisplayNames, attSource);

                String expSubj = mail.Subject;
                String expConvId = (String)mail.ConversationID;
                ((Outlook._MailItem)mail).Send();

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, toAddresses, null, null, expSubj, expConvId, attDisplayNames, "Email Received");

                String[] expectedScrapes = { "To", "Mailbox", "Subject", "ConversationId", "ConversationTopic", "Id" };
                SqlDataValidator.CheckScrapePresence(sqlConnectionString, expectedScrapes);

                }


            [Test]
            public void MassSendMail_To(
            [Values("enk.lead1@gmail.com", "enk.lead2@gmail.com", "enk.lead3@gmail.com", "enk.lead4@gmail.com", "enk.contact1@gmail.com", "enk.contact2@gmail.com", "enk.contact3@gmail.com", "enk.contact4@gmail.com")] String toAddress,
            [Values("account_8@enkata.com", "account_9@enkata.com", "account_10@enkata.com", "opportunity_8@enkata.com", "opportunity_9@enkata.com", "opportunity_10@enkata.com")] String fromAddress
            )
                {

                toAddresses.Add(toAddress);
               
                Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAddress, toAddresses, null, null, attDisplayNames, attSource);

                mail.Subject = "Please ignore this message. This is created for E2E test purposes. E-mail to: " + toAddress;
                mail.Recipients.Add(toAddress);
                mail.Body = "This is body of-email from Exchnage account to " + toAddress;

                ((Outlook._MailItem)mail).Send();

                Thread.Sleep(3000);

                }

            [Test]
            public void SendMailOutlook_SinglePersonInCc(
                [Values("enk.lead3@gmail.com")] String toAddress,
                [Values("account_1@enkata.com")] String ccAddress,
                [Values("kkhrebtov@enkata.com")] String fromAddress)
                {

                toAddresses.Add(toAddress);
                ccAddresses.Add(ccAddress);

                Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAddress, toAddresses, ccAddresses, null, attDisplayNames, attSource);

                String expSubj = mail.Subject;
                String expConvId = (String)mail.ConversationID;

                ((Outlook._MailItem)mail).Send();

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, toAddresses, ccAddresses, null, expSubj, expConvId, attDisplayNames, "Email Received");
                String[] expectedScrapes = { "To", "Cc", "Mailbox", "Subject", "ConversationId", "ConversationTopic", "Id" };

                SqlDataValidator.CheckScrapePresence(sqlConnectionString, expectedScrapes);



                }

            [Test]
            public void SendMailOutlook_MultiplePersonsInCc(
                [Values("account_1@enkata.com")] String toAddress,
                [Values("enk.lead3@gmail.com")] String ccAddress,
                [Values("kkhrebtov@enkata.com")] String fromAddress)
                {

                toAddresses.Add(toAddress);
                ccAddresses.Add(ccAddress);
                ccAddresses.Add("enk.lead2@gmail.com");
               
                Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAddress, toAddresses, ccAddresses, null, attDisplayNames, attSource);

                String expSubj = mail.Subject;
                String expConvId = (String)mail.ConversationID;
                
                ((Outlook._MailItem)mail).Send();

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, toAddresses, ccAddresses, null, expSubj, expConvId, attDisplayNames, "Email Received");

                String[] expectedScrapes = { "To", "Cc", "Mailbox", "Subject", "ConversationId", "ConversationTopic", "Id" };

                SqlDataValidator.CheckScrapePresence(sqlConnectionString, expectedScrapes);

                }


            [Test]
            public void SendMailOutlook_SinglePersonInBcc(
                [Values("account_1@enkata.com")] String toAddress,
                [Values("enk.lead1@gmail.com")] String bccAddress,
                [Values("kkhrebtov@enkata.com")] String fromAddress)
                {

                toAddresses.Add(toAddress);
                bccAddresses.Add(bccAddress);

                Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAddress, toAddresses, null, bccAddresses, attDisplayNames, attSource);

                String expSubj = mail.Subject;
                String expConvId = (String)mail.ConversationID;

                ((Outlook._MailItem)mail).Send();

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, toAddresses, null, bccAddresses, expSubj, expConvId, attDisplayNames, "Email Received");

                String[] expectedScrapes = { "To", "Bcc", "Mailbox", "Subject", "ConversationId", "ConversationTopic", "Id" };

                SqlDataValidator.CheckScrapePresence(sqlConnectionString, expectedScrapes);

                }

            [Test]
            public void SendMailOutlook_MultiplePersonsInBcc(
                [Values("account_1@enkata.com")] String toAddress,
                [Values("enk.lead1@gmail.com")] String bccAddress,
                [Values("kkhrebtov@enkata.com")] String fromAddress)
                {

                toAddresses.Add(toAddress);
                bccAddresses.Add(bccAddress);
                bccAddresses.Add("enk.lead2@gmail.com");
               
                Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAddress, toAddresses, null, bccAddresses, attDisplayNames, attSource);

                String expSubj = mail.Subject;
                String expConvId = (String)mail.ConversationID;

                ((Outlook._MailItem)mail).Send();

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, toAddresses, null, bccAddresses, expSubj, expConvId, attDisplayNames, "Email Received");

                String[] expectedScrapes = { "To", "Bcc", "Mailbox", "Subject", "ConversationId", "ConversationTopic", "Id" };

                SqlDataValidator.CheckScrapePresence(sqlConnectionString, expectedScrapes);

                }

            [Test]
            public void SendMailOutlook_MultiplePersonsInCcBcc(
                [Values("account_1@enkata.com")] String toAddress,
                [Values("enk.lead1@gmail.com")] String bccAddress,
                [Values("enk.lead2@gmail.com")] String ccAddress,
                [Values("kkhrebtov@enkata.com")] String fromAddress)
                {

                toAddresses.Add(toAddress);
                ccAddresses.Add(ccAddress);
                ccAddresses.Add("enk.contact2@gmail.com");
                bccAddresses.Add(bccAddress);
                bccAddresses.Add("enk.contact1@gmail.com");
                                
                Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAddress, toAddresses, ccAddresses, bccAddresses, attDisplayNames, attSource);

                String expSubj = mail.Subject;
                String expConvId = (String)mail.ConversationID;

                ((Outlook._MailItem)mail).Send();

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, toAddresses, ccAddresses, bccAddresses, expSubj, expConvId, attDisplayNames, "Email Received");

                String[] expectedScrapes = { "To", "Cc", "Bcc", "Mailbox", "Subject", "ConversationId", "ConversationTopic", "Id" };

                SqlDataValidator.CheckScrapePresence(sqlConnectionString, expectedScrapes);

                }


            [Test]
            public void SendMailOutlook_EmptyTo_Bcc(

                [Values("account_1@enkata.com")] String bccAddress,
                [Values("enk.lead2@gmail.com")]  String ccAddress,
                [Values("kkhrebtov@enkata.com")] String fromAddress)
                {

                ccAddresses.Add(ccAddress);
                ccAddresses.Add("enk.contact2@gmail.com");
                bccAddresses.Add(bccAddress);
                bccAddresses.Add("enk.contact1@gmail.com");

                Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAddress, null, ccAddresses, bccAddresses, attDisplayNames, attSource);

                String expSubj = mail.Subject;
                String expConvId = (String)mail.ConversationID;

                ((Outlook._MailItem)mail).Send();

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, null, ccAddresses, bccAddresses, expSubj, expConvId, attDisplayNames, "Email Received");

                String[] expectedScrapes = { "Cc", "Bcc", "Mailbox", "Subject", "ConversationId", "ConversationTopic", "Id" };

                SqlDataValidator.CheckScrapePresence(sqlConnectionString, expectedScrapes);

                }

            [Test]
            public void SendMailOutlook_EmptyTo_Cc(

                [Values("account_1@enkata.com")] String ccAddress,
                [Values("enk.lead2@gmail.com")]  String bccAddress,
                [Values("kkhrebtov@enkata.com")] String fromAddress)
                {

                ccAddresses.Add(ccAddress);
                ccAddresses.Add("enk.contact2@gmail.com");
                bccAddresses.Add(bccAddress);
                bccAddresses.Add("enk.contact1@gmail.com");
                
                Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAddress, null, ccAddresses, bccAddresses, attDisplayNames, attSource);

                String expSubj = mail.Subject;
                String expConvId = (String)mail.ConversationID;

                ((Outlook._MailItem)mail).Send();

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, null, ccAddresses, bccAddresses, expSubj, expConvId, attDisplayNames, "Email Received");

                String[] expectedScrapes = { "Cc", "Bcc", "Mailbox", "Subject", "ConversationId", "ConversationTopic", "Id" };

                SqlDataValidator.CheckScrapePresence(sqlConnectionString, expectedScrapes);

                }

            [Test]
            public void SendMailOutlook_EmptySubject(

                [Values("account_1@enkata.com")] String toAddress,
                [Values("enk.lead2@gmail.com")] String ccAddress,
                [Values("kkhrebtov@enkata.com")] String fromAddress)
                {

                toAddresses.Add(toAddress);
                ccAddresses.Add(ccAddress);
                ccAddresses.Add("enk.contact2@gmail.com");
                
                Outlook._MailItem mail = EMailPrepare.CreateOutlookEmailWithEmptySubject(fromAddress, toAddresses, ccAddresses, null, attDisplayNames, attSource);

                String expSubj = "";
                String expConvId = (String)mail.ConversationID;

                ((Outlook._MailItem)mail).Send();

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, toAddresses, ccAddresses, null, expSubj, expConvId, attDisplayNames, "Email Received");

                String[] expectedScrapes = { "To", "Cc", "Mailbox", "Subject", "ConversationId", "ConversationTopic", "Id" };

                SqlDataValidator.CheckScrapePresence(sqlConnectionString, expectedScrapes);

                }

            [Test]
            public void IncomingEmailWithSingleTo(
                [Values("enk.lead1@gmail.com")] String fromAddress,
                [Values("account_1@enkata.com")] String toAddress)
                {
                               
                toAddresses.Add(toAddress);

                String subject = "This is subj of e-mail  from " + fromAddress + " to " + toAddress;
                String body = "This is body of e-mail from " + fromAddress + " to " + toAddress;

                EMailPrepare.SendGmailMessage(fromAddress, toAddresses, null, null, null, subject, body);

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, toAddresses, null, null, subject, null, null, "Email Received");

                }

            [Test]
            public void IncomingEmailWithSingleToCcBcc_To(
                [Values("enk.lead1@gmail.com")] String fromAddress,
                [Values("account_1@enkata.com")] String toAddress,
                [Values("enk.lead2@gmail.com")] String ccAddress,
                [Values("enk.lead3@gmail.com")] String bccAddress)
                {

                toAddresses.Add(toAddress);
                ccAddresses.Add(ccAddress);
                bccAddresses.Add(bccAddress);

                String subject = "This is subj of e-mail  from " + fromAddress + " to " + toAddress + " and " + ccAddress + ", " + bccAddress;
                String body = "This is body of e-mail from " + fromAddress + " to " + toAddress + " and " + ccAddress + ", " + bccAddress;

                EMailPrepare.SendGmailMessage(fromAddress, toAddresses, ccAddresses, bccAddresses, null, subject, body);

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, toAddresses, ccAddresses, bccAddresses, subject, null, null, "Email Received");

                }

            [Test]
            public void IncomingEmailWithSingleToCcBcc_Cc(
                [Values("enk.lead1@gmail.com")] String fromAddress,
                [Values("enk.lead3@gmail.com")] String toAddress,
                [Values("account_1@enkata.com")] String ccAddress,
                [Values("enk.lead2@gmail.com")] String bccAddress)
                {

                toAddresses.Add(toAddress);
                ccAddresses.Add(ccAddress);
                bccAddresses.Add(bccAddress);

                String subject = "This is subj of e-mail  from " + fromAddress + " to " + toAddress + " and " + ccAddress + ", " + bccAddress;
                String body = "This is body of e-mail from " + fromAddress + " to " + toAddress + " and " + ccAddress + ", " + bccAddress;

                EMailPrepare.SendGmailMessage(fromAddress, toAddresses, ccAddresses, bccAddresses, null, subject, body);

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, toAddresses, ccAddresses, bccAddresses, subject, null, null, "Email Received");

                }

            [Test]
            public void IncomingEmailWithSingleToCcBcc_Bcc(
                [Values("enk.lead1@gmail.com")] String fromAddress,
                [Values("enk.lead3@gmail.com")] String toAddress,
                [Values("enk.lead2@gmail.com")] String ccAddress,
                [Values("account_1@enkata.com")] String bccAddress)
                {

                toAddresses.Add(toAddress);
                ccAddresses.Add(ccAddress);
                bccAddresses.Add(bccAddress);

                String subject = "This is subj of e-mail  from " + fromAddress + " to " + toAddress + " and " + ccAddress + ", " + bccAddress;
                String body = "This is body of e-mail from " + fromAddress + " to " + toAddress + " and " + ccAddress + ", " + bccAddress;

                EMailPrepare.SendGmailMessage(fromAddress, toAddresses, ccAddresses, bccAddresses, null, subject, body);

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, toAddresses, ccAddresses, bccAddresses, subject, null, null, "Email Received");

                String[] expectedScrapes = { "To", "Cc", "Bcc", "Mailbox", "Subject", "ConversationId", "ConversationTopic", "Id" };

                SqlDataValidator.CheckScrapePresence(sqlConnectionString, expectedScrapes);

                }

            }



        [TestFixture]
        public class TestCalendarEvents
            {

            [SetUp]
            public void Initialize()
                {
                attDisplayNames.Add("Attachment_to.log");

                }

            [TearDown]
            public void Cleanup()
                {

                attDisplayNames.Clear();
                toAddresses.Clear();
                ccAddresses.Clear();
                bccAddresses.Clear();
                requiredAttendies.Clear();
                optionalAttendies.Clear();

                }

            [Test]
            public void SendMeetingToSingleRecipient(
                [Values("kkhrebtov@enkata.com")] String fromAddress,
                [Values("account_1@enkata.com")] String requiredAttendy)
                {
                              
                requiredAttendies.Add(requiredAttendy);
                requiredAttendies.Add(fromAddress);

                String subj = DateTime.UtcNow + " This is the subject for my appointment with ";
                String body = "This is the body for my appointment with ";
                foreach (String toAdress in requiredAttendies)
                    {
                    subj += toAdress;
                    subj += ", ";
                    body += toAdress;
                    body += ", ";
                    }

                String expiCalUid = AppointmentManagement.SendOutlookAppointment(fromAddress, "10/10/2014 10:10:00 AM", 0.1f, requiredAttendies, null, subj, body, attDisplayNames, attSource);

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMeetingOutlookResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, requiredAttendies, null, subj, expiCalUid, 360, null, "Appointment Created");

                AppointmentManagement.AppointmentCancel(subj);

                }

            [Test]
            public void SendMeetingToReqAndOptAttendees(
                [Values("kkhrebtov@enkata.com")] String fromAddress,
                [Values("enk.lead1@gmail.com")] String requiredAttendy,
                [Values("account_1@enkata.com")] String optionalAttendy)
                {

                requiredAttendies.Add(requiredAttendy);
                requiredAttendies.Add(fromAddress);

                optionalAttendies.Add(optionalAttendy);
                optionalAttendies.Add("enk.lead3@gmail.com");

                String subj = DateTime.UtcNow + " This is the subject for my appointment with ";
                String body = "This is the body for my appointment with ";

                foreach (String toAdress in requiredAttendies)
                    {
                    subj += toAdress;
                    subj += ", ";
                    body += toAdress;
                    body += ", ";
                    }

                String expiCalUid = AppointmentManagement.SendOutlookAppointment(fromAddress, "10/10/2014 10:10:00 AM", 0.1f, requiredAttendies, optionalAttendies, subj, body, attDisplayNames, attSource);
                Thread.Sleep(timeout);

                SqlDataValidator.CheckMeetingOutlookResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, requiredAttendies, optionalAttendies, subj, expiCalUid, 360, null, "Appointment Created");

                AppointmentManagement.AppointmentCancel(subj);

                }

            [Test]
            public void SendMeeting_SameAddressInReqAndOptAttendees(
                [Values("kkhrebtov@enkata.com")] String fromAddress,
                [Values("account_1@enkata.com")] String requiredAttendy,
                [Values("account_1@enkata.com")] String optionalAttendy)
                {

                requiredAttendies.Add(requiredAttendy);
                requiredAttendies.Add(fromAddress);

                optionalAttendies.Add(optionalAttendy);

                String subj = DateTime.UtcNow + " This is the subject for my appointment with ";
                String body = "This is the body for my appointment with ";

                foreach (String toAdress in requiredAttendies)
                    {
                    subj += toAdress;
                    subj += ", ";
                    body += toAdress;
                    body += ", ";
                    }

                String expiCalUid = AppointmentManagement.SendOutlookAppointment(fromAddress, "10/10/2014 10:10:00 AM", 0.1f, requiredAttendies, optionalAttendies, subj, body, attDisplayNames, attSource);

                Thread.Sleep(80000);

                SqlDataValidator.CheckMeetingOutlookResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, requiredAttendies, optionalAttendies, subj, expiCalUid, 360, null, "Appointment Created");

                //SqlDataValidator.CheckScrapePresence(sqlConnectionString, "OptionalAttendees");

                AppointmentManagement.AppointmentCancel(subj);

                }

            [Test]
            public void SendMeetingOnlyOptAttendees(
                [Values("kkhrebtov@enkata.com")] String fromAddress,
                [Values("account_1@enkata.com")] String optionalAttendy)
                {

                requiredAttendies.Add(fromAddress);
                optionalAttendies.Add(optionalAttendy);

                String subj = DateTime.UtcNow + " This is the subject for my appointment with ";
                String body = "This is the body for my appointment with ";

                foreach (String toAdress in optionalAttendies)
                    {
                    subj += toAdress;
                    subj += ", ";
                    body += toAdress;
                    body += ", ";
                    }

                String expiCalUid = AppointmentManagement.SendOutlookAppointment(fromAddress, "10/10/2014 10:10:00 AM", 0.1f, null, optionalAttendies, subj, body, attDisplayNames, attSource);

                Thread.Sleep(80000);

                SqlDataValidator.CheckMeetingOutlookResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, requiredAttendies, optionalAttendies, subj, expiCalUid, 360, null, "Appointment Created");

                //SqlDataValidator.CheckScrapePresence(sqlConnectionString, "OptionalAttendees");

                AppointmentManagement.AppointmentCancel(subj);

                }


            [Test]
            public void SendMeetingUpdate(
                [Values("kkhrebtov@enkata.com")] String fromAddress,
                [Values("account_1@enkata.com")] String requiredAttendy,
                [Values("enk.lead2@gmail.com")] String optionalAttendy)
                {

                Outlook.Application oApp = null;
                Outlook.NameSpace mapiNamespace = null;
                Outlook.MAPIFolder CalendarFolder = null;
                Outlook.Items outlookCalendarItems = null;

                requiredAttendies.Add(requiredAttendy);
                requiredAttendies.Add(fromAddress);

                optionalAttendies.Add(optionalAttendy);
                optionalAttendies.Add("enk.lead3@gmail.com");

                String subj = DateTime.UtcNow + " This is the subject for my appointment with ";
                String body = "This is the body for my appointment with ";
                foreach (String toAdress in requiredAttendies)
                    {
                    subj += toAdress;
                    subj += ", ";
                    body += toAdress;
                    body += ", ";
                    }

                String expiCalUid = AppointmentManagement.SendOutlookAppointment(fromAddress, "10/10/2014 10:10:00 AM", 0.1f, requiredAttendies, optionalAttendies, subj, body, attDisplayNames, attSource);

                Thread.Sleep(20000);

                Console.WriteLine("Appointment sent to recipients");

                //SqlDataValidator.CheckMeetingOutlookResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, requiredAttendies, null, subj, expiCalUid, 3600, null);

                oApp = new Outlook.Application();
                mapiNamespace = oApp.GetNamespace("MAPI");

                CalendarFolder = mapiNamespace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderCalendar);
                outlookCalendarItems = CalendarFolder.Items;

                foreach (Outlook.AppointmentItem item in outlookCalendarItems)
                    {
                    Console.WriteLine("Current item: " + item.Subject);
                    if (item.Subject == subj)
                        Console.WriteLine("Start updating Appointment");
                        {
                        item.Subject = "Update: " + subj;

                        Outlook.Recipient recipient = item.Recipients.Add("enk.lead4@gmail.com");
                        recipient.Type = (int)Outlook.OlMeetingRecipientType.olRequired;

                        item.ForceUpdateToAllAttendees = true;
                        item.Save();
                        ((Outlook._AppointmentItem)item).Send();

                        ((Outlook._AppointmentItem)item).Close(Outlook.OlInspectorClose.olSave);
                        break;
                        }
                    }

                Thread.Sleep(timeout);


                requiredAttendies.Add("enk.lead4@gmail.com");
                subj = "Update: " + subj;

                SqlDataValidator.CheckMeetingOutlookResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, requiredAttendies, optionalAttendies, subj, expiCalUid, 360, null, "Appointment Updated");

                AppointmentManagement.AppointmentCancel(subj);

                }

            [Test]
            public void SendMeetingWithEmptySubject(
                [Values("kkhrebtov@enkata.com")] String fromAddress,
                [Values("account_1@enkata.com")] String requiredAttendy)
                {

                requiredAttendies.Add(requiredAttendy);
                requiredAttendies.Add(fromAddress);

                String body = "This is the body for my appointment with ";

                String expiCalUid = AppointmentManagement.SendOutlookAppointment(fromAddress, "10/10/2014 10:10:00 AM", 0.1f, requiredAttendies, null, null, body, attDisplayNames, attSource);

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMeetingOutlookResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, requiredAttendies, null, "", expiCalUid, 360, null, "Appointment Created");

                AppointmentManagement.AppointmentCancel("");

                }

            [Test]
            public void CancelMeeting(
                [Values("kkhrebtov@enkata.com")] String fromAddress,
                [Values("account_1@enkata.com")] String requiredAttendy,
                [Values("enk.lead2@gmail.com")] String optionalAttendy)
                {

                requiredAttendies.Add(requiredAttendy);
                requiredAttendies.Add(fromAddress);

                optionalAttendies.Add(optionalAttendy);

                String subj = DateTime.UtcNow + " This is the subject for my appointment with ";
                String body = "This is the body for my appointment with ";
                foreach (String toAdress in requiredAttendies)
                    {
                    subj += toAdress;
                    subj += " ";
                    body += toAdress;
                    body += " ";
                    }

                String expiCalUid = AppointmentManagement.SendOutlookAppointment(fromAddress, "10/10/2014 10:10:00 AM", 0.1f, requiredAttendies, optionalAttendies, subj, body, attDisplayNames, attSource);

                Thread.Sleep(20000);

                Console.WriteLine("Appointment sent to recipients");

                AppointmentManagement.AppointmentCancel(subj);

                Thread.Sleep(timeout);

                subj = "Canceled: " + subj;

                SqlDataValidator.CheckMeetingOutlookResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, requiredAttendies, optionalAttendies, subj, expiCalUid, 360, null, "Appointment Canceled");

                }

            [Test]
            public void SendFullDayEventWithSingleRecipient(
                [Values("kkhrebtov@enkata.com")] String fromAddress,
                [Values("account_1@enkata.com")] String requiredAttendy)
                {

                requiredAttendies.Add(requiredAttendy);
                requiredAttendies.Add(fromAddress);

                String subj = DateTime.UtcNow + " This is full day event with ";
                String body = "This is the body of the full day with ";

                foreach (String toAdress in requiredAttendies)
                    {
                    subj += toAdress;
                    subj += ", ";
                    body += toAdress;
                    body += ", ";
                    }

                String expiCalUid = AppointmentManagement.SendFullDayAppointment(fromAddress, DateTime.Today.AddDays(45).ToString(), requiredAttendies, null, subj, body, attDisplayNames, attSource);

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMeetingOutlookResults(sqlConnectionString, "account_1_alias@enkata.com", fromAddress, requiredAttendies, null, subj, expiCalUid, 86400, null, "Appointment Created");

                AppointmentManagement.AppointmentCancel(subj);

                }

           


            }

        [TestFixture]
        public class TestGmailStreamer
            {

            [TearDown]
            public void Cleanup()
                {

                attDisplayNames.Clear();
                toAddresses.Clear();
                ccAddresses.Clear();
                bccAddresses.Clear();
                requiredAttendies.Clear();
                optionalAttendies.Clear();

                }

            [Test]
            public void GMailSendTo(
                [Values("enk.lead1@gmail.com", "enk.lead2@gmail.com", "enk.lead3@gmail.com", "enk.lead4@gmail.com", "enk.contact1@gmail.com", "enk.contact2@gmail.com", "enk.contact3@gmail.com", "enk.contact4@gmail.com")] string fromAccount,
                [Values("account_1@enkata.com", "account_1_alias@enkata.com", "account_2@enkata.com", "account_3@enkata.com", "account_4@enkata.com", "account_5@enkata.com", "account_6@enkata.com", "account_8@enkata.com", "account_9@enkata.com")] string toAccount

                )
                {

                var fromAddress = new MailAddress(fromAccount, "From Name");
                var toAddress = new MailAddress(toAccount, "To Name");

                var ccList = new MailAddressCollection();
                //ccList.Add(cc);

                String fromPassword = "Enkata#10";
                String subject = "This is subj of e-mail  from " + fromAccount + " to " + toAccount;// +"CC: " + cc;
                String body = "This is body of e-mail from " + fromAccount + " to " + toAccount;// +". CC: " + cc;

                var smtp = new SmtpClient
                   {
                       Host = "smtp.gmail.com",
                       Port = 587,
                       EnableSsl = true,
                       DeliveryMethod = SmtpDeliveryMethod.Network,
                       UseDefaultCredentials = false,
                       Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                   };

                using (var message = new MailMessage(fromAddress, toAddress)
                          {
                              Subject = subject,
                              Body = body
                          })

                    smtp.Send(message);

                Thread.Sleep(timeout);

                SqlDataValidator.CheckMailResults(sqlConnectionString, fromAccount, fromAccount, null, null, null, subject, null, null, "Email Received");

                }



            [Test]
            public void GMailSendCc(
                [Values("enk.lead1@gmail.com")] string fromAccount,
                [Values("account_1_alias@enkata.com")] string toAccount
                )
                {

                MailAddress fromAddress = new MailAddress(fromAccount, "From Name");
                MailAddress toAddress = new MailAddress(toAccount, "To Name");

                String[] ccList = { "enk.lead2@gmail.com", "enk.lead3@gmail.com" };


                String fromPassword = "Enkata#10";
                String subject = "This is subj of e-mail  from " + fromAccount + " to " + toAccount + " with " + ccList[0] + " in copy.";
                String body = "This is body of e-mail from " + fromAccount + " to " + toAccount + " with " + ccList[0] + " in copy.";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                MailMessage message = new MailMessage(fromAddress, toAddress);
                message.Subject = subject;
                message.Body = body;
                message.CC.Add("enk.lead2@gmail.com");
                message.CC.Add("enk.lead3@gmail.com");


                smtp.Send(message);
                Thread.Sleep(80000);

                toAddresses.Add("account_1@enkata.com");
                ccAddresses.Add("enk.lead2@gmail.com");
                ccAddresses.Add("enk.lead3@gmail.com");

                SqlDataValidator.CheckMailResults(sqlConnectionString, toAccount, fromAccount, toAddresses, ccAddresses, null, subject, null, null, "Email Received");

                }


            [Test]
            //[Ignore("Ignore a test")]
            public void GMailSendToEmilySmith(
                [Values("enk.lead3@gmail.com", "enk.lead1@gmail.com", "enk.lead2@gmail.com", "enk.lead4@gmail.com")] string fromAccount,
                [Values("account_8@enkata.com", "account_9@enkata.com", "account_10@enkata.com", "opportunity_8@enkata.com", "opportunity_9@enkata.com", "opportunity_10@enkata.com")] string toAccount

                )
                {

                //ArrayList toAddresses = new ArrayList();
                //toAddresses.Add(fromAccount);

                var fromAddress = new MailAddress(fromAccount, "From Name");
                var toAddress = new MailAddress(toAccount, "To Name");

                var ccList = new MailAddressCollection();

                //ccList.Add(cc);

                string fromPassword = "Enkata#10";
                string subject = "This is subj of e-mail  from " + fromAccount + " to " + toAccount;// +"CC: " + cc;
                string body = "This is body of e-mail from " + fromAccount + " to " + toAccount;// +". CC: " + cc;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                    {
                    smtp.Send(message);
                    }


                }

            }


        [TestFixture]
        public class LoadTest
            {

            [SetUp]
            public void Initialize()
                {
                attDisplayNames.Add("Attachment_to.log");

                }

            [TearDown]
            public void Cleanup()
                {

                attDisplayNames.Clear();
                toAddresses.Clear();
                ccAddresses.Clear();
                bccAddresses.Clear();
                requiredAttendies.Clear();
                optionalAttendies.Clear();

                }
            
            [Test]
            public void SanityTest(
                [Values("enk.lead3@gmail.com", "enk.lead1@gmail.com", "enk.lead2@gmail.com", "enk.lead4@gmail.com")] string fromAccount,
                [Values("account_1@enkata.com")] string toAccount

                )
                {
                
                string toAddressEx = ""; 
                string ccAddressEx = ""; 
                string bccAddressEx = "";
                int k = 0;
                int account1receivedemails = 0;

                while (true)
                    {
                    
                    requiredAttendies.Clear();
                    optionalAttendies.Clear();
                    ccAddresses.Clear();
                    bccAddresses.Clear();
                    toAddresses.Clear();

                    for (int i = 1; i < 20; i++)
                        {

                        switch (i % 10)
                            {
                            case 0: fromAccount = "enk.lead1@gmail.com";
                                break;
                            case 1: fromAccount = "enk.lead2@gmail.com";
                                break;
                            case 2: fromAccount = "enk.lead3@gmail.com";
                                break;
                            case 3: fromAccount = "enk.lead4@gmail.com";
                                break;
                            case 4: fromAccount = "enk.lead5@gmail.com";
                                break;
                            case 5: fromAccount = "enk.lead6@gmail.com";
                                break;
                            case 6: fromAccount = "enk.lead7@gmail.com";
                                break;
                            case 7: fromAccount = "enk.lead8@gmail.com";
                                break;
                            case 8: fromAccount = "enk.lead9@gmail.com";
                                break;
                            case 9: fromAccount = "enk.lead10@gmail.com";
                                break;
                           
                            }
                    

                        var fromAddress = new MailAddress(fromAccount, "From Name");
                        var toAddress = new MailAddress("account_1@enkata.com", "To Name");
                                                         
                        string fromPassword = "Enkata#10";



                        string subject = "This is subj of e-mail from " + fromAccount + " to " + toAddress.DisplayName;
                        string body    = "This is body of e-mail from " + fromAccount + " to " + toAddress.DisplayName;

                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                        };

                        using (var message = new MailMessage(fromAddress, toAddress)
                        {
                            Subject = subject,
                            Body = body                            
                        })
                                   
                            {
                            message.CC.Add("account_2@enkata.com");
                            message.CC.Add("account_3@enkata.com");
                            Console.WriteLine("Send e-mail from GMail account: " + fromAccount);
                            smtp.Send(message);
                            }

                        account1receivedemails++;

                        Console.WriteLine("Total number of e-mails sent to account_1 is:" + account1receivedemails);
                        Thread.Sleep(60000);
                        }

                    for (int i = 1; i < 20; i++)
                        {
                        
                        switch (i % 4)
                            {
                            case 0:
                                toAddressEx = "enk.lead1@gmail.com";
                                ccAddressEx = "enk.lead2@gmail.com";
                                bccAddressEx = "enk.lea3@gmail.com";
                                break;
                            case 1:
                                toAddressEx = "enk.lead2@gmail.com";
                                ccAddressEx = "enk.lead3@gmail.com";
                                bccAddressEx = "enk.lea1@gmail.com";
                                break;
                            case 2:
                                toAddressEx = "enk.lead3@gmail.com";
                                ccAddressEx = "enk.lead2@gmail.com";
                                bccAddressEx = "enk.lea1@gmail.com";
                                break;
                            case 3:
                                toAddressEx = "enk.lead2@gmail.com";
                                ccAddressEx = "enk.lead1@gmail.com";
                                bccAddressEx = "enk.lea3@gmail.com";
                                break;
                            }

                        toAddresses.Add(toAddressEx);
                        toAddresses.Add("enk.contact2@gmail.com");

                        bccAddresses.Add(bccAddressEx);
                        bccAddresses.Add("enk.contact3@gmail.com");

                        ccAddresses.Add(ccAddressEx);
                        ccAddresses.Add("enk.contact3@gmail.com");


                        Console.WriteLine("Send e-mail from Exchange account: " + fromAccount);

                        Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAccount, toAddresses, ccAddresses, bccAddresses, attDisplayNames, attSource);

                        String expSubj = mail.Subject;
                        String expConvId = (String)mail.ConversationID;

                        ((Outlook._MailItem)mail).Send();


                        }

                    requiredAttendies.Add(fromAccount);
                    requiredAttendies.Add(toAccount);

                    String subj        = DateTime.UtcNow + " This is the subject for my appointment with ";
                    String meetingBody = "This is the body for my appointment with ";

                    foreach (String toAdress in requiredAttendies)
                        {
                        subj += toAdress;
                        subj += ", ";
                        meetingBody += toAdress;
                        meetingBody += ", ";
                        }

                    Thread.Sleep(60000);

                    Console.WriteLine("Send Meeting");
                    String expiCalUid = AppointmentManagement.SendOutlookAppointment("kkhrebtov@enkata.com", DateTime.Today.AddDays(5999 + k).ToString(), 0.1f, requiredAttendies, null, subj, meetingBody, attDisplayNames, attSource);

                    k++;

                    }

                }



            [Test]
            public void Exchange2013LoadTest(
                [Values("enk.lead3@gmail.com")] string fromAccount,
                [Values("account_1@enkata.com")] string toAccount

                )
                {

                string toAddressEx  = "";
                string ccAddressEx  = "";
                string bccAddressEx = "";
                
                int countOfLoopIterations = 0;
                int numberOfTestedAccounts = 3;             // Number of Internal Accounts that will be used for test. Permission script should be run in advance for all Accounts that are intended  to be tested
                int numberofExternalAccounts = 3;           // Number of external Accounts that will be used as recipients
                int meetingsDelay = 5;                      //Delay between Meetings creation
                
                String mailDomain = "@enkata.com";          // 
                String externalAddressPrefix = "contact_";  //Addresses that will represent external accounts
                String internalAddressPrefix = "account_";  // Addresses that will be used for Streamers validation
                
                int numberOfOutcomingMails = 0;
                int numberOfIncomingMails = 0;
                int numberOfCreatedMeetings = 0;

                DateTime curTime = System.DateTime.Now;
                TimeSpan meetingsTimeout;

                while (true)
                    {

                    requiredAttendies.Clear();
                    optionalAttendies.Clear();
                    ccAddresses.Clear();
                    bccAddresses.Clear();
                    toAddresses.Clear();
                                                             
                    //// Define external account that will be used to send e-mail on the tested account
                    //fromAccount = externalAddressPrefix + (countOfLoopIterations % (numberofExternalAccounts -1) + 1).ToString() + mailDomain;
                    //Console.WriteLine("From Account: " + fromAccount);
                    
                    ////Define list of addresses we sent e-mail to.
                    //
                    //for (int j = 0; j <= numberOfTestedAccounts; j++)
                    //    {
                    //        toAddressEx = internalAddressPrefix + j.ToString() + mailDomain;

                    //        if (j < numberOfTestedAccounts) 
                    //            ccAddressEx = internalAddressPrefix + (j + 1).ToString() + mailDomain;
                    //        else ccAddressEx = internalAddressPrefix + "1" + mailDomain;
                    //        if (j > 1) 
                    //            bccAddressEx = internalAddressPrefix + (j - 1).ToString() + mailDomain;
                    //        else bccAddressEx = internalAddressPrefix + numberOfTestedAccounts.ToString() + mailDomain;
                        
                    //    toAddresses.Add(toAddressEx);
                    //    bccAddresses.Add(bccAddressEx);
                    //    ccAddresses.Add(ccAddressEx);

                    //    Console.WriteLine("Send e-mail from Exchange account: " + fromAccount + " to Account: " + toAddressEx);
                                             

                    //    Outlook._MailItem mail = EMailPrepare.CreateOutlookEmail(fromAccount, toAddresses, ccAddresses, bccAddresses, attDisplayNames, attSource);

                    //    // We should distinguish e-mail by subject in order to place it in 10 different folders. So we will use remainder of division and place it
                    //    // in subject. Later in Outlook we will create a Rule that will place each e-mail in corresponding folder depending on it subject.
                    //    mail.Subject = "Subject Num#" + countOfLoopIterations % 5 + " of Incoming e-mail from " + fromAccount + " to " + toAddressEx;
                        
                    //    ((Outlook._MailItem)mail).Send();
                    //    numberOfIncomingEmails++;

                    //    Thread.Sleep(60000/numberOfTestedAccounts);

                    //   ccAddresses.Clear();
                    //   bccAddresses.Clear();
                    //   toAddresses.Clear();

                    //    }
                    
                        

                        // Prepare outcoming e-mail
                        fromAccount = internalAddressPrefix + (countOfLoopIterations % numberOfTestedAccounts + 1).ToString() + mailDomain;
                        
                        //Define list of addresses we sent e-mail to.
                        //
                        for (int j = 1; j <= numberofExternalAccounts; j++)
                        {
                        toAddressEx = externalAddressPrefix + j.ToString() + mailDomain;

                            if (j < numberofExternalAccounts)
                                ccAddressEx = externalAddressPrefix + (j + 1).ToString() + mailDomain;
                            else ccAddressEx = externalAddressPrefix + "1" + mailDomain;
                            if (j > 1)
                                bccAddressEx = externalAddressPrefix + (j - 1).ToString() + mailDomain;
                            else bccAddressEx = externalAddressPrefix + numberofExternalAccounts.ToString() + mailDomain;
                        

                        toAddresses.Add(toAddressEx);
                        bccAddresses.Add(bccAddressEx);
                        ccAddresses.Add(ccAddressEx);

                        Console.WriteLine("Send e-mail from Exchange account: " + fromAccount + " to Account: " + toAddressEx);

                        Outlook._MailItem mailOutcoming = EMailPrepare.CreateOutlookEmail(fromAccount, toAddresses, ccAddresses, bccAddresses, attDisplayNames, attSource);

                        // We should distinguish e-mail by subject in order to place it in 10 different folders. So we will use remainder of division and place it
                        // in subject. Later in Outlook we will create a Rule that will place each e-mail in corresponding folder depending on it subject.
                        mailOutcoming.Subject = "Subject Num#" + countOfLoopIterations % 5 + " of Incoming e-mail from " + fromAccount + " to " + toAddressEx;
                        
                        ((Outlook._MailItem)mailOutcoming).Send();

                        numberOfOutcomingMails++;

                        Thread.Sleep(60000/numberofExternalAccounts);

                        ccAddresses.Clear();
                        bccAddresses.Clear();
                        toAddresses.Clear();
                    
                     }
                    
                    meetingsTimeout = DateTime.Now - curTime;

                    Console.WriteLine("Time left from previos Meeting sent: " + meetingsTimeout.Minutes + " minutes");

                    if (meetingsTimeout.Minutes > meetingsDelay)
                        {

                        Console.WriteLine("--------------#######  Prepare Meeting #########---------------");
                        for (int j = 1; j <= numberofExternalAccounts; j++)
                            {

                            toAccount = externalAddressPrefix + j.ToString() + mailDomain;

                            requiredAttendies.Add(fromAccount);
                            requiredAttendies.Add(toAccount);


                            if (j < numberofExternalAccounts)
                                optionalAttendies.Add(externalAddressPrefix + (j + 1).ToString() + mailDomain);
                            else optionalAttendies.Add(externalAddressPrefix + "1" + mailDomain);


                            String subj = DateTime.UtcNow + " This is the subject for my appointment with ";
                            String meetingBody = "This is the body for my appointment with ";

                            foreach (String toAdress in requiredAttendies)
                                {
                                subj += toAdress;
                                subj += ", ";
                                meetingBody += toAdress;
                                meetingBody += ", ";
                                }

                            Console.WriteLine("Send Meeting");
                            String expiCalUid = AppointmentManagement.SendOutlookAppointment("kkhrebtov@enkata.com", DateTime.Now.AddHours(24 + countOfLoopIterations).ToString(), 0.1f, requiredAttendies, null, subj, meetingBody, attDisplayNames, attSource);
                                                       
                            requiredAttendies.Clear();
                            optionalAttendies.Clear();
                            
                            }
                        numberOfCreatedMeetings++;
                        curTime = DateTime.Now;
                     }

                    Console.WriteLine("Outcoming Emails Created: " + numberOfOutcomingMails);
                    Console.WriteLine("Incoming Emails Created: " + numberOfIncomingMails);
                    Console.WriteLine("Meetings Scheduled: " + numberOfCreatedMeetings);

                    countOfLoopIterations++;

                    }

                }



            [Test]
            public void CreateFolders() {
  
            List<String> newFolders = new List<String>();

            newFolders.Add("enk.lead1");
            newFolders.Add("enk.lead2");
            newFolders.Add("enk.lead3");
            newFolders.Add("enk.lead4");
            //newFolders.Add("enk.contact5");

            OutlookManage.CreateFolders(newFolders, "opportunity_7@enkata.com");
            
                }


            [Test]
            public void GMailSendMultipleEMails(
                [Values("enk.lead1@gmail.com", "enk.lead2@gmail.com", "enk.lead3@gmail.com", "enk.lead4@gmail.com", "enk.contact1@gmail.com", "enk.contact2@gmail.com", "enk.contact3@gmail.com", "enk.contact4@gmail.com")] string fromAccount,
                [Values("account_1@enkata.com", "account_1_alias@enkata.com", "account_2@enkata.com", "account_2_alias@enkata.com", "account_3@enkata.com", "account_3_alias@enkata.com", "account_4@enkata.com", "account_4_alias@enkata.com", "account_5@enkata.com", "account_6@enkata.com")] string toAccount

                )
                {
                                

                var fromAddress = new MailAddress(fromAccount, "From Name");
                var toAddress = new MailAddress(toAccount, "To Name");

                var ccList = new MailAddressCollection();
                //ccList.Add(cc);

                String fromPassword = "Enkata#10";
                String subject = "This is subj of e-mail  from " + fromAccount + " to " + toAccount;// +"CC: " + cc;
                String body = "This is body of e-mail from " + fromAccount + " to " + toAccount;// +". CC: " + cc;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })

                    smtp.Send(message);

               //Thread.Sleep(timeout);

               // SqlDataValidator.CheckMailResults(sqlConnectionString, fromAccount, fromAccount, null, null, null, subject, null, null, "Email Received");

                }



            [Test]
            public void SendMassiveEmailsFromGmailAccounts()
                {


                string fromAccount;
                string toAccount = "account_1@enkata.com";
                int countOfLoopIterations = 1;

                while (true)
                    {
                    int index = countOfLoopIterations % 4;
                    if (index == 0) index = 4;
                    fromAccount = "enk.contact" + index.ToString() + "@gmail.com";

                    Console.WriteLine("Send Mail From " + fromAccount);

                    var fromAddress = new MailAddress(fromAccount, "From Name");
                    var toAddress = new MailAddress(toAccount, "To Name");

                    var ccList = new MailAddressCollection();
                    
                    String fromPassword = "Enkata#10";
                    String subject = "This is subj of e-mail #" + countOfLoopIterations + " from " + fromAccount + " to " + toAccount;// +"CC: " + cc;
                    Console.WriteLine(subject);
                    String body = "This is body of e-mail #" + countOfLoopIterations + "  from " + fromAccount + " to " + toAccount;// +". CC: " + cc;

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };

                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })

                        smtp.Send(message);
                    
                    Thread.Sleep(5000);
                    countOfLoopIterations++;

                    }

                }

            }
        }
    }
    