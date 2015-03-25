package automationtests.end2end.enkata.com;


import org.openqa.selenium.WebDriver;
import org.testng.annotations.AfterTest;
import org.testng.annotations.Parameters;
import org.testng.annotations.Test;
import selenium.enkata.com.RepositorySalesForce;
import selenium.enkata.com.SalesForceModule;
import selenium.enkata.com.SeleniumFireFoxDriver;

public class Account {

	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test01_CreateAccount(String url, String login, String password,
			String connectionString, String userDb, String passwordDb)
			throws InterruptedException {
		String fax = "12312312";
		String phone = "44444-";
		String webSite = "http://mordorcity.com";
		int employees = 155;
		int annualRevenue = 125;
		String description = "Get deeper customer insights with Social Accounts and Contacts. Click these icons to get started. Tell me more!";

		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);

		for (int i = 1; i < 11; i++) {
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getAccountsTab());
			Thread.sleep(2000);
			SalesForceModule.selectViewAccounts("All Accounts", driver);
			Thread.sleep(2000);
			SalesForceModule.createAccount("account_" + i, fax + i, phone + i,
					webSite, employees, annualRevenue, description, driver);
			Thread.sleep(10000);
		}
	}
	
	
	@AfterTest
	public void closeAllSession() throws Exception {
		SalesForceModule.closeFireFox();
	}

}
