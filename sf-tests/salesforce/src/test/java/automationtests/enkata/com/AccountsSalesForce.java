package automationtests.enkata.com;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.Statement;
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

public class AccountsSalesForce {

	private static String name = "Slava";
	ArrayList<String> valuesFromDb = new ArrayList<String>();
	private String faxUpdated = "9999999999";
	private String phoneUpdated = "+777777777";
	private String webSiteUpdated = "http://sheer.com";

	private String fax = "12312312";
	private String phone = "+79292929293";
	private String webSite = "http://mordorcity.com";
	private int employees = 155;
	private int annualRevenue = 125;
	private String description = "Get deeper customer insights with Social Accounts and Contacts. Click these icons to get started. Tell me more!";
	private int timeout = 60000;
	

	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test01_CreateAccount(String url, String login, String password,
			String connectionString, String userDb, String passwordDb)
			throws InterruptedException {
		Date date = new Date();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
		String formattedDate = sdf.format(date);

		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getAccountsTab());
		Thread.sleep(2000);
		SalesForceModule.selectViewAccounts("All Accounts", driver);
		Thread.sleep(2000);
		SalesForceModule.createAccount(name, fax, phone, webSite, employees,
				annualRevenue, description, driver);
		Thread.sleep(timeout);

		// Connect to db and check fields
		String accountID = SalesForceModule.getId(connectionString, userDb,
				passwordDb);
		CheckDataBase(accountID, name, webSite, phone, employees,
				annualRevenue, description, connectionString, userDb,
				passwordDb, formattedDate);
	}

	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test02_UpdateAccount(String url, String login, String password,
			String connectionString, String userDb, String passwordDb)
			throws InterruptedException {

		String phoneNew = "+777777777";
		String webSiteNew = "http://sheer.com";
		int employeesNew = 1;
		int annualRevenueNew = 99;
		String descriptionNew = "Improvement Opportunities";
		String accountNameUpdated = "newName";

		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getAccountsTab());
		Thread.sleep(2000);
		SalesForceModule.selectViewAccounts("All Accounts", driver);
		Thread.sleep(2000);

		// Open account for edit.
		driver = SalesForceModule.openAccountForEdit(name, driver);
		driver = SalesForceModule.fillInAccountForm(accountNameUpdated,
				faxUpdated, phoneNew, webSiteNew, employeesNew, annualRevenueNew,
				descriptionNew, driver);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getSaveButton());
		Thread.sleep(timeout);


		Date date = new Date();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
		String formattedDate = sdf.format(date);

		String accountID = SalesForceModule.getId(connectionString, userDb,
				passwordDb);
		CheckDataBase(accountID, accountNameUpdated, webSiteNew, phoneNew, employeesNew,
				annualRevenueNew, descriptionNew, connectionString, userDb,
				passwordDb, formattedDate);
		CheckDataBaseOLDTable(accountID, name, this.webSite, this.phone,
				this.employees, this.annualRevenue, this.description,
				connectionString, userDb, passwordDb);

	}

	@Test
	@Parameters({ "url", "login", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test03_BulckUpdateAccount(String url, String login,
			String password, String connectionString, String userDb,
			String passwordDb) throws InterruptedException {
		String fax = "9999999999";
		int employees = 1;
		int annualRevenue = 99;
		String description = "Improvement Opportunities";

		// Create several users
		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		for (int i = 0; i < 3; i++) {
			SeleniumFireFoxDriver.Click(driver,
					RepositorySalesForce.getAccountsTab());
			Thread.sleep(2000);
			SalesForceModule.selectViewAccounts("All Accounts", driver);
			Thread.sleep(2000);

			SalesForceModule.createAccount(name + i, fax, phoneUpdated,
					webSiteUpdated, employees + i, annualRevenue + i,
					description + i, driver);
			Thread.sleep(10000);
		}

		// Perform Bulk update
		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getAccountsTab());
		Thread.sleep(2000);
		SalesForceModule.selectViewAccounts("All Accounts", driver);
		Thread.sleep(2000);
		selectAccounts(driver);

		// Fax update
		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		String newFax = "123";
		SalesForceModule.clickOnFaxAccountByName(name + 0, driver);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getBulkFieldFax(), newFax);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getRadioButtonAllFields());
		SeleniumFireFoxDriver
				.Click(driver, RepositorySalesForce.getBulckSave());
		Thread.sleep(timeout);
		GetSeveralFields("Fax", connectionString, userDb, passwordDb);
		Assert.assertTrue(valuesFromDb.size() == 3);
		for (String element : valuesFromDb) {
			Assert.assertEquals(newFax, element);
		}
		valuesFromDb.clear();

		valuesFromDb = SalesForceModule.getSeveralFieldsFromOLDTable("Fax",
				connectionString, userDb, passwordDb);
		Assert.assertTrue(valuesFromDb.size() == 3);
		for (String element : valuesFromDb) {
			Assert.assertTrue(faxUpdated.equals(element.replace(")", "").replace("(", "").replace("-", "").replace(" ", "")));
		}
		valuesFromDb.clear();

		// Phone updated
		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		String newPhone = "1234567890";
		SalesForceModule.clickOnPhoneAccountByName(name + 0, driver);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getBulkFieldPhone(), newPhone);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getRadioButtonAllFields());
		SeleniumFireFoxDriver
				.Click(driver, RepositorySalesForce.getBulckSave());
		Thread.sleep(timeout);
		GetSeveralFields("Phone", connectionString, userDb, passwordDb);
		Assert.assertTrue(valuesFromDb.size() == 3);
		for (String element : valuesFromDb) {
			Assert.assertEquals(newPhone, element);
		}
		valuesFromDb.clear();

		valuesFromDb = SalesForceModule.getSeveralFieldsFromOLDTable("Phone",
				connectionString, userDb, passwordDb);
		Assert.assertTrue(valuesFromDb.size() == 3);
		for (String element : valuesFromDb) {
			Assert.assertEquals(phoneUpdated, element);
		}
		valuesFromDb.clear();

		// WebSite updated
		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		String newWebSite = "https://fix.com";
		SalesForceModule.clickOnWebSiteByName(name + 0, driver);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getBulkFieldWebSite(), newWebSite);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getRadioButtonAllFields());
		SeleniumFireFoxDriver
				.Click(driver, RepositorySalesForce.getBulckSave());
		Thread.sleep(timeout);
		GetSeveralFields("Website", connectionString, userDb, passwordDb);
		Assert.assertTrue(valuesFromDb.size() == 3);
		for (String element : valuesFromDb) {
			Assert.assertEquals(newWebSite, element);
		}
		valuesFromDb.clear();
		valuesFromDb = SalesForceModule.getSeveralFieldsFromOLDTable("Website",
				connectionString, userDb, passwordDb);
		Assert.assertTrue(valuesFromDb.size() == 3);
		for (String element : valuesFromDb) {
			Assert.assertEquals(webSiteUpdated, element);
		}
		valuesFromDb.clear();
	}

	// ///////////////////////
	// //Private methods//////
	// ///////////////////////
	@AfterTest
	public void closeAllSession() throws Exception {
		SalesForceModule.closeFireFox();
	}

	private WebDriver selectAccounts(WebDriver driver)
			throws InterruptedException {
		SalesForceModule.selectAccountByName(name + 0, driver);
		SalesForceModule.selectAccountByName(name + 1, driver);
		SalesForceModule.selectAccountByName(name + 2, driver);

		return driver;
	}

	private void CheckDataBase(String accountID, String nameAccount,
			String webSite, String phone, int employees, int annualRevenue,
			String description, String connectionString, String userDb,
			String passwordDb, String formattedDate) {
		Assert.assertEquals(
				nameAccount.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Name",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				webSite.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Website",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				phone.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Phone",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				Integer.toString(employees).replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "NumberOfEmployees",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				Integer.toString(annualRevenue).replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "AnnualRevenue",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				description.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Description",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertTrue(SalesForceModule
				.getFieldValue(accountID, "CreatedDate", connectionString,
						userDb, passwordDb).replace(" ", "")
				.contains(formattedDate));
		Assert.assertTrue(SalesForceModule
				.getFieldValue(accountID, "LastModifiedDate", connectionString,
						userDb, passwordDb).replace(" ", "")
				.contains(formattedDate));
	}

	private void GetSeveralFields(String nameField, String connectionString,
			String userDb, String passwordDb) {

		try {
			Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
			Connection conn = DriverManager.getConnection(connectionString,
					userDb, passwordDb);
			// System.out.println("connected");
			Statement statement = conn.createStatement();
			String queryString = "SELECT [attr_value] FROM [ADA_RAW].[dbo].[AAL_SFDC_SNAPSHOT_DATA] WHERE attr_name='"
					+ nameField + "'";
			ResultSet rs = statement.executeQuery(queryString);
			while (rs.next()) {
				String value = rs.getString(1);
				valuesFromDb.add(value);
			}
			conn.close();
		} catch (Exception e) {
			e.printStackTrace();
		}

	}

	private void CheckDataBaseOLDTable(String accountID, String nameAccount,
			String webSite, String phone, int employees, int annualRevenue,
			String description, String connectionString, String userDb,
			String passwordDb) {
		Assert.assertEquals(
				nameAccount.replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID, "Name",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				webSite.replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID, "Website",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				phone.replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID, "Phone",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				Integer.toString(employees).replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID,
						"NumberOfEmployees", connectionString, userDb,
						passwordDb).replace(" ", ""));
		Assert.assertEquals(
				Integer.toString(annualRevenue).replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID,
						"AnnualRevenue", connectionString, userDb, passwordDb)
						.replace(" ", ""));
		Assert.assertEquals(
				description.replace(" ", ""),
				SalesForceModule.getFieldValueOldTable(accountID,
						"Description", connectionString, userDb, passwordDb)
						.replace(" ", ""));		
	}

}
