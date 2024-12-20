using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Threading;
using Assert = NUnit.Framework.Assert;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System.Runtime.InteropServices;


namespace Nhom8.The
{
    [TestFixture]
    public class TestLienHe
    {
        private IWebDriver driver;

         private string testLienhe = "https://yoyo1sneaker.com/lien-he-yoyo1-sneaker/";

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }
        private IWebElement WaitForElement(By by, int timeoutInSeconds = 100)
        {
            for (int i = 0; i < timeoutInSeconds * 2; i++)
            {
                try
                {
                    var element = driver.FindElement(by);
                    if (element.Displayed && element.Enabled)
                        return element;
                }
                catch (NoSuchElementException)
                {

                }
                Thread.Sleep(500);
            }
            throw new Exception($"Element {by} not found after {timeoutInSeconds} seconds");
        }
        //đầy đủ các trường
        [Test]
        public void ĐayDuCacTruong()
        {
            // 1. Truy cập vào trang liên hệ
           driver.Navigate().GoToUrl(testLienhe);
            // 2. Điền thông tin vào các trường
            // Điền Họ và Tên
            var nameField = driver.FindElement(By.CssSelector("input[name='your-name']"));
            nameField.SendKeys("Nguyen Van A");
            // Điền Email
            var emailField = driver.FindElement(By.CssSelector("input[name='your-email']"));
            emailField.SendKeys("nguyenvana@gmail.com");
            // Điền Nội dung tin nhắn
            var messageField = driver.FindElement(By.CssSelector("textarea[name='your-message']"));
            messageField.SendKeys("Đây là tin nhắn kiểm thử chức năng liên hệ trên website.");
            // 3. Gửi form
            var submitButton = driver.FindElement(By.CssSelector(".wpcf7-form-control[type='submit']"));
            submitButton.Click();
            // 4. Kiểm tra thông báo thành công
            Thread.Sleep(5000);
            var successMessage = driver.FindElement(By.CssSelector(".wpcf7-response-output[aria-hidden=\"true\"]"));           
            Assert.IsNotNull(successMessage, "Phần tử thông báo không tồn tại trong DOM!");
            Assert.IsTrue(successMessage.Displayed, "Thông báo gửi tin nhắn không hiển thị!");
            Assert.AreEqual("Thank you for your message. It has been sent.", successMessage.Text.Trim(),
               "Nội dung thông báo không đúng!");
            
        }
        //trống trường lời nhắn
        [Test]
        public void TrongTruongLoiNhan()
        {
            // 1. Truy cập vào trang liên hệ
            driver.Navigate().GoToUrl(testLienhe);
            // 2. Điền thông tin vào các trường
            // Điền Họ và Tên
            var nameField = driver.FindElement(By.CssSelector("input[name='your-name']"));
            nameField.SendKeys("Nguyen Van A");
            // Điền Email
            var emailField = driver.FindElement(By.CssSelector("input[name='your-email']"));
            emailField.SendKeys("nguyenvana@gmail.com");
            // Nội dung tin nhắn tin nhắn để trống
            var messageField = driver.FindElement(By.CssSelector("textarea[name='your-message']"));
            messageField.SendKeys("");
            // 3. Gửi form
            var submitButton = driver.FindElement(By.CssSelector("input[type='submit']"));
            submitButton.Click();
            Thread.Sleep(5000);
            // 4. Kiểm tra thông báo thành công
            var successMessage = driver.FindElement(By.CssSelector(".wpcf7-response-output[aria-hidden=\"true\"]"));
            Assert.IsTrue(successMessage.Displayed, "Thông báo gửi tin nhắn không hiển thị!");
            Assert.AreEqual("Thank you for your message. It has been sent.", successMessage.Text.Trim(),
                "Nội dung thông báo không đúng!");
        }
        //trống trường họ tên
        [Test]
        public void TrongTruongHoTen()
        {
            // 1. Truy cập vào trang liên hệ
            driver.Navigate().GoToUrl(testLienhe);
            // 2. Điền thông tin vào các trường
            // Họ và Tên để trống
            var nameField = driver.FindElement(By.CssSelector("input[name='your-name']"));
            nameField.SendKeys("");
            // Điền Email
            var emailField = driver.FindElement(By.CssSelector("input[name='your-email']"));
            emailField.SendKeys("nguyenvana@gmail.com");
            // Nội dung tin nhắn tin nhắn để trống
            var messageField = driver.FindElement(By.CssSelector("textarea[name='your-message']"));
            messageField.SendKeys("Đây là tin nhắn kiểm thử chức năng liên hệ trên website.");
            // 3. Gửi form
            var submitButton = driver.FindElement(By.CssSelector("input[type='submit']"));
            submitButton.Click();
            Thread.Sleep(5000);
            // 4. Kiểm tra thông báo thành công
            var successMessage = driver.FindElement(By.CssSelector(".wpcf7-response-output[aria-hidden=\"true\"]"));
            Assert.IsTrue(successMessage.Displayed, "Thông báo gửi tin nhắn không hiển thị!");
            Assert.AreEqual("One or more fields have an error. Please check and try again.", successMessage.Text.Trim(),
                "Nội dung thông báo không đúng!");
        }
        //trống trường email
        [Test]
        public void TrongTruongEmail()
        {
           
            driver.Navigate().GoToUrl(testLienhe);
           
            var nameField = driver.FindElement(By.CssSelector("input[name='your-name']"));
            nameField.SendKeys("Nguyen Van A");
            
            var emailField = driver.FindElement(By.CssSelector("input[name='your-email']"));
            emailField.SendKeys("");
            
            var messageField = driver.FindElement(By.CssSelector("textarea[name='your-message']"));
            messageField.SendKeys("Đây là tin nhắn kiểm thử chức năng liên hệ trên website.");
           
            var submitButton = driver.FindElement(By.CssSelector("input[type='submit']"));
            submitButton.Click();
            Thread.Sleep(5000);
            
            var successMessage = driver.FindElement(By.CssSelector(".wpcf7-response-output[aria-hidden=\"true\"]"));
            Assert.IsTrue(successMessage.Displayed, "Thông báo gửi tin nhắn không hiển thị!");
            Assert.AreEqual("One or more fields have an error. Please check and try again.", successMessage.Text.Trim(),
                "Nội dung thông báo không đúng!");
        }
        //trống tất cả các trường
        [Test]
        public void TrongTatCaCacTruong()
        {
            
            driver.Navigate().GoToUrl(testLienhe);
            var nameField = driver.FindElement(By.CssSelector("input[name='your-name']"));
            nameField.SendKeys("Nguyen Van A");
            
            var emailField = driver.FindElement(By.CssSelector("input[name='your-email']"));
            emailField.SendKeys("");
            
            var messageField = driver.FindElement(By.CssSelector("textarea[name='your-message']"));
            messageField.SendKeys("");
            
            var submitButton = driver.FindElement(By.CssSelector("input[type='submit']"));
            submitButton.Click();
            Thread.Sleep(5000);
            
            var successMessage = driver.FindElement(By.CssSelector(".wpcf7-response-output[aria-hidden=\"true\"]"));
            Assert.IsTrue(successMessage.Displayed, "Thông báo gửi tin nhắn không hiển thị!");
            Assert.AreEqual("One or more fields have an error. Please check and try again.", successMessage.Text.Trim(),
                "Nội dung thông báo không đúng!");
        }
        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                Console.WriteLine("Chương trình đã kết thúc và trình duyệt đã được đóng.");
            }
        }
        
    }
}
