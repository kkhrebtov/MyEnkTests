package automationtests.end2end.enkata.com;

import org.openqa.selenium.WebDriver;
import org.testng.annotations.AfterTest;
import org.testng.annotations.Parameters;
import org.testng.annotations.Test;
import selenium.enkata.com.RepositorySalesForce;
import selenium.enkata.com.SalesForceModule;
import selenium.enkata.com.SeleniumFireFoxDriver;

public class Contact {

	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test01_CreateContact(String url, String login, String password,
			String connectionString, String userDb, String passwordDb)
			throws InterruptedException {
		String name = "account_";
		String firstName = "Sinor";
		String lastName = "Tester";
		String email = "contact_";
		String phone = "33333-";
		String description = "Get deeper customer insights with Social Accounts and Contacts. Click these icons to get started. Tell me more!";
		String title = "title for";

		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);

		for (int i = 1; i < 11; i++) {
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getContactsTab());
			Thread.sleep(2000);
			SalesForceModule.selectViewAccounts("All Contacts", driver);
			Thread.sleep(2000);
			SalesForceModule.createContact(email + i + "@enkata.com", firstName + i, lastName + i, name + i,
					phone + i, title + i, description + i, driver);
			Thread.sleep(5000);
		}
	}
	
	@AfterTest
	public void closeAllSession() throws Exception {
		SalesForceModule.closeFireFox();
	}

}
