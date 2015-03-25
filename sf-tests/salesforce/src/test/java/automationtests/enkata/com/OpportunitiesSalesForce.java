package automationtests.enkata.com;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

import junit.framework.Assert;

import org.openqa.selenium.WebDriver;
import org.testng.annotations.AfterTest;
import org.testng.annotations.Parameters;
import org.testng.annotations.Test;

import selenium.enkata.com.RepositorySalesForce;
import selenium.enkata.com.SalesForceModule;
import selenium.enkata.com.SeleniumFireFoxDriver;

public class OpportunitiesSalesForce {

	private String opportunityName = "opportunityTest";
	private String accountName = "Slava0";
	private String closeDate = "12/12/2014";
	private String description = "Get deeper customer insights with Social Accounts and Contacts. Click these icons to get started. Tell me more!";
	private String stage = "Prospecting";
	private String amount = "1000";
	private int timeout = 60000;

	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test01_CreateOpportunities(String url, String login,
			String password, String connectionString, String userDb,
			String passwordDb) throws InterruptedException {

		Date date = new Date();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
		String formattedDate = sdf.format(date);

		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getOpportunitiesTab());
		Thread.sleep(2000);
		SalesForceModule.selectViewAccounts("All Opportunities", driver);
		Thread.sleep(2000);
		SalesForceModule.createOpportunity(description, opportunityName,
				accountName, closeDate, stage, amount, driver);
		Thread.sleep(timeout);

