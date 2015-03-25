package selenium.enkata.com;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;

public class SalesForceModule {

	public static WebDriver logInToCigna(String url, String email,
			String password) throws InterruptedException {
		// Login to cigna site

		WebDriver driver = SeleniumFireFoxDriver.openUrl(url);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getLoginField(), email);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getPasswordField(), password);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getLogInButton());
		driver.manage().window().maximize();

		return driver;
	}

	public static WebDriver logInToCigna_withChrome(String url, String email,
										 String password) throws InterruptedException {
		// Login to cigna site

		System.setProperty("webdriver.chrome.driver", "D:\\Git\\sfdc-streamer-tests\\salesforce\\resources\\chromedriver.exe");



		WebDriver driver = SeleniumFireFoxDriver.openUrl(url, "Chrome");
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getLoginField(), email);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getPasswordField(), password);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getLogInButton());
		driver.manage().window().maximize();

		return driver;
	}

	public static void clearDB(String connectionString, String userDb,
			String passwordDb) {
		try {
			Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
			Connection conn = DriverManager.getConnection(connectionString,
					userDb, passwordDb);
			System.out.println("connected");
			Statement statement = conn.createStatement();
			String queryString1 = "DELETE FROM [ADA_RAW].[dbo].[AAL_SFDC_SNAPSHOT]";
			String queryString2 = "DELETE FROM [ADA_RAW].[dbo].[AAL_SFDC_SNAPSHOT_DATA]";
			String queryString3 = "DELETE FROM [ADA_RAW].[dbo].[AAL_SFDC_SNAPSHOT_DATA_OLD]";
			statement.execute(queryString1);
			statement.execute(queryString2);
			statement.execute(queryString3);

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void selecUserByFirstName(WebDriver driver, String name) {
		for (int i = 1; i < 6; i++) {

			try {
				String emailCurrent = SeleniumFireFoxDriver
						.GetTextContent(
								driver,
								"/html/body/div/div[2]/table/tbody/tr/td[2]/form/div[2]/div/div[2]/table/tbody/tr["
										+ i + "]/td[2]/span");
				if (emailCurrent.replace(" ", "").equals(name.replace(" ", ""))) {
					SeleniumFireFoxDriver
							.Click(driver,
									"/html/body/div/div[2]/table/tbody/tr/td[2]/form/div[2]/div/div[2]/table/tbody/tr["
											+ i + "]/td/input");
					break;
				}
			} catch (Exception e) {
			}
		}

	}
	
	
	public static void clickEditRemoteSiteByLink(WebDriver driver, String name) {
		for (int i = 1; i < 6; i++) {

			try {
				String emailCurrent = SeleniumFireFoxDriver
						.GetTextContent(
								driver,
								"/html/body/div/div[2]/table/tbody/tr/td[2]/div[5]/div/div[2]/table/tbody/tr[" + i + "]/td[4]/a");
				if (emailCurrent.replace(" ", "").equals(name.replace(" ", ""))) {
					SeleniumFireFoxDriver
							.Click(driver,
									"/html/body/div/div[2]/table/tbody/tr/td[2]/div[5]/div/div[2]/table/tbody/tr[" + i + "]/td/a");
					break;
				}
			} catch (Exception e) {
			}
		}

	}

	public static WebDriver selectViewAccounts(String value, WebDriver driver)
			throws InterruptedException {

		SeleniumFireFoxDriver.Click(driver, "fcf");
		Thread.sleep(2000);
		for (int i = 1; i < 7; i++) {
			if (SeleniumFireFoxDriver
					.GetTextContent(driver,
							"//*[@id=\"fcf\"]/option[" + i + "]")
					.replace(" ", "").equals(value.replace(" ", ""))) {

				SeleniumFireFoxDriver.SubmitElement(driver,
						"//*[@id=\"fcf\"]/option[" + i + "]");
				return driver;
			}
		}
		return driver;
	}

	public static WebDriver createAccount(String accountName, String fax,
			String phone, String webSite, int employees, int annualRevenue,
			String description, WebDriver driver) throws InterruptedException {
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getNewAccountsButton());
		Thread.sleep(2000);
		driver = fillInAccountForm(accountName, fax, phone, webSite, employees,
				annualRevenue, description, driver);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getSaveButton());
		Thread.sleep(5000);

		return driver;
	}

	public static WebDriver fillInAccountForm(String accountName, String fax,
			String phone, String webSite, int employees, int annualRevenue,
			String description, WebDriver driver) throws InterruptedException {
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getAccountNameField(), accountName);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getPhoneField(), phone);
		SeleniumFireFoxDriver.Type(driver, RepositorySalesForce.getFaxField(),
				fax);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getWebSiteField(), webSite);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getEmployeesField(),
				Integer.toString(employees));
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getAnnualRevenueField(),
				Integer.toString(annualRevenue));
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getDescription(), description);
		return driver;
	}

	public static WebDriver createContact(String email, String firstName,
			String lastName, String name, String phone, String title,
			String description, WebDriver driver) throws InterruptedException {
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getNewAccountsButton());
		Thread.sleep(2000);
		driver = fillInContactForm(email, firstName, lastName, name, phone,
				title, description, driver);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getSaveButtonTop());
		Thread.sleep(5000);

		return driver;
	}

	public static ArrayList<String> getSeveralFields(String nameField,
			String connectionString, String userDb, String passwordDb) {
		ArrayList<String> valuesFromDb = new ArrayList<String>();
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

		return valuesFromDb;
	}
	
	
	public static ArrayList<String> getCountObject(String objectType,
			String connectionString, String userDb, String passwordDb) {
		ArrayList<String> valuesFromDb = new ArrayList<String>();
		try {
			Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
			Connection conn = DriverManager.getConnection(connectionString,
					userDb, passwordDb);
			Statement statement = conn.createStatement();
			String queryString = "SELECT [object_type] FROM [ADA_RAW].[dbo].[AAL_SFDC_SNAPSHOT] WHERE object_type='"
					+ objectType + "'";
			ResultSet rs = statement.executeQuery(queryString);
			while (rs.next()) {
				String value = rs.getString(1);
				valuesFromDb.add(value);
			}
			conn.close();
		} catch (Exception e) {
			e.printStackTrace();
		}

		return valuesFromDb;
	}
	
	public static ArrayList<String> getObjectIDList(String objectType,
			String connectionString, String userDb, String passwordDb) {
		ArrayList<String> valuesFromDb = new ArrayList<String>();
		try {
			Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
			Connection conn = DriverManager.getConnection(connectionString,
					userDb, passwordDb);
			// System.out.println("connected");
			Statement statement = conn.createStatement();
			String queryString = "SELECT [object_id] FROM [ADA_RAW].[dbo].[AAL_SFDC_SNAPSHOT] WHERE object_type='"
					+ objectType + "'";
			ResultSet rs = statement.executeQuery(queryString);
			while (rs.next()) {
				String value = rs.getString(1);
				valuesFromDb.add(value);
			}
			conn.close();
		} catch (Exception e) {
			e.printStackTrace();
		}

		return valuesFromDb;
	}

	public static ArrayList<String> getSeveralFieldsFromOLDTable(
			String nameField, String connectionString, String userDb,
			String passwordDb) {
		ArrayList<String> valuesFromDb = new ArrayList<String>();
		try {
			Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
			Connection conn = DriverManager.getConnection(connectionString,
					userDb, passwordDb);
			// System.out.println("connected");
			Statement statement = conn.createStatement();
			String queryString = "SELECT [attr_old_value] FROM [ADA_RAW].[dbo].[AAL_SFDC_SNAPSHOT_DATA_OLD] WHERE attr_name='"
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

		return valuesFromDb;
	}

	public static WebDriver fillInContactForm(String email, String firstName,
			String lastName, String name, String phone, String title,
			String description, WebDriver driver) throws InterruptedException {
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getContactEmail(), email);
		SeleniumFireFoxDriver.Type(driver, RepositorySalesForce.getFirstName(),
				firstName);
		SeleniumFireFoxDriver.Type(driver, RepositorySalesForce.getLastName(),
				lastName);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getAccountNameContact(), name);

		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getPhoneContact(), phone);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getTitleContact(), title);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getDescriptionContact(), description);

		return driver;
	}

	public static String getFieldValue(String id, String nameField,
			String connectionString, String userDb, String passwordDb) {
		try {
			Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
			Connection conn = DriverManager.getConnection(connectionString,
					userDb, passwordDb);
			// System.out.println("connected");
			Statement statement = conn.createStatement();
			String queryString = "SELECT [attr_value] FROM [ADA_RAW].[dbo].[AAL_SFDC_SNAPSHOT_DATA] WHERE [snapshot_id] = '"
					+ id + "' AND [attr_name] = '" + nameField + "'";
			ResultSet rs = statement.executeQuery(queryString);
			while (rs.next()) {
				String value = rs.getString(1);
				// System.out.println(id);
				conn.close();
				return value;
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}
	
	public static ArrayList<String> getFieldValueByObjectId(String object_id, String attr_name,
			String connectionString, String userDb, String passwordDb) {
		ArrayList<String> valuesFromDb = new ArrayList<String>();
		try {
			Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
			Connection conn = DriverManager.getConnection(connectionString,
					userDb, passwordDb);
			// System.out.println("connected");
			Statement statement = conn.createStatement();
			String queryString = "SELECT [attr_value] FROM [ADA_RAW].[dbo].[AAL_SFDC_SNAPSHOT_DATA] WHERE object_id='"
					+ object_id + "' AND attr_name='" + attr_name + "'";
			ResultSet rs = statement.executeQuery(queryString);
			while (rs.next()) {
				String value = rs.getString(1);
				valuesFromDb.add(value);
			}
			conn.close();
		} catch (Exception e) {
			e.printStackTrace();
		}

		return valuesFromDb;
	}
	
	public static String getFieldValueFromLogsTable(String message, 
			String connectionString, String userDb, String passwordDb) {

		try {
			Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
			Connection conn = DriverManager.getConnection(connectionString,
					userDb, passwordDb);
			// System.out.println("connected");
			Statement statement = conn.createStatement();
			String queryString = "SELECT [Message] FROM [LogRecords].[dbo].[LogRecords] WHERE [Message] = '"
					+ message + "'";
			ResultSet rs = statement.executeQuery(queryString);
			while (rs.next()) {
				String value = rs.getString(1);
				// System.out.println(id);
				conn.close();
				return value.replace(" ", "");
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

	public static boolean getFieldValueFromLogsTableLike(String message, 
			String connectionString, String userDb, String passwordDb) {

		try {
			Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
			Connection conn = DriverManager.getConnection(connectionString,
					userDb, passwordDb);
			// System.out.println("connected");
			Statement statement = conn.createStatement();
			String queryString = "SELECT [Message] FROM [LogRecords].[dbo].[LogRecords] WHERE [Message] LIKE '%"
					+ message + "%'";
			ResultSet rs = statement.executeQuery(queryString);
			while (rs.next()) {
				String value = rs.getString(1);
				// System.out.println(id);
				conn.close();
				return true;
			}

		} catch (Exception e) {
			e.printStackTrace();
		}		
		return false;
	}

	
	public static String getFieldValueOldTable(String id, String nameField,
			String connectionString, String userDb, String passwordDb) {

		try {
			Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
			Connection conn = DriverManager.getConnection(connectionString,
					userDb, passwordDb);
			// System.out.println("connected");
			Statement statement = conn.createStatement();
			String queryString = "SELECT [attr_old_value] FROM [ADA_RAW].[dbo].[AAL_SFDC_SNAPSHOT_DATA_OLD] WHERE [snapshot_id] = '"
					+ id + "' AND [attr_name] = '" + nameField + "'";
			ResultSet rs = statement.executeQuery(queryString);
			while (rs.next()) {
				String value = rs.getString(1);
				// System.out.println(id);
				conn.close();
				return value;
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

	public static String getId(String connectionString, String userDb,
			String passwordDb) {
		try {
			Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
			Connection conn = DriverManager.getConnection(connectionString,
					userDb, passwordDb);
			System.out.println("connected");
			Statement statement = conn.createStatement();
			String queryString = "SELECT [snapshot_id] FROM [ADA_RAW].[dbo].[AAL_SFDC_SNAPSHOT]";
			ResultSet rs = statement.executeQuery(queryString);
			while (rs.next()) {
				String id = rs.getString(1);
				System.out.println(id);
				conn.close();
				return id;
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

	public static WebDriver openAccountForEdit(String accountName,
			WebDriver driver) throws InterruptedException {
		for (int i = 0; i < 20; i++) {
			if (i == 0) {
				try {
					String emailCurrent = SeleniumFireFoxDriver
							.GetTextContent(
									driver,
									"//html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div/table/tbody/tr/td[4]/div/a/span");
					if (emailCurrent.replace(" ", "").equals(
							accountName.replace(" ", ""))) {
						SeleniumFireFoxDriver
								.Click(driver,
										"/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div/table/tbody/tr/td[3]/div/a/span");
						break;
					}
				} catch (Exception e) {
				}
			} else {
				try {
					String nameEmail = SeleniumFireFoxDriver
							.GetTextContent(
									driver,
									"/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div["
											+ i
											+ "]/table/tbody/tr/td[4]/div/a/span");
					if (nameEmail.replace(" ", "").equals(
							accountName.replace(" ", ""))) {
						SeleniumFireFoxDriver
								.Click(driver,
										"/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div["
												+ i
												+ "]/table/tbody/tr/td[3]/div/a/span");
						break;
					}
				} catch (Exception e) {
				}
			}
		}

		return driver;
	}

	public static WebDriver openAccount(String accountName, WebDriver driver)
			throws InterruptedException {
		for (int i = 0; i < 20; i++) {
			if (i == 0) {
				try {
					String emailCurrent = SeleniumFireFoxDriver
							.GetTextContent(
									driver,
									"//html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div/table/tbody/tr/td[4]/div/a/span");
					if (emailCurrent.replace(" ", "").equals(
							accountName.replace(" ", ""))) {
						SeleniumFireFoxDriver
								.Click(driver,
										"//html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div/table/tbody/tr/td[4]/div/a/span");
						break;
					}
				} catch (Exception e) {
				}
			} else {
				try {
					String nameEmail = SeleniumFireFoxDriver
							.GetTextContent(
									driver,
									"/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div["
											+ i
											+ "]/table/tbody/tr/td[4]/div/a/span");
					if (nameEmail.replace(" ", "").equals(
							accountName.replace(" ", ""))) {
						SeleniumFireFoxDriver
								.Click(driver,
										"/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div["
												+ i
												+ "]/table/tbody/tr/td[4]/div/a/span");
						break;
					}
				} catch (Exception e) {
				}
			}
		}

		return driver;
	}

	public static WebDriver openStandartUserForEdit(String accountName,
			WebDriver driver) throws InterruptedException {
		for (int i = 2; i < 20; i++) {
			try {
				String nameEmail = SeleniumFireFoxDriver
						.GetTextContent(
								driver,
								"/html/body/div/div[2]/table/tbody/tr/td[2]/div[4]/div/form/div[2]/table/tbody/tr["
										+ i + "]/th/a");
				if (nameEmail.replace(" ", "").equals(
						accountName.replace(" ", ""))) {
					SeleniumFireFoxDriver
							.Click(driver,
									"/html/body/div/div[2]/table/tbody/tr/td[2]/div[4]/div/form/div[2]/table/tbody/tr["
											+ i + "]/td/a");
					break;
				}
			} catch (Exception e) {
			}

		}

		return driver;
	}

	public static WebDriver selectAccountByName(String accountName,
			WebDriver driver) throws InterruptedException {
		for (int i = 0; i < 20; i++) {
			if (i == 0) {
				try {
					String emailCurrent = SeleniumFireFoxDriver
							.GetTextContent(
									driver,
									"//html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div/table/tbody/tr/td[4]/div/a/span");
					if (emailCurrent.replace(" ", "").equals(
							accountName.replace(" ", ""))) {
						SeleniumFireFoxDriver
								.Click(driver,
										"/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div/table/tbody/tr/td/div/input");
						break;
					}
				} catch (Exception e) {
				}
			} else {
				try {
					String nameEmail = SeleniumFireFoxDriver
							.GetTextContent(
									driver,
									"/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div["
											+ i
											+ "]/table/tbody/tr/td[4]/div/a/span");
					if (nameEmail.replace(" ", "").equals(
							accountName.replace(" ", ""))) {
						SeleniumFireFoxDriver
								.Click(driver,
										"/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div["
												+ i
												+ "]/table/tbody/tr/td/div/input");
						break;
					}
				} catch (Exception e) {
				}
			}
		}

		return driver;
	}

	public static WebDriver clickOnFaxAccountByName(String accountName,
			WebDriver driver) throws InterruptedException {
		for (int i = 0; i < 20; i++) {
			if (i == 0) {
				try {
					String emailCurrent = SeleniumFireFoxDriver
							.GetTextContent(
									driver,
									"//html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div/table/tbody/tr/td[4]/div/a/span");
					if (emailCurrent.replace(" ", "").equals(
							accountName.replace(" ", ""))) {
						SeleniumFireFoxDriver
								.DoubleClick(
										driver,
										"/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div/table/tbody/tr/td[5]/div");
						break;
					}
				} catch (Exception e) {
				}
			} else {
				try {
					String nameEmail = SeleniumFireFoxDriver
							.GetTextContent(
									driver,
									"/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div["
											+ i
											+ "]/table/tbody/tr/td[4]/div/a/span");
					if (nameEmail.replace(" ", "").equals(
							accountName.replace(" ", ""))) {
						SeleniumFireFoxDriver
								.DoubleClick(
										driver,
										"/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div["
												+ i
												+ "]/table/tbody/tr/td[5]/div");
						break;
					}
				} catch (Exception e) {
				}
			}
		}

		return driver;
	}

	public static WebDriver clickOnPhoneAccountByName(String accountName,
			WebDriver driver) throws InterruptedException {
		for (int i = 0; i < 20; i++) {
			if (i == 0) {
				try {
					String emailCurrent = SeleniumFireFoxDriver
							.GetTextContent(
									driver,
									"//html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div/table/tbody/tr/td[4]/div/a/span");
					if (emailCurrent.replace(" ", "").equals(
							accountName.replace(" ", ""))) {
						SeleniumFireFoxDriver
								.DoubleClick(
										driver,
										"/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div/table/tbody/tr/td[6]/div");
						break;
					}
				} catch (Exception e) {
				}
			} else {
				try {
					String nameEmail = SeleniumFireFoxDriver
							.GetTextContent(
									driver,
									"/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div["
											+ i
											+ "]/table/tbody/tr/td[4]/div/a/span");
					if (nameEmail.replace(" ", "").equals(
							accountName.replace(" ", ""))) {
						SeleniumFireFoxDriver
								.DoubleClick(
										driver,
										"/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div["
												+ i
												+ "]/table/tbody/tr/td[6]/div");
						break;
					}
				} catch (Exception e) {
				}
			}
		}

		return driver;
	}

	public static WebDriver clickOnWebSiteByName(String accountName,
			WebDriver driver) throws InterruptedException {
		for (int i = 0; i < 20; i++) {
			if (i == 0) {
				try {
					String emailCurrent = SeleniumFireFoxDriver
							.GetTextContent(
									driver,
									"//html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div/table/tbody/tr/td[4]/div/a/span");
					if (emailCurrent.replace(" ", "").equals(
							accountName.replace(" ", ""))) {
						SeleniumFireFoxDriver
								.DoubleClick(
										driver,
										"/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div/table/tbody/tr/td[7]/div");
						break;
					}
				} catch (Exception e) {
				}
			} else {
				try {
					String nameEmail = SeleniumFireFoxDriver
							.GetTextContent(
									driver,
									"/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div["
											+ i
											+ "]/table/tbody/tr/td[4]/div/a/span");
					if (nameEmail.replace(" ", "").equals(
							accountName.replace(" ", ""))) {
						SeleniumFireFoxDriver
								.DoubleClick(
										driver,
										"/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/form/div[3]/div[4]/div/div/div/div/div[2]/div/div["
												+ i
												+ "]/table/tbody/tr/td[7]/div");
						break;
					}
				} catch (Exception e) {
				}
			}
		}

		return driver;
	}

	public static void closeFireFox() throws Exception {
		if (isProcessRunging("firefox.exe")) {
			killProcess("firefox.exe");
		}
	}

	public static void killProcess(String serviceName) throws Exception {
		Runtime.getRuntime().exec(KILL + serviceName);
	}

	private static final String TASKLIST = "tasklist";
	private static final String KILL = "taskkill /IM ";

	public static boolean isProcessRunging(String serviceName) throws Exception {

		Process p = Runtime.getRuntime().exec(TASKLIST);
		BufferedReader reader = new BufferedReader(new InputStreamReader(
				p.getInputStream()));
		String line;
		while ((line = reader.readLine()) != null) {

			System.out.println(line);
			if (line.contains(serviceName)) {
				return true;
			}
		}

		return false;
	}

	public static WebDriver createLead(String company, String firstName,
			String lastName, String email, String phone, String title,
			String description, WebDriver driver) throws InterruptedException {
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getNewAccountsButton());
		Thread.sleep(2000);
		driver = fillInLeadForm(company, firstName, lastName, email, phone,
				title, description, driver);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getSaveButtonTop());
		Thread.sleep(5000);

		return driver;

	}

	public static WebDriver fillInLeadForm(String company, String firstName,
			String lastName, String email, String phone, String title,
			String description, WebDriver driver) throws InterruptedException {
		SeleniumFireFoxDriver.Type(driver, RepositorySalesForce.getLeadEmail(),
				email);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getLeadFirstName(), firstName);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getLeadLastName(), lastName);
		SeleniumFireFoxDriver.Type(driver, RepositorySalesForce.getLeadPhone(),
				phone);
		SeleniumFireFoxDriver.Type(driver, RepositorySalesForce.getLeadTitle(),
				title);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getCompanyLead(), company);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getDescriptionLead(), description);

		return driver;
	}

	public static WebDriver createOpportunity(String description,
			String opportunityName, String accountName, String closeDate,
			String stage, String amount, WebDriver driver)
			throws InterruptedException {
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getNewAccountsButton());
		Thread.sleep(2000);
		driver = fillInOpportunity(opportunityName, accountName, closeDate,
				stage, amount, description, driver);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getSaveButtonTop());
		Thread.sleep(5000);

		return driver;

	}

	public static WebDriver fillInOpportunity(String opportunityName,
			String accountName, String closeDate, String stage, String amount,
			String description, WebDriver driver) throws InterruptedException {

		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getOpportunityName(), opportunityName);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getOpportunityaccountName(), accountName);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getOpportunityCloseDate(), closeDate);
		selectStage(stage, driver);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getOpportunityAmount(), amount);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getOpportunityDescription(), description);

		return driver;
	}

	public static WebDriver selectStage(String value, WebDriver driver)
			throws InterruptedException {

		SeleniumFireFoxDriver.Click(driver, "opp11");
		Thread.sleep(2000);
		for (int i = 2; i < 10; i++) {
			if (SeleniumFireFoxDriver
					.GetTextContent(driver,
							"//*[@id=\"opp11\"]/option[" + i + "]")

					.replace(" ", "").equals(value.replace(" ", ""))) {

				SeleniumFireFoxDriver.DoubleClick(driver,
						"//*[@id=\"opp11\"]/option[" + i + "]");
				SeleniumFireFoxDriver.Click(driver,
						"//*[@id=\"opp11\"]/option[" + i + "]");
				SeleniumFireFoxDriver.SubmitElement(driver,
						"//*[@id=\"opp11\"]/option[" + i + "]");
				return driver;
			}
		}
		return driver;
	}

	public static WebDriver createStandartUser(String email, String username,
			String firstName, String lastName, String alias, String nickname,
			String title, String company, String department, String division,
			String license, String profile, WebDriver driver, String phone)
			throws InterruptedException {
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getNewUserButton());
		Thread.sleep(2000);
		driver = fillInStandartUserForm(email, username, firstName, lastName,
				alias, nickname, title, company, department, division, profile,
				license, driver, phone);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getTrackCheckBox());
		Thread.sleep(2000);
		SeleniumFireFoxDriver.Click(driver,
				RepositorySalesForce.getSaveButton());
		Thread.sleep(5000);

		return driver;
	}

	public static WebDriver fillInStandartUserForm(String email,
			String username, String firstName, String lastName, String alias,
			String nickname, String title, String company, String department,
			String division, String profile, String license, WebDriver driver,
			String phone) throws InterruptedException {

		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getStandartUserEmail(), email);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getStandartUserUsername(), username);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getStandartUserFirstName(), firstName);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getStandartUserLastName(), lastName);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getStandartUserAlias(), alias);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getStandartUserNickname(), nickname);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getStandartUserTitle(), title);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getStandartUserCompany(), company);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getStandartUserDepartment(), department);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getStandartUserDivision(), division);
		SeleniumFireFoxDriver.Type(driver,
				RepositorySalesForce.getStandartUserPhone(), phone);

		driver = selectLicense(license, driver);
		driver = selectProfile(profile, driver);

		return driver;
	}

	public static WebDriver selectProfile(String value, WebDriver driver)
			throws InterruptedException {

		SeleniumFireFoxDriver.Click(driver, "Profile");
		Thread.sleep(2000);
		for (int i = 2; i < 10; i++) {
			if (SeleniumFireFoxDriver
					.GetTextContent(driver,
							"//*[@id=\"Profile\"]/option[" + i + "]")

					.replace(" ", "").equals(value.replace(" ", ""))) {

				SeleniumFireFoxDriver.DoubleClick(driver,
						"//*[@id=\"Profile\"]/option[" + i + "]");
				SeleniumFireFoxDriver.Click(driver,
						"//*[@id=\"Profile\"]/option[" + i + "]");
				SeleniumFireFoxDriver.SubmitElement(driver,
						"//*[@id=\"Profile\"]/option[" + i + "]");
				return driver;
			}
		}
		return driver;
	}

	public static WebDriver selectLicense(String value, WebDriver driver)
			throws InterruptedException {

		SeleniumFireFoxDriver.Click(driver, "user_license_id");
		Thread.sleep(2000);
		for (int i = 2; i < 10; i++) {
			if (SeleniumFireFoxDriver
					.GetTextContent(driver,
							"//*[@id=\"user_license_id\"]/option[" + i + "]")
					.replace(" ", "").equals(value.replace(" ", ""))) {
				SeleniumFireFoxDriver.DoubleClick(driver,
						"//*[@id=\"user_license_id\"]/option[" + i + "]");
				SeleniumFireFoxDriver.Click(driver,
						"//*[@id=\"user_license_id\"]/option[" + i + "]");
				SeleniumFireFoxDriver.SubmitElement(driver,
						"//*[@id=\"user_license_id\"]/option[" + i + "]");
				return driver;
			}
		}
		return driver;
	}

	public static WebDriver selectRole0(String value, WebDriver driver)
			throws InterruptedException {

		SeleniumFireFoxDriver.Click(driver, "role0");
		Thread.sleep(2000);
		for (int i = 2; i < 10; i++) {
			if (SeleniumFireFoxDriver
					.GetTextContent(driver,
							"//*[@id=\"role0\"]/option[" + i + "]")
					.replace(" ", "").equals(value.replace(" ", ""))) {
				SeleniumFireFoxDriver.DoubleClick(driver,
						"//*[@id=\"role0\"]/option[" + i + "]");
				SeleniumFireFoxDriver.Click(driver,
						"//*[@id=\"role0\"]/option[" + i + "]");
				SeleniumFireFoxDriver.SubmitElement(driver,
						"//*[@id=\"role0\"]/option[" + i + "]");
				return driver;
			}
		}
		return driver;
	}

	public static WebDriver selectRole1(String value, WebDriver driver)
			throws InterruptedException {

		SeleniumFireFoxDriver.Click(driver, "role1");
		Thread.sleep(2000);
		for (int i = 2; i < 10; i++) {
			if (SeleniumFireFoxDriver
					.GetTextContent(driver,
							"//*[@id=\"role1\"]/option[" + i + "]")
					.replace(" ", "").equals(value.replace(" ", ""))) {
				SeleniumFireFoxDriver.DoubleClick(driver,
						"//*[@id=\"role1\"]/option[" + i + "]");
				SeleniumFireFoxDriver.Click(driver,
						"//*[@id=\"role1\"]/option[" + i + "]");
				SeleniumFireFoxDriver.SubmitElement(driver,
						"//*[@id=\"role2\"]/option[" + i + "]");
				return driver;
			}
		}
		return driver;
	}
}
