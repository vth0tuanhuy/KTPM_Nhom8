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
            Assert.AreEqual("Giày Nike Air Jordan 1 Low 'Brown Elephant' - Like Auth", productName, "Tên sản phẩm không đúng trong giỏ hàng!");

            // Kiểm tra size của sản phẩm
            var productSize = cartItem.FindElement(By.CssSelector(".variation-Size p")).Text;
            Assert.AreEqual("43", productSize, "Size sản phẩm không đúng!");
            // Kiểm tra giá sản phẩm trong giỏ hàng
            var productPrice = cartItem.FindElement(By.CssSelector(".product-price span")).Text;
            Assert.AreEqual("965.000 ₫", productPrice, "Giá sản phẩm không đúng trong giỏ hàng!");
        }
       

        [Test]
        public void Test_Increase_Product_Quantity()
        {
            // 1. Truy cập trực tiếp vào sản phẩm "Nike Air Jordan 1 Low Brown Elephant-Like Auth"
            driver.Navigate().GoToUrl(productUrl);

            // 2. Chọn size từ UX Swatches (Chọn size 43 trong ví dụ này)
            var sizeOption = WaitForElement(By.CssSelector(".ux-swatch[data-value='43']"));
            sizeOption.Click(); // Nhấn vào size 43

            // 3. Thêm sản phẩm vào giỏ hàng
            var addToCartButton = WaitForElement(By.CssSelector("button.single_add_to_cart_button"));
            addToCartButton.Click();
            driver.Navigate().GoToUrl(cartUrl);

            var quantityInput = WaitForElement(By.CssSelector("input.input-text.qty"));
            quantityInput.Clear();
            quantityInput.SendKeys("2");

            var updateButton = WaitForElement(By.CssSelector("button[name='update_cart']"));
            updateButton.Click();

            Thread.Sleep(2000); // Chờ cập nhật xong
            var updateMessage = WaitForElement(By.CssSelector(".woocommerce-message .message-container"));
            string messageText = updateMessage.Text;

            // 9. Kiểm tra thông báo cập nhật giỏ hàng
            Assert.IsTrue(messageText.Contains("Giỏ hàng đã được cập nhật"),
                          "Thông báo cập nhật giỏ hàng không chính xác!");
            // Kiểm tra tổng giá
            var totalPrice = WaitForElement(By.CssSelector(".order-total .woocommerce-Price-amount")).Text;
            Assert.AreEqual("1.930.000 ₫", totalPrice, "Tổng giá không đúng sau khi tăng số lượng sản phẩm!");
        }

        [Test]
        public void Test_Decrease_Product_Quantity()
        {
            // 1. Truy cập trực tiếp vào sản phẩm "Nike Air Jordan 1 Low Brown Elephant-Like Auth"
            driver.Navigate().GoToUrl(productUrl);

            // 2. Chọn size từ UX Swatches (Chọn size 43 trong ví dụ này)
            var sizeOption = WaitForElement(By.CssSelector(".ux-swatch[data-value='43']"));
            sizeOption.Click(); // Nhấn vào size 43
            var quantityProduct = WaitForElement(By.CssSelector("input.input-text.qty"));
            quantityProduct.Clear(); // Xóa giá trị mặc định
            quantityProduct.SendKeys("2");
            // 3. Thêm sản phẩm vào giỏ hàng
            var addToCartButton = WaitForElement(By.CssSelector("button.single_add_to_cart_button"));
            addToCartButton.Click();
            driver.Navigate().GoToUrl(cartUrl);

            var quantityInput = WaitForElement(By.CssSelector("input.input-text.qty"));
            quantityInput.Clear();
            quantityInput.SendKeys("1");

            var updateButton = WaitForElement(By.CssSelector("button[name='update_cart']"));
            updateButton.Click();

            Thread.Sleep(2000); // Chờ cập nhật xong
            var updateMessage = WaitForElement(By.CssSelector(".woocommerce-message .message-container"));
            string messageText = updateMessage.Text;

            // 9. Kiểm tra thông báo cập nhật giỏ hàng
            Assert.IsTrue(messageText.Contains("Giỏ hàng đã được cập nhật"),
                          "Thông báo cập nhật giỏ hàng không chính xác!");

            // Kiểm tra tổng giá
            var totalPrice = WaitForElement(By.CssSelector(".order-total .woocommerce-Price-amount")).Text;
            Assert.AreEqual("995.000 ₫", totalPrice, "Tổng giá không đúng sau khi giảm số lượng sản phẩm!");
        }
        [Test]
        public void Test_Empty_Cart_Message()
        {
            // 1. Truy cập vào trang giỏ hàng
            driver.Navigate().GoToUrl(cartUrl);

            try
            {
                // 2. Kiểm tra thông báo khi giỏ hàng trống
                var emptyMessage = WaitForElement(By.CssSelector(".wc-empty-cart-message .message-container"));
                string messageText = emptyMessage.Text;

                // 3. So sánh với thông báo mong đợi
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
            // 1. Truy cập trực tiếp vào sản phẩm "Nike Air Jordan 1 Low Brown Elephant-Like Auth"
            driver.Navigate().GoToUrl(productUrl);

            // 2. Chọn size từ UX Swatches (Chọn size 43 trong ví dụ này)
            var sizeOption = WaitForElement(By.CssSelector(".ux-swatch[data-value='43']"));
            sizeOption.Click(); // Nhấn vào size 43

            // 3. Thêm sản phẩm vào giỏ hàng
            var addToCartButton = WaitForElement(By.CssSelector("button.single_add_to_cart_button"));
            addToCartButton.Click();

            // 4. Truy cập vào trang giỏ hàng
            driver.Navigate().GoToUrl(cartUrl);

            // 5. Xóa sản phẩm khỏi giỏ hàng
            var removeButton = WaitForElement(By.CssSelector(".product-remove a"));
            removeButton.Click();

            // 6. Kiểm tra thông báo xác nhận xóa sản phẩm
            var removeMessage = WaitForElement(By.CssSelector(".woocommerce-message .message-container"));
            string removeMessageText = removeMessage.Text;
            Assert.IsTrue(removeMessageText.Contains("“Giày Nike Air Jordan 1 Low 'Brown Elephant' - Like Auth” đã xóa"),
                          "Thông báo xác nhận xóa sản phẩm không chính xác!");

            // 7. Kiểm tra thông báo giỏ hàng trống
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
                driver.Quit(); // Đóng trình duyệt
                Console.WriteLine("Chương trình đã kết thúc và trình duyệt đã được đóng.");
            }
        }
    }
}
