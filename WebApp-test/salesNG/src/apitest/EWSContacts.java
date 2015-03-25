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
public class EWSContacts {
    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void createNewContact(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"John\", " +
                "\"last_name\": \"Doe\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"johner.doe@example.com\", " +
                "\"phone\": \"(650) 555-5555\", " +
                "\"title\": \"General Manager\", " +
                "\"ext_id\": \"cont211111\", " +
                "\"discovered\": \"2014-12-17\", " +
                "\"description\": \"Test\" " +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void createNewContactWithSameEmail(String enkataLumenApiContacts,String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Larsson\", " +
                "\"last_name\": \"Smith\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"johner.doe@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"General Manager\", " +
                "\"ext_id\": \"cont211121\", " +
                "\"discovered\": \"2015-01-15\", " +
                "\"description\": \"Test\" " +
                "}]";

       
        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void updateContact(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Johnson\", " +
                "\"last_name\": \"Dowson\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"john.doe@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont211111\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void createSeveralContacts(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{" +
                "\"first_name\": \"Black\", " +
                "\"last_name\": \"London\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"black.london@example.com\", " +
                "\"phone\": \"(650) 551-5555\", " +
                "\"title\": \"General Manager\", " +
                "\"ext_id\": \"cont211112\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\"" +
                "}," +
                "{\"first_name\": \"White\", " +
                "\"last_name\": \"Smith\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"white.smith@example.com\", " +
                "\"phone\": \"(650) 552-5555\", " +
                "\"title\": \"General Manager\", " +
                "\"ext_id\": \"cont211113\", " +
                "\"discovered\": \"2015-01-11\", " +
                "\"description\": \"Test\"" +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void createSeveralContactsWithSameEmail(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Markus\", " +
                "\"last_name\": \"Twen\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"Markus.Twen@example.com\", " +
                "\"phone\": \"(650) 551-5555\", " +
                "\"title\": \"General Manager\", " +
                "\"ext_id\": \"cont211122\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\"" +
                "}," +
                "{\"first_name\": \"Markus\", " +
                "\"last_name\": \"Twenson\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"Markus.Twen@example.com\", " +
                "\"phone\": \"(650) 552-5556\", " +
                "\"title\": \"General Manager\", " +
                "\"ext_id\": \"cont211123\", " +
                "\"discovered\": \"2015-01-11\", " +
                "\"description\": \"Test\"" +
                "}]";

       
        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }


    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void updateSeveralContacts(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Jackson\", " +
                "\"last_name\": \"Londonson\", " +
                //"\"account_id\": \"001j000000B61vQ\", " +
                "\"account_id\": \"001j000000B61vT\", " +
                "\"email\": \"black.london@example.com\", " +
                "\"phone\": \"(651) 551-5555\", " +
                "\"title\": \"Employee\", " +
                "\"ext_id\": \"cont211112\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\"" +
                "}," +
                "{\"first_name\": \"Alberter\", " +
                "\"last_name\": \"Smithter\", " +
                //"\"account_id\": \"001j000000B61vQ\", " +
                "\"account_id\": \"001j000000B61vT\", " +
                "\"email\": \"white.smith@example.com\", " +
                "\"phone\": \"(651) 552-5555\", " +
                "\"title\": \"Employee\", " +
                "\"ext_id\": \"cont211113\", " +
                "\"discovered\": \"2015-01-11\", " +
                "\"description\": \"Test\"" +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void newAndUpdateContact(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Jackson\", " +
                "\"last_name\": \"Londonson\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"jack.london@example.com\", " +
                "\"phone\": \"(651) 551-5555\", " +
                "\"title\": \"Employee\", " +
                "\"ext_id\": \"cont211112\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\"" +
                "}," +
                "{\"first_name\": \"Kurt\", " +
                "\"last_name\": \"Russel\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"kurt.russel@example.com\", " +
                "\"phone\": \"(651) 553-5555\", " +
                "\"title\": \"Employee\", " +
                "\"ext_id\": \"cont211114\", " +
                "\"discovered\": \"2015-01-11\", " +
                "\"description\": \"Test\"" +
                "}]";

       
        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void newContactWithSameEmailAndAccountId(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Daniel\", " +
                "\"last_name\": \"Smith\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"john.doe@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont211115\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

     
        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void contactWrongFieldNameFirstName(String enkataLumenApiContacts,String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"fffirst_name\": \"Alex\", " +
                "\"last_name\": \"Jones\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"ajones@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont211116\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void contactWrongFieldNameLastName(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alex\", " +
                "\"lllast_name\": \"Jones\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"ajones@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont211116\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";
            
        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);
        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void contactWrongFieldNameAccountId(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alex\", " +
                "\"last_name\": \"Jones\", " +
                "\"aaaccount_id\": \"001j000000B61vQ\", " +
                "\"email\": \"ajones@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont211116\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

       
        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);
        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void contactWrongFieldNameEmail(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alex\", " +
                "\"last_name\": \"Jones\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"eeemail\": \"ajones@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont211116\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

       
        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void contactWrongFieldNamePhone(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alexis\", " +
                "\"last_name\": \"Joness\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"ajoness@example.com\", " +
                "\"ppphone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont211117\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

      
        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);
        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void contactWrongFieldNameTitle(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alexander\", " +
                "\"last_name\": \"Yones\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"ayones@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"ttttitle\": \"Executive\", " +
                "\"ext_id\": \"cont211118\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);
        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void contactWrongFieldNameExtId(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alex\", " +
                "\"last_name\": \"Jones\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"ajones@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"eeeext_id\": \"cont211116\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";
          

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void contactWrongFieldNameDateDiscovered(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alex\", " +
                "\"last_name\": \"Jones\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"ajones@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont211116\", " +
                "\"dddddiscovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        
        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void contactWrongFieldNameDescr(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alexa\", " +
                "\"last_name\": \"Jonston\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"ajonston@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont211119\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"ddddescription\": \"Test\" " +
                "}]";

        
        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void contactWrongEmailAddress(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Robin\", " +
                "\"last_name\": \"Smith\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"rsmithexamplecom\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont211121\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void contactWrongDateFormat(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Robinson\", " +
                "\"last_name\": \"Smith\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"rob.smith@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont211122\", " +
                "\"discovered\": \"2015/01/12\", " +
                "\"description\": \"Test\" " +
                "}]";
                
        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void contactTooLongDescription(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Roben\", " +
                "\"last_name\": \"VanPersy\", " +
                "\"account_id\": \"001j000000B61vQ\", " +
                "\"email\": \"rvpersy@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont211123\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESAAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_INSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESattr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUESINSERT INTO AAL_SFDC_SNAPSHOT_DATA (snapshot_id, object_id, attr_name, attr_value) VALUES\" " +
                "}]";
        
        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
        

    }

    @Test
    @Parameters({"enkataLumenApiContacts", "tenantId"})
    public void contactWithInvalidAccountId(String enkataLumenApiContacts, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"first_name\": \"Alex\", " +
                "\"last_name\": \"Jones\", " +
                "\"account_id\": \"ZZZZZZZZZZ\", " +
                "\"email\": \"ajones@example.com\", " +
                "\"phone\": \"(650) 555-5556\", " +
                "\"title\": \"Executive\", " +
                "\"ext_id\": \"cont211116\", " +
                "\"discovered\": \"2015-01-12\", " +
                "\"description\": \"Test\" " +
                "}]";

        
        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApiContacts, tenantId, testJson);
        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }



}
