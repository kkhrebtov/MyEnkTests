package selenium.enkata.com;

import com.sun.org.apache.xpath.internal.operations.*;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.ExpectedCondition;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

import java.lang.String;
import java.util.concurrent.TimeoutException;

/**
 * Created by kkhrebtov on 3/13/2015.
 */
public class SalesNGHelper {


    public static WebElement waitElementPresence_byId(WebDriver driver, String elementId, int duration) throws InterruptedException {

        final String elId = elementId;

        System.out.println("waitElementPresence_byId: " + elementId);

        WebElement foundElement = (new WebDriverWait(driver, duration))
                .until(new ExpectedCondition<WebElement>() {
                    @Override
                    public WebElement apply(WebDriver d) {
                        return d.findElement(By.id(elId));
                    }
                });

        Thread.sleep(1500);

        while (!foundElement.isDisplayed()) {
            System.out.println("Found Element is displayed: " + foundElement.isDisplayed());
            Thread.sleep(1000);
        }

        System.out.println("Element with " + elementId + " displayed");

        return foundElement;

    }

    public static void waitElement_byXPath(WebDriver driver, String elementId, int duration) {

        System.out.println("waitElement_byXPath: " + elementId);
        WebDriverWait wait = new WebDriverWait(driver, duration);
        wait.until(ExpectedConditions.presenceOfElementLocated(By.xpath(elementId)));

    }

    public static void waitElement_byId(WebDriver driver, String elementId, int duration) {

        System.out.println("waitElementPresence_byId: " + elementId);
        WebDriverWait wait = new WebDriverWait(driver, duration);
        wait.until(ExpectedConditions.presenceOfElementLocated(By.id(elementId)));

    }

}
