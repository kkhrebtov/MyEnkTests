package apitest;

import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.json.JSONException;
import org.testng.annotations.Parameters;
import org.testng.annotations.Test;

import java.io.IOException;

/**
 * Created by kkhrebtov on 1/13/2015.
 */
public class SFAPIActivitiesContacts {

    @Test
    @Parameters({"enkataUrlActivity"})
    public void activitiesFromFile(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson =  ResourceManagement.loadResource("../resources/testActivities.json");

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlActivity"})
    public void createNewActivity(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Placed\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_id\": \"111111\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"ABC\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlActivity"})
    public void updateActivity(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_id\": \"111111\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"ABC Upd\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlActivity"})
    public void createSeveralActivities(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Placed\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_id\": \"111112\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 111112\", " +
                "\"my_type\": \"Call\"" +
                "}," +
                "{\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_id\": \"111113\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 111113\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlActivity"})
    public void updateSeveralActivities(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Email Sent\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_id\": \"111112\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Email 111112\", " +
                "\"my_type\": \"Email\"" +
                "}," +
                "{\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Email Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_id\": \"111113\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Email 111113\", " +
                "\"my_type\": \"Email\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlActivity"})
    public void newAndUpdateActivities(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_id\": \"111114\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 111114\", " +
                "\"my_type\": \"Call\"" +
                "}," +
                "{\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Email Sent\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_id\": \"111113\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Email 111113\", " +
                "\"my_type\": \"Email\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }


    @Test
    @Parameters({"enkataUrlActivity"})
    public void longFieldsActivity(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Email Received: Long text Long text Long text Long text Long text Long text Long text Long Long text Long text Long text Long text Long text Long\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_id\": \"2211114\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Long text Long text Long text Long text Long text Long text Long text Long text Long text Long text Long text Long text Long text Long text Long\", " +
                "\"my_type\": \"Email\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }



    @Test
    @Parameters({"enkataUrlActivity"})
    public void fourNewActivities(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-12\", "+
                "\"ext_id\": \"111115\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 111115\", " +
                "\"my_type\": \"Call\"" +
                "}," +
                "{\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Email Sent\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-12\", "+
                "\"ext_id\": \"111116\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Email 111116\", " +
                "\"my_type\": \"Email\"" +
                "}," +
                "{\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Placed\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-12\", "+
                "\"ext_id\": \"111117\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 111117\", " +
                "\"my_type\": \"Call\"" +
                "}," +
                "{\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Email Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-12\", "+
                "\"ext_id\": \"111118\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Email 111118\", " +
                "\"my_type\": \"Email\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlActivity"})
    public void activityWrongFieldNameSubject(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subjjject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_iddd\": \"111119\", " +
                "\"who_iddd\": \"003j0000004vGkT\", " +
                "\"descriptionnn\": \"Call 111119\", " +
                "\"my_typeee\": \"Call\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Subject missing");
    }

    @Test
    @Parameters({"enkataUrlActivity"})
    public void activityWrongFieldNameExtId(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_iddd\": \"111119\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 111119\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "External Id missing");
    }

    @Test
    @Parameters({"enkataUrlActivity"})
    public void activityWrongFieldNameWhoId(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_id\": \"111119\", " +
                "\"who_iddd\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 111119\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlActivity"})
    public void activityWrongFieldNameDescr(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-01\", "+
                "\"ext_id\": \"111119\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"descriptionnn\": \"Call 111119\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlActivity"})
    public void activityWrongFieldNameType(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-01\", "+
                "\"ext_id\": \"111119\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 111119\", " +
                "\"my_typeee\": \"Call\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Test");
    }

    @Test
    @Parameters({"enkataUrlActivity"})
    public void activityWrongFieldNameOwnerId(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_idd\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-01\", "+
                "\"ext_id\": \"111119\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 111119\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Owner Id missing");
    }

    @Test
    @Parameters({"enkataUrlActivity"})
    public void activityWrongFieldNameWhatId(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_idd\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-01\", "+
                "\"ext_id\": \"111119\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 111119\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlActivity"})
    public void activityWrongFieldNameActivityDate(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"aactivity_date\": \"2015-01-01\", "+
                "\"ext_id\": \"111119\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 111119\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Activity Date missing");
    }

    @Test
    @Parameters({"enkataUrlActivity"})
    public void ActivityWrongDateFormat(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015/01/02\", "+
                "\"ext_id\": \"111119\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 111119\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Date string doesn't match the pattern");
    }

    @Test
    @Parameters({"enkataUrlActivity"})
    public void ActivityTooLongDescr(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-02\", "+
                "\"ext_id\": \"111119\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUES\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Upsert failed. First exception on row 0; first error: STRING_TOO_LONG, Description: data value too large: INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFD... (max length=32000): Description");
    }

    @Test
    @Parameters({"enkataUrlActivity"})
    public void activityWrongFieldWhoIdWhatId(String enkataUrlActivity) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_idddd\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_id\": \"111119\", " +
                "\"who_iddd\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 111119\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlActivity, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "What Id and Who Id missing");
    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void createNewContact(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"John\", " +
                "\"last_name\": \"Doe\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"john.doe@example.com\", " +
                "\"phone\": \"(650) 555-5555\", " +
                "\"title\": \"General Manager\", " +
                "\"ext_id\": \"cont11111\", " +
                "\"discovered\": \"2014-12-17\", " +
                "\"description\": \"Test\" " +
               "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);
        ResourceManagement.checkResponseBody(response, "error", "What Id and Who Id missing");

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void createNewContactWithSameEmail(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Larry\", " +
                "\"last_name\": \"Smith\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"john.doe@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"General Manager\", " +
                "\"ext_id\": \"cont11121\", " +
                "\"discovered\": \"2015-01-15\", " +
                "\"description\": \"Test\" " +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Contact with specified email and account id already exists");

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void updateContact(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Johnson\", " +
                "\"last_name\": \"Dowson\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"john.doe@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont11111\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Contact with specified email and account id already exists");

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void createSeveralContacts(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Jack\", " +
                "\"last_name\": \"London\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"jack.london@example.com\", " +
                "\"phone\": \"(650) 551-5555\", " +
                "\"title\": \"General Manager\", " +
                "\"ext_id\": \"cont11112\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\"" +
                "}," +
                "{\"first_name\": \"Albert\", " +
                "\"last_name\": \"Smith\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"albert.smith@example.com\", " +
                "\"phone\": \"(650) 552-5555\", " +
                "\"title\": \"General Manager\", " +
                "\"ext_id\": \"cont11113\", " +
                "\"discovered\": \"2015-01-11\", " +
                "\"description\": \"Test\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void createSeveralContactsWithSameEmail(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Mark\", " +
                "\"last_name\": \"Twen\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"jack.london@example.com\", " +
                "\"phone\": \"(650) 551-5555\", " +
                "\"title\": \"General Manager\", " +
                "\"ext_id\": \"cont11122\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\"" +
                "}," +
                "{\"first_name\": \"Silver\", " +
                "\"last_name\": \"Locky\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"albert.smith@example.com\", " +
                "\"phone\": \"(650) 552-5556\", " +
                "\"title\": \"General Manager\", " +
                "\"ext_id\": \"cont11123\", " +
                "\"discovered\": \"2015-01-11\", " +
                "\"description\": \"Test\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Contact with specified email and account id already exists");

    }


    @Test
    @Parameters({"enkataUrlContact"})
    public void updateSeveralContacts(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Jackson\", " +
                "\"last_name\": \"Londonson\", " +
                //"\"account_id\": \"001j000000B61vQ\", " +
                "\"account_id\": \"001j000000B61vT\", " +
                "\"email\": \"jack.london@example.com\", " +
                "\"phone\": \"(651) 551-5555\", " +
                "\"title\": \"Employee\", " +
                "\"ext_id\": \"cont11112\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\"" +
                "}," +
                "{\"first_name\": \"Alberter\", " +
                "\"last_name\": \"Smithter\", " +
                //"\"account_id\": \"001j000000B61vQ\", " +
                "\"account_id\": \"001j000000B61vT\", " +
                "\"email\": \"albert.smith@example.com\", " +
                "\"phone\": \"(651) 552-5555\", " +
                "\"title\": \"Employee\", " +
                "\"ext_id\": \"cont11113\", " +
                "\"discovered\": \"2015-01-11\", " +
                "\"description\": \"Test\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void newAndUpdateContact(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Jackson\", " +
                "\"last_name\": \"Londonson\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"jack.london@example.com\", " +
                "\"phone\": \"(651) 551-5555\", " +
                "\"title\": \"Employee\", " +
                "\"ext_id\": \"cont11112\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\"" +
                "}," +
                "{\"first_name\": \"Kurt\", " +
                "\"last_name\": \"Russel\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"kurt.russel@example.com\", " +
                "\"phone\": \"(651) 553-5555\", " +
                "\"title\": \"Employee\", " +
                "\"ext_id\": \"cont11114\", " +
                "\"discovered\": \"2015-01-11\", " +
                "\"description\": \"Test\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Test");

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void newContactWithSameEmailAndAccountId(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Daniel\", " +
                "\"last_name\": \"Smith\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"john.doe@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont11115\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Contact with specified email and account id already exists");

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void contactWrongFieldNameFirstName(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"fffirst_name\": \"Alex\", " +
                "\"last_name\": \"Jones\", " +
                "\"account_id\": \"001j000000B61vR\", " +
                "\"email\": \"ajones@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont11116\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void contactWrongFieldNameLastName(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alex\", " +
                "\"lllast_name\": \"Jones\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"ajones@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont11116\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Last name missing");

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void contactWrongFieldNameAccountId(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alex\", " +
                "\"last_name\": \"Jones\", " +
                "\"aaaccount_id\": \"001j000000B61vQ\", " +
                "\"email\": \"ajones@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont11116\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Account ID missing or invalid");

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void contactWrongFieldNameEmail(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alex\", " +
                "\"last_name\": \"Jones\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"eeemail\": \"ajones@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont11116\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Email missing");

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void contactWrongFieldNamePhone(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alexis\", " +
                "\"last_name\": \"Jonesss\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"ajonesss@example.com\", " +
                "\"ppphone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont11117\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void contactWrongFieldNameTitle(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alexander\", " +
                "\"last_name\": \"Yoness\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"ayoness@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"ttttitle\": \"Executive\", " +
                "\"ext_id\": \"cont11118\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void contactWrongFieldNameExtId(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alex\", " +
                "\"last_name\": \"Jones\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"ajones@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"eeeext_id\": \"cont11116\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "External ID missing");

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void contactWrongFieldNameDateDiscovered(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alex\", " +
                "\"last_name\": \"Jones\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"ajones@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont11116\", " +
                "\"dddddiscovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Date discovered missing");

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void contactWrongFieldNameDescr(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alexa\", " +
                "\"last_name\": \"Jonston\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"ajonston@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont11119\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"ddddescription\": \"Test\" " +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);
        ResourceManagement.checkResponseBody(response, "error", "Date discovered missing");

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void contactWrongEmailAddress(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Robin\", " +
                "\"last_name\": \"Smith\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"rsmithexamplecom\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont11121\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Upsert failed. First exception on row 0; first error: INVALID_EMAIL_ADDRESS, Email: invalid email address: rsmithexamplecom: Email");

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void contactWrongDateFormat(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Robinson\", " +
                "\"last_name\": \"Smith\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"rob.smith@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont11122\", " +
                "\"discovered\": \"2015/01/12\", " +
                "\"description\": \"Test\" " +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Date string doesn't match the pattern");

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void contactTooLongDescription(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Roben\", " +
                "\"last_name\": \"VanPersy\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"rvpersy@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont11123\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUES\" " +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Upsert failed. First exception on row 0; first error: STRING_TOO_LONG, Enkata Description: data value too large: INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFD... (max length=32768): EnkataNGDev__Description__c");

    }

    @Test
    @Parameters({"enkataUrlContact"})
    public void contactWithInvalidAccountId(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alex\", " +
                "\"last_name\": \"Jones\", " +
                "\"account_id\": \"ZZZZZZZZZZ\", " +
                "\"email\": \"ajones@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont11116\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Account ID missing or invalid");

    }


    @Test
    @Parameters({"enkataUrlContact"})
    public void contactWithManyOpps(String enkataUrlContact) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alexander\", " +
                "\"last_name\": \"Maxes\", " +
                "\"account_id\": \"ZZZZZZZZZZ\", " +
                "\"email\": \"ajones@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont11116\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlContact, token, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 412 Precondition Failed");
        ResourceManagement.checkResponseBody(response, "error", "Account ID missing or invalid");

    }



}
