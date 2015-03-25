package automationtests.enkata.com;

import java.text.SimpleDateFormat;
import java.util.Date;
import junit.framework.Assert;
import org.openqa.selenium.WebDriver;
import org.testng.annotations.AfterTest;
import org.testng.annotations.Parameters;
import org.testng.annotations.Test;
import selenium.enkata.com.RepositorySalesForce;
import selenium.enkata.com.SalesForceModule;
import selenium.enkata.com.SeleniumFireFoxDriver;

public class StandartUserSalesForce {

	private String login = "vmurashko@e2e.enkata.com";
	private String increment = "22";
	private String firstName = "Sinor" + increment;
	private String lastName = "Developer2" + increment;
	private String email = "testenkata" + increment + "@enkata.com";
	private String phone = "+79112194" + increment;
	private int timeout = 60000;

	@Test
	@Parameters({ "url", "password", "connectionString", "userDb", "passwordDb" })
	public void Test01_CreateStandartUser(String url, String password,
			String connectionString, String userDb, String passwordDb)
			throws InterruptedException {	
		String alias = "al1" + increment;
		String nickname = "tes"+ increment;
		String title = "test"+ increment;
		String company = "Mordor"+ increment;
		String department = "Depart"+ increment;
		String division = "Dev"+ increment;
		String license = "Salesforce";
		String profile = "Standard User";
		

		Date date = new Date();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
		String formattedDate = sdf.format(date);

		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getUserContextMenu());
		Thread.sleep(2000);
		SeleniumFireFoxDriver
				.Click(driver, RepositorySalesForce.getSetUpItem());
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getManageUsers());
		Thread.sleep(2000);
		SeleniumFireFoxDriver
				.Click(driver, RepositorySalesForce.getUsersItem());
		SalesForceModule.createStandartUser(this.email, email, firstName,
				lastName, alias, nickname, title, company, department,
				division, license, profile, driver, phone);
		Thread.sleep(timeout);

		// Connect to db and check fields
		String accountID = SalesForceModule.getId(connectionString, userDb,
				passwordDb);
		CheckDataBase(accountID, connectionString, userDb, passwordDb, email,
				email, firstName, lastName, alias, nickname, title, company,
				department, division, formattedDate);
	}

	@Test
	@Parameters({ "url", "password", "connectionString", "userDb",
			"passwordDb" })
	public void Test02_UpdateStandartUser(String url, String password, String connectionString, String userDb,
			String passwordDb) throws InterruptedException {
		String firstNameNew = "baginsNew" + increment;
		String lastNameNew = "sheerNew" + increment;
		String aliasNew = "a" + increment;
		String nicknameNew = "golUpdated" + increment;
		String titleNew = "yeyNe"+ increment;
		String companyNew = "MordorNe" + increment;
		String departmentNew = "DepartNe" + increment;
		String divisionNew = "Dev4New" + increment;;
		String license = "Salesforce";
		String profile = "Standard User";
		String phoneNew = "+7921777" + increment;

		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
		WebDriver driver = SalesForceModule.logInToCigna(url, this.login, password);
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getUserContextMenu());
		Thread.sleep(2000);
		SeleniumFireFoxDriver
				.Click(driver, RepositorySalesForce.getSetUpItem());
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getManageUsers());
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Click(driver, RepositorySalesForce.getUsersItem());

		// Open account for edit.
		driver = SalesForceModule.openStandartUserForEdit(this.lastName + ","
				+ this.firstName, driver);
		driver = SalesForceModule.fillInStandartUserForm(email, email,
				firstNameNew, lastNameNew, aliasNew, nicknameNew, titleNew,
				companyNew, departmentNew, divisionNew, profile, license,
				driver, phoneNew);
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
				this.email, email, firstNameNew, lastNameNew, aliasNew,
				nicknameNew, titleNew, companyNew, departmentNew, divisionNew,
				formattedDate);
		// Add checking DB old after fix bug		
		CheckDataBaseOldTabel(accountID, connectionString, userDb, passwordDb, firstName, lastName, phone);
		
		
	}

	// //////////////////////////
	// ///private methods////////
	// /////////////////////////
	private void CheckDataBase(String accountID, String connectionString,
			String userDb, String passwordDb, String email, String username,
			String firstName, String lastName, String alias, String nickname,
			String title, String company, String department, String division,
			String formattedDate) {

		Assert.assertEquals(
				email.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Email",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				username.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Username",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(
				firstName.replace(" ", ""),
				SalesForceModule.getFieldValue(accountID, "Firstname",
						connectionString, userDb, passwordDb).replace(" ", ""));
		Assert.assertEquals(lastName.replace(" ", ""),SalesForceModule.getFieldValue(accountID, "Lastname",	connectionString, userDb, passwordDb).replace(" ", ""));
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
			String firstName, String lastName, String phone) {
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
	}

	 @AfterTest
	 public void closeAllSession() throws Exception {
	 SalesForceModule.closeFireFox();
	 }

}
