package selenium.enkata.com;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

/**
 * Created by kkhrebtov on 3/16/2015.
 */
public class SalesNGActivities {

    private final WebDriver driver;

    private WebElement activitiesTable;
    private WebElement activityDetails;

    public SalesNGActivities(WebDriver driver) {
        this.driver = driver;
        //activitiesTable = driver.findElement(By.xpath("//*[@id='j_id0:j_id3:activities']"));
    }

    public SalesNGActivities selectActivity(int activityNumber) {
        WebElement activity = activitiesTable.findElement(By.xpath(".//tr["+activityNumber+"]/td/a"));
        activity.click();
        return this;
    }




}
