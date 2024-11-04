using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Selenium_Practice_2
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            try
            {
                driver.Navigate().GoToUrl("https://pastebin.com/");

                IWebElement cookiesButton = driver.FindElement(By.XPath("//button[span[text()='AGREE']]"));
                cookiesButton.Click();

                // Paste text
                IWebElement newPasteField = driver.FindElement(By.Id("postform-text"));
                newPasteField.Click();
                string codeContent = @"git config --global user.name ""New Sheriff in Town""
                    git reset $(git commit-tree HEAD^{tree} -m ""Legacy code"")
                    git push origin master --force";
                newPasteField.SendKeys(codeContent);

                // Syntax Highlighting
                IWebElement syntaxHighlightingField = driver.FindElement(By.Id("select2-postform-format-container"));
                syntaxHighlightingField.Click();

                IWebElement highlightedSyntaxOption = driver.FindElement(By.XPath("//li[contains(@class, 'select2-results__option') and text()='Bash']"));
                highlightedSyntaxOption.Click();

                // Paste expiration
                IWebElement pasteExpirationField = driver.FindElement(By.Id("select2-postform-expiration-container"));
                pasteExpirationField.Click();

                IWebElement highlightedExpirationOption = driver.FindElement(By.XPath("//li[contains(@class, 'select2-results__option') and text()='10 Minutes']"));
                highlightedExpirationOption.Click();

                // Paste name
                IWebElement pasteName = driver.FindElement(By.Id("postform-name"));
                string pasteTitle = "how to gain dominance among developers";
                pasteName.Click();
                pasteName.SendKeys(pasteTitle);

                // Save paste
                IWebElement savePaste = driver.FindElement(By.XPath("//button[text()='Create New Paste']"));
                savePaste.Click();

                // Verify the page title contains the paste title
                IWebElement checkPasteTitle = driver.FindElement(By.CssSelector("h1"));
                Assert.IsTrue(checkPasteTitle.Text.Contains(pasteTitle));
                
                // Verify syntax highlighting is set to "Bash"
                IWebElement checkSyntax = driver.FindElement(By.XPath("//a[@class='btn -small h_800' and text()='Bash']"));
                Assert.IsNotNull(checkSyntax, "Syntax highlighting is not set to Bash.");

                // Verify the code content matches
                IWebElement rawButton = driver.FindElement(By.XPath("//a[contains(@href, '/raw') and contains(@class, 'btn -small') and text()='raw']"));
                rawButton.Click();
                string pageText = driver.FindElement(By.TagName("body")).Text;
                Assert.IsTrue(pageText.Contains(codeContent));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Assert.Fail("Test failed due to an exception.");
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}