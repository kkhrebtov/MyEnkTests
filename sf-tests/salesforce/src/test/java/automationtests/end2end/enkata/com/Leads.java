package automationtests.end2end.enkata.com;

import org.openqa.selenium.WebDriver;
import org.testng.annotations.AfterTest;
import org.testng.annotations.Parameters;
import org.testng.annotations.Test;
import selenium.enkata.com.RepositorySalesForce;
import selenium.enkata.com.SalesForceModule;
import selenium.enkata.com.SeleniumFireFoxDriver;

public class Leads {
	
	private String firstName = "firstName";
	private String lastName = "lastName";
	private String email = "lead_";
	private String phone = "22222-";
	private String company = "Enkata_";
	private String description = "Get deeper customer insights with Social Accounts and Contacts. Click these icons to get started. Tell me more!";
	private String title = "title for";
	

	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test01_CreateLead(String url, String login, String password,
			String connectionString, String userDb, String passwordDb)
			throws InterruptedException {
		

		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		for (int i = 1; i < 11; i++) {
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getLeadsTab());
			Thread.sleep(2000);
			SalesForceModule.selectViewAccounts("All Open Leads", driver);
			Thread.sleep(2000);
			SalesForceModule.createLead(company + i, firstName + i, lastName
					+ i, email + i + "@enkata.com", phone + i, title + i,
					description + i, driver);
			Thread.sleep(5000);
		}

	}	
	
	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test02_ConvertLeadToContact(String url, String login, String password,
			String connectionString, String userDb, String passwordDb)
			throws InterruptedException {		

		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		for (int i = 1; i < 6; i++) {
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getLeadsTab());
			Thread.sleep(2000);
			SalesForceModule.selectViewAccounts("All Open Leads", driver);
			Thread.sleep(2000);
			
			driver = SalesForceModule.openAccount(this.lastName + i +","
					+ this.firstName + i, driver);
			SeleniumFireFoxDriver.Click(driver, RepositorySalesForce.getConvertButton());
			SeleniumFireFoxDriver.Type(driver, RepositorySalesForce.getSubjectField(), "Send Letter");
			SeleniumFireFoxDriver.Click(driver, RepositorySalesForce.getSaveButton());			
			Thread.sleep(5000);
		}
		
		for (int i = 6; i < 11; i++) {
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getLeadsTab());
			Thread.sleep(2000);
			SalesForceModule.selectViewAccounts("All Open Leads", driver);
			Thread.sleep(2000);
			
			driver = SalesForceModule.openAccount(this.lastName + i +","
					+ this.firstName + i, driver);
			SeleniumFireFoxDriver.Click(driver, RepositorySalesForce.getConvertButton());
			SeleniumFireFoxDriver.Type(driver, RepositorySalesForce.getSubjectField(), "Send Letter");
			SeleniumFireFoxDriver.Click(driver, RepositorySalesForce.getDonCreateOpportunity());
			SeleniumFireFoxDriver.Click(driver, RepositorySalesForce.getSaveButton());			
			Thread.sleep(5000);
		}

	}
	
	@AfterTest
	public void closeAllSession() throws Exception {
		SalesForceModule.closeFireFox();
	}

}
