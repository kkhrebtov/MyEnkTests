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

public class LeadsSalesForce {

	private String firstName = "Sauron";
	private String lastName = "Frodovich";
	private String email = "account_9@enkata.com";
	private String phone = "+79292929293";
	private String company = "MordorCo.";
	private String description = "Get deeper customer insights with Social Accounts and Contacts. Click these icons to get started. Tell me more!";
	private String title = "title for";
	private int timeout = 60000*5;

	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test01_CreateLead(String url, String login, String password,
			String connectionString, String userDb, String passwordDb)
			throws InterruptedException {

		Date date = new Date();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
		String formattedDate = sdf.format(date);

		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Click(driver, RepositorySalesForce.getLeadsTab());
		Thread.sleep(2000);
		SalesForceModule.selectViewAccounts("All Open Leads", driver);
		Thread.sleep(2000);
		SalesForceModule.createLead(company, firstName, lastName, email, phone,
				title, description, driver);
		Thread.sleep(timeout);

		// Connect to db and check fields
		String accountID = SalesForceModule.getId(connectionString, userDb,
				passwordDb);
		CheckDataBase(accountID, connectionString, userDb, passwordDb, email,
				firstName, lastName, phone, title, description, formattedDate,
				company);
	}

	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test02_UpdateLeads(String url, String login, String password,
			String connectionString, String userDb, String passwordDb)
			throws InterruptedException {
		String emailNew = "account_1@enkata.com";
		String phoneNew = "+111111111111";
		String companyNew = "Sheer.";
		String firstNameNew = "Bilbo";
		String lastNameNew = "Bagins";
		String descriptionNew = "A Long Time Ago, in a Galaxy Far Far Away...";
		String titleNew = "A Long Time";

		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Click(driver, RepositorySalesForce.getLeadsTab());
		Thread.sleep(2000);
		SalesForceModule.selectViewAccounts("All Open Leads", driver);
		Thread.sleep(2000);

		// Open account for edit.
		driver = SalesForceModule.openAccountForEdit(this.lastName + ","
				+ this.firstName, driver);
		driver = SalesForceModule.fillInLeadForm(companyNew, firstNameNew,
				lastNameNew, emailNew, phoneNew, titleNew, descriptionNew,
				driver);
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
				emailNew, firstNameNew, lastNameNew, phoneNew, titleNew,
				descriptionNew, formattedDate, companyNew);
		CheckDataBaseOldTabel(accountID, connectionString, userDb, passwordDb,
				this.email, this.firstName, this.lastName, this.phone,
				this.title, this.description, this.company);

	}

	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test03_BulckUpdateContacts(String url, String login,
			String password, String connectionString, String userDb,
			String passwordDb) throws InterruptedException {
		String emailNew = "new_1@enkata.com";
		String phoneNew = "+77777";
		String companyNew = "Colobok";
		String descriptionNew = "A Long Time Ago, in a Galaxy Far Far Away...";
		String titleNew = "Long story";

		// Create several users
		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		for (int i = 0; i < 3; i++) {
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getLeadsTab());
			Thread.sleep(2000);
			SalesForceModule.selectViewAccounts("All Open Leads", driver);
			Thread.sleep(2000);
			SalesForceModule.createLead(i + companyNew, i + firstName, i
					+ lastName, i + emailNew, phoneNew, titleNew, i
					+ descriptionNew, driver);
			Thread.sleep(10000);
		}

		// Perform Bulk update
		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		SeleniumFireFoxDriver.Click(driver, RepositorySalesForce.getLeadsTab());
		Thread.sleep(2000);
		SalesForceModule.selectViewAccounts("All Open Leads", driver);
		Thread.sleep(2000);
		selectAccounts(driver);

		// Phone update
		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		String newPhone = "1235";
		SalesForceModule.clickOnFaxAccountByName(0 + this.lastName + "," + 0
				+ this.firstName, driver);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getBulkFieldPhone(), newPhone);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getRadioButtonAllFields());
		SeleniumFireFoxDriver
				.Click(driver, RepositorySalesForce.getBulckSave());
		Thread.sleep(timeout);
		ArrayList<String> valuesFromDb = new ArrayList<String>();
		valuesFromDb = SalesForceModule.getSeveralFields("Phone",
				connectionString, userDb, passwordDb);
		Assert.assertTrue(valuesFromDb.size() == 3);
		for (String element : valuesFromDb) {
			Assert.assertEquals(newPhone, element);
		}
		valuesFromDb.clear();
		
		valuesFromDb = SalesForceModule.getSeveralFieldsFromOLDTable("Phone",
				connectionString, userDb, passwordDb);
		Assert.assertTrue(valuesFromDb.size() == 3);
		for (String element : valuesFromDb) {
			Assert.assertEquals(phoneNew, element);
		}
		valuesFromDb.clear();
		

		// Title updated
		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		String newTitle = "titleNew";
		SalesForceModule.clickOnPhoneAccountByName(0 + this.lastName + "," + 0
				+ this.firstName, driver);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getBulkFieldTitle(), newTitle);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getRadioButtonAllFields());
		SeleniumFireFoxDriver
				.Click(driver, RepositorySalesForce.getBulckSave());
		Thread.sleep(timeout);
		valuesFromDb = SalesForceModule.getSeveralFields("Title",
				connectionString, userDb, passwordDb);
		Assert.assertTrue(valuesFromDb.size() == 3);
		for (String element : valuesFromDb) {
			Assert.assertEquals(newTitle, element);
		}
		valuesFromDb.clear();
		
		valuesFromDb = SalesForceModule.getSeveralFieldsFromOLDTable("Title",
				connectionString, userDb, passwordDb);
		Assert.assertTrue(valuesFromDb.size() == 3);
		for (String element : valuesFromDb) {
			Assert.assertEquals(titleNew, element);
		}
		valuesFromDb.clear();
	}

	// //////////////////////////
	// ///private methods////////
	// /////////////////////////
	private void CheckDataBase(String accountID, String connectionString,
			String userDb, String passwordDb, String email, String firstName,
			String lastName, String phone, String title, String description,
			String formattedDate, String company) {
		Assert.assertEquals(
				email.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Email",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				"Open".replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Status",
						connectionString, userDb, passwordDb).replace(" ", ""));

		Assert.assertEquals(
				firstName.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Firstname",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				lastName.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Lastname",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				phone.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Phone",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				company.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Company",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				title.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Title",
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

	private void CheckDataBaseOldTabel(String accountID,
			String connectionString, String userDb, String passwordDb,
			String email, String firstName, String lastName, String phone,
			String title, String description, String company) {
		Assert.assertEquals(
				email.replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID, "Email",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				firstName.replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID, "Firstname",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				lastName.replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID, "Lastname",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				phone.replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID, "Phone",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				company.replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID, "Company",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				title.replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID, "Title",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				description.replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID,
						"Description", connectionString, userDb, passwordDb)
						.replace(" ", ""));
	}

	private WebDriver selectAccounts(WebDriver driver)
			throws InterruptedException {
		SalesForceModule.selectAccountByName(0 + this.lastName + "," + 0
				+ this.firstName, driver);
		SalesForceModule.selectAccountByName(1 + this.lastName + "," + 1
				+ this.firstName, driver);
		SalesForceModule.selectAccountByName(2 + this.lastName + "," + 2
				+ this.firstName, driver);

		return driver;
	}

	@AfterTest
	public void closeAllSession() throws Exception {
		SalesForceModule.closeFireFox();
	}

}
