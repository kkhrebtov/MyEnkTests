using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TestMailStreamers
    {
     class AppointmentManagement
        {


         public static String SendOutlookAppointment(String fromAddress, String startDate, float duration, List<String> requiredAttendies, List<String> optionalAttendies, String subj, String body, List<String> attachments, String attachLocation)
             {
             
         Outlook.Application application = new Outlook.Application();
         Outlook.AppointmentItem oAppointment = (Outlook.AppointmentItem)application.CreateItem (Outlook.OlItemType.olAppointmentItem);

         oAppointment.Subject = subj;
         oAppointment.Body = body; 
         oAppointment.Location = "Appointment location"; 
          
         // Set the start date
         oAppointment.Start = Convert.ToDateTime(startDate);
         // End date 
         oAppointment.End = Convert.ToDateTime(startDate).AddHours(duration);
         // Set the reminder 15 minutes before start
         oAppointment.ReminderSet = true; 
         oAppointment.ReminderMinutesBeforeStart = 15;
                      
         //Setting the importance: 
         //use OlImportance enum to set the importance to low, medium or high
          
         oAppointment.Importance = Outlook.OlImportance.olImportanceHigh; 
          
         /* OlBusyStatus is enum with following values:
         olBusy
         olFree
         olOutOfOffice
         olTentative
         */
         oAppointment.BusyStatus = Outlook.OlBusyStatus.olBusy;
         oAppointment.MeetingStatus = Outlook.OlMeetingStatus.olMeeting;
                       
        
        if (requiredAttendies != null)
            {
            foreach (String toAddress in requiredAttendies)
                {
                Outlook.Recipient recipient = oAppointment.Recipients.Add(toAddress);
                recipient.Type =  (int)Outlook.OlMeetingRecipientType.olRequired;
                }
            }

        if (optionalAttendies != null)
            {
            foreach (String optAddress in optionalAttendies)
                {
                Outlook.Recipient optRecipient = oAppointment.Recipients.Add(optAddress);
                optRecipient.Type = (int)Outlook.OlMeetingRecipientType.olOptional;
                }
            }

        //Outlook.Recipient organizer = oAppointment.Recipients.Add(fromAddress);
        //organizer.Type = (int)Outlook.OlMeetingRecipientType.olOrganizer;

        oAppointment.Recipients.ResolveAll();
        //String iCalUid = oAppointment.GlobalAppointmentID;


        if (!attachments.Count.Equals(0))
            {

            foreach (String attachName in attachments)
                {
                String sSource = attachLocation;
                String sDisplayName = attachName;

                int iPosition = (int)oAppointment.Body.Length + 1;
                int iAttachType = (int)Outlook.OlAttachmentType.olByValue;

                oAppointment.Attachments.Add(sSource, iAttachType, iPosition, sDisplayName);
                }
            }
     
        
        // Save the appointment
        oAppointment.SaveAs(@"D:\Temp\Test.ics", Outlook.OlSaveAsType.olICal);

        //Outlook.Account acc = GetAccount(application, fromAddress);
        //oAppointment.SendUsingAccount = acc;
                     
        ((Outlook._AppointmentItem)oAppointment).Send();
      
        String conversationId = oAppointment.ConversationID;
        //Console.WriteLine("ConversationId: " + conversationId);
                  
        String guid = oAppointment.GlobalAppointmentID;
        //Console.WriteLine("guid: " + guid);

        ((Outlook._AppointmentItem)oAppointment).Close(Outlook.OlInspectorClose.olSave);

        //Outlook.MailItem mailItem = oAppointment.ForwardAsVcal(); 
        //     mailItem.SendUsingAccount = GetAccount(application, fromAddress); 
        //// email address to send to 
        //     mailItem.To = "account_3@enkata.com";
        //
        //((Outlook._MailItem)mailItem).Send(); 

        return guid;

        }

         public static String SendFullDayAppointment(String fromAddress, String startDate, List<String> requiredAttendies, List<String> optionalAttendies, String subj, String body, List<String> attachments, String attachLocation)
             {

             Outlook.Application application = new Outlook.Application();
             Outlook.AppointmentItem oAppointment = (Outlook.AppointmentItem)application.CreateItem(Outlook.OlItemType.olAppointmentItem);

             oAppointment.Subject = subj;
             oAppointment.Body = body;
             oAppointment.Location = "Appointment location";

             // Set the start date
             oAppointment.Start = Convert.ToDateTime(startDate);

             oAppointment.AllDayEvent = true;
            
            // Set the reminder 15 minutes before start
             oAppointment.ReminderSet = true;
             oAppointment.ReminderMinutesBeforeStart = 15;

             //Setting the importance: 
             //use OlImportance enum to set the importance to low, medium or high

             oAppointment.Importance = Outlook.OlImportance.olImportanceHigh;

             /* OlBusyStatus is enum with following values:
             olBusy
             olFree
             olOutOfOffice
             olTentative
             */
             oAppointment.BusyStatus = Outlook.OlBusyStatus.olBusy;
             oAppointment.MeetingStatus = Outlook.OlMeetingStatus.olMeeting;
            
             if (requiredAttendies != null)
                 {
                 foreach (String toAddress in requiredAttendies)
                     {
                     Outlook.Recipient recipient = oAppointment.Recipients.Add(toAddress);
                     recipient.Type = (int)Outlook.OlMeetingRecipientType.olRequired;
                     }
                 }

             if (optionalAttendies != null)
                 {
                 foreach (String optAddress in optionalAttendies)
                     {
                     Outlook.Recipient optRecipient = oAppointment.Recipients.Add(optAddress);
                     optRecipient.Type = (int)Outlook.OlMeetingRecipientType.olOptional;
                     }
                 }

             //Outlook.Recipient organizer = oAppointment.Recipients.Add(fromAddress);
             //organizer.Type = (int)Outlook.OlMeetingRecipientType.olOrganizer;

             oAppointment.Recipients.ResolveAll();
             //String iCalUid = oAppointment.GlobalAppointmentID;

             if (!attachments.Count.Equals(0))
                 {

                 foreach (String attachName in attachments)
                     {
                     String sSource = attachLocation;
                     String sDisplayName = attachName;

                     int iPosition = (int)oAppointment.Body.Length + 1;
                     int iAttachType = (int)Outlook.OlAttachmentType.olByValue;

                     oAppointment.Attachments.Add(sSource, iAttachType, iPosition, sDisplayName);
                     }
                 }
            
             // Save the appointment
             oAppointment.SaveAs(@"D:\Temp\Test.ics", Outlook.OlSaveAsType.olICal);

             //Outlook.Account acc = OutlookManage.GetAccount(application, fromAddress);
             //oAppointment.SendUsingAccount = acc;

             ((Outlook._AppointmentItem)oAppointment).Send();
                        
             String guid = oAppointment.GlobalAppointmentID;
             Console.WriteLine("guid: " + guid);

             ((Outlook._AppointmentItem)oAppointment).Close(Outlook.OlInspectorClose.olSave);

             //Outlook.MailItem mailItem = oAppointment.ForwardAsVcal(); 
             //     mailItem.SendUsingAccount = GetAccountForEmailAddress(application, fromAddress); 
             //// email address to send to 
             //     mailItem.To = "account_3@enkata.com";
             //
             //((Outlook._MailItem)mailItem).Send(); 

             return guid;

             }

        public static void AppointmentCancel(String subject)
            {
            
            Outlook.Application oApp = null;
            Outlook.NameSpace mapiNamespace = null;
            Outlook.MAPIFolder CalendarFolder = null;
            Outlook.Items outlookCalendarItems = null;

            oApp = new Outlook.Application();
            mapiNamespace = oApp.GetNamespace("MAPI");

            CalendarFolder = mapiNamespace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderCalendar);
            outlookCalendarItems = CalendarFolder.Items;

            foreach (Outlook.AppointmentItem item in outlookCalendarItems)
                {
                Console.WriteLine("Current item: " + item.Subject);
                if (item.Subject == subject)
                    Console.WriteLine("Cancelling Appointment");
                    {
                    item.MeetingStatus = Outlook.OlMeetingStatus.olMeetingCanceled;

                    item.ForceUpdateToAllAttendees = true;
                    item.Save();
                    ((Outlook._AppointmentItem)item).Send();

                    ((Outlook._AppointmentItem)item).Close(Outlook.OlInspectorClose.olSave);
                    item.Delete();
                    
                    break;
                    }
                }
            }
                  
         }
    
    }
