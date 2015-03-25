package apitest;

import org.apache.commons.httpclient.HttpClient;
import org.apache.commons.httpclient.methods.PostMethod;
import org.apache.commons.httpclient.params.HttpClientParams;
import org.apache.commons.io.IOUtils;
import org.apache.http.*;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.HttpClientBuilder;
import org.apache.http.message.BasicHeader;
import org.apache.http.params.HttpParams;
import org.apache.http.protocol.HTTP;
import org.apache.http.util.EntityUtils;
import org.json.JSONException;
import org.json.JSONObject;
import org.testng.Assert;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.SocketException;
import java.net.URL;
import java.util.Iterator;
import java.util.Locale;

/**
 * Created by kkhrebtov on 1/12/2015.
 */


public class ResourceManagement {

    static String sfUrl = "https://login.salesforce.com/services/oauth2/token";


    //JiveDemo Environment
    //static String clientId = "3MVG9fMtCkV6eLhdrhrfPbwi39v8HpM4a9SnI1auOSEQA5QTfjo11yuSXr8pRjnqr_8nehU2cJqRotuo_nlL5";  // For JiveDemo
    //static String clientSecret = "6901417068411306149";      // For JiveDemo
    //static String username = "tdidier@jivedemo.enkata.com"; // For JiveDemo
    //static String password = "NotASecret108XeIwUIqM7tGLs7a6gAPPoey";    // For JiveDemo


    //AShutov Environment
    //static String clientId = "3MVG9fMtCkV6eLhdrhrfPbwi39v8HpM4a9SnI1auOSEQA5QTfjo11yuSXr8pRjnqr_8nehU2cJqRotuo_nlL5";  // For AShutov
    //static String clientSecret = "6901417068411306149";      // For AShutov
    //static String username = "ashutov@pdo.enkata.com";      // For AShutov
    //static String password = "Thesum_415bNbH4bOZ5zisIILe4ahSDH7z";    // For AShutov

    //SalesDemo Environment
    //static String clientId = "3MVG9fMtCkV6eLhdrhrfPbwi39hC8zOGKaDdtNhcZuwzufkHSRReebc73y8v1maSS91Oq.ahfnngd1YnPXAaC";  // For SalesDemo
    //static String clientSecret = "5992168519852312061";      // For SalesDemo
    //static String username = "azhuk@demo.enkata.com"; // For Salesdemo
    //static String password = "NotASecret10FWDff2J6ZcJl4o14cXg9nADeR";    // For Salesdemo


    // Test Environment
    static String clientId = "3MVG9fMtCkV6eLhc_NgPC456XT5BWm40m2LB1U_TOmh6G0pTU_Q8xhbJYOVHw4yzs8m5udbeWadB2XG3r_RAT";
    static String clientSecret = "797201356838430098";
    static String username = "kkhrebtov@demong.enkata.com"; // For Testing
    static String password = "Enkata#10Zh1KRvvhZeOdskb8PcZndshW";  // For Testing

    //static String username = "azhuk@demong.enkata.com"; // For Testing AZhuk
    //static String password = " NotASecret10J1ttcLp72Baa99werGqgR6wD";  // For Testing Azhuk

    //static String username = "ainzhebeikin@enkata.com"; // For Testing AInzhebeikin
    //static String password = "Password10kXiO9ZJpbgaHEeM8SZQlRN6OA";  // For Testing AInzhebeikin


