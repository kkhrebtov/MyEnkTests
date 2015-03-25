package automationtests.enkata.com;

import java.awt.AWTException;
import java.awt.Robot;
import java.awt.event.KeyEvent;
import java.io.IOException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.Sleeper;
import org.testng.annotations.Parameters;
import org.testng.annotations.Test;
import selenium.enkata.com.RepositorySalesForce;
import selenium.enkata.com.SalesForceModule;
import selenium.enkata.com.SeleniumFireFoxDriver;
import java.io.File;
import java.io.FileWriter;
import java.util.ArrayList;
import junit.framework.Assert;
import org.jdom.Document;
import org.jdom.Element;
import org.jdom.JDOMException;
import org.jdom.input.SAXBuilder;
import org.jdom.output.Format;
import org.jdom.output.XMLOutputter;

public class HistoricalLoad {

	private static String login = "vmurashko@e2e.enkata.com";
	private static Process runtimeProcess;
	private static String installationLink = "https://login.salesforce.com/packaging/installPackage.apexp?p0=04tF0000000FyYb";
	private static String connectionQueue = "QueueName=historical;Endpoint=sb://sfdc-dev.servicebus.windows.net/;SharedAccessKeyName=apex;SharedAccessKey=MHbR9U89CFSw3OcmZpaFM6ENhxXGSzk8iTN1UEVntjI=";
	private static String encriptionKey = "";
	private static String storageConnection = "AccountName=vmurashkoe2e;AccessKeyValue=Xoq7dUFe1bNNduRLfxFLTVn7dgqw4psQI7CEePLOFH4PsU7NclI3pOAz1hrLdl0liGwA/YDNkK6wcN2ekkPvvQ==;TenantUUID=ee4258c5-5609-484e-9049-fffd5217eb0c";

//	@Test
//	@Parameters({ "url", "password", "connectionString", "userDb", "passwordDb" })
//	public static void Test01_StopEnkataLoaderAndInstallStreamer(String url,
//			String password, String connectionString, String userDb,
//			String passwordDb) throws IOException, InterruptedException,
//			AWTException {
//		// 1.Stop loader.
//		runtimeProcess = manageService(runtimeProcess, "EnkataLoader", "stop");
//		SalesForceModule.clearDB(connectionString, userDb, passwordDb);
//
//		// 2.UnInstall streamer.
//		WebDriver driver = SalesForceModule.logInToCigna(url, login, password);
//		Thread.sleep(12000);
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getUserContextMenu());
//		Thread.sleep(5000);
//		SeleniumFireFoxDriver
//				.Click(driver, RepositorySalesForce.getSetUpItem());
//		Thread.sleep(5000);
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getInstallPackageLink());
//
//		if (SeleniumFireFoxDriver.GetObject(driver,
//				RepositorySalesForce.getUnInstallLink()) != null) {
//			SeleniumFireFoxDriver.Click(driver,
//					RepositorySalesForce.getUnInstallLink());
//			SeleniumFireFoxDriver.Click(driver,
//					RepositorySalesForce.getCheckBoxUninstal());
//			Thread.sleep(5000);
//			SeleniumFireFoxDriver.Click(driver,
//					RepositorySalesForce.getUninstallButton());
//			Thread.sleep(60000);
//		}
//
//		// 3.Install streamer
//		driver.get(installationLink);
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getContinueButton());
//		Thread.sleep(10000);
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getCheckBoxInstall());
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getContinue2Button());
//		Thread.sleep(2000);
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getNext1Button());
//		Thread.sleep(2000);
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getRadioButtonGruntAll());
//		Thread.sleep(2000);
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getNext2Button());
//		Thread.sleep(2000);
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getInstallButton());
//		Thread.sleep(2000);
//
//		// 4.Edit remote site settings.
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getSecurityFont());
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getSecurityRemoteProxyFont());
//		SalesForceModule.clickEditRemoteSiteByLink(driver,
//				"https://namespace.servicebus.windows.net");
//		SeleniumFireFoxDriver.Type(driver,
//				RepositorySalesForce.getRemoteSiteURLField(),
//				"https://sfdc-dev.servicebus.windows.net");
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getSaveButtonRemoteSite());
//		Thread.sleep(5000);
//		SalesForceModule.clickEditRemoteSiteByLink(driver,
//				"https://account.blob.core.windows.net");
//		SeleniumFireFoxDriver.Type(driver,
//				RepositorySalesForce.getRemoteSiteURLField(),
//				"https://vmurashkoe2e.blob.core.windows.net");
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getSaveButtonRemoteSite());
//		Thread.sleep(5000);
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getInstallPackageLink());
//
//		// 5.Configure streamer
//		driver.getWindowHandle();
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getConfigureLink());
//		for (String winHandle : driver.getWindowHandles()) {
//			driver.switchTo().window(winHandle);
//		}
//		SeleniumFireFoxDriver
//				.Type(driver, RepositorySalesForce.getQueueConnectionField(),
//						connectionQueue);
//		SeleniumFireFoxDriver.Type(driver,
//				RepositorySalesForce.getStorageConnectionField(),
//				storageConnection);
//
//		SeleniumFireFoxDriver.Type(driver,
//				RepositorySalesForce.getHistoricalPeriodStartDate(),
//				"01/01/2014");
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getGenerateKeyButton());
//		Thread.sleep(5000);
//		encriptionKey = SeleniumFireFoxDriver.GetProperty(driver,
//				RepositorySalesForce.getPrivateEncryptonKey(), "value");
//		System.out.println("Key:" + encriptionKey);
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getTestandSave());
//		Thread.sleep(15000);
//
//		// 6.Select users
//		SalesForceModule.selecUserByFirstName(driver, "Vyacheslav");
//		SalesForceModule.selecUserByFirstName(driver, "Slava");
//		Thread.sleep(5000);
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getSaveSelectionButton());
//		Thread.sleep(15000);
//		SeleniumFireFoxDriver.Click(driver,
//				RepositorySalesForce.getStartDataCollection());
//		Thread.sleep(5000);
//		Robot pressEnter = new Robot();
//		pressEnter.keyPress(KeyEvent.VK_ENTER);
//		pressEnter.keyPress(KeyEvent.VK_TAB);
//		Thread.sleep(1000);
//		pressEnter.keyPress(KeyEvent.VK_ENTER);
//		pressEnter.keyPress(KeyEvent.VK_TAB);
//		Thread.sleep(1000);
//		pressEnter.keyPress(KeyEvent.VK_ENTER);
//
//		// 7.Edit xml config for loader and start service.
//		updateConfigForLoader("C:\\EnkataLoader\\");
//		manageService(runtimeProcess, "EnkataLoader", "stop");
//		Thread.sleep(5000);
//	}

	@Test
	@Parameters({ "url", "password", "connectionString", "userDb", "passwordDb" })
	public static void Test02_CheckDataBaseHistoricalLoadLogs(String url,
			String password, String connectionString, String userDb,
			String passwordDb) throws IOException, InterruptedException {

		// Wait until load is finished.
		waitHistoricalLoad(connectionString, userDb, passwordDb);

		ArrayList<String> logList = new ArrayList<String>();
		logList.add("Configuration is correct and successfully applied");
		logList.add("Started Historical Contacts transfer");
		logList.add("Finished Historical Contacts transfer");
		logList.add("Started Historical Accounts transfer");
		logList.add("Finished Historical Accounts transfer");
		logList.add("Started Historical Opportunities transfer");
		logList.add("Finished Historical Opportunities transfer");
		logList.add("Started Historical OpportunityContactRole transfer");
		logList.add("Finished Historical OpportunityContactRole transfer");
		logList.add("Started Historical Opportunity History transfer");
		logList.add("Finished Historical Opportunity History transfer");
		logList.add("Started Historical Opportunity Stage transfer");
		logList.add("Finished Historical Opportunity Stage transfer");
		logList.add("Started Historical Users transfer");
		logList.add("Finished Historical Users transfer");
		logList.add("Started Historical Leads transfer");
		logList.add("Finished Historical Leads transfer");
		logList.add("Started Historical Leads2 transfer");
		logList.add("Finished Historical Leads transfer");
		logList.add("All historical data transferred successfully");

		ArrayList<String> logList2 = new ArrayList<String>();
		logList2.add("Log Event queue size");
		logList2.add("Scheduled Person Configuration job to run at");
		logList2.add("Scheduled Historical Data Loader job to run at");
		logList2.add("Scheduled OpportunityContactRole job to run at");
		logList2.add("Scheduled Failed Chunk resend job");
		logList2.add("Scheduled Event Log send job");

		// Check Logs table
		checkLogsForHistoricalLoad(connectionString, userDb, passwordDb,
				logList);
		checkLogsForHistoricalLoadLike(connectionString, userDb, passwordDb,
				logList2);
	}

	@Test
	@Parameters({ "url", "password", "connectionString", "userDb", "passwordDb" })
	public static void Test03_CheckDataBaseHistoricalLoadAccount(String url,
			String password, String connectionString, String userDb,
			String passwordDb) throws IOException, InterruptedException {

		// Wait until load is finished.
		waitHistoricalLoad(connectionString, userDb, passwordDb);

		// Check Account
		Assert.assertEquals(
				4,
				SalesForceModule.getCountObject("Account", connectionString,
						userDb, passwordDb).size());
		ArrayList<String> objetcIdList = SalesForceModule.getObjectIDList(
				"Account", connectionString, userDb, passwordDb);
		for (String object_id : objetcIdList) {
			checkAccountFields(object_id, connectionString, userDb, passwordDb);
		}
	}

	@Test
	@Parameters({ "url", "password", "connectionString", "userDb", "passwordDb" })
	public static void Test04_CheckDataBaseHistoricalLoadLead(String url,
			String password, String connectionString, String userDb,
			String passwordDb) throws IOException, InterruptedException {

		// Wait until load is finished.
		waitHistoricalLoad(connectionString, userDb, passwordDb);

		// Check Leads
		Assert.assertEquals(
				4,
				SalesForceModule.getCountObject("Lead", connectionString,
						userDb, passwordDb).size());
		ArrayList<String> objetcIdList = SalesForceModule.getObjectIDList(
				"Lead", connectionString, userDb, passwordDb);
		for (String object_id : objetcIdList) {
			checkLeadFields(object_id, connectionString, userDb, passwordDb);
		}

	}

	@Test
	@Parameters({ "url", "password", "connectionString", "userDb", "passwordDb" })
	public static void Test05_CheckDataBaseHistoricalLoadContacts(String url,
			String password, String connectionString, String userDb,
			String passwordDb) throws IOException, InterruptedException {

		// Wait until load is finished.
		waitHistoricalLoad(connectionString, userDb, passwordDb);

		// Check Contacts
		Assert.assertEquals(
				4,
				SalesForceModule.getCountObject("Contact", connectionString,
						userDb, passwordDb).size());
		ArrayList<String> objetcIdList = SalesForceModule.getObjectIDList(
				"Contact", connectionString, userDb, passwordDb);
		for (String object_id : objetcIdList) {
			checkContactFields(object_id, connectionString, userDb, passwordDb);
		}

	}

	@Test
	@Parameters({ "url", "password", "connectionString", "userDb", "passwordDb" })
	public static void Test06_CheckDataBaseHistoricalLoadOpportunity(
			String url, String password, String connectionString,
			String userDb, String passwordDb) throws IOException,
			InterruptedException {

		// Wait until load is finished.
		waitHistoricalLoad(connectionString, userDb, passwordDb);

		// Check Opportunity
		Assert.assertEquals(
				4,
				SalesForceModule.getCountObject("Opportunity",
						connectionString, userDb, passwordDb).size());
		ArrayList<String> objetcIdList = SalesForceModule.getObjectIDList(
				"Opportunity", connectionString, userDb, passwordDb);
		for (String object_id : objetcIdList) {
			checkOpportunityFields(object_id, connectionString, userDb,
					passwordDb);
		}
	}

	@Test
	@Parameters({ "url", "password", "connectionString", "userDb", "passwordDb" })
	public static void Test07_CheckDataBaseHistoricalLoadUser(String url,
			String password, String connectionString, String userDb,
			String passwordDb) throws IOException, InterruptedException {

		// Wait until load is finished.
		waitHistoricalLoad(connectionString, userDb, passwordDb);

		// Check Users
		Assert.assertEquals(
				2,
				SalesForceModule.getCountObject("User", connectionString,
						userDb, passwordDb).size());
		ArrayList<String> objetcIdList = SalesForceModule.getObjectIDList(
				"User", connectionString, userDb, passwordDb);
		for (String object_id : objetcIdList) {
			checkUserFields(object_id, connectionString, userDb, passwordDb);
		}
	}

	@Test
	@Parameters({ "url", "password", "connectionString", "userDb", "passwordDb" })
	public static void Test08_CheckDataBaseHistoricalOpportunityHistory(
			String url, String password, String connectionString,
			String userDb, String passwordDb) throws IOException,
			InterruptedException {

		// Wait until load is finished.
		waitHistoricalLoad(connectionString, userDb, passwordDb);

		// Check Users
		Assert.assertEquals(
				8,
				SalesForceModule.getCountObject("OpportunityHistory",
						connectionString, userDb, passwordDb).size());
		ArrayList<String> objetcIdList = SalesForceModule.getObjectIDList(
				"OpportunityHistory", connectionString, userDb, passwordDb);
		for (String object_id : objetcIdList) {
			checkOpportunityHistoryFields(object_id, connectionString, userDb,
					passwordDb);
		}
	}

	@Test
	@Parameters({ "url", "password", "connectionString", "userDb", "passwordDb" })
	public static void Test09_CheckDataBaseHistoricalLoadOpportunityStage(
			String url, String password, String connectionString,
			String userDb, String passwordDb) throws IOException,
			InterruptedException {

		// Wait until load is finished.
		waitHistoricalLoad(connectionString, userDb, passwordDb);

		// Check Users
		Assert.assertEquals(
				10,
				SalesForceModule.getCountObject("OpportunityStage",
						connectionString, userDb, passwordDb).size());
		ArrayList<String> objetcIdList = SalesForceModule.getObjectIDList(
				"OpportunityStage", connectionString, userDb, passwordDb);
		for (String object_id : objetcIdList) {
			checkOpportunityStageFields(object_id, connectionString, userDb,
					passwordDb);
		}
	}

	// //////////////////////
	// //Private methods/////
	// /////////////////////
	private static Process manageService(Process runtimeProcess,
			String serviceName, String command) throws IOException,
			InterruptedException {

		String executeCmd = "cmd /c net " + command + " \"" + serviceName
				+ "\"";
		runtimeProcess = Runtime.getRuntime().exec(executeCmd);
		int processComplete = runtimeProcess.waitFor();

		System.out.println("processComplete: " + processComplete);

		if (processComplete == 1) {// if values equal 1 process failed
			System.out.println("Service failed");
		}

		else if (processComplete == 0) {
			System.out.println("Service Success");
		}
		return runtimeProcess;

	}

	private static void updateConfigForLoader(String pathToFile) {
		try {

			SAXBuilder builder = new SAXBuilder();
			File xmlFile = new File(pathToFile + "EnkataLoader.exe.config");
			Document doc = (Document) builder.build(xmlFile);
			Element rootNode = doc.getRootElement();

			// update staff SalesForceEncryptionKey attribute
			Element staff = rootNode.getChild("Queues");
			Element queList = staff.getChild("QueueList");
			Element connectionString = queList.getChild("Queue");
			connectionString.getAttribute("SalesForceEncryptionKey").setValue(
					encriptionKey);
			XMLOutputter xmlOutput = new XMLOutputter();

			// display nice nice
			xmlOutput.setFormat(Format.getPrettyFormat());
			xmlOutput.output(doc, new FileWriter(pathToFile
					+ "EnkataLoader.exe.config"));
			System.out.println("File updated!");
		} catch (IOException io) {
			io.printStackTrace();
		} catch (JDOMException e) {
			e.printStackTrace();
		}
	}

	private static void checkLogsForHistoricalLoad(String connectionString,
			String userDb, String passwordDb, ArrayList<String> logList) {

		for (String string : logList) {
			Assert.assertTrue(SalesForceModule.getFieldValueFromLogsTable(
					string, connectionString, userDb, passwordDb).equals(
					string.replace(" ", "")));
		}
	}

	private static void waitHistoricalLoad(String connectionString,
			String userDb, String passwordDb) throws InterruptedException {
		for (int i = 0; i < 30; i++) {

			if (SalesForceModule.getFieldValueFromLogsTable(
					"All historical data transferred successfully",
					connectionString, userDb, passwordDb) != null) {
				if (SalesForceModule.getFieldValueFromLogsTable(
						"All historical data transferred successfully",
						connectionString, userDb, passwordDb).equals(
						"All historical data transferred successfully".replace(
								" ", "")))
					break;
			}
			Thread.sleep(1000 * 60);
		}

	}

	private static void checkLogsForHistoricalLoadLike(String connectionString,
			String userDb, String passwordDb, ArrayList<String> logList) {

		for (String string : logList) {
			Assert.assertTrue("Value:\"" + string + "\"absent.",SalesForceModule.getFieldValueFromLogsTableLike(
					string, connectionString, userDb, passwordDb));
		}

	}

	private static void checkAccountFields(String object_id,
			String connectionString, String userDb, String passwordDb) {

		Assert.assertEquals(
				1,
				SalesForceModule.getFieldValueByObjectId(object_id, "Id",
						connectionString, userDb, passwordDb).size());
		Assert.assertEquals(
				1,
				SalesForceModule.getFieldValueByObjectId(object_id,
						"CreatedDate", connectionString, userDb, passwordDb)
						.size());

		Assert.assertEquals(
				1,
				SalesForceModule.getFieldValueByObjectId(object_id,
						"Description", connectionString, userDb, passwordDb)
						.size());
		Assert.assertEquals(
				1,
				SalesForceModule.getFieldValueByObjectId(object_id,
						"LastModifiedDate", connectionString, userDb,
						passwordDb).size());
		Assert.assertEquals(
				1,
				SalesForceModule.getFieldValueByObjectId(object_id, "Name",
						connectionString, userDb, passwordDb).size());
		Assert.assertEquals(
				1,
				SalesForceModule.getFieldValueByObjectId(object_id,
						"LastModifiedById", connectionString, userDb,
						passwordDb).size());
		Assert.assertEquals(
				1,
				SalesForceModule.getFieldValueByObjectId(object_id,
						"CreatedById", connectionString, userDb, passwordDb)
						.size());
		Assert.assertEquals(
				1,
				SalesForceModule.getFieldValueByObjectId(object_id,
						"AccountNumber", connectionString, userDb, passwordDb)
						.size());
		Assert.assertEquals(
				1,
				SalesForceModule.getFieldValueByObjectId(object_id,
						"AccountSource", connectionString, userDb, passwordDb)
						.size());

	}

	private static void checkLeadFields(String object_id,
			String connectionString, String userDb, String passwordDb) {
		String[] leadList = { "Id", "OwnerId", "CreatedDate", "CreatedById",
				"LastModifiedDate", "LastModifiedById", "Company", "FirstName",
				"LastName", "Name", "Email", "Phone", "Status", "Title",
				"IsConverted", "ConvertedAccountId", "ConvertedContactId",
				"ConvertedOpportunityId", "ConvertedDate" };
		for (String string : leadList) {
			Assert.assertEquals(
					1,
					SalesForceModule.getFieldValueByObjectId(object_id, string,
							connectionString, userDb, passwordDb).size());
		}
	}

	private static void checkContactFields(String object_id,
			String connectionString, String userDb, String passwordDb) {
		String[] contactList = { "Id", "AccountId", "CreatedDate", "Email",
				"FirstName", "LastName", "Name", "Phone", "Title",
				"CreatedById", "LastModifiedDate", "LastModifiedById" };
		for (String string : contactList) {
			Assert.assertEquals(
					1,
					SalesForceModule.getFieldValueByObjectId(object_id, string,
							connectionString, userDb, passwordDb).size());
		}

	}

	private static void checkOpportunityFields(String object_id,
			String connectionString, String userDb, String passwordDb) {
		String[] opportunityList = { "Id", "AccountId", "OwnerId",
				"CreatedDate", "ExpectedRevenue", "CreatedById",
				"LastModifiedDate", "LastModifiedById", "CloseDate",
				"StageName", "Name", "isClosed", "IsWon", "Amount",
				"LeadSource" };
		for (String string : opportunityList) {
			Assert.assertTrue("Field: \"" + string + "" + "\" not found.",
					1 == SalesForceModule.getFieldValueByObjectId(object_id, string,
							connectionString, userDb, passwordDb).size());
		}

	}

	private static void checkUserFields(String object_id,
			String connectionString, String userDb, String passwordDb) {
		String[] userList = { "Id", "CreatedDate", "Email", "Username",
				"LastModifiedDate", "LastModifiedById", "CreatedById" };
		for (String string : userList) {
			Assert.assertEquals(
					1,
					SalesForceModule.getFieldValueByObjectId(object_id, string,
							connectionString, userDb, passwordDb).size());
		}

	}

	private static void checkOpportunityHistoryFields(String object_id,
			String connectionString, String userDb, String passwordDb) {
		String[] opportunityHistoryList = { "Id", "StageName", "Probability",
				"OpportunityId", "IsDeleted", "ForecastCategory",
				"ExpectedRevenue", "CreatedDate", "CreatedById", "CloseDate",
				"Amount" };
		for (String string : opportunityHistoryList) {
			Assert.assertEquals(
					1,
					SalesForceModule.getFieldValueByObjectId(object_id, string,
							connectionString, userDb, passwordDb).size());
		}
	}

	private static void checkOpportunityStageFields(String object_id,
			String connectionString, String userDb, String passwordDb) {
		String[] opportunityStageList = { "Id", "SortOrder", "MasterLabel",
				"LastModifiedDate", "LastModifiedById", "IsWon", "IsClosed",
				"IsActive", "ForecastCategoryName", "ForecastCategory",
				"DefaultProbability", "CreatedDate", "CreatedById",
				"Description" };
		for (String string : opportunityStageList) {
			Assert.assertTrue("Field: \"" + string + "\" not found in Db.",1 == SalesForceModule.getFieldValueByObjectId(object_id, string,
							connectionString, userDb, passwordDb).size());
		}

	}
}
