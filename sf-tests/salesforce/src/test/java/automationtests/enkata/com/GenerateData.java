package automationtests.enkata.com;

import org.openqa.selenium.WebDriver;
import org.testng.annotations.Parameters;
import org.testng.annotations.Test;

import selenium.enkata.com.RepositorySalesForce;
import selenium.enkata.com.SalesForceModule;
import selenium.enkata.com.SeleniumFireFoxDriver;

public class GenerateData {
	
	private String fax = "888888888";
	private String phone = "+777777777";
	private String webSite = "http://mordorcity1.com";
	private int employees = 155;
	private int annualRevenue = 125;
	private String name = "nameNew";
	private String 	description = "descriptionNew";
	
	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
	"passwordDb" })
	public void Create100Account(String url, String login, String password,
			String connectionString, String userDb, String passwordDb) throws InterruptedException{		
		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		
		for (int i = 0; i < 200; i++) {
			Thread.sleep(2000);
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getContactsTab());
			Thread.sleep(2000);
			SalesForceModule.selectViewAccounts("All Contacts", driver);
			Thread.sleep(2000);
			SalesForceModule.createContact("email"+i+"@mail.ru", "firstName" + i,
					"lastName" + i, "name" + i, "888888888", "title" + i, description + i,
					driver);
			Thread.sleep(5000);
		}
		
		
	}

}
