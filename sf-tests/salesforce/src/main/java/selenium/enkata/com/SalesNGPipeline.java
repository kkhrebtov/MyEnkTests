package selenium.enkata.com;

import junit.framework.Assert;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

/**
 * Created by kkhrebtov on 3/16/2015.
 */
public class SalesNGPipeline {

    private final WebDriver driver;
    private WebElement stageFilter = null;

    public SalesNGPipeline(WebDriver driver) {
        this.driver = driver;
    }

    private WebElement getCanvas(WebDriver driver) {

        WebElement canvas = driver.findElement(By.xpath("//*[@id='tabViewer']/div[5]"));

        return canvas;
    }

    public void verifyFilter_Stage(WebDriver driver) {

        driver.switchTo().frame("canvas-frame-_:Enkata:j_id0:j_id1:canvasapp");
        driver.switchTo().frame("DisplayFrame");
        WebElement filter = driver.findElement(By.xpath("//*[@id='tableau_base_widget_CategoricalFilter_0']/div/div[3]/span/div"));

        Assert.assertNotNull(filter);

        driver.switchTo().defaultContent();

    }

}
