package apitest;

import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.json.JSONException;
import org.testng.annotations.Parameters;
import org.testng.annotations.Test;

import java.io.IOException;

/**
 * Created by kkhrebtov on 1/12/2015.
 */
public class EWSRecommendations {

    @Test
    @Parameters({"enkataLumenApi", "tenantId"})
    public void newRecsFromfile(String enkataLumenApi, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        String typesJson = ResourceManagement.loadResource("../resources/forDemoVideo.json");


        System.out.println(typesJson);

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApi, tenantId, typesJson);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }


    @Test
    @Parameters({"enkataLumenApi", "tenantId"})
    public void createNewRecommendation(String enkataLumenApi, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String newRecommendation =  "[{"+
                "\"ext_id\": \"A5UTWx4Ku6QGrofLvWUP\","+
                "\"type_id\": \"opp_deal_unlikely\"," +
                "\"obj_type\": \"Opportunity\"," +
                "\"obj_id\": \"006j0000002CWGe\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                "\"action_id\": \"action_deal_unlikely\"," +
                "\"key\": \"dummy\"," +
                "\"value\": \"dummy\"" +
                "}]," +
                "\"contacts\": []" +
                "}]";

        System.out.println(newRecommendation);

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApi, tenantId, newRecommendation);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");


    }

    @Test
    @Parameters({"enkataLumenApi", "tenantId"})
    public void updateRecommendation(String enkataLumenApi, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String newRecommendation =  "[{"+
                "\"ext_id\": \"A5UTWx4Ku6QGrofLvWUP\","+
                "\"type_id\": \"opp_deal_unlikely\"," +
                "\"obj_type\": \"Opportunity\"," +
                "\"obj_id\": \"006j0000002CWGe\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                "\"action_id\": \"action_deal_unlikely\"," +
                "\"key\": \"dummy\"," +
                "\"value\": \"dummy\"" +
                "}]," +
                "\"contacts\": []" +
                "}]";

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApi, tenantId, newRecommendation);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");

    }

    @Test
    @Parameters({"enkataLumenApi", "tenantId"})
    public void createMultipleRecommendations(String enkataLumenApi, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String newRecommendation =  "[{"+
                "\"ext_id\": \"C5UTWx4Ku6QGrofLvWUP\","+
                "\"type_id\": \"opp_deal_unlikely\"," +
                "\"obj_type\": \"Opportunity\"," +
                "\"obj_id\": \"006j0000002CWGe\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                "\"action_id\": \"action_deal_unlikely\"," +
                "\"key\": \"dummy\"," +
                "\"value\": \"dummy\"" +
                "}]," +
                "\"contacts\": []" +
                "}," +
                "{"+
                "\"ext_id\": \"D6UTWx4Ku6QGrofLvWUP\","+
                "\"type_id\": \"opp_time_to_touch_base\"," +
                "\"obj_type\": \"Opportunity\"," +
                "\"obj_id\": \"006j0000002CWGe\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                "\"action_id\": \"action_close_opp\"," +
                "\"key\": \"dummy\"," +
                "\"value\": \"dummy\"" +
                "}]," +
                "\"contacts\": []" +
                "}]";


        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApi, tenantId, newRecommendation);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }

    @Test
    @Parameters({"enkataLumenApi", "tenantId"})
    public void updateMultipleRecommendations(String enkataLumenApi, String tenantId) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String newRecommendation =  "[{"+
                "\"ext_id\": \"C5UTWx4Ku6QGrofLvWUP\","+
                "\"type_id\": \"opp_time_to_touch_base\"," +
                "\"obj_type\": \"Opportunity\"," +
                "\"obj_id\": \"006j0000002CWGa\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                "\"action_id\": \"action_close_opp\"," +
                "\"key\": \"dummy\"," +
                "\"value\": \"dummy\"" +
                "}]," +
                "\"contacts\": []" +
                "}," +
                "{"+
                "\"ext_id\": \"D6UTWx4Ku6QGrofLvWUP\","+
                "\"type_id\": \"opp_time_to_touch_base\"," +
                "\"obj_type\": \"Opportunity\"," +
                "\"obj_id\": \"006j0000002CWGa\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                "\"action_id\": \"action_close_opp\"," +
                "\"key\": \"dummy\"," +
                "\"value\": \"dummy\"" +
                "}]," +
                "\"contacts\": []" +
                "}]";


        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApi, tenantId, newRecommendation);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 201 Created");
    }


    @Test
    @Parameters({"enkataLumenApi"})
    public void wrongAuthorizationHeader(String enkataLumenApi) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String newRecommendation =  "[{"+
                "\"ext_id\": \"A5UTWx4Ku6QGrofLvWUP\","+
                "\"type_id\": \"opp_time_to_touch_base\"," +
                "\"obj_type\": \"Opportunity\"," +
                "\"obj_id\": \"006j0000002CWGe\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                "\"action_id\": \"action_close_opp\"," +
                "\"key\": \"dummy\"," +
                "\"value\": \"dummy\"" +
                "}]," +
                "\"contacts\": []" +
                "}]";

        System.out.println(newRecommendation);

        HttpResponse response = ResourceManagement.sendJSon(enkataLumenApi, "wrongHeader", newRecommendation);

        ResourceManagement.checkHTTPResponse(response, "HTTP/1.1 403 Forbidden");

    }

}
