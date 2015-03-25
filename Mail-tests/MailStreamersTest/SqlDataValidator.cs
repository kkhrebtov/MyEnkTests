using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TestMailStreamers
    {
    public static class SqlDataValidator
        
        {

        /// <summary>
        /// Method intended to connect to SQL database and select from it all scrapes of the latest received or sent Email. After that it passes query results and expected results to ReadMailSingleRow method for validation.
        /// </summary>
        /// <param name="sqlConnectionString">Represents connection string for SQL database</param>
        /// <param name="mailbox">Represents name of the mailbox that is tracked by Exchange Streamer</param>
        /// <param name="fromAddress">Represents eexpected -mail address that was used for sending e-mail</param>
        /// <param name="toAddress">Array of the e-mail adresses that we expect to see in To scrapes of the email</param>
        /// <param name="ccList">Array of the e-mail adresses that  expect to see in Cc scrapes of the email</param>
        /// <param name="bccList">Array of the e-mail adresses that  expect to see in Bcc scrapes of the email</param>
        /// <param name="subj">Attachment name that we expect to see in the ConversationTopic and Subject scrapes</param>
        /// <param name="convId">Expected ConversationId of the e-mail</param>
        /// <param name="attach">Name of the attached file</param>
        /// <returns>void</returns>
        /// 

        public static void CheckMailResults(String sqlConnectionString, String mailbox, String fromAddress, List<String> toAddress, List<String> ccList, List<String> bccList, String subj, String convId, List<String> attach, String category)
            {
            String queryString = "SELECT a.name_, a.value_, b.category_ FROM AAL_EVENT_DUMP_EVENTS_DATA as a INNER JOIN AAL_EVENT_DUMP_EVENTS as b ON a.event_id_ = b.event_id_ where b.event_id_ =  (select max(event_id_) from AAL_EVENT_DUMP_EVENTS  where category_ like '%Email%')";

                SqlDataReader rdr = null;
                SqlConnection conn = new SqlConnection(sqlConnectionString);
                SqlCommand cmd = new SqlCommand(queryString, conn);

                conn.Open();

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    ReadMailSingleRow((IDataRecord)rdr, mailbox, fromAddress, toAddress, subj, convId, attach, ccList, bccList, category);
                    }

                conn.Close();
                conn.Dispose();
            
            }

        /// <summary>
        /// Method intended to connect to SQL database and select from it all scrapes of the latest received or sent Meeting. After that it passes query results and expected results to ReadMailSingleRow method for validation.
        /// </summary>
        /// <param name="sqlConnectionString">Represents connection string for SQL database</param>
        /// <param name="mailbox">Represents name of the mailbox that is tracked by Exchange Streamer</param>
        /// <param name="organizer">Represents expected e-mail address that was used for Meeting creation</param>
        /// <param name="reqAttendees">ArrayList of the e-mail adresses that we expect to see in RequiredAttendees scrapes of the Meeting</param>
        /// <param name="optAttendees">Array of the e-mail adresses that  expect to see in OptionalAttendees scrapes of the Meeting</param>
        /// <param name="subj">Attachment name that we expect to see in the ConversationTopic and Subject scrapes</param>
        /// <param name="iCalUid">Expected iCalUid of the Meeting</param>
        /// <param name="duration">Expected Meeting duration</param>
        /// <param name="attach">Name of the attached file</param>
        /// <returns>void</returns>
        /// 

        public static void CheckMeetingOutlookResults(String sqlConnectionString, String mailbox, String organizer, List<String> reqAttendees, List<String> optAttendees, String subj, String iCalUid, int duration, List<String> attach, String category)
            {
            String queryString = "SELECT a.name_, a.value_ FROM AAL_EVENT_DUMP_EVENTS_DATA as a INNER JOIN AAL_EVENT_DUMP_EVENTS as b ON a.event_id_ = b.event_id_ where b.event_id_ =  (select max(event_id_) from AAL_EVENT_DUMP_EVENTS  where category_ like '%Appointment%')";

            SqlDataReader rdr = null;
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            SqlCommand cmd = new SqlCommand(queryString, conn);

            conn.Open();

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
                {
                ReadMeetingSingleRow((IDataRecord)rdr, mailbox, organizer, reqAttendees, optAttendees, subj, iCalUid, duration, attach, null);
                }

            conn.Close();
            conn.Dispose();

            }

        /// <summary>
        /// Method verifies wheteher all scrapes passed to the method present in the database
        /// </summary>
        /// <param name="sqlConnectionString">Represents connection string for SQL database</param>
        /// <param name="scrapeNames">List of all expected scrapes</param>
        /// <returns>void</returns>
        /// 

        public static void CheckScrapePresence(String sqlConnectionString, String[] scrapeNames)
            {
            String queryString = "SELECT name_, value_ FROM AAL_EVENT_DUMP_EVENTS_DATA where event_id_ =  (select max(event_id_) from AAL_EVENT_DUMP_EVENTS  where category_ like '%Email%' or category_ like '%Appointment%')";

            foreach (String scrapeName in scrapeNames)
                {
                SqlDataReader rdr = null;
                SqlConnection conn = new SqlConnection(sqlConnectionString);
                SqlCommand cmd = new SqlCommand(queryString, conn);


                conn.Open();

                rdr = cmd.ExecuteReader();

                bool scrapeFound = false;
                while (rdr.Read())
                    {
                    if (((IDataRecord)rdr)[0].ToString().Equals(scrapeName)) scrapeFound = true;
                    }
                Assert.IsTrue(scrapeFound, "Scrape " + scrapeName + " not Found");

                conn.Close();
                conn.Dispose();
                }

            }

        /// <summary>
        /// This method reads results of the query line by line and checks whether actual scrape value equal to the expected one
        /// </summary>
        /// <param name="sqlConnectionString">Represents connection string for SQL database</param>
        /// <param name="scrapeNames">List of all expected scrapes</param>
        /// <returns>void</returns>
        /// 

        private static void ReadMailSingleRow(IDataRecord record, String mailbox, String fromAddress, List<String> toAddress, String subject, String convId, List<String> attachments, List<String> ccList, List<String> bccList, String category)
            {
            Console.WriteLine(String.Format("{0}, {1}", record[0], record[1]));

            bool testPassTo = false;
            bool testPassCc = false;
            bool testPassBcc = false;
            bool testPassAttach = false;
            
            switch ((String)record[0])
                {
                case "Mailbox":
                    Console.WriteLine("Mailbox actual: " + (String)record[1] + "    Mailbox Expected: " + mailbox);
                    if (mailbox != null) StringAssert.AreEqualIgnoringCase(mailbox, (String)record[1], "Mailbox field verification failed");
                    break;
                case "From":
                    if (fromAddress != null) StringAssert.AreEqualIgnoringCase(fromAddress, (String)record[1], "From field verification failed");
                    break;
                case "To":
                    if (toAddress != null) {

                        foreach (String address in toAddress)
                        {
                            if (address == (String)record[1]) testPassTo = true;
                        }

                        Assert.IsTrue(testPassTo, "To field verification failed");
                        }
                    break;
                case "Subject":
                    if (subject != null) StringAssert.AreEqualIgnoringCase(subject, (String)record[1], "Subject field verification failed");
                    break;
                case "ConversationTopic":
                    if (subject != null) StringAssert.AreEqualIgnoringCase(subject, (String)record[1], "ConversationTopic field verification failed");
                    break;
                case "ConversationId":
                    if (convId != null) StringAssert.AreEqualIgnoringCase(convId, (String)record[1], "ConversationId field verification failed");
                    break;
                case "AttachmentNames":
                    if (!attachments.Count.Equals(0))
                        foreach (String attach in attachments)
                            {
                            if (attach == (String)record[1]) testPassAttach = true;
                            }
                       Assert.IsTrue(testPassAttach, "Attachments field verification failed");
                    break;
                case "Cc":
                    if (ccList != null)
                        {
                        foreach (String cc in ccList)
                            {
                            if (cc == (String)record[1]) testPassCc = true;
                            }
                        Assert.IsTrue(testPassCc, "Cc field verification failed");
                        }     
                    break;
                case "Bcc":
                    if (bccList != null)
                        {
                        foreach (String bcc in bccList)
                            {
                            if (bcc == (String)record[1]) testPassBcc = true;
                            }
                        Assert.IsTrue(testPassBcc, "Bcc field verification failed");
                        }     
                    break;
                default:
                    break;
                }

            StringAssert.AreEqualIgnoringCase(category, (String)record[2], "Category field verification failed");

            }

        private static void ReadMeetingSingleRow(IDataRecord record, String mailbox, String organizer, List<String> reqAttendees, List<String> optAttendees, String subject, String iCalUid, int duration, List<String> attachments, String category)
            {
            Console.WriteLine(String.Format("{0}: {1}", record[0], record[1]));

            bool testPassReqAtt = false;
            bool testPassOptAtt = false;
            bool testPassAttach = false;

            switch ((String)record[0])
                {
                case "Mailbox":
                    Console.WriteLine("Mailbox actual: " + (String)record[1] + "    Mailbox Expected: " + mailbox);
                    if (mailbox != null) StringAssert.AreEqualIgnoringCase(mailbox, (String)record[1], "Mailbox field verification failed");
                    break;
                case "Organizer":
                    if (organizer != null) StringAssert.AreEqualIgnoringCase(organizer, (String)record[1], "Organizer field verification failed");
                    break;
                case "RequiredAttendees":
                    if (reqAttendees != null)
                        {
                        foreach (String address in reqAttendees)
                            {
                            Console.WriteLine("Actual ReqAttendee: " + (String)record[1] + "       Expected ReqAttendee: " + address);
                            if (address == (String)record[1]) testPassReqAtt = true;
                            }
                        Assert.IsTrue(testPassReqAtt, "RequiredAttendees field verification failed");
                        }
                    break;
                case "OptionalAttendees":
                    if (optAttendees != null)
                        {

                        foreach (String address in optAttendees)
                            {
                            Console.WriteLine("Actual OptAttendee: " + (String)record[1] + "       Expected ReqAttendee: " + address);
                            if (address == (String)record[1]) testPassOptAtt = true;
                            }
                        Assert.IsTrue(testPassOptAtt, "OptionalAttendees field verification failed");
                        }
                    break;
                case "Subject":
                    if (subject != null) StringAssert.AreEqualIgnoringCase(subject, (String)record[1], "Subject field verification failed");
                    break;
                case "iCalUid":
                    if (iCalUid != null) StringAssert.AreEqualIgnoringCase(iCalUid, (String)record[1], "iCalUid field verification failed");
                    break;
                case "AttachmentNames":
                    if(!attachments.Count.Equals(0)) {
                        foreach (String attach in attachments)
                            {
                            if (attach == (String)record[1]) testPassAttach = true;
                            }
                       Assert.IsTrue(testPassAttach, "Attachments field verification failed");
                    }
                    break;
                case "IsMeeting":
                    StringAssert.AreEqualIgnoringCase("1", (String)record[1], "IsMeeting field verification failed");
                    break;
                case "Duration":
                    if (duration != null) StringAssert.AreEqualIgnoringCase(duration.ToString(), (String)record[1], "Duration field verification failed");
                    break;
                 default:
                    //Assert.IsTrue(false, "Required scrapes not found for latest event_id_");
                    break;
                }

            if(category != null) StringAssert.AreEqualIgnoringCase(category, (String)record[2], "Category field verification failed");

            }

        }
    }
