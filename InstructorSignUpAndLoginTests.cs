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

namespace InstructorSignUpAndLoginTests
{
    [TestFixture]
    public class InstructorSignUpAndLoginTests
    {
        //Creating Webdriver instance
        IWebDriver driver = new ChromeDriver();
        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            Console.WriteLine("OneTimeSetup");
        }

        [Test]
        //This test method is to test opening a browser and opening the GoReact site.
        public void OpenSite_Successful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Verify if site is opened
            var actual = driver.Title;
            var expected = "GoReact Login";

            Assert.That(expected, Is.EqualTo(actual));

            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }

        [Test]
        //This test is to successfully create a Non Resident Instructor account.
        public void CreateInstructorAccount_NonResident_Successful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Click the 'Create Account' button
            driver.FindElement(By.XPath("//button[@aria-label='create account']")).Click();
            Thread.Sleep(1000);

            //Click 'No' button
            driver.FindElement(By.XPath("//button[@aria-label='Not a resident']")).Click();
            Thread.Sleep(1000);

            //Click 'Instructor' button
            driver.FindElement(By.XPath("//button[@aria-label='instructor']")).Click();
            Thread.Sleep(1000);

            //Populating the fields - First Part of Account Setup
            //First Name
            driver.FindElement(By.XPath("//input[@name='first_name']")).SendKeys("Jess");
            //Last Name
            driver.FindElement(By.XPath("//input[@name='last_name']")).SendKeys("Mariano");
            //Phone Number
            driver.FindElement(By.XPath("//input[@name='phone_number']")).SendKeys("09123456789");
            //Email
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys("jm6@goreact.com");
            //Password
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("Test123!");
            //Confirm Password
            driver.FindElement(By.XPath("//input[@name='confirm_password']")).SendKeys("Test123!");
            //Accept Terms and Privacy Policy
            driver.FindElement(By.XPath("//input[@name='terms_accepted']")).Click();

            //Click 'Continue' button
            driver.FindElement(By.XPath("//button[@aria-label='continue']")).Click();
            Thread.Sleep(1000);

            //Populating the fields - Second Part of Account Setup
            //I'm using a GoReact at a (Choose 'Personal Use' option)
            driver.FindElement(By.XPath("//div//uib-dropdown[@name='org_type']//button//span[1]//span[@class='rich-dropdown-placeholder']")).Click();
            driver.FindElement(By.XPath("//div//uib-dropdown[@name='org_type']//ul//li[4]//span//span[@class='ng-binding']")).Click();

            //Training Type (Choose 'Foreign Language Teaching'option)
            driver.FindElement(By.XPath("//div//uib-dropdown[@name='use_type']//button//span[1]//span[@class='rich-dropdown-placeholder']")).Click();
            driver.FindElement(By.XPath("//div//uib-dropdown[@name='use_type']//ul//li[4]//span//span[@class='ng-binding']")).Click();

            //Course Format (Choose 'Online' option)
            driver.FindElement(By.XPath("//div//uib-dropdown[@name='course_format']//button//span[1]//span[@class='rich-dropdown-placeholder']")).Click();
            driver.FindElement(By.XPath("//div//uib-dropdown//ul//li[1]//span[@aria-label='Online']//span")).Click();
            Thread.Sleep(1000);

            //Click 'Continue' button
            driver.FindElement(By.XPath("//button[@aria-label='continue']")).Click();
            Thread.Sleep(5000);
            
            //Verify if account has been created 
            IWebElement logintext = driver.FindElement(By.XPath("//div[@class='heading']//zero-state-title//span"));
            string getText = logintext.Text;
            string login = "Welcome to GoReact!";

            Assert.That((login.Contains(getText)), Is.True);

            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }

        [Test]
        //This test is to successfully create a Resident Instructor account. 
        public void CreateInstructorAccount_Resident_Successful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Click the 'Create Account' button
            driver.FindElement(By.XPath("//button[@aria-label='create account']")).Click();
            Thread.Sleep(1000);

            //Click 'Yes' button
            driver.FindElement(By.XPath("//button[@aria-label='Yes, a resident']")).Click();
            Thread.Sleep(1000);

            //Click 'Instructor' button
            driver.FindElement(By.XPath("//button[@aria-label='instructor']")).Click();
            Thread.Sleep(1000);

            //Populating the fields - First Part of Account Setup
            //First Name
            driver.FindElement(By.XPath("//input[@name='first_name']")).SendKeys("Lorelai");
            //Last Name
            driver.FindElement(By.XPath("//input[@name='last_name']")).SendKeys("Gilmore");
            //Phone Number
            driver.FindElement(By.XPath("//input[@name='phone_number']")).SendKeys("09123456798");
            //Email
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys("lg6@codev.com");
            //Password
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("Test123!");
            //Confirm Password
            driver.FindElement(By.XPath("//input[@name='confirm_password']")).SendKeys("Test123!");
            //Accept Terms and Privacy Policy
            driver.FindElement(By.XPath("//input[@name='terms_accepted']")).Click();

            //Click 'Continue' button
            driver.FindElement(By.XPath("//button[@aria-label='continue']")).Click();
            Thread.Sleep(1000);

            //Populating the fields - Second Part of Account Setup
            //I'm using a GoReact at a (Choose 'Personal Use' option)
            driver.FindElement(By.XPath("//div//uib-dropdown[@name='org_type']//button//span[1]//span[@class='rich-dropdown-placeholder']")).Click();
            driver.FindElement(By.XPath("//div//uib-dropdown[@name='org_type']//ul//li[4]//span//span")).Click();

            //Training Type (Choose 'Teacher Preparation'option)
            driver.FindElement(By.XPath("//div//uib-dropdown[@name='use_type']//button//span[1]//span[@class='rich-dropdown-placeholder']")).Click();
            driver.FindElement(By.XPath("//div//uib-dropdown[@name='use_type']//ul//li[1]//span//span")).Click();

            //Course Format (Choose 'Other' option)
            driver.FindElement(By.XPath("//div//uib-dropdown[@name='course_format']//button//span[1]//span[@class='rich-dropdown-placeholder']")).Click();
            driver.FindElement(By.XPath("//div//uib-dropdown//ul//li[4]//span[@aria-label='Other']//span")).Click();
            Thread.Sleep(1000);

            //Click 'Continue' button
            driver.FindElement(By.XPath("//button[@aria-label='continue']")).Click();
            Thread.Sleep(5000);

            //Verify if account has been created 
            IWebElement logintext = driver.FindElement(By.XPath("//div[@class='heading']//zero-state-title//span"));
            string getText = logintext.Text;
            string login = "Welcome to GoReact!";

            Assert.That((login.Contains(getText)), Is.True);

            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }

        [Test]
        //This test is to successfully login as a Non Resident Instructor - Valid credentials
        public void LoginAsInstructor_NonResident_Successful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Login as a Non Resident Instructor
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys("rorygilmore@goreact.com");
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("Test123!");
            driver.FindElement(By.XPath("//button[@aria-label='log in']")).Click();
            Thread.Sleep(5000);

            //Verify if account has logged in successfully
            IWebElement logintext = driver.FindElement(By.XPath("//div[@class='heading']//zero-state-title//span"));
            string getText = logintext.Text;
            string login = "Welcome to GoReact!";

            Assert.That((login.Contains(getText)), Is.True);

            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }

        [Test]
        //This test is to successfully login and sign out as a Non Resident Instructor. 
        public void LogoutAsInstructor_NonResident_Successful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Login as a Non Resident Instructor
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys("rorygilmore@goreact.com");
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("Test123!");
            driver.FindElement(By.XPath("//button[@aria-label='log in']")).Click();
            Thread.Sleep(5000);

            //Logout as Non Resident Instructor
            driver.FindElement(By.XPath("//div[@class='toolbar']//uib-dropdown//button")).Click();
            driver.FindElement(By.XPath("//a[@aria-label='Log Out']")).Click();
            Thread.Sleep(5000);

            //Verify if account has logged out successfully
            IWebElement logouttext = driver.FindElement(By.XPath("//button[@aria-label='forgot password']"));
            string getText = logouttext.Text;
            string logout = "FORGOT PASSWORD";

            Assert.That((logout.Contains(getText)), Is.True);


            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }

        [Test]
        //This test is to unsuccessfully login as a Non Resident Instructor - Invalid credentials
        public void LoginAsInstructor_NonResident_Unsuccessful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Login as a Non Resident Instructor
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys("jessmariano@goreacts.com");
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("Test123!!");
            driver.FindElement(By.XPath("//button[@aria-label='log in']")).Click();
            Thread.Sleep(5000);

            //Verify if login was not successful
            IWebElement logouttext = driver.FindElement(By.XPath("//button[@aria-label='forgot password']"));
            string getText = logouttext.Text;
            string logout = "FORGOT PASSWORD";

            Assert.That((logout.Contains(getText)), Is.True);

            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }

        [Test]
        //This test is to successfully login as a Non Resident Instructor by populating the Password field first.
        public void LoginAsInstructor_NonResident_PasswordFirst_Successful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Login as a Non Resident Instructor            
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("Test123!");
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys("rorygilmore@goreact.com");
            driver.FindElement(By.XPath("//button[@aria-label='log in']")).Click();
            Thread.Sleep(5000);

            //Verify if account has logged in successfully
            IWebElement logintext = driver.FindElement(By.XPath("//div[@class='heading']//zero-state-title//span"));
            string getText = logintext.Text;
            string login = "Welcome to GoReact!";

            Assert.That((login.Contains(getText)), Is.True);

            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }
        
        [Test]
        //This test is to successfully login as a Resident Instructor - Valid credentials
        public void LoginAsInstructor_Resident_Successful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Login as a Resident Instructor
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys("lg6@codev.com");
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("Test123!");
            driver.FindElement(By.XPath("//button[@aria-label='log in']")).Click();
            Thread.Sleep(5000);

            //Verify if account has logged in successfully
            IWebElement logintext = driver.FindElement(By.XPath("//div[@class='heading']//zero-state-title//span"));
            string getText = logintext.Text;
            string login = "Welcome to GoReact!";

            Assert.That((login.Contains(getText)), Is.True);

            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }

        [Test]
        //This test is to successfully login and sign out as a Resident Instructor.
        public void LogoutAsInstructor_Resident_Successful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Login as a Resident Instructor
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys("lg6@codev.com");
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("Test123!");
            driver.FindElement(By.XPath("//button[@aria-label='log in']")).Click();
            Thread.Sleep(5000);

            //Logout as a Resident Instructor
            driver.FindElement(By.XPath("//div[@class='toolbar']//uib-dropdown//button//span[@class='profile-name ng-binding']")).Click();
            driver.FindElement(By.XPath("//a[@aria-label='Log Out']")).Click();
            Thread.Sleep(5000);

            //Verify if account has logged out successfully
            IWebElement logouttext = driver.FindElement(By.XPath("//button[@aria-label='forgot password']"));
            string getText = logouttext.Text;
            string logout = "FORGOT PASSWORD";

            Assert.That((logout.Contains(getText)), Is.True);

            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }

        [Test]
        //This test is to unsuccessfully login as a Resident Instructor - Invalid credentials
        public void LoginAsInstructor_Resident_Unsuccessful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Login as a Resident Instructor
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys("jackpearson@goreacts.com");
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("Test123!!");
            driver.FindElement(By.XPath("//button[@aria-label='log in']")).Click();
            Thread.Sleep(5000);

            //Verify if login was not successful
            IWebElement logouttext = driver.FindElement(By.XPath("//button[@aria-label='forgot password']"));
            string getText = logouttext.Text;
            string logout = "FORGOT PASSWORD";

            Assert.That((logout.Contains(getText)), Is.True);

            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }

        [Test]
        //This test is to successfully login as a Resident Instructor by populating the Password field first.
        public void LoginAsInstructor_Resident_PasswordFirst_Successful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Login as a Resident Instructor            
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("Test123!");
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys("lg6@codev.com");
            driver.FindElement(By.XPath("//button[@aria-label='log in']")).Click();
            Thread.Sleep(5000);

            //Verify if account has logged in successfully
            IWebElement logintext = driver.FindElement(By.XPath("//div[@class='heading']//zero-state-title//span"));
            string getText = logintext.Text;
            string login = "Welcome to GoReact!";

            Assert.That((login.Contains(getText)), Is.True);

            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }

        [Test]
        //This is to test the Forget Password feature of the page for a Non Resident Instructor.
        public void ForgotPassword_NonResident_Successful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Click 'Forgot Password' button
            driver.FindElement(By.XPath("//button[@aria-label='forgot password']")).Click();

            //Populate Email field and click 'Send' button
            driver.FindElement(By.XPath("//input[@aria-label='email']")).SendKeys("jessmariano@goreact.com");
            driver.FindElement(By.XPath("//button[@aria-label='reset password']")).Click();
            Thread.Sleep(5000);

            //Click 'Close' button
            driver.FindElement(By.XPath("//button[@aria-label='log in']//span")).Click();
            Thread.Sleep(5000);

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
        //This is to test the Forget Password feature of the page for a Resident Instructor.
        public void ForgotPassword_Resident_Successful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Click 'Forgot Password' button
            driver.FindElement(By.XPath("//button[@aria-label='forgot password']")).Click();
            Thread.Sleep(1000);

            //Populate Email field and click 'Send' button
            driver.FindElement(By.XPath("//input[@aria-label='email']")).SendKeys("jackpearson@goreact.com");
            driver.FindElement(By.XPath("//button[@aria-label='reset password']")).Click();
            Thread.Sleep(5000);            

            //Click 'Close' button
            driver.FindElement(By.XPath("//button[@aria-label='log in']//span")).Click();
            Thread.Sleep(5000);

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
        //This is to test trying to Login without any values in the required fields.
        public void BlankLoginFields_Unsuccessful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Click 'Log in' button
            driver.FindElement(By.XPath("//button[@aria-label='log in']")).Click();
            Thread.Sleep(1000);

            //Verify that login page is still displayed
            IWebElement logouttext = driver.FindElement(By.XPath("//button[@aria-label='forgot password']"));
            string getText = logouttext.Text;
            string logout = "FORGOT PASSWORD";

            Assert.That((logout.Contains(getText)), Is.True);

            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }

        [Test]
        //This is to test trying to create a Non Resident Instructor account without populating the required fields.
        public void BlankCreateAccountFields_InstructorNonResident_Unsuccessful()
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

            //Click 'Instructor' button
            driver.FindElement(By.XPath("//button[@aria-label='instructor']")).Click();
            Thread.Sleep(1000);

            //Click 'Continue' button 
            driver.FindElement(By.XPath("//button[@aria-label='continue']")).Click();
            Thread.Sleep(1000);

            //Verify if error message is displayed
            IWebElement invalidtext = driver.FindElement(By.XPath("//ng-message[@id='first-name-required-error']//span"));
            string getText = invalidtext.Text;
            string invalid = "First name is required.";

            Assert.That((invalid.Contains(getText)), Is.True);

            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }

        [Test]
        //This is to test trying to create a Resident Instructor account without populating the required fields.
        public void BlankCreateAccountFields_InstructorResident_Unsuccessful()
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

            //Click 'Instructor' button
            driver.FindElement(By.XPath("//button[@aria-label='instructor']")).Click();
            Thread.Sleep(1000);

            //Click 'Continue' button 
            driver.FindElement(By.XPath("//button[@aria-label='continue']")).Click();
            Thread.Sleep(1000);

            //Verify if error message is displayed
            IWebElement invalidtext = driver.FindElement(By.XPath("//ng-message[@id='first-name-required-error']//span"));
            string getText = invalidtext.Text;
            string invalid = "First name is required.";

            Assert.That((invalid.Contains(getText)), Is.True);

            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }

        [Test]
        //This test is trying to create an existing Instructor account.
        public void CreateExistingAccount_Unsuccessful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Click the 'Create Account' button
            driver.FindElement(By.XPath("//button[@aria-label='create account']")).Click();
            Thread.Sleep(1000);

            //Click 'No' button
            driver.FindElement(By.XPath("//button[@aria-label='Not a resident']")).Click();
            Thread.Sleep(1000);

            //Click 'Instructor' button
            driver.FindElement(By.XPath("//button[@aria-label='instructor']")).Click();
            Thread.Sleep(1000);

            //Populating the fields - First Part of Account Setup
            //First Name
            driver.FindElement(By.XPath("//input[@name='first_name']")).SendKeys("Jess");
            //Last Name
            driver.FindElement(By.XPath("//input[@name='last_name']")).SendKeys("Mariano");
            //Phone Number
            driver.FindElement(By.XPath("//input[@name='phone_number']")).SendKeys("09123456789");
            //Email
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys("jessmariano2@goreact.com");
            //Password
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("Test123!");
            //Confirm Password
            driver.FindElement(By.XPath("//input[@name='confirm_password']")).SendKeys("Test123!");
            //Accept Terms and Privacy Policy
            driver.FindElement(By.XPath("//input[@name='terms_accepted']")).Click();
            Thread.Sleep(5000);

            //Click 'Continue' button
            driver.FindElement(By.XPath("//button[@aria-label='continue']")).Click();
            Thread.Sleep(5000);

            //Populating the fields - Second Part of Account Setup
            //I'm using a GoReact at a (Choose 'Personal Use' option)
            driver.FindElement(By.XPath("//div//uib-dropdown[@name='org_type']//button//span[1]//span[@class='rich-dropdown-placeholder']")).Click();
            driver.FindElement(By.XPath("//div//uib-dropdown[@name='org_type']//ul//li[4]//span//span")).Click();

            //Training Type (Choose 'Teacher Preparation'option)
            driver.FindElement(By.XPath("//div//uib-dropdown[@name='use_type']//button//span[1]//span[@class='rich-dropdown-placeholder']")).Click();
            driver.FindElement(By.XPath("//div//uib-dropdown[@name='use_type']//ul//li[1]//span//span")).Click();

            //Course Format (Choose 'Other' option)
            driver.FindElement(By.XPath("//div//uib-dropdown[@name='course_format']//button//span[1]//span[@class='rich-dropdown-placeholder']")).Click();
            driver.FindElement(By.XPath("//div//uib-dropdown//ul//li[4]//span[@aria-label='Other']//span")).Click();

            //Click 'Continue' button
            driver.FindElement(By.XPath("//button[@aria-label='continue']")).Click();
            Thread.Sleep(5000);

            //Verify if error message is displayed
            IWebElement invalidtext = driver.FindElement(By.XPath("//p[@class='alert-message ng-scope']"));
            string getText = invalidtext.Text;
            string invalid = "Email address is already in use. Please log in or use a different email.";

            Assert.That((invalid.Contains(getText)), Is.True);

            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }

        [Test]
        //This test is trying to create an Instructor account with an invalid password.
        public void CreateAccount_InvalidPassword_Unsuccessful()
        {
            //Opening the site
            driver.Navigate().GoToUrl("https://dev.goreact.com/dashboard/auth/login");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            //Click the 'Create Account' button
            driver.FindElement(By.XPath("//button[@aria-label='create account']")).Click();
            Thread.Sleep(1000);

            //Click 'No' button
            driver.FindElement(By.XPath("//button[@aria-label='Not a resident']")).Click();
            Thread.Sleep(1000);

            //Click 'Instructor' button
            driver.FindElement(By.XPath("//button[@aria-label='instructor']")).Click();
            Thread.Sleep(1000);

            //Populating the fields - First Part of Account Setup
            //First Name
            driver.FindElement(By.XPath("//input[@name='first_name']")).SendKeys("Jess");
            //Last Name
            driver.FindElement(By.XPath("//input[@name='last_name']")).SendKeys("Mariano");
            //Phone Number
            driver.FindElement(By.XPath("//input[@name='phone_number']")).SendKeys("09123456789");
            //Email
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys("jessmariano@goreact.com");
            //Password
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("sample");
            //Confirm Password
            driver.FindElement(By.XPath("//input[@name='confirm_password']")).SendKeys("sample");
            //Accept Terms and Privacy Policy
            driver.FindElement(By.XPath("//input[@name='terms_accepted']")).Click();

            //Click 'Continue' button
            driver.FindElement(By.XPath("//button[@aria-label='continue']")).Click();
            Thread.Sleep(1000);

            //Verify if error message is displayed
            IWebElement invalidtext = driver.FindElement(By.XPath("//ng-message[@id='password-invalid-error']//span"));
            string getText = invalidtext.Text;
            string invalid = "Password does not meet requirements";

            Assert.That((invalid.Contains(getText)), Is.True);

            //Closing the browser and ending the session
            driver.Close();
            driver.Quit();
        }
    }
}