		// Connect to db and check fields
		String accountID = SalesForceModule.getId(connectionString, userDb,
				passwordDb);
		CheckDataBase(accountID, connectionString, userDb, passwordDb,
				description, opportunityName, accountName, "2014-12-12", stage,
				amount, formattedDate);
	}

	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test02_UpdateOpportunity(String url, String login,
			String password, String connectionString, String userDb,
			String passwordDb) throws InterruptedException {
		String accountNameNew = "Slava1";
		String closeDateNew = "12/11/2014";
		String descriptionNew = "New description Updated";
		String stageNew = "Qualification";
		String amountNew = "2000";
		String opportunityNameNew = "New name OPP";

		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getOpportunitiesTab());
		Thread.sleep(2000);
		SalesForceModule.selectViewAccounts("All Opportunities", driver);
		Thread.sleep(2000);

		// Open account for edit.
		driver = SalesForceModule.openAccountForEdit(this.opportunityName,
				driver);
		driver = SalesForceModule.fillInOpportunity(opportunityNameNew,
				accountNameNew, closeDateNew, stageNew, amountNew,
				descriptionNew, driver);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getSaveButton());
		Thread.sleep(timeout);

		Date date = new Date();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
		String formattedDate = sdf.format(date);

		// Check DB
		String accountID = SalesForceModule.getId(connectionString, userDb,
				passwordDb);
		CheckDataBase(accountID, connectionString, userDb, passwordDb,
				descriptionNew, opportunityNameNew, accountNameNew,
				"2014-12-11", stageNew, amountNew, formattedDate);
		CheckDataBaseOldTable(accountID, connectionString, userDb, passwordDb,
				this.description, this.opportunityName, this.accountName,
				"2014-12-12", this.stage, this.amount);

	}

	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test03_BulckUpdateContacts(String url, String login,
			String password, String connectionString, String userDb,
			String passwordDb) throws InterruptedException {
		
		String closeDateNew = "12/11/2014";
		String descriptionNew = "New description Updated";
		String stageNew = "Qualification";
		String amountNew = "2000";

		// Create several users
		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		for (int i = 0; i < 3; i++) {
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getOpportunitiesTab());
			Thread.sleep(2000);
			SalesForceModule.selectViewAccounts("All Opportunities", driver);
			Thread.sleep(2000);
			SalesForceModule.createOpportunity(descriptionNew + i,
					this.opportunityName + i, "Slava" + i, closeDateNew,
					stageNew, amountNew + i, driver);
			Thread.sleep(10000);
		}

		// Perform Bulk update
		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getOpportunitiesTab());
		Thread.sleep(2000);
		SalesForceModule.selectViewAccounts("All Opportunities", driver);
		Thread.sleep(2000);
		selectAccounts(driver);

		// NexTstep update
		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		String nextStep = "1235";
		SalesForceModule.clickOnFaxAccountByName(this.opportunityName + 0,
				driver);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getBulkFieldNextStep(), nextStep);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getRadioButtonAllFields());
		SeleniumFireFoxDriver
				.Click(driver, RepositorySalesForce.getBulckSave());
		Thread.sleep(timeout);
		ArrayList<String> valuesFromDb = new ArrayList<String>();
		valuesFromDb = SalesForceModule.getSeveralFields("NextStep",
				connectionString, userDb, passwordDb);
		Assert.assertTrue(valuesFromDb.size() == 3);
		for (String element : valuesFromDb) {
			Assert.assertEquals(nextStep, element);
		}
		valuesFromDb.clear();
		
		valuesFromDb = SalesForceModule.getSeveralFieldsFromOLDTable("NextStep",
				connectionString, userDb, passwordDb);
		Assert.assertTrue(valuesFromDb.size() == 3);
		for (String element : valuesFromDb) {
			Assert.assertEquals(null, element);
		}
		valuesFromDb.clear();
		

		// CloseDate updated
		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		String closeDate = "12/10/2014";
		SalesForceModule.clickOnPhoneAccountByName(this.opportunityName + 0,
				driver);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getBulkFieldCloseDate(), closeDate);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getRadioButtonAllFields());
		SeleniumFireFoxDriver
				.Click(driver, RepositorySalesForce.getBulckSave());
		Thread.sleep(timeout);
		valuesFromDb = SalesForceModule.getSeveralFields("CloseDate",
				connectionString, userDb, passwordDb);
		Assert.assertTrue(valuesFromDb.size() == 3);
		for (String element : valuesFromDb) {
			Assert.assertTrue(element.contains("2014-12-10"));
		}
		valuesFromDb.clear();
		
		valuesFromDb = SalesForceModule.getSeveralFieldsFromOLDTable("CloseDate",
				connectionString, userDb, passwordDb);
		Assert.assertTrue(valuesFromDb.size() == 3);
		for (String element : valuesFromDb) {
			Assert.assertTrue(element.contains("2014-12-11"));
		}
		valuesFromDb.clear();
	}

	// //////////////////////////
	// ///private methods////////
	// /////////////////////////
	private void CheckDataBase(String accountID, String connectionString,
			String userDb, String passwordDb, String description,
			String opportunityName, String accountName, String closeDate,
			String stage, String amount, String formattedDate) {

		Assert.assertEquals(
				amount.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Amount",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				opportunityName.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Name",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				stage.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "StageName",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				closeDate.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "CloseDate",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				description.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Description",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertTrue(SalesForceModule
				.getFieldValue(accountID, "CreatedDate", connectionString,
						userDb, passwordDb).replace(" ", "")
				.contains(formattedDate.replace(" ", "")));
		Assert.assertTrue(SalesForceModule
				.getFieldValue(accountID, "LastModifiedDate", connectionString,
						userDb, passwordDb).replace(" ", "")
				.contains(formattedDate.replace(" ", "")));
	}

	private void CheckDataBaseOldTable(String accountID,
			String connectionString, String userDb, String passwordDb,
			String description, String opportunityName, String accountName,
			String closeDate, String stage, String amount) {
		Assert.assertEquals(
				amount.replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID, "Amount",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				opportunityName.replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID, "Name",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				stage.replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID, "StageName",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				closeDate.replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID, "CloseDate",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				description.replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID,
						"Description", connectionString, userDb, passwordDb)
						.replace(" ", ""));
	}

	private WebDriver selectAccounts(WebDriver driver)
			throws InterruptedException {
		SalesForceModule.selectAccountByName(this.opportunityName + 0, driver);
		SalesForceModule.selectAccountByName(this.opportunityName + 1, driver);
		SalesForceModule.selectAccountByName(this.opportunityName + 2, driver);

		return driver;
	}

	@AfterTest
	public void closeAllSession() throws Exception {
		SalesForceModule.closeFireFox();
	}

}
