package automationtests.end2end.enkata.com;

import java.text.SimpleDateFormat;
import java.util.Date;

import org.openqa.selenium.WebDriver;
import org.testng.annotations.AfterTest;
import org.testng.annotations.Parameters;
import org.testng.annotations.Test;
import selenium.enkata.com.RepositorySalesForce;
import selenium.enkata.com.SalesForceModule;
import selenium.enkata.com.SeleniumFireFoxDriver;

@Test
public class Opportunities {

	private String opportunityName = "opportunity_";
	private String accountName = "account_";
	private String closeDate = "12/12/2014";
	private String description = "Get deeper customer insights with Social Accounts and Contacts. Click these icons to get started. Tell me more!";
	private String stage = "Prospecting";
	private String amount = "1000";

	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test01_CreateOpportunities(String url, String login,
			String password, String connectionString, String userDb,
			String passwordDb) throws InterruptedException {

		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);

		for (int i = 1; i < 11; i++) {
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getOpportunitiesTab());
			Thread.sleep(2000);
			SalesForceModule.selectViewAccounts("All Opportunities", driver);
			Thread.sleep(2000);
			SalesForceModule.createOpportunity(description,
					opportunityName + i, accountName + i, closeDate, stage,
					amount, driver);
			Thread.sleep(30000);
		}

	}

	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test02_UpdateStageOpportunities(String url, String login,
			String password, String connectionString, String userDb,
			String passwordDb) throws InterruptedException {
		String stageCollection[];
		stageCollection = new String[] { "Qualification", "Needs Analysis",
				"Value Proposition", "Id. Decision Makers",
				"Perception Analysis", "Proposal/Price Quote",
				"Negotiation/Review", "Closed Won", "Closed Lost",
				"Qualification", "Id. Decision Makers" };

		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		for (int i = 1; i < 11; i++) {
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getOpportunitiesTab());
			Thread.sleep(2000);
			SalesForceModule.selectViewAccounts("All Opportunities", driver);
			Thread.sleep(2000);
			driver = SalesForceModule.openAccountForEdit(this.opportunityName
					+ i, driver);
			driver = SalesForceModule.fillInOpportunity(opportunityName + i,
					accountName, closeDate, stageCollection[i], amount,
					description, driver);
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getSaveButton());
			Thread.sleep(5000);
		}
	}

	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test03_UpdateOthersOpportunities(String url, String login,
			String password, String connectionString, String userDb,
			String passwordDb) throws InterruptedException {
		String stageCollection[];
		stageCollection = new String[] { "Qualification", "Needs Analysis",
				"Value Proposition", "Id. Decision Makers",
				"Perception Analysis", "Proposal/Price Quote",
				"Negotiation/Review", "Closed Won", "Closed Lost",
				"Qualification", "Id. Decision Makers" };
		String amountCollection[];
		amountCollection = new String[] { "120", "130", "140", "150", "160",
				"170", "180", "190", "200", "210", "220" };
		String closeDatetCollection[];
		closeDatetCollection = new String[] { "12/01/2014", "12/02/2014",
				"12/03/2014", "12/04/2014", "12/05/2014", "12/06/2014",
				"12/07/2014", "12/08/2014", "12/09/2014", "12/10/2014",
				"12/11/2014" };

		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		for (int i = 1; i < 11; i++) {
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getOpportunitiesTab());
			Thread.sleep(2000);
			SalesForceModule.selectViewAccounts("All Opportunities", driver);
			Thread.sleep(2000);
			driver = SalesForceModule.openAccountForEdit(this.opportunityName
					+ i, driver);
			driver = SalesForceModule.fillInOpportunity(opportunityName + i,
					accountName, closeDatetCollection[i], stageCollection[i],
					amountCollection[i], description, driver);
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getSaveButton());
			Thread.sleep(5000);
		}
	}

	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test04_CreateContactRole(String url, String login,
			String password, String connectionString, String userDb,
			String passwordDb) throws InterruptedException {

		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		for (int i = 1; i < 11; i++) {
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getOpportunitiesTab());
			Thread.sleep(2000);
			SalesForceModule.selectViewAccounts("All Opportunities", driver);
			Thread.sleep(2000);
			driver = SalesForceModule.openAccount(this.opportunityName + i,
					driver);
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getNewButtonForContactRole());
			SeleniumFireFoxDriver.Type(driver, RepositorySalesForce
					.getContact1Field(), SeleniumFireFoxDriver.GetProperty(
					driver, RepositorySalesForce.getContact0Field(), "value"));

			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getRadioButtonContact1());
			SalesForceModule.selectRole0("Business User", driver);
			SalesForceModule.selectRole1("Decision Maker", driver);
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getSaveButton());
			Thread.sleep(5000);
		}
	}

	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test05_CreateOpportunitiesCloseIt(String url, String login,
			String password, String connectionString, String userDb,
			String passwordDb) throws InterruptedException {
		String stageCollection[];
		stageCollection = new String[] { "Qualification", "Needs Analysis",
				"Value Proposition", "Id. Decision Makers",
				"Perception Analysis", "Proposal/Price Quote",
				"Negotiation/Review", "Closed Won", "Closed Lost",
				"Qualification", "Id. Decision Makers" };
		String amountCollection[];
		amountCollection = new String[] { "120", "130", "140", "150", "160",
				"170", "180", "190", "200", "210", "220" };
		String closeDatetCollection[];
		closeDatetCollection = new String[] { "12/01/2014", "12/02/2014",
				"12/03/2014", "12/04/2014", "12/05/2014", "12/06/2014",
				"12/07/2014", "12/08/2014", "12/09/2014", "12/10/2014",
				"12/11/2014" };

		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);

		//Create opportunities and change stage
		for (int i = 1; i < 2; i++) {
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getOpportunitiesTab());
			Thread.sleep(2000);
			SalesForceModule.selectViewAccounts("All Opportunities", driver);
			Thread.sleep(2000);
			SalesForceModule.createOpportunity(description,
					opportunityName + i+"Close", accountName + i, closeDate, stage,
					amount, driver);
			Thread.sleep(30000);
		
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getOpportunitiesTab());
		Thread.sleep(2000);
		SalesForceModule.selectViewAccounts("All Opportunities", driver);
		Thread.sleep(2000);
		driver = SalesForceModule.openAccountForEdit(this.opportunityName + i+"Close", driver);
		driver = SalesForceModule.fillInOpportunity(this.opportunityName + i+"Close",
				accountName, closeDatetCollection[i], stageCollection[i],
				amountCollection[i], description, driver);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getSaveButton());
		Thread.sleep(5000);
		}
		
		//Close opportunities
		Date date = new Date();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
		String formattedDate = sdf.format(date);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getOpportunitiesTab());
		Thread.sleep(2000);
		SalesForceModule.selectViewAccounts("All Opportunities", driver);
		Thread.sleep(2000);
		driver = SalesForceModule.openAccountForEdit(this.opportunityName + 1 + "Close", driver);
		driver = SalesForceModule.fillInOpportunity(opportunityName + 1,
				accountName, formattedDate, stageCollection[7],
				amountCollection[2], description, driver);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getSaveButton());
		Thread.sleep(5000);
		

	}

	@AfterTest
	public void closeAllSession() throws Exception {
		SalesForceModule.closeFireFox();
	}

}
