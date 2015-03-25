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

public class ContactsSalesForce {

	private String name = "Slava0";
	private String firstName = "Sinor";
	private String lastName = "Tester";
	private String email = "account_1@enkata.com";
	private String phone = "+79292929293";
	private String description = "Get deeper customer insights with Social Accounts and Contacts. Click these icons to get started. Tell me more!";
	private String title = "title for";
	private String phoneNew = "+7777777777";
	private String titleNew = "title for updated";
	private int timeout = 60000;

	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test01_CreateContact(String url, String login, String password,
			String connectionString, String userDb, String passwordDb)
			throws InterruptedException {

		Date date = new Date();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
		String formattedDate = sdf.format(date);

		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getContactsTab());
		Thread.sleep(2000);
		SalesForceModule.selectViewAccounts("All Contacts", driver);
		Thread.sleep(2000);
		SalesForceModule.createContact(this.email, this.firstName,
				this.lastName, this.name, this.phone, this.title, description,
				driver);
		Thread.sleep(timeout);

		// Connect to db and check fields
		String accountID = SalesForceModule.getId(connectionString, userDb,
				passwordDb);
		CheckDataBase(accountID, connectionString, userDb, passwordDb, email,
				firstName, lastName, phone, title, description, formattedDate);
	}

	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test02_UpdateContact(String url, String login, String password,
			String connectionString, String userDb, String passwordDb)
			throws InterruptedException {
		String emailNew = "account_10@enkata.com";
		String firstNameNew = "SinorUpdated";
		String lastNameNew = "TesterUpdated";
		String nameNew = "Slava1";
		String descriptionNew = "UPDATE";

		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getContactsTab());
		Thread.sleep(2000);
		SalesForceModule.selectViewAccounts("All Contacts", driver);
		Thread.sleep(2000);

		// Open account for edit.
		driver = SalesForceModule.openAccountForEdit(this.lastName + ","
				+ this.firstName, driver);
		driver = SalesForceModule.fillInContactForm(emailNew, firstNameNew,
				lastNameNew, nameNew, phoneNew, titleNew, descriptionNew,
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
				descriptionNew, formattedDate);

		// Add checking DB old after fix bug
		CheckDataBaseOldTable(accountID, connectionString, userDb, passwordDb,
				this.email, this.firstName, this.lastName, this.phone,
				this.title, this.description);

	}

	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test03_BulckUpdateContacts(String url, String login,
			String password, String connectionString, String userDb,
			String passwordDb) throws InterruptedException {
		String emailNew = "account_10@enkata.com";
		String phoneNew = "+7777777777";
		String nameNew = "Slava1";
		String descriptionNew = "UPDATE";
		String titleNew = "title for updated";

		// Create several users
		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		for (int i = 0; i < 3; i++) {
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getContactsTab());
			Thread.sleep(2000);
			SalesForceModule.selectViewAccounts("All Contacts", driver);
			Thread.sleep(2000);
			SalesForceModule.createContact(emailNew + i, firstName + i,
					lastName + i, nameNew, phoneNew, titleNew, descriptionNew
							+ i, driver);
			Thread.sleep(10000);
		}

		// Perform Bulk update
		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getContactsTab());
		Thread.sleep(2000);
		SalesForceModule.selectViewAccounts("All Contacts", driver);
		Thread.sleep(2000);
		selectAccounts(driver);

		// Fax update
		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		String newFax = "1235";
		SalesForceModule.clickOnFaxAccountByName(this.lastName + 0 + ","
				+ this.firstName + 0, driver);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getBulkFieldFax(), newFax);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getRadioButtonAllFields());
		SeleniumFireFoxDriver
				.Click(driver, RepositorySalesForce.getBulckSave());
		Thread.sleep(timeout);
		ArrayList<String> valuesFromDb = new ArrayList<String>();
		valuesFromDb = SalesForceModule.getSeveralFields("Fax",
				connectionString, userDb, passwordDb);
		Assert.assertTrue(valuesFromDb.size() == 3);
		for (String element : valuesFromDb) {
			Assert.assertEquals(newFax, element);
		}
		valuesFromDb.clear();

		valuesFromDb = SalesForceModule.getSeveralFieldsFromOLDTable("Fax",
				connectionString, userDb, passwordDb);
		Assert.assertTrue(valuesFromDb.size() == 3);
		for (String element : valuesFromDb) {
			Assert.assertEquals(null, element);
		}
		valuesFromDb.clear();

		// Phone updated
		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		String newPhone = "1234567890";
		SalesForceModule.clickOnPhoneAccountByName(this.lastName + 0 + ","
				+ this.firstName + 0, driver);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getBulkFieldPhone(), newPhone);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getRadioButtonAllFields());
		SeleniumFireFoxDriver
				.Click(driver, RepositorySalesForce.getBulckSave());
		Thread.sleep(timeout);
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
		String title = "bulk title";
		SalesForceModule.clickOnWebSiteByName(this.lastName + 0 + ","
				+ this.firstName + 0, driver);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getBulkFieldTitle(), title);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getRadioButtonAllFields());
		SeleniumFireFoxDriver
				.Click(driver, RepositorySalesForce.getBulckSave());
		Thread.sleep(timeout);
		valuesFromDb = SalesForceModule.getSeveralFields("Title",
				connectionString, userDb, passwordDb);
		Assert.assertTrue(valuesFromDb.size() == 3);
		for (String element : valuesFromDb) {
			Assert.assertEquals(title, element);
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
			String formattedDate) {
		Assert.assertEquals(
				email.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Email",
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

	private void CheckDataBaseOldTable(String accountID,
			String connectionString, String userDb, String passwordDb,
			String email, String firstName, String lastName, String phone,
			String title, String description) {
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
		SalesForceModule.selectAccountByName(this.lastName + 0 + ","
				+ this.firstName + 0, driver);
		SalesForceModule.selectAccountByName(this.lastName + 1 + ","
				+ this.firstName + 1, driver);
		SalesForceModule.selectAccountByName(this.lastName + 2 + ","
				+ this.firstName + 2, driver);

		return driver;
	}

	@AfterTest
	public void closeAllSession() throws Exception {
		SalesForceModule.closeFireFox();
	}

}
