package apitest;

import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.json.JSONException;
import org.testng.annotations.Parameters;
import org.testng.annotations.Test;

import java.io.IOException;



public class SFAPIRecommendations {

    @Test
    @Parameters({"enkataUrlRecType", "enkataUrlRecAction", "enkataUrlRecommendation"})
    public void createComplete(String enkataUrlRecType, String enkataUrlRecAction, String enkataUrlRecommendation) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testType ="[{"+
                "\"name\": \"Test Recommendation\","+
                "\"explanation\": \"Recommended for test purposes\","+
                "\"summary\": \"Summary of the test recommendation\","+
                "\"ext_id\": \"bbbbbbbbbbbbb1\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse responseType = ResourceManagement.sendJSon(enkataUrlRecType, token, testType);

        ResourceManagement.checkHTTPResponseOK(responseType);

        // Create json
        String testAction =  "[{"+
                "\"ext_id\": \"aaaaaaaaaaaaaa1\","+
                "\"name\": \"Make Call\""+
                "},{" +
                "\"ext_id\": \"aaaaaaaaaaaaaa2\","+
                "\"name\": \"Send Email\""+
                "}]";

        //String authToken = ResourceManagement.getAuthToken();
        //String token = "Bearer " + authToken;
        //System.out.println("Auth Token: " + authToken);

        HttpResponse responseAction = ResourceManagement.sendJSon(enkataUrlRecAction, token, testAction);

        ResourceManagement.checkHTTPResponseOK(responseAction);

        // Create json
        String newRecommendation =  "[{"+
                "\"ext_id\": \"ccccccccccc11\","+
                "\"type_id\": \"bbbbbbbbbbbbb1\"," +
                "\"obj_type\": \"Opportunity\"," +
                "\"obj_id\": \"006j0000002CWGa\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                    "\"action_id\": \"aaaaaaaaaaaaaa1\"," +
                    "\"key\": \"dummy\"," +
                    "\"value\": \"dummy\"" +
                "},{" +
                "\"action_id\": \"aaaaaaaaaaaaaa2\"," +
                    "\"key\": \"dummy\"," +
                    "\"value\": \"dummy\"" +
                "}]," +
                "\"contacts\": [" +
                "{"+
                    "\"action_id\": \"aaaaaaaaaaaaaa1\"," +
                    "\"contact\": \"003j0000004vGkv\"" +
                "},{"+
                    "\"action_id\": \"aaaaaaaaaaaaaa2\"," +
                    "\"contact\": \"003j0000004vGkv\"" +
                "}]" +
                "}]";

        //String authToken = ResourceManagement.getAuthToken();
        //System.out.println("Auth Token: " + authToken);
        //String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement. sendJSon(enkataUrlRecommendation, token, newRecommendation);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlRecType", "enkataUrlRecAction", "enkataUrlRecommendation"})
    public void createComplete2(String enkataUrlRecType, String enkataUrlRecAction, String enkataUrlRecommendation) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {


        // Create json
        String newRecommendation =  "[{"+
                "\"ext_id\": \"ccccccccccc2\","+
                "\"type_id\": \"bbbbbbbbbbbbb1\"," +
                "\"obj_type\": \"Opportunity\"," +
                "\"obj_id\": \"006j0000002CWGa\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": []," +
                "\"contacts\": [" +
                "{"+
                "\"action_id\": \"aaaaaaaaaaaaaa1\"," +
                "\"contact\": \"003j0000004vGkv\"" +
                "},{"+
                "\"action_id\": \"aaaaaaaaaaaaaa2\"," +
                "\"contact\": \"003j0000004vGkv\"" +
                "}]" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();
        System.out.println("Auth Token: " + authToken);
        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement. sendJSon(enkataUrlRecommendation, token, newRecommendation);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlRecType", "enkataUrlRecAction", "enkataUrlRecommendation"})
    public void demoRecommendations(String enkataUrlRecType, String enkataUrlRecAction, String enkataUrlRecommendation) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String typesJson = ResourceManagement.loadResource("../resources/recTypesEmptyTemplate.json");
        String actionsJson = ResourceManagement.loadResource("../resources/recActionsDemo.json");

        String recsJson1 = ResourceManagement.loadResource("../resources/RecommendationsDemoFeb17.json");

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse responseTypes = ResourceManagement.sendJSon(enkataUrlRecType, token, typesJson);
        ResourceManagement.checkHTTPResponseOK(responseTypes);

        //HttpResponse responseActions = ResourceManagement.sendJSon(enkataUrlRecAction, token, actionsJson);
        //ResourceManagement.checkHTTPResponseOK(responseActions);

        //HttpResponse responseRecs1 = ResourceManagement.sendJSon(enkataUrlRecommendation, token, recsJson1);
        //ResourceManagement.checkHTTPResponseOK(responseRecs1);


    }

