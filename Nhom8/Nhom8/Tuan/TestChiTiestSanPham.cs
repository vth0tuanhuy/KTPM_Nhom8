using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Diagnostics;

namespace Tuan
{
    [TestFixture]
    internal class TestChiTiestSanPham
    {
        // Khai báo
        private IWebDriver driver;
        private string Url = "https://yoyo1sneaker.com/";

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void xemChiTietSanPhamTheoDanhMuc()
        {
            // Vào trang chủ
            driver.Navigate().GoToUrl(Url);

            // Click vào "Sản phẩm"
            IWebElement sanPham_li = driver.FindElement(By.XPath("//*[@id=\"menu-item-35\"]/a"));
            sanPham_li.Click();
            Thread.Sleep(2000);

            // Chọn danh mục "Asics"
            IWebElement checkboxDanhMuc = driver.FindElement(By.XPath("//*[@id=\"woof_widget-2\"]/div/div/div/div[2]/div[2]/div/ul/li[1]/div/ins"));
            checkboxDanhMuc.Click();
            Thread.Sleep(2000);

            // Click vào sản phẩm đầu tiên trong danh mục
            IWebElement sanPham = driver.FindElement(By.CssSelector(".products .product:nth-child(1) .box-image"));
            sanPham.Click();
            Thread.Sleep(2000);

            // Kiểm tra tiêu đề sản phẩm hiển thị
            var productTitle = driver.FindElement(By.CssSelector(".product-title")).Text;
            Assert.IsNotEmpty(productTitle, "Lỗi: Không tìm thấy tiêu đề sản phẩm.");
        }

        [Test]
        public void xemChiTietSanPhamTheoSize()
        {
            // Vào trang chủ
            driver.Navigate().GoToUrl(Url);

            // Click vào "Sản phẩm"
            IWebElement sanPham_li = driver.FindElement(By.XPath("//*[@id=\"menu-item-35\"]/a"));
            sanPham_li.Click();
            Thread.Sleep(2000);

            // Chọn size 36
            IWebElement checkboxSize = driver.FindElement(By.XPath("//*[@id=\"woof_widget-2\"]/div/div/div/div[3]/div[2]/div/ul/li[1]/div/ins"));
            checkboxSize.Click();
            Thread.Sleep(2000);

            // Click vào sản phẩm đầu tiên theo size
            IWebElement sanPham = driver.FindElement(By.CssSelector(".products .product:nth-child(1) .box-image"));
            sanPham.Click();
            Thread.Sleep(2000);

            // Kiểm tra tiêu đề sản phẩm hiển thị
            var productTitle = driver.FindElement(By.CssSelector(".product-title")).Text;
            Assert.IsNotEmpty(productTitle, "Lỗi: Không tìm thấy tiêu đề sản phẩm.");
        }

        [Test]
        public void xemChiTietSanPham()
        {
            // Vào trang chủ
            driver.Navigate().GoToUrl(Url);

            // Click vào "Sản phẩm"
            IWebElement sanPham_li = driver.FindElement(By.XPath("//*[@id=\"menu-item-35\"]/a"));
            sanPham_li.Click();
            Thread.Sleep(2000);

            // Click vào sản phẩm bất kỳ
            IWebElement sanPham = driver.FindElement(By.CssSelector(".products .product:nth-child(1) .box-image"));
            sanPham.Click();
            Thread.Sleep(2000);

            // Kiểm tra tiêu đề sản phẩm hiển thị
            var productTitle = driver.FindElement(By.CssSelector(".product-title")).Text;
            Assert.IsNotEmpty(productTitle, "Lỗi: Không tìm thấy tiêu đề sản phẩm.");
        }

        [Test]
        public void xemChiTietSanPhamTheoDanhMucVaSize()
        {
            // Vào trang chủ
            driver.Navigate().GoToUrl(Url);

            // Click vào "Sản phẩm"
            IWebElement sanPham_li = driver.FindElement(By.XPath("//*[@id=\"menu-item-35\"]/a"));
            sanPham_li.Click();
            Thread.Sleep(2000);

            // Chọn danh mục "Asics"
            IWebElement checkboxDanhMuc = driver.FindElement(By.XPath("//*[@id=\"woof_widget-2\"]/div/div/div/div[2]/div[2]/div/ul/li[1]/div/ins"));
            checkboxDanhMuc.Click();
            Thread.Sleep(2000);

            // Chọn size 36
            IWebElement checkboxSize = driver.FindElement(By.XPath("//*[@id=\"woof_widget-2\"]/div/div/div/div[3]/div[2]/div/ul/li[1]/div/ins"));
            checkboxSize.Click();
            Thread.Sleep(2000);

            // Click vào sản phẩm đầu tiên trong danh mục và size
            IWebElement sanPham = driver.FindElement(By.CssSelector(".products .product:nth-child(1) .box-image"));
            sanPham.Click();
            Thread.Sleep(2000);

            // Kiểm tra tiêu đề sản phẩm hiển thị
            var productTitle = driver.FindElement(By.CssSelector(".product-title")).Text;
            Assert.IsNotEmpty(productTitle, "Lỗi: Không tìm thấy tiêu đề sản phẩm.");
        }


        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
