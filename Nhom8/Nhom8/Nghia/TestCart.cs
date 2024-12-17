using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;


namespace Nhom8.Nghia
{
    [TestFixture]
    class TestCart
    {

        private IWebDriver driver;
        private string productUrl = "https://yoyo1sneaker.com/giay-nike-air-jordan-1-low-brown-elephant-like-auth/";
        private string cartUrl = "https://yoyo1sneaker.com/gio-hang/";

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        //[TearDown]
        //public void TearDown()
        //{
        //    driver.Quit();
        //}

        // Hàm chờ phần tử hiển thị bằng vòng lặp đơn giản
        private IWebElement WaitForElement(By by, int timeoutInSeconds = 100)
        {
            for (int i = 0; i < timeoutInSeconds * 2; i++) // Kiểm tra mỗi 500ms
            {
                try
                {
                    var element = driver.FindElement(by);
                    if (element.Displayed && element.Enabled)
                        return element;
                }
                catch (NoSuchElementException)
                {
                    // Không làm gì nếu phần tử chưa tìm thấy
                }
                Thread.Sleep(500); // Chờ 500ms trước khi thử lại
            }
            throw new Exception($"Element {by} not found after {timeoutInSeconds} seconds");
        }

        [Test]
        public void Test_Add_Product_And_Validate_Cart()
        {
            // 1. Truy cập trực tiếp vào sản phẩm "Nike Air Jordan 1 Low Brown Elephant-Like Auth"
            driver.Navigate().GoToUrl(productUrl);

            // 2. Chọn size từ UX Swatches (Chọn size 43 trong ví dụ này)
            var sizeOption = WaitForElement(By.CssSelector(".ux-swatch[data-value='43']"));
            sizeOption.Click(); // Nhấn vào size 43

            // 3. Thêm sản phẩm vào giỏ hàng
            var addToCartButton = WaitForElement(By.CssSelector("button.single_add_to_cart_button"));
            addToCartButton.Click();
            // 5. Truy cập vào trang giỏ hàng
            driver.Navigate().GoToUrl(cartUrl);

            // 6. Kiểm tra sản phẩm trong giỏ hàng
            var cartItem = WaitForElement(By.CssSelector(".cart_item"));

            // Kiểm tra tên sản phẩm
            var productName = cartItem.FindElement(By.CssSelector(".product-name a")).Text;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Giày Nike Air Jordan 1 Low 'Brown Elephant' - Like Auth", productName, "Tên sản phẩm không đúng trong giỏ hàng!");

            // Kiểm tra size của sản phẩm
            var productSize = cartItem.FindElement(By.CssSelector(".variation-Size p")).Text;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("43", productSize, "Size sản phẩm không đúng!");
        }
    }
}