    /*
    // Load file containing list of Json (actions, types, recommendations etc)
    */
    public static String loadResource(String resourceName) {
        try {
            InputStream stream = SFAPIRecommendations.class.getResourceAsStream(resourceName);
            if (stream == null) {
                throw new RuntimeException("Resource not found");
            }
            return IOUtils.toString(stream);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }


    /*
    // Receive authentication token from Salesforce
    */
    public static String getAuthToken() throws IOException {

        String accessToken = "";
        InputStream responseBody = new InputStream() {
            @Override
            public int read() throws IOException {
                return 0;
            }
        };

        HttpClient httpclient = new HttpClient();
        HttpClientParams params = new HttpClientParams();
        params.setConnectionManagerTimeout(100000);

        PostMethod post = new PostMethod(sfUrl);
        post.addParameter("grant_type", "password");

        post.addParameter("client_id", clientId);
        post.addParameter("client_secret", clientSecret);
        post.addParameter("username", username);
        post.addParameter("password", password);

        //System.out.println("Auth Request:" + post.toString());

        httpclient.executeMethod(post);

        responseBody = post.getResponseBodyAsStream();

        BufferedReader in = new BufferedReader(new InputStreamReader(responseBody));

        String inputLine;

        StringBuffer response = new StringBuffer();

        while ((inputLine = in.readLine()) != null) {

            response.append(inputLine);

        }

        in.close();
        String list = response.toString();

        System.out.println(list);


        JSONObject json = null;
        try {
            json = new JSONObject(list);
            accessToken = json.getString("access_token");

        } catch (JSONException e) {
            e.printStackTrace();
        }

        return accessToken;
    }

    /*
    // Send json to the corresponding URL using authentication token returned by getAuthToken()
    */
    public static HttpResponse sendJSon(String url, String token, String json)
            throws ClientProtocolException, IOException, JSONException {

        System.out.println("Test JSON: " + json);

        HttpPost request = new HttpPost(url); // method post

        StringEntity entity = new StringEntity(json);
        entity.setContentType("application/json;charset=UTF-8");
        entity.setContentEncoding(new BasicHeader(HTTP.CONTENT_TYPE,
                "application/json;charset=UTF-8"));
        request.addHeader("Authorization", token); // if we need authorization
        request.setEntity(entity);
        HttpResponse response = null;
        org.apache.http.client.HttpClient httpclient = HttpClientBuilder.create().build();

        try {
            response = httpclient.execute(request);
            System.out.println(response);
            return response;
        } catch (SocketException se) {
            throw se;
        }
    }


    public static String getRequestBasic(String url) throws IOException {

        URL obj = new URL(url);

        HttpURLConnection con = (HttpURLConnection) obj.openConnection();

        con.setRequestMethod("GET");
        con.addRequestProperty("Authorization", "Basic key");

        int responseCode = con.getResponseCode();

        System.out.println("\nSending 'GET' request to URL : " + url);
        System.out.println("Response Code : " + responseCode);

        BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));

        String inputLine;
        StringBuffer response = new StringBuffer();
        while ((inputLine = in.readLine()) != null) {
            response.append(inputLine);
        }

        in.close();

        String list = response.toString();
        System.out.println(list);

        return list;

    }

    public static String getRequestToken(String url, String token) throws IOException {

        URL obj = new URL(url);

        HttpURLConnection con = (HttpURLConnection) obj.openConnection();

        con.setRequestMethod("GET");
        con.addRequestProperty("Authorization", "Basic key");

        int responseCode = con.getResponseCode();

        System.out.println("\nSending 'GET' request to URL : " + url);
        System.out.println("Response Code : " + responseCode);

        BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));

        String inputLine;
        StringBuffer response = new StringBuffer();
        while ((inputLine = in.readLine()) != null) {
            response.append(inputLine);
        }

        in.close();

        String list = response.toString();
        System.out.println(list);

        return list;

    }

    /*
    // Validate result of sending Json. If response is other than "HTTP/1.1 200 OK" then we consider that
    // API returned error. Printout error message on the console.
    */

    public static void checkHTTPResponseOK(HttpResponse responseBody) throws IOException {

        String statusLine = responseBody.getStatusLine().toString();


        System.out.println("Response body:" + EntityUtils.toString(responseBody.getEntity()));

        System.out.println("Current status of response: " + statusLine);

        Assert.assertEquals(statusLine, "HTTP/1.1 200 OK");

    }

    public static void checkHTTPResponse(HttpResponse responseBody, String expStatusLine) throws IOException {


        String statusLine = responseBody.getStatusLine().toString();

        //System.out.println("Response body:" + EntityUtils.toString(responseBody.getEntity()));
        System.out.println("Current status of response: " + statusLine);

        Assert.assertEquals(statusLine, expStatusLine);

    }

    public static void checkResponseBody(HttpResponse responseBody, String inpKey, String expValue) throws IOException, JSONException {

        String jsonString = EntityUtils.toString(responseBody.getEntity());

        jsonString = jsonString.replace("[", "").replace("]", "").replace("\\", "");

        int l = jsonString.length();

        jsonString = jsonString.substring(1, l - 1);

        System.out.println("Response body:" + jsonString) ;

        JSONObject json = new JSONObject(jsonString );

        Iterator<?> keys = json.keys();

        while( keys.hasNext() ) {
            String key = (String) keys.next();
            if (key.equals(inpKey)) {
                System.out.println("Current value of " + key + " is: " +  json.getString(key));
                Assert.assertEquals(json.getString(key), expValue);
            }
        }
    }

}
