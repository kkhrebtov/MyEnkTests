package apitest;

import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.json.JSONException;
import org.testng.annotations.Parameters;
import org.testng.annotations.Test;

import java.io.IOException;

/**
 * Created by kkhrebtov on 1/26/2015.
 */
public class EWSActivities {

    @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void activitiesFromFile(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json

        String testJson =  ResourceManagement.loadResource("../resources/testActivities.json");

        System.out.println(testJson);

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }


    @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void createNewActivity(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
        "\"owner_id\": \"005j0000000ah8c\", " +
        "\"subject\": \"Call Placed\", " +
        "\"what_id\": \"006j0000002CWGa\", " +
        "\"activity_date\": \"2014-12-17\", "+
        "\"ext_id\": \"2111111\", " +
        "\"who_id\": \"003j0000004vGkT\", " +
        "\"description\": \"ABCD\", " +
        "\"my_type\": \"Call\"" +
        "}]";

        System.out.println(testJson);

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }

    @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void upsertActivity(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
        "\"owner_id\": \"005j0000000ah8c\", " +
        "\"subject\": \"Call Received\", " +
        "\"what_id\": \"006j0000002CWGa\", " +
        "\"activity_date\": \"2014-12-17\", "+
        "\"ext_id\": \"2111111\", " +
        "\"who_id\": \"003j0000004vGkT\", " +
        "\"description\": \"ABCD Upd\", " +
        "\"my_type\": \"Call\"" +
        "}]";

        System.out.println(testJson);

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }

 @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void createSeveralActivities(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
        "\"owner_id\": \"005j0000000ah8c\", " +
        "\"subject\": \"Call Placed\", " +
        "\"what_id\": \"006j0000002CWGa\", " +
        "\"activity_date\": \"2014-12-17\", "+
        "\"ext_id\": \"2111111\", " +
        "\"who_id\": \"003j0000004vGkT\", " +
        "\"description\": \"ABCD 2111111\", " +
        "\"my_type\": \"Call\"" +
        "}]";

        System.out.println(testJson);

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }

    @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void updateSeveralActivities(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Placed\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_id\": \"2111112\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 2111112\", " +
                "\"my_type\": \"Call\"" +
                "}," +
                "{\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_id\": \"2111113\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 2111113\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        System.out.println(testJson);

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }

    @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void newAndUpdateActivities(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_id\": \"2111114\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 2111114\", " +
                "\"my_type\": \"Call\"" +
                "}," +
                "{\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Email Sent\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_id\": \"2111113\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Email 2111113\", " +
                "\"my_type\": \"Email\"" +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }

    @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void fourNewActivities(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-12\", "+
                "\"ext_id\": \"2111115\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 2111115\", " +
                "\"my_type\": \"Call\"" +
                "}," +
                "{\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Email Sent\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-12\", "+
                "\"ext_id\": \"2111116\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Email 2111116\", " +
                "\"my_type\": \"Email\"" +
                "}," +
                "{\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Placed\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-12\", "+
                "\"ext_id\": \"2111117\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 2111117\", " +
                "\"my_type\": \"Call\"" +
                "}," +
                "{\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Email Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-12\", "+
                "\"ext_id\": \"2111118\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Email 2111118\", " +
                "\"my_type\": \"Email\"" +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }

    @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void activityWrongFieldNameSubject(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subjjject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_iddd\": \"212111119\", " +
                "\"who_iddd\": \"003j0000004vGkT\", " +
                "\"descriptionnn\": \"Call 212111119\", " +
                "\"my_typeee\": \"Call\"" +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void activityWrongFieldNameExtId(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_iddd\": \"29111119\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 29111119\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void activityWrongFieldNameWhoId(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_id\": \"27111119\", " +
                "\"who_iddd\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 27111119\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void activityWrongFieldNameDescr(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-01\", "+
                "\"ext_id\": \"28111119\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"descriptionnn\": \"Call 28111119\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }

    @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void activityWrongFieldNameType(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-01\", "+
                "\"ext_id\": \"2111119\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 2111119\", " +
                "\"my_typeee\": \"Call\"" +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void activityWrongFieldNameOwnerId(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_idd\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-01\", "+
                "\"ext_id\": \"2111119\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 2111119\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void activityWrongFieldNameWhatId(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_idd\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-01\", "+
                "\"ext_id\": \"22111119\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 22111119\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void activityWrongFieldNameActivityDate(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"aactivity_date\": \"2015-01-01\", "+
                "\"ext_id\": \"23111119\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 23111119\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void ActivityWrongDateFormat(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015/01/02\", "+
                "\"ext_id\": \"24111119\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 24111119\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void ActivityTooLongDescr(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_id\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2015-01-02\", "+
                "\"ext_id\": \"25111119\", " +
                "\"who_id\": \"003j0000004vGkT\", " +
                "\"description\": \"25INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUES\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiActivities", "tenantId"})
    public void activityWrongFieldWhoIdWhatId(String enkataLumenApiActivities, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"owner_id\": \"005j0000000ah8c\", " +
                "\"subject\": \"Call Received\", " +
                "\"what_idddd\": \"006j0000002CWGa\", " +
                "\"activity_date\": \"2014-12-17\", "+
                "\"ext_id\": \"26111119\", " +
                "\"who_iddd\": \"003j0000004vGkT\", " +
                "\"description\": \"Call 26111119\", " +
                "\"my_type\": \"Call\"" +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiActivities, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }

}
