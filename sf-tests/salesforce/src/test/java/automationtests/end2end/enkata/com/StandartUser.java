package automationtests.end2end.enkata.com;

import org.openqa.selenium.WebDriver;
import org.testng.annotations.AfterTest;
import org.testng.annotations.Parameters;
import org.testng.annotations.Test;

import selenium.enkata.com.RepositorySalesForce;
import selenium.enkata.com.SalesForceModule;
import selenium.enkata.com.SeleniumFireFoxDriver;

public class StandartUser {

	private String login = "vmurashko@e2e.enkata.com";

	@Test
	@Parameters({ "url", "password", "connectionString", "userDb", "passwordDb" })
	public void Test01_CreateStandartUsers(String url, String password,
			String connectionString, String userDb, String passwordDb)
			throws InterruptedException {
		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		for (int i = 1; i < 11; i++) {
			String email = "account_" + i + "@enkata.com";
			String username = "account_" + i + "@enkata.com";
			String alias = "al" + i;
			String nickname = "nick" + i;
			String title = "tit" + i;
			String company = "Company" + i;
			String department = "Depart" + i;
			String division = "Dev1" + i;
			String license = "Salesforce" + i;
			String profile = "Standard User" + i;
			String firstName = "Sinor" + i;
			String lastName = "Developer" + i;
			String phone = "11111-" + i;

			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getUserContextMenu());
			Thread.sleep(2000);
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getSetUpItem());
			Thread.sleep(2000);
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getManageUsers());
			Thread.sleep(2000);
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getUsersItem());
			SalesForceModule.createStandartUser(email, username, firstName,
					lastName, alias, nickname, title, company, department,
					division, license, profile, driver, phone);
			Thread.sleep(5000);
		}
	}
	
	@AfterTest
	public void closeAllSession() throws Exception {
		SalesForceModule.closeFireFox();
	}

}
