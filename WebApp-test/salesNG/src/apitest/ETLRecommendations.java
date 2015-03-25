package apitest;

import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.json.JSONException;
import org.testng.Assert;
import org.testng.annotations.Parameters;
import org.testng.annotations.Test;

import java.io.IOException;

/**
 * Created by kkhrebtov on 1/26/2015.
 */
public class ETLRecommendations {

    @Test
    @Parameters({"etlRecActionsApi"})
    public void getActions(String etlRecActionsApi) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

       String response = ResourceManagement.getRequestBasic(etlRecActionsApi);

       System.out.println("Response:   " + response);

       String expectedResponse = "[{\"ext_id\":\"rcmNrawtQ4mchkaABtLVyQ\",\"name\":\"Make Call\"},{\"ext_id\":\"E06oDX9rTO-piAowVcqGSA\",\"name\":\"Set Up Meeting\"},{\"ext_id\":\"feFqTJdCRT-dxbxyn14G3A\",\"name\":\"Change Close Date\"},{\"ext_id\":\"wKO9edzsTm6SqJQYmHGGqg\",\"name\":\"Send Email\"}]";

        Assert.assertEquals(response, expectedResponse);

    }

    @Test
    @Parameters({"etlRecTypesApi"})
    public void getTypes(String etlRecTypesApi) throws InterruptedException,
            ClientProtocolException, IOException, JSONException {

        String response = ResourceManagement.getRequestBasic(etlRecTypesApi);

        System.out.println("Response:   " + response);

        String expectedResponse = "[{\"ext_id\":\"rcmNrawtQ4mchkaABtLVyQ\",\"name\":\"Make Call\"},{\"ext_id\":\"E06oDX9rTO-piAowVcqGSA\",\"name\":\"Set Up Meeting\"},{\"ext_id\":\"feFqTJdCRT-dxbxyn14G3A\",\"name\":\"Change Close Date\"},{\"ext_id\":\"wKO9edzsTm6SqJQYmHGGqg\",\"name\":\"Send Email\"}]";

        Assert.assertEquals(response, expectedResponse);

    }


}
