package automationtests.salesng.com;

import junit.framework.Assert;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.ITestContext;
import org.testng.annotations.*;
import selenium.enkata.com.*;

import java.util.List;
import java.util.NoSuchElementException;

/**
 * Created by kkhrebtov on 3/12/2015.
 */
public class SalesNGTests {

    WebDriver driver = null;


    @BeforeClass
    @Parameters({ "url", "login", "password"})
    public void beforeClass(ITestContext context, String url, String login, String password)
            throws InterruptedException {

        driver = SalesForceModule.logInToCigna_withChrome(url, login, password);

        WebDriverWait wait = new WebDriverWait(driver, 10);
        WebElement homeTab = wait.until(ExpectedConditions.elementToBeClickable(By.id("home_Tab")));

        WebElement tabBar = driver.findElement(By.id("tabBar"));

        List<WebElement> tabs = tabBar.findElements(By.tagName("li"));

        for(WebElement tab: tabs) {

            if(tab.findElement(By.tagName("a")).getText().equals("newUI")) {
                System.out.println("Found Tab:" + tab.findElement(By.tagName("a")).getText());
                tab.findElement(By.tagName("a")).click();
                break;
            }
        }

        try {
            WebElement sideBar = SalesNGHelper.waitElementPresence_byId(driver, "sidebar-wrapper", 15);
        } catch(InterruptedException e) {
            e.printStackTrace();
        }

        System.out.println("------------- End of @BeforeTest -----------");
    }

    @AfterClass
    public void afterClass(ITestContext context) {

        driver.quit();
    }

    @Test
    public void Test_01_SalesNG_MenuNavigation()  {

        SalesNGMenu menu = new SalesNGMenu(driver);

        SalesNGContacts contactsPage = menu.selectMenuItem_Contacts();
        SalesNGHelper.waitElement_byXPath(driver, ".//*[@id='j_id0:j_id3:contacts']", 15);

        org.testng.Assert.assertEquals(menu.getTasksMenuItem().getText(), "Tasks");
        org.testng.Assert.assertEquals(menu.getPipelineMenuItem().getText(), "Pipeline");
        org.testng.Assert.assertEquals(menu.getLeadsMenuItem().getText(), "Leads");
        org.testng.Assert.assertEquals(menu.getAccountsMenuItem().getText(), "Accounts");
        org.testng.Assert.assertEquals(menu.getOpportunitiesMenuItem().getText(), "Opportunities");
        org.testng.Assert.assertEquals(menu.getContactsMenuItem().getText(), "Discovered Contacts");
        org.testng.Assert.assertEquals(menu.getLoggingMenuItem().getText(), "Logging");
        org.testng.Assert.assertEquals(menu.getActivitiesMenuItem().getText(), "Activities");
        org.testng.Assert.assertEquals(menu.getOptionsMenuItem().getText(), "Options");

        SalesNGActivities activitiesPage = menu.selectMenuItem_Activities();
        SalesNGHelper.waitElement_byXPath(driver, ".//*[@id='j_id0:j_id3:activities']", 15);

        org.testng.Assert.assertEquals(menu.getTasksMenuItem().getText(), "Tasks");
        org.testng.Assert.assertEquals(menu.getPipelineMenuItem().getText(), "Pipeline");
        org.testng.Assert.assertEquals(menu.getLeadsMenuItem().getText(), "Leads");
        org.testng.Assert.assertEquals(menu.getAccountsMenuItem().getText(), "Accounts");
        org.testng.Assert.assertEquals(menu.getOpportunitiesMenuItem().getText(), "Opportunities");
        org.testng.Assert.assertEquals(menu.getContactsMenuItem().getText(), "Discovered Contacts");
        org.testng.Assert.assertEquals(menu.getLoggingMenuItem().getText(), "Logging");
        org.testng.Assert.assertEquals(menu.getActivitiesMenuItem().getText(), "Activities");
        org.testng.Assert.assertEquals(menu.getOptionsMenuItem().getText(), "Options");


        SalesNGPipeline pipelinePage = menu.selectMenuItem_Pipeline();
        SalesNGHelper.waitElement_byXPath(driver, ".//*[@id='canvas-frame-_:Enkata:j_id0:j_id1:canvasapp']", 15);

        org.testng.Assert.assertEquals(menu.getTasksMenuItem().getText(), "Tasks");
        org.testng.Assert.assertEquals(menu.getPipelineMenuItem().getText(), "Pipeline");
        org.testng.Assert.assertEquals(menu.getLeadsMenuItem().getText(), "Leads");
        org.testng.Assert.assertEquals(menu.getAccountsMenuItem().getText(), "Accounts");
        org.testng.Assert.assertEquals(menu.getOpportunitiesMenuItem().getText(), "Opportunities");
        org.testng.Assert.assertEquals(menu.getContactsMenuItem().getText(), "Discovered Contacts");
        org.testng.Assert.assertEquals(menu.getLoggingMenuItem().getText(), "Logging");
        org.testng.Assert.assertEquals(menu.getActivitiesMenuItem().getText(), "Activities");
        org.testng.Assert.assertEquals(menu.getOptionsMenuItem().getText(), "Options");


    }


    @Test
    public void Test_02_SalesNG_CollapseMenu() throws InterruptedException {

        boolean collapsed = false;
        SalesNGMenu menu = new SalesNGMenu(driver);

        SalesNGHelper.waitElement_byId(driver, "menu-toggle", 15);
        if (!driver.findElement(By.id("wrapper")).getClass().toString().equals("tab-container")) collapsed = true;

        menu.collapseMenu();
        Thread.sleep(1000);

        if (collapsed) {
            System.out.println("Wrapper class: " + driver.findElement(By.id("wrapper")).getAttribute("Class").toString());
            Assert.assertEquals("tab-container toggled", driver.findElement(By.id("wrapper")).getAttribute("Class").toString());
            collapsed = false;
        } else {
            System.out.println("Wrapper class: " + driver.findElement(By.id("wrapper")).getAttribute("Class").toString());
            Assert.assertEquals(driver.findElement(By.id("wrapper")).getAttribute("Class").toString(), "tab-container");
            collapsed = true;
        }

        menu.collapseMenu();
        Thread.sleep(1000);

        if (collapsed) {
          Assert.assertEquals(driver.findElement(By.id("wrapper")).getAttribute("Class").toString(), "tab-container toggled");
        } else {
            Assert.assertEquals(driver.findElement(By.id("wrapper")).getAttribute("Class").toString(), "tab-container");
        }

    }


    @Test
    public void Test_03_SalesNG_CollapsedMenuNavigation() throws InterruptedException {

        boolean collapsed = false;
        SalesNGMenu menu = new SalesNGMenu(driver);

        if (!driver.findElement(By.id("wrapper")).getClass().toString().equals("tab-container")) collapsed = true;

        menu.collapseMenu();
        Thread.sleep(1000);

        SalesNGContacts contactsPage = menu.selectMenuItem_Contacts();
        SalesNGHelper.waitElement_byXPath(driver, ".//*[@id='j_id0:j_id3:contacts']", 15);
        Thread.sleep(1000);

        SalesNGActivities activitiesPage = menu.selectMenuItem_Activities();
        SalesNGHelper.waitElement_byXPath(driver, ".//*[@id='j_id0:j_id3:activities']", 15);
        Thread.sleep(1000);

        SalesNGPipeline pipelinePage = menu.selectMenuItem_Pipeline();
        SalesNGHelper.waitElement_byXPath(driver, ".//*[@id='canvas-frame-_:Enkata:j_id0:j_id1:canvasapp']", 15);
        Thread.sleep(1000);

    }

}
