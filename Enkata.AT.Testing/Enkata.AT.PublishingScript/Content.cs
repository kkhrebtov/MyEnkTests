
namespace Enkata.ActivityTracker.Testing.PublishingScript
{
    public static class Content
    {
        public static string ContentXml
        {
            get
            {
                var content1 =
                    "<project-version-info name=\"EnkataProject\" fileName=\"Project3.dll\" assemblyName=\"Project3.dll\" id=\"Project-8CEE6E8E7BCD1B0\" version=\"5.1.285.0\" " +
                    " deploymentVersion=\"1.0\" configuration=\"\" deploymentSecurity=\"None\" obfuscated=\"False\" runtime=\"True\" digest=\"09400570B61445D9451687756D9D57\" />" +
                    "<event-dump time=\"2012-05-02T03:20:03.451-07:00\" trigger=\"OpenSpan\" category=\"OpenSpan:start\" />";
                const string content2 =
                    "<event-dump time=\"2012-05-02T03:20:09.545-07:00\" trigger=\"\" category=\"Behavior:FocusIn\"><data name=\"application_id\" value=\"&quot;C:\\Program Files (x86)\\Enkata Technologies Inc\\Activity Tracker\\OS Runtime\\Enkata.Analytics.VideoRecordingHost.exe&quot; &quot; -session:{CC4A8062-440C-41C7-8D28-98EECB7A667F} -restarted:0 -stopEvent:Global{162ACBF7-FFAE-49D0-B8CA-EE0D32191CDC} -start\" /></event-dump>" +
                    "<event-dump time=\"2012-05-02T03:20:09.528-07:00\" trigger=\"Behavior\" category=\"Behavior:idle-start\" />" +
                    "<event-dump time=\"2012-05-02T03:20:24.576-07:00\" trigger=\"Behavior\" category=\"Behavior:idle-stop\"><data name=\"idle_start\" value=\"2012-05-02T03:20:09.528-07:00\" /></event-dump>" +
                    "<event-dump time=\"2012-05-02T03:20:24.576-07:00\" trigger=\"\" category=\"Behavior:FocusIn\"><data name=\"application_id\" value=\"&quot;C:\\Set\\calc.exe&quot; \" /></event-dump>" +
                    "<event-dump time=\"2012-05-02T03:20:29.326-07:00\" trigger=\"Behavior\" category=\"Behavior:hotkey\"><data name=\"key\" value=\"F1\" /></event-dump>" +
                    "<event-dump time=\"2012-05-02T03:20:35.326-07:00\" trigger=\"Behavior\" category=\"Behavior:hotkey\"><data name=\"modifier\" value=\"Alt\" /><data name=\"key\" value=\"S\" /></event-dump>" +
                    "<event-dump time=\"2012-05-02T03:20:41.326-07:00\" trigger=\"Behavior\" category=\"Behavior:hotkey\"><data name=\"modifier\" value=\"Alt\" /><data name=\"key\" value=\"F4\" /></event-dump>" +
                    "<event-dump time=\"2012-05-02T03:20:41.482-07:00\" trigger=\"\" category=\"Behavior:FocusIn\"><data name=\"application_id\" value=\"&quot;C:\\Program Files (x86)\\NUnit 2.6\\bin\\nunit.exe&quot; \" /></event-dump>" +
                    "<event-dump time=\"2012-05-02T03:20:46.435-07:00\" trigger=\"\" category=\"Behavior:FocusIn\"><data name=\"application_id\" value=\"&quot;C:\\Set\\calc.exe&quot; \" /></event-dump>" +
                    "<event-dump time=\"2012-05-02T03:20:54.858-07:00\" trigger=\"Automation1.enkataEvent1\" category=\"\"><data name=\"Field1\" value=\"31. \" /></event-dump>" +
                    "<event-dump time=\"2012-05-02T03:20:54.825-07:00\" trigger=\"Behavior\" category=\"Behavior:idle-start\" />" +
                    "<event-dump time=\"2012-05-02T03:22:02.326-07:00\" trigger=\"Behavior\" category=\"Behavior:idle-stop\"><data name=\"idle_start\" value=\"2012-05-02T03:20:54.825-07:00\" /></event-dump>" +
                    "<event-dump time=\"2012-05-02T03:22:02.326-07:00\" trigger=\"\" category=\"Behavior:FocusIn\"><data name=\"application_id\" value=\"&quot;C:\\Set\\calc.exe&quot; \" /></event-dump>" +
                    "<event-dump time=\"2012-05-02T03:22:05.825-07:00\" trigger=\"Behavior\" category=\"Behavior:idle-start\" />" +
                    "<event-dump time=\"2012-05-02T03:23:13.358-07:00\" trigger=\"Behavior\" category=\"Behavior:idle-stop\"><data name=\"idle_start\" value=\"2012-05-02T03:22:05.825-07:00\" /></event-dump>" +
                    "<event-dump time=\"2012-05-02T03:23:13.358-07:00\" trigger=\"OpenSpan\" category=\"OpenSpan:stop\" />";

                for (var i = 0; i < 40; i++)
                {
                    content1 = content1 + content2;
                }
                return content1;
            }

        }

        public static string ContentLog
        {

            get
            {
                const string content1 = "INFO  | 2012-05-16 07:07:20,437 | 0x0000007c |   |   | Watchdog.Configuration         | Configuration settings are loaded successfully" +
                                        "ERROR | 2012-05-16 07:07:20,453 | 0x0000007c |   |   | Watchdog.Configuration         | Configuration cannot be read: Configuration error: The network path was not found." +
                                        "INFO  | 2012-05-16 07:07:21,625 | 0x0000007c |   |   | Watchdog.Configuration         | Configuration settings are loading..." +
                                        "INFO  | 2012-05-16 07:07:21,625 | 0x0000007c |   |   | Watchdog.Configuration         | WATCHDOG CONFIGURATION SETTINGS" +
                                        "INFO  | 2012-05-16 07:07:21,625 | 0x0000007c |   |   | Watchdog.Configuration         | <?xml version=\"1.0\" encoding=\"utf-8\"?>;";
                var content2 = "INFO  | 2012-05-16 07:07:20,437 | 0x0000007c |   |   | Watchdog.Configuration         | Configuration settings are loaded successfully" +
                    "ERROR | 2012-05-16 07:07:20,453 | 0x0000007c |   |   | Watchdog.Configuration         | Configuration cannot be read: Configuration error: The network path was not found." +
                    "INFO  | 2012-05-16 07:07:21,625 | 0x0000007c |   |   | Watchdog.Configuration         | Configuration settings are loading..." +
                    "INFO  | 2012-05-16 07:07:21,625 | 0x0000007c |   |   | Watchdog.Configuration         | WATCHDOG CONFIGURATION SETTINGS" +
                    "INFO  | 2012-05-16 07:07:21,625 | 0x0000007c |   |   | Watchdog.Configuration         | <?xml version=\"1.0\" encoding=\"utf-8\"?>;"; ;

                for (var i = 0; i < 40; i++)
                {
                    content2 = content1 + content2;
                }
                return content2;
            }

        }
    }
}
