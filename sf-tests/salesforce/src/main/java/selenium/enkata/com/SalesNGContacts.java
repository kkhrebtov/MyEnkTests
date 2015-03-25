package selenium.enkata.com;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

import java.util.List;

/**
 * Created by kkhrebtov on 3/16/2015.
 */
public class SalesNGContacts {

    private final WebDriver driver;

    private String timeFilter;
    private String ownerFilter;

    private String panelViewButtonId;
    private String listViewButtonId;


    // --------- Icons View ---------- //
    private String topLeftContactId;
    private String topMidContactId;
    private String topRightContactId;
    private String midLeftContactId;
    private String midMidContactId;
    private String midRightContactId;
    private String bottomLeftContactId;
    private String bottomMidContactId;
    private String bottomRightContactId;

    // --------- List View --------- //



    public SalesNGContacts(WebDriver driver) {
        this.driver = driver;
        panelViewButtonId = "";
        listViewButtonId = "";
    }

    public SalesNGContacts switchToListView() {

        WebElement listButton = driver.findElement(By.id(listViewButtonId));

        listButton.click();
        return this;
    }

    public WebElement findContactByName(String contactName) {

        String requiredContact;
        WebElement contact = null;
        List<WebElement> allContacts = driver.findElements(By.xpath(""));

        for (WebElement cont : allContacts) {
            requiredContact = cont.findElement(By.name("")).getText();
            if (requiredContact.equals(contactName)) {
                contact = cont;
                break;
            }

        }

        return contact;

    }

    public void editContact(String contactName) {
        WebElement contact = findContactByName(contactName);
        WebElement contextMenuButton = contact.findElement(By.name(""));
        contextMenuButton.click();





    }






 }



