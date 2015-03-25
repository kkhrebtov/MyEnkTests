package selenium.enkata.com;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.interactions.Actions;


public class SeleniumFireFoxDriver {

	public static WebDriver openUrl(String url) {
		WebDriver driver = new FirefoxDriver();
		driver.get(url);
		return driver;
	}

	public static WebDriver openUrl(String url, String browser) {

		WebDriver driver = null;

		switch(browser) {
			case "Chrome":
				System.setProperty("webdriver.chrome.driver", "D:\\Git\\sfdc-streamer-tests\\salesforce\\resources\\chromedriver.exe");
				driver = new ChromeDriver();
				driver.get(url);
				break;
			default:
				driver = new FirefoxDriver();
				driver.get(url);
		}
		return driver;

	}

	public static void Type(WebDriver driver, String locator, String text) throws InterruptedException {
		waitObject(driver, locator);
		GetObject(driver, locator).clear();
		GetObject(driver, locator).sendKeys(text);
	}

	public static void Click(WebDriver driver, String locator) throws InterruptedException {
		waitObject(driver, locator);
		GetObject(driver, locator).click();	
			
	}
	
	public static void DoubleClick(WebDriver driver, String locator) throws InterruptedException {
		waitObject(driver, locator);
		Actions action = new Actions(driver);
		action.doubleClick(GetObject(driver, locator));
		action.perform();
			
	}
	
	public static void SubmitElement(WebDriver driver, String locator) throws InterruptedException {
		waitObject(driver, locator);	
		WebElement a = GetObject(driver, locator);
		a.submit();		
	}
	
	
	
	
	
	public static String GetInnerHtml(WebDriver driver, String locator) throws InterruptedException {
		waitObject(driver, locator);
		return GetObject(driver, locator).getAttribute("innerHTML");
	}

	public static String GetChecked(WebDriver driver, String locator) {
		return GetObject(driver, locator).getAttribute("checked");
	}

	public static String GetTextContent(WebDriver driver, String locator) throws InterruptedException {
		waitObject(driver, locator);
		return GetObject(driver, locator).getAttribute("textContent");
	}

	public static String GetProperty(WebDriver driver, String locator,
			String nameProperty) {
		return GetObject(driver, locator).getAttribute(nameProperty);
	}

	public static String GetDisabled(WebDriver driver, String locator) {
		return GetObject(driver, locator).getAttribute("disabled");
	}

	public static void waitObject(WebDriver driver, String locator)
			throws InterruptedException {
		for (int i = 0; i < 10; i++) {
			Thread.sleep(500);
			if (GetObject(driver, locator) != null)
				break;
		}
	}

	// / <summary>
	// / Private methods
	// / </summary>
	public static WebElement GetObject(WebDriver driver, String locator) {
		// Get object by xpath
		try {
			WebElement element = driver.findElement(By.xpath(locator));
			if (element != null)
				return element;
		} catch (Exception e) {

		}

		// Get object by id
		try {
			WebElement element = driver.findElement(By.id(locator));
			if (element != null)
				return element;
		} catch (Exception e) {

		}

		try {
			WebElement element = driver.findElement(By.cssSelector(locator));
			if (element != null)
				return element;
		} catch (Exception e) {

		}

		try {
			WebElement element = driver.findElement(By.linkText(locator));
			if (element != null)
				return element;
		} catch (Exception e) {

		}

		try {
			WebElement element = driver
					.findElement(By.partialLinkText(locator));
			if (element != null)
				return element;
		} catch (Exception e) {

		}

		try {
			WebElement element = driver.findElement(By.tagName(locator));
			if (element != null)
				return element;
		} catch (Exception e) {

		}

		try {
			WebElement element = driver.findElement(By.name(locator));
			if (element != null)
				return element;
		} catch (Exception e) {

		}

		try {
			WebElement element = driver.findElement(By.className(locator));
			if (element != null){
				return element;						
			}
			
		} catch (Exception e) {

		}
		System.out.println("Couldn't find element by:" + locator);
		return null;
	}

}
