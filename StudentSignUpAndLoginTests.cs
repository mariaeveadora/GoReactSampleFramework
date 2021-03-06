//--------READ ME-------------
//Setup is easy. All you need to do is to create a new 'Project' and select 'Class Library'. 
//Once project has been created, select 'Tools'. 
//Select 'NuGet Package Manager' then select 'Manage Packages for Solution'. 
//Download and Install the following packages:
//1. NUnit (Latest version)
//2. NUnit.Console (Latest version)
//3. NUnit.ConsoleRunner (Latest version)
//4. NUnit.Extension.NUnitProjectLoader (Latest version)
//5. NUnit.Extension.NUnitV2Driver (Latest version)
//6. NUnit.Extension.NUnitV2ResultWriter (Latest version)
//7. NUnit.Extension.TeamCityEventListener (Latest version)
//8. NUnit.Extension.VSProjectLoader (Latest version)
//9. NUnit.Runners (Latest version)
//10. NUnit3TestAdapter (Note: Sometimes there is a conflict when using the latest version, I change versions from 16.0 to 17.0)
//11. Selenium.Chrome.WebDriver (Latest version)
//12. Selenium.Support (Latest version)
//13. Selenium.WebDriver (Latest version)
//14. NUnitTestAdapter (Latest version)
//Once these packages have been installed, you may now use these test scripts for execution.
//Just create your own project and your own class/es.

//How to run:
//1. After setup, add the needed project and classes
//2. Paste the codes
//3. To run, select 'Test' 
//4. Select 'Windows' then 'Test Explorer'
//5. When all the tests are displayed, try to Build or Rebuild Solution. 
//You might encounter an issue when there are no test cases displayed in the Test Explorer, just update the TestAdapter in the Nuget Package to a different version and rebuild solution.

//NOTES: 
//I created 2 separate classes for both Instructor (Sign Up and Log In) and Student (Sign Up and Log In)
//There is an existing issue when logging in using a Resident Instructor Account, tried manually testing this and issue is also encountered but intermittent.
//I was not able to proceed with the Student Account creation since it required an invitation from an Instructor. 

//Creating this solution took me around 10 hours since it was a first and I had to do some research. Thank you!

//Created by Ma. Eve Adora S. Macasero 

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System;
using System.Threading;


namespace StudentSignUpAndLoginTests
{
    [TestFixture]
    public class StudentSignUpAndLoginTests
    {
        //Creating Webdriver instance
        IWebDriver driver = new ChromeDriver();

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            Console.WriteLine("OneTimeSetup");
        }

        [Test]
        //This test is to unsuccessfully create a Non Resident Student account - No notification
        public void CreateStudentAccount_NonResident_Unsuccessful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Clicking the 'Create Account' button
            driver.FindElement(By.XPath("//button[@aria-label='create account']")).Click();
            Thread.Sleep(1000);

            //Click 'No' button
            driver.FindElement(By.XPath("//button[@aria-label='Not a resident']")).Click();
            Thread.Sleep(1000);

            //Click 'Student' button
            driver.FindElement(By.XPath("//button[@aria-label='student']")).Click();
            Thread.Sleep(1000);

            //Click 'back' button
            driver.FindElement(By.XPath("//button[@aria-label='back']")).Click();
            Thread.Sleep(1000);

            //Verify if login page is displayed
            IWebElement logouttext = driver.FindElement(By.XPath("//button[@aria-label='forgot password']"));
            string getText = logouttext.Text;
            string logout = "FORGOT PASSWORD";

            Assert.That((logout.Contains(getText)), Is.True);

            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }

        [Test]
        //This test is to unsuccessfully create a Resident Student account - No notification
        public void CreateStudentAccount_Resident_Unsuccessful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Clicking the 'Create Account' button
            driver.FindElement(By.XPath("//button[@aria-label='create account']")).Click();
            Thread.Sleep(1000);

            //Click 'Yes' button
            driver.FindElement(By.XPath("//button[@aria-label='Yes, a resident']")).Click();
            Thread.Sleep(1000);

            //Click 'Student' button
            driver.FindElement(By.XPath("//button[@aria-label='student']")).Click();
            Thread.Sleep(1000);

            //Click 'back' button
            driver.FindElement(By.XPath("//button[@aria-label='back']")).Click();
            Thread.Sleep(1000);

            //Verify if login page is displayed
            IWebElement logouttext = driver.FindElement(By.XPath("//button[@aria-label='forgot password']"));
            string getText = logouttext.Text;
            string logout = "FORGOT PASSWORD";

            Assert.That((logout.Contains(getText)), Is.True);

            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }        
    }
}
