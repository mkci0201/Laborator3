using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.EndToEnd
{
    class Test_AddToDoTask
    {
        private IWebDriver _driver;

        [SetUp]
        public void SetupDriver()
        {
            _driver = new ChromeDriver("D:\\Postuniversitar_UBB_Info\\DotNet");
        }

        [Test]
        public void test_AddToDoTask()
        {
            _driver.Url = "http://localhost:8100/ToDoTasks";
            var navBar = _driver.FindElement(By.TagName("app-navbar"));
            bool foundToDoTaskPage = false;
            if (navBar.Text == "ToDoTasks")
            {
                foundToDoTaskPage = true;
            }

            Assert.IsTrue(foundToDoTaskPage);
            Thread.Sleep(4000);

            var addToDoTaskButton = _driver.FindElement(By.TagName("ion-button"));
            bool buttonFound = false;
            if (addToDoTaskButton.Text == "ADD TODOTASK")
            {
                buttonFound = true;
                
            }

            Assert.IsTrue(buttonFound);
            addToDoTaskButton.Click();

            Thread.Sleep(3000);

            // Check that the login page is active
            var loginTitle = _driver.FindElement(By.TagName("ion-title"));
            Assert.IsNotNull(loginTitle);

            var inputFields = _driver.FindElements(By.TagName("input"));
            // There must be two input fields
            Assert.IsTrue(inputFields.Count == 2);

            var nameInput = inputFields[0];
            nameInput.SendKeys("kinga.abram@email.com");

            var passwordInput = inputFields[1];
            passwordInput.SendKeys("AbcD12!ab");

            Thread.Sleep(1000);

            var logInButton = _driver.FindElements(By.TagName("ion-button"))[2];
            Assert.IsNotNull(logInButton);
            Assert.IsTrue(logInButton.Text == "LOG IN");
            logInButton.Click();

            Thread.Sleep(3000);
            
            var addToDoTaskTitle = _driver.FindElements(By.TagName("ion-title"))[2];
            Assert.IsNotNull(addToDoTaskTitle);
            Assert.IsTrue(addToDoTaskTitle.Text == "Add toDoTask");

            Thread.Sleep(2000);


        }
    }
}