    @Test
    @Parameters({"enkataUrlRecType", "enkataUrlRecAction", "enkataUrlRecommendation"})
    public void sortTest(String enkataUrlRecType, String enkataUrlRecAction, String enkataUrlRecommendation) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String typesJson = ResourceManagement.loadResource("../resources/sortTypes.json");
        String actionsJson = ResourceManagement.loadResource("../resources/sortActions.json");

        String recsJson1 = ResourceManagement.loadResource("../resources/sortRecommendations_part1.json");
        //String recsJson1 = ResourceManagement.loadResource("../resources/sortRecommendations_part1.json");
        //String recsJson2 = ResourceManagement.loadResource("../resources/sortRecommendations_part2.json");

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse responseTypes = ResourceManagement.sendJSon(enkataUrlRecType, token, typesJson);
        ResourceManagement.checkHTTPResponseOK(responseTypes);

        HttpResponse responseActions = ResourceManagement.sendJSon(enkataUrlRecAction, token, actionsJson);
        ResourceManagement.checkHTTPResponseOK(responseActions);

        HttpResponse responseRecs1 = ResourceManagement.sendJSon(enkataUrlRecommendation, token, recsJson1);
        ResourceManagement.checkHTTPResponseOK(responseRecs1);

        //HttpResponse responseRecs2 = ResourceManagement.sendJSon(enkataUrlRecommendation, token, recsJson2);
        //ResourceManagement.checkHTTPResponseOK(responseRecs2);

    }


    @Test
    @Parameters({"enkataUrlRecType"})
    public void createRecTypeFromFile(String enkataUrlRecType) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson = ResourceManagement.loadResource("../resources/recTypesEmptyTemplate.json");

        System.out.println(testJson);

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecType, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);


    }

    @Test
    @Parameters({"enkataUrlRecAction"})
    public void createNewActionFromFile(String enkataUrlRecAction) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String newActionJson = ResourceManagement.loadResource("../resources/recActionsDemo.json");

        System.out.println(newActionJson);

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecAction, token, newActionJson);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlRecommendation"})
    public void createRecommendationFromFile(String enkataUrlRecommendation) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson = ResourceManagement.loadResource("../resources/RecommendationsDemoJFeb5.json");

        System.out.println(testJson);

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecommendation, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);
    }


    @Test
    @Parameters({"enkataUrlRecType"})
    public void createNewRecType(String enkataUrlRecType) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
         String testJson ="[{"+
            "\"name\": \"ABC\","+
            "\"explanation\": \"ABC\","+
            "\"summary\": \"DEF\","+
            "\"ext_id\": \"PSey8gx88UqYx36Rt6RNLQ\"" +
            "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecType, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlRecType"})
    public void upsertRecType(String enkataUrlRecType) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"name\": \"ABCUpd\","+
                "\"explanation\": \"ABCUpd\","+
                "\"summary\": \"DEFUpd\","+
                "\"ext_id\": \"PSey8gx88UqYx36Rt6RNLQ\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecType, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlRecType"})
    public void createSeveralRecTypes(String enkataUrlRecType) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"name\": \"ABCD1\","+
                "\"explanation\": \"ABCD1\","+
                "\"summary\": \"DEFG1\","+
                "\"ext_id\": \"PSey8gx88UqYx36Rt6RNLR\"" +
                "}," +
                "{\"name\": \"ABCD2\","+
                "\"explanation\": \"ABCD2\"," +
                "\"summary\": \"DEFG2\"," +
                "\"ext_id\": \"PSey8gx88UqYx36Rt6RNLS\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecType, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlRecType"})
    public void upsertSeveralRecTypes(String enkataUrlRecType) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"name\": \"ABCD1 Upd\","+
                "\"explanation\": \"ABCD1 Upd\","+
                "\"summary\": \"DEFG1 Upd\","+
                "\"ext_id\": \"PSey8gx88UqYx36Rt6RNLR\"" +
                "}," +
                "{\"name\": \"ABCD2 Upd\","+
                "\"explanation\": \"ABCD2 Upd\"," +
                "\"summary\": \"DEFG2 Upd\"," +
                "\"ext_id\": \"PSey8gx88UqYx36Rt6RNLS\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecType, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlRecType"})
    public void upsertInsertSeveralRecTypes(String enkataUrlRecType) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String testJson ="[{"+
                "\"name\": \"ABCD1 Upd2\","+
                "\"explanation\": \"ABCD1 Upd2\","+
                "\"summary\": \"DEFG1 Upd2\","+
                "\"ext_id\": \"PSey8gx88UqYx36Rt6RNLR\"" +
                "}," +
                "{\"name\": \"ABCD2 Upd2\","+
                "\"explanation\": \"ABCD2 Upd2\"," +
                "\"summary\": \"DEFG2 Upd2\"," +
                "\"ext_id\": \"PSey8gx88UqYx36Rt6RNLS\"" +
                "}," +
                "{\"name\": \"ABCD3\","+
                "\"explanation\": \"ABCD3\"," +
                "\"summary\": \"DEFG3\"," +
                "\"ext_id\": \"PSey8gx88UqYx36Rt6RKLS\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecType, token, testJson);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlRecAction"})
    public void createNewAction(String enkataUrlRecAction) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String newActionJson =  "[{"+
                "\"ext_id\": \"Zsk78y88UqYx36Rt6RNLZ\","+
                "\"name\": \"Recommendation action 1\""+
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        String token = "Bearer " + authToken;

        System.out.println("Auth Token: " + authToken);

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecAction, token, newActionJson);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlRecAction"})
    public void upsertAction(String enkataUrlRecAction) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String newActionJson =  "[{"+
                "\"ext_id\": \"Zsk78y88UqYx36Rt6RNLZ\","+
                "\"name\": \"Recommendation action 1 updated\" "+
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecAction, token, newActionJson);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlRecAction"})
    public void createSeveralActions(String enkataUrlRecAction) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String newActionJson =  "[{"+
                "\"ext_id\": \"Zsk78y88UqYx36Rt6RNLS\","+
                "\"name\": \"Recommendation action 2\" "+
                "}," +
                "{"+
                "\"ext_id\": \"Zsk78y88UqYx36Rt6RNLQ\","+
                "\"name\": \"Recommendation action 3\" "+
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecAction, token, newActionJson);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlRecAction"})
    public void upsertSeveralActions(String enkataUrlRecAction) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String newActionJson =  "[{"+
                "\"ext_id\": \"Zsk78y88UqYx36Rt6RNLS\","+
                "\"name\": \"Make & Log Call\" "+
                "}," +
                "{"+
                "\"ext_id\": \"Zsk78y88UqYx36Rt6RNLQ\","+
                "\"name\": \"Send Email\" "+
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecAction, token, newActionJson);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlRecommendation"})
    public void createNewRecommendation(String enkataUrlRecommendation) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String newRecommendation =  "[{"+
            "\"ext_id\": \"O5UTWx4Ku6QGrofLvWUP\","+
                "\"type_id\": \"PSey8gx88UqYx36Rt6RNLQ\"," +
                "\"obj_type\": \"Opportunity\"," +
                "\"obj_id\": \"006j0000002CWGa\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                "\"action_id\": \"Zsk78y88UqYx36Rt6RNLS\"," +
                    "\"key\": \"dummy\"," +
                    "\"value\": \"dummy\"" +
            "}]," +
                "\"contacts\": [" +
                "{"+
                "\"action_id\": \"Zsk78y88UqYx36Rt6RNLS\"," +
                "\"contact\": \"003j0000004vGkT\"" +
                "},{"+
                "\"action_id\": \"Zsk78y88UqYx36Rt6RNLQ\"," +
                "\"contact\": \"003j0000004vGkT\"" +
                "}]" +
        "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement. sendJSon(enkataUrlRecommendation, token, newRecommendation);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlRecommendation"})
    public void updateRecommendation(String enkataUrlRecommendation) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String newRecommendation =  "[{"+
                "\"ext_id\": \"O5UTWx4Ku6QGrofLvWUP\","+
                "\"type_id\": \"PSey8gx88UqYx36Rt6RNLQ\"," +
                "\"obj_type\": \"Opportunity\"," +
                "\"obj_id\": \"006j0000002CWGa\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                "\"action_id\": \"Zsk78y88UqYx36Rt6RNLQ\"," +
                "\"key\": \"dummy\"," +
                "\"value\": \"dummy\"" +
                "}]," +
                "\"contacts\": []" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecommendation, token, newRecommendation);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlRecommendation"})
    public void createMultipleRecommendations(String enkataUrlRecommendation) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String newRecommendation =  "[{"+
                "\"ext_id\": \"Q5UTWx4Ku6QGrofLvWUP\","+
                "\"type_id\": \"PSey8gx88UqYx36Rt6RNLQ\"," +
                "\"obj_type\": \"Opportunity\"," +
                "\"obj_id\": \"006j0000002CWGe\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                "\"action_id\": \"Zsk78y88UqYx36Rt6RNLS\"," +
                "\"key\": \"dummy\"," +
                "\"value\": \"dummy\"" +
                "}]," +
                "\"contacts\": []" +
                "}," +
                "{"+
                "\"ext_id\": \"Q6UTWx4Ku6QGrofLvWUP\","+
                "\"type_id\": \"PSey8gx88UqYx36Rt6RNLQ\"," +
                "\"obj_type\": \"Opportunity\"," +
                "\"obj_id\": \"006j0000002CWGe\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                "\"action_id\": \"Zsk78y88UqYx36Rt6RNLS\"," +
                "\"key\": \"dummy\"," +
                "\"value\": \"dummy\"" +
                "}]," +
                "\"contacts\": []" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecommendation, token, newRecommendation);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlRecommendation"})
    public void updateMultipleRecommendations(String enkataUrlRecommendation) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String newRecommendation =  "[{"+
                "\"ext_id\": \"Q5UTWx4Ku6QGrofLvWUP\","+
                "\"type_id\": \"PSey8gx88UqYx36Rt6RNLQ\"," +
                "\"obj_type\": \"Opportunity\"," +
                "\"obj_id\": \"006j0000002CWGa\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                "\"action_id\": \"Zsk78y88UqYx36Rt6RNLS\"," +
                "\"key\": \"dummy\"," +
                "\"value\": \"dummy\"" +
                "}]," +
                "\"contacts\": []" +
                "}," +
                "{"+
                "\"ext_id\": \"Q6UTWx4Ku6QGrofLvWUP\","+
                "\"type_id\": \"PSey8gx88UqYx36Rt6RNLQ\"," +
                "\"obj_type\": \"Opportunity\"," +
                "\"obj_id\": \"006j0000002CWGa\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                "\"action_id\": \"Zsk78y88UqYx36Rt6RNLS\"," +
                "\"key\": \"dummy\"," +
                "\"value\": \"dummy\"" +
                "}]," +
                "\"contacts\": []" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecommendation, token, newRecommendation);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlRecommendation"})
    public void newRecWithContact(String enkataUrlRecommendation) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String newRecommendation =  "[{"+
                "\"ext_id\": \"Q7USWx4Ku6QGrofLvWUP\","+
                "\"type_id\": \"PSey8gx88UqYx36Rt6RNLQ\"," +
                "\"obj_type\": \"Opportunity\"," +
                "\"obj_id\": \"006j0000002CWGe\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                     "\"action_id\": \"Zsk78y88UqYx36Rt6RNLS\"," +
                     "\"key\": \"dummy\"," +
                     "\"value\": \"dummy\"" +
                "}]," +
                "\"contacts\": [" +
                "{" +
                    "\"action_id\": \"Zsk78y88UqYx36Rt6RNLS\"," +
                    "\"contact\": \"003j0000004vGkT\"" +
                "}]" +
                "}," +
                "{"+
                "\"ext_id\": \"Q6UFWx4Ku6QGrofLvWUP\","+
                "\"type_id\": \"PSey8gx88UqYx36Rt6RNLQ\"," +
                "\"obj_type\": \"Opportunity\"," +
                "\"obj_id\": \"006j0000002CWGa\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                    "\"action_id\": \"Zsk78y88UqYx36Rt6RNLS\"," +
                    "\"key\": \"dummy\"," +
                    "\"value\": \"dummy\"" +
                "},{" +
                    "\"action_id\": \"Zsk78y88UqYx36Rt6RNLQ\"," +
                    "\"key\": \"dummy\"," +
                    "\"value\": \"dummy\"" +
                "}]," +
                "\"contacts\": [" +
                "{"+
                    "\"action_id\": \"Zsk78y88UqYx36Rt6RNLS\"," +
                    "\"contact\": \"003j0000007jd9iAAA\"" +
                "},{"+
                    "\"action_id\": \"Zsk78y88UqYx36Rt6RNLQ\"," +
                    "\"contact\": \"003j0000004vGkUAAU\"" +
                "}]" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecommendation, token, newRecommendation);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlRecType"})
    public void newLeadRecType(String enkataUrlRecType) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        String newLeadRecType = "[{\n" +
                "\"ext_id\": \"lead_eval_lead_for_close\"," +
                "\"name\": \"Evaluate Lead for Closing\"," +
                "\"explanation\": \"This lead has not been touched for a long time. Recommended: Closing this lead as disqualified.\"," +
                "\"summary\":\"Status: <b>{!LeadStatus}</b></br>Source: <b>LeadSourceValue</b><br>Create Date: <b>CreateDate</b>\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecType, token, newLeadRecType);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlRecType"})
    public void updateLeadRecType(String enkataUrlRecType) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        String newLeadRecType = "[{\n" +
                "\"ext_id\": \"lead_eval_lead_for_close\"," +
                "\"name\": \"Evaluate Lead for Closing Upd\"," +
                "\"explanation\": \"Upd: This lead has not been touched for a long time. Recommended: Closing this lead as disqualified.\"," +
                "\"summary\":\"Status: <b>{!LeadStatus}</b></br>Source: <b>LeadSourceValue</b><br>Create Date: <b>CreateDate</b>\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecType, token, newLeadRecType);

        ResourceManagement.checkHTTPResponseOK(response);

    }

    @Test
    @Parameters({"enkataUrlRecAction"})
    public void newLeadRecAction(String enkataUrlRecAction) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        String newLeadRecAction = "[{" +
                "\"ext_id\": \"action_close_lead\"," +
                "\"name\": \"Close Lead\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecAction, token, newLeadRecAction);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlRecAction"})
    public void updateLeadRecAction(String enkataUrlRecAction) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        String newLeadRecAction = "[{" +
                "\"ext_id\": \"action_close_lead\"," +
                "\"name\": \"Close Lead Upd\"" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecAction, token, newLeadRecAction);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlRecommendation"})
    public void newLeadRecommendation(String enkataUrlRecommendation) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String newLeadRecommendation =  "[{"+
                "\"ext_id\": \"leadrec1\","+
                "\"type_id\": \"lead_eval_lead_for_close\"," +
                "\"obj_type\": \"Lead\"," +
                "\"obj_id\": \"00Qj0000002kLv2EAE\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                "\"action_id\": \"action_close_lead\"," +
                "\"key\": \"dummy\"," +
                "\"value\": \"dummy\"" +
                "}]," +
                "\"contacts\": []" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecommendation, token, newLeadRecommendation);

        ResourceManagement.checkHTTPResponseOK(response);
    }

    @Test
    @Parameters({"enkataUrlRecommendation"})
    public void newLeadRecommendationWithContact(String enkataUrlRecommendation) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        // Create json
        String newLeadRecommendation =  "[{"+
                "\"ext_id\": \"leadrec2\","+
                "\"type_id\": \"lead_eval_lead_for_close\"," +
                "\"obj_type\": \"Lead\"," +
                "\"obj_id\": \"00Qj0000002kLv2EAE\"," +
                "\"priority\": \"1\"," +
                "\"status\": \"New\"," +
                "\"context\": [{" +
                "\"action_id\": \"action_make_call\"," +
                "\"key\": \"dummy\"," +
                "\"value\": \"dummy\"" +
                "},{" +
                "\"action_id\": \"action_close_lead\"," +
                "\"key\": \"dummy\"," +
                "\"value\": \"dummy\"" +
                "}]," +
                "\"contacts\": []" +
                "}]";

        String authToken = ResourceManagement.getAuthToken();

        System.out.println("Auth Token: " + authToken);

        String token = "Bearer " + authToken;

        HttpResponse response = ResourceManagement.sendJSon(enkataUrlRecommendation, token, newLeadRecommendation);

        ResourceManagement.checkHTTPResponseOK(response);
    }




}

