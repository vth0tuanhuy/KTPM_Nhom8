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

        [Test]
        public void Test_Add_Product_And_Validate_Cart()
        {
            driver.Navigate().GoToUrl(productUrl);

            var sizeOption = WaitForElement(By.CssSelector(".ux-swatch[data-value='43']"));
            sizeOption.Click(); 
            var addToCartButton = WaitForElement(By.CssSelector("button.single_add_to_cart_button"));
            addToCartButton.Click();
            driver.Navigate().GoToUrl(cartUrl);

            var cartItem = WaitForElement(By.CssSelector(".cart_item"));

            var productName = cartItem.FindElement(By.CssSelector(".product-name a")).Text;
            Assert.AreEqual("Giày Nike Air Jordan 1 Low 'Brown Elephant' - Like Auth", productName, "Tên sản phẩm không đúng trong giỏ hàng!");

            var productSize = cartItem.FindElement(By.CssSelector(".variation-Size p")).Text;
            Assert.AreEqual("43", productSize, "Size sản phẩm không đúng!");
            var productPrice = cartItem.FindElement(By.CssSelector(".product-price span")).Text;
            Assert.AreEqual("965.000 ₫", productPrice, "Giá sản phẩm không đúng trong giỏ hàng!");
        }
       

        [Test]
        public void Test_Increase_Product_Quantity()
        {
            driver.Navigate().GoToUrl(productUrl);

            var sizeOption = WaitForElement(By.CssSelector(".ux-swatch[data-value='43']"));
            sizeOption.Click();

            var addToCartButton = WaitForElement(By.CssSelector("button.single_add_to_cart_button"));
            addToCartButton.Click();
            driver.Navigate().GoToUrl(cartUrl);

            var quantityInput = WaitForElement(By.CssSelector("input.input-text.qty"));
            quantityInput.Clear();
            quantityInput.SendKeys("2");

            var updateButton = WaitForElement(By.CssSelector("button[name='update_cart']"));
            updateButton.Click();

            Thread.Sleep(2000); 
            var updateMessage = WaitForElement(By.CssSelector(".woocommerce-message .message-container"));
            string messageText = updateMessage.Text;

            Assert.IsTrue(messageText.Contains("Giỏ hàng đã được cập nhật"),
                          "Thông báo cập nhật giỏ hàng không chính xác!");
            var totalPrice = WaitForElement(By.CssSelector(".order-total .woocommerce-Price-amount")).Text;
            Assert.AreEqual("1.930.000 ₫", totalPrice, "Tổng giá không đúng sau khi tăng số lượng sản phẩm!");
        }

        [Test]
        public void Test_Decrease_Product_Quantity()
        {
            driver.Navigate().GoToUrl(productUrl);

            var sizeOption = WaitForElement(By.CssSelector(".ux-swatch[data-value='43']"));
            sizeOption.Click();
            var quantityProduct = WaitForElement(By.CssSelector("input.input-text.qty"));
            quantityProduct.Clear(); 
            quantityProduct.SendKeys("2");
            var addToCartButton = WaitForElement(By.CssSelector("button.single_add_to_cart_button"));
            addToCartButton.Click();
            driver.Navigate().GoToUrl(cartUrl);

            var quantityInput = WaitForElement(By.CssSelector("input.input-text.qty"));
            quantityInput.Clear();
            quantityInput.SendKeys("1");

            var updateButton = WaitForElement(By.CssSelector("button[name='update_cart']"));
            updateButton.Click();

            Thread.Sleep(2000); 
            var updateMessage = WaitForElement(By.CssSelector(".woocommerce-message .message-container"));
            string messageText = updateMessage.Text;

            Assert.IsTrue(messageText.Contains("Giỏ hàng đã được cập nhật"),
                          "Thông báo cập nhật giỏ hàng không chính xác!");

            var totalPrice = WaitForElement(By.CssSelector(".order-total .woocommerce-Price-amount")).Text;
            Assert.AreEqual("995.000 ₫", totalPrice, "Tổng giá không đúng sau khi giảm số lượng sản phẩm!");
        }
        [Test]
        public void Test_Empty_Cart_Message()
        {
            driver.Navigate().GoToUrl(cartUrl);

            try
            {
                var emptyMessage = WaitForElement(By.CssSelector(".wc-empty-cart-message .message-container"));
                string messageText = emptyMessage.Text;

                Assert.IsTrue(messageText.Contains("Chưa có sản phẩm nào trong giỏ hàng"),
                              "Thông báo giỏ hàng trống không chính xác!");
            }
            catch (Exception)
            {
                Assert.Fail("Giỏ hàng không trống như dự kiến hoặc không tìm thấy thông báo giỏ hàng trống.");
            }
        }
        [Test]
        public void Test_Remove_Product_From_Cart()
        {
            driver.Navigate().GoToUrl(productUrl);

            var sizeOption = WaitForElement(By.CssSelector(".ux-swatch[data-value='43']"));
            sizeOption.Click(); 

            var addToCartButton = WaitForElement(By.CssSelector("button.single_add_to_cart_button"));
            addToCartButton.Click();

            driver.Navigate().GoToUrl(cartUrl);

            var removeButton = WaitForElement(By.CssSelector(".product-remove a"));
            removeButton.Click();

            var removeMessage = WaitForElement(By.CssSelector(".woocommerce-message .message-container"));
            string removeMessageText = removeMessage.Text;
            Assert.IsTrue(removeMessageText.Contains("“Giày Nike Air Jordan 1 Low 'Brown Elephant' - Like Auth” đã xóa"),
                          "Thông báo xác nhận xóa sản phẩm không chính xác!");

            var emptyMessage = WaitForElement(By.CssSelector(".woocommerce-info .message-container"));
            string emptyMessageText = emptyMessage.Text;
            Assert.IsTrue(emptyMessageText.Contains("Chưa có sản phẩm nào trong giỏ hàng"),
                          "Thông báo giỏ hàng trống không chính xác!");
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
