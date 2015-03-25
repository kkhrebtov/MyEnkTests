package selenium.enkata.com;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

import java.util.List;

/**
 * Created by kkhrebtov on 3/12/2015.
 */
public class SalesNGMenu {

    private final WebDriver driver;

    private String tasksMenuItem;
    private String pipelineMenuItem;
    private String leadsMenuItem;
    private String accountsMenuItem;
    private String opportunitiesMenuItem;
    private String contactsMenuItem;
    private String loggingMenuItem;
    private String activitiesMenuItem;
    private String optionsMenuItem;

    public SalesNGMenu(WebDriver driver) {

        int counter = 1;

        this.driver = driver;
        WebElement menuList = driver.findElement(By.xpath("//*[@id=\"sidebar-wrapper\"]/ul"));

        List<WebElement> menuLinks = menuList.findElements(By.tagName("li"));

        for(WebElement li: menuLinks) {
            switch(li.findElement(By.xpath(".//a/span[2]")).getText())  {
                case ("Tasks"):
                    tasksMenuItem = "//*[@id='sidebar-wrapper']/ul/li["+counter+"]/a/span[2]";
                    break;
                case ("Pipeline"):
                    pipelineMenuItem = "//*[@id='sidebar-wrapper']/ul/li["+counter+"]/a/span[2]";
                    break;
                case ("Leads"):
                    leadsMenuItem = "//*[@id='sidebar-wrapper']/ul/li["+counter+"]/a/span[2]";
                    break;
                case ("Opportunities"):
                    opportunitiesMenuItem = "//*[@id='sidebar-wrapper']/ul/li["+counter+"]/a/span[2]";
                    break;
                case ("Accounts"):
                    accountsMenuItem = "//*[@id='sidebar-wrapper']/ul/li["+counter+"]/a/span[2]";
                    break;
                case ("Discovered Contacts"):
                    contactsMenuItem = "//*[@id='sidebar-wrapper']/ul/li["+counter+"]/a/span[2]";
                    break;
                case ("Logging"):
                    loggingMenuItem = "//*[@id='sidebar-wrapper']/ul/li["+counter+"]/a/span[2]";
                    break;
                case ("Activities"):
                    activitiesMenuItem = "//*[@id='sidebar-wrapper']/ul/li["+counter+"]/a/span[2]";
                    break;
                case ("Options"):
                    optionsMenuItem = "//*[@id='sidebar-wrapper']/ul/li["+counter+"]/a/span[2]";
                    break;
                default:
                   break;

            }

            counter++;
         }
    }

    public SalesNGMenu collapseMenu() throws InterruptedException {

        WebElement menuToggle = driver.findElement(By.xpath("//*[@id='menu-toggle']"));

        while (!menuToggle.isEnabled()) {
            Thread.sleep(500);
            System.out.println("Waiting for menuToggle to be displayed");
        }

        menuToggle.click();
        return this;
    }

    public SalesNGContacts selectMenuItem_Contacts() {
       clickMenuItem("Discovered Contacts");
       return new SalesNGContacts(driver);
   }

    public SalesNGActivities selectMenuItem_Activities() {
        clickMenuItem("Activities");
        return new SalesNGActivities(driver);
    }

    public SalesNGPipeline selectMenuItem_Pipeline() {
        clickMenuItem("Pipeline");
        return new SalesNGPipeline(driver);
    }

    public SalesNGPipeline selectMenuItem_Leads() {
        clickMenuItem("Leads");
        return new SalesNGPipeline(driver);
    }

    public SalesNGPipeline selectMenuItem_Accounts() {
        clickMenuItem("Accounts");
        return new SalesNGPipeline(driver);
    }

    public SalesNGPipeline selectMenuItem_Opportunities() {
        clickMenuItem("Opportunities");
        return new SalesNGPipeline(driver);
    }

    public SalesNGPipeline selectMenuItem_Logging() {
        clickMenuItem("Logging");
        return new SalesNGPipeline(driver);
    }

    public SalesNGPipeline selectMenuItem_Options() {
        clickMenuItem("Options");
        return new SalesNGPipeline(driver);
    }

    public WebElement getTasksMenuItem() {
        return driver.findElement(By.xpath(tasksMenuItem));
    };

    public WebElement getPipelineMenuItem() {
        return driver.findElement(By.xpath(pipelineMenuItem));
    }

    public WebElement getLeadsMenuItem() {
        return driver.findElement(By.xpath(leadsMenuItem));
    }

    public WebElement getAccountsMenuItem() {
        return driver.findElement(By.xpath(accountsMenuItem));
    }

    public WebElement getOpportunitiesMenuItem() {
        return driver.findElement(By.xpath(opportunitiesMenuItem));
    }

    public WebElement getContactsMenuItem() {
        return driver.findElement(By.xpath(contactsMenuItem));
    }

    public WebElement getLoggingMenuItem() {
        return driver.findElement(By.xpath(loggingMenuItem));
    }

    public WebElement getActivitiesMenuItem() {
        return driver.findElement(By.xpath(activitiesMenuItem));
    }

    public WebElement getOptionsMenuItem() {
        return driver.findElement(By.xpath(optionsMenuItem));
    }

    public WebElement getNull() {
        return driver.findElement(By.xpath(optionsMenuItem+"/a/a"));
    }

    private void clickMenuItem(String menuItem) {

        //WebElement menu = driver.findElement(By.xpath("//*[@id=\"sidebar-wrapper\"]/ul"));
        ////List<WebElement> menuLinks = menu.findElements(By.tagName("li"));
        //List<WebElement> menuLinks = menuList.findElements(By.tagName("li"));
        //
        //for(WebElement li: menuLinks) {
        //
        //    String lnkName = li.findElement(By.xpath(".//a/span[2]")).getText();
        //
        //    if (lnkName.equals(menuItem)) {
        //        li.click();
        //    }

        switch(menuItem)  {
            case ("Tasks"):
                driver.findElement(By.xpath(tasksMenuItem)).click();
                break;
            case ("Pipeline"):
                driver.findElement(By.xpath(pipelineMenuItem)).click();
                break;
            case ("Leads"):
                driver.findElement(By.xpath(leadsMenuItem)).click();
                break;
            case ("Opportunities"):
                driver.findElement(By.xpath(opportunitiesMenuItem)).click();
                break;
            case ("Accounts"):
                driver.findElement(By.xpath(accountsMenuItem)).click();
                break;
            case ("Discovered Contacts"):
                driver.findElement(By.xpath(contactsMenuItem)).click();
                break;
            case ("Logging"):
                driver.findElement(By.xpath(loggingMenuItem)).click();
                break;
            case ("Activities"):
                driver.findElement(By.xpath(activitiesMenuItem)).click();
                break;
            case ("Options"):
                driver.findElement(By.xpath(optionsMenuItem)).click();
                break;
            default:
                break;

        }


    }

}
