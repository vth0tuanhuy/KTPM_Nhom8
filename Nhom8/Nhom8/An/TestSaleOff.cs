﻿using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Diagnostics;

namespace Nhom8.An
{
    [TestFixture]
    internal class TestSaleOff
    {
        //Khai báo
        private IWebDriver driver;
        private string Url = "https://yoyo1sneaker.com/";

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }
        [Test]
        public void TC21_testSale()
        {
            //Vào trang yoyo1sneaker.com
            driver.Navigate().GoToUrl(Url);
            //Click "Sale Off"
            IWebElement saleOff_li = driver.FindElement(By.XPath("//*[@id=\"menu-item-15013\"]/a"));
            saleOff_li.Click();
            //Click checkbox Nike Air
            IWebElement checkbox1 = driver.FindElement(By.XPath("//*[@id=\"woof_widget-2\"]/div/div/div/div[2]/div[2]/div/ul/li[1]/div/ins"));
            checkbox1.Click();
            Thread.Sleep(2000);
            //Kiểm tra đúng danh mục không
            var dmCheck = driver.FindElement(By.CssSelector("#main > div > div.col.large-9 > div > div.woof_products_top_panel > ul > li:nth-child(2) > ul > li:nth-child(2) > a > span")).Text;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Asics", dmCheck, "Lỗi tìm danh mục sai!!!");
            IWebElement btnXoaTuyChon = driver.FindElement(By.XPath("//*[@id=\"woof_widget-2\"]/div/div/div/div[1]/button"));
            btnXoaTuyChon.Click();
            Thread.Sleep(2000);
            driver.Quit();
        }
        [Test]
        public void TC22_testSale()
        {
            driver.Navigate().GoToUrl(Url);
            Actions actions = new Actions(driver);
            IWebElement saleOff_li = driver.FindElement(By.XPath("//*[@id=\"menu-item-15013\"]/a"));
            actions.MoveToElement(saleOff_li).Perform();
            Thread.Sleep(2000);
            IWebElement nike = driver.FindElement(By.XPath("//*[@id=\"menu-item-1054\"]/a"));
            nike.Click();
            Thread.Sleep(2000);
            IWebElement nike_air = driver.FindElement(By.XPath("//*[@id=\"woof_widget-2\"]/div/div/div/div[2]/div[2]/div/ul/li[1]/div/ins"));
            nike_air.Click();
            Thread.Sleep(2000);
            var dmCheck = driver.FindElement(By.XPath("//*[@id=\"main\"]/div/div[2]/div/div[2]/ul/li[2]/ul/li[2]/a/span")).Text;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Nike Air Force 1", dmCheck, "Lỗi tìm danh mục sai!!!");
            Thread.Sleep(5000);
            driver.Quit();
        }
        [Test]
        public void TC23_testSale()
        {
            driver.Navigate().GoToUrl(Url);
            Actions actions = new Actions(driver);
            IWebElement saleOff_li = driver.FindElement(By.XPath("//*[@id=\"menu-item-15013\"]/a"));
            actions.MoveToElement(saleOff_li).Perform();
            Thread.Sleep(2000);
            IWebElement nike_air = driver.FindElement(By.XPath("//*[@id=\"menu-item-1055\"]/a"));
            nike_air.Click();
            Thread.Sleep(2000);
            //Kiểm tra đúng danh mục không
            var dmCheck = driver.FindElement(By.XPath("//*[@id=\"wrapper\"]/div/div/div[1]/div[1]/nav")).Text;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("TRANG CHỦ / NIKE / NIKE AIR FORCE 1", dmCheck, "Lỗi tìm danh mục sai!!!");
            Thread.Sleep(5000);
            driver.Quit();
        }
        [Test]
        public void TC24_testSale()
        {
            //Vào trang yoyo1sneaker.com
            driver.Navigate().GoToUrl(Url);
            //Click "Sale Off"
            IWebElement saleOff_li = driver.FindElement(By.XPath("//*[@id=\"menu-item-15013\"]/a"));
            saleOff_li.Click();
            Thread.Sleep(2000);
            //Click checkbox Nike Air
            IWebElement checkbox1 = driver.FindElement(By.XPath("//*[@id=\"woof_widget-2\"]/div/div/div/div[2]/div[2]/div/ul/li[1]/div/ins"));
            checkbox1.Click();
            Thread.Sleep(2000);
            //Kiểm tra đúng danh mục không
            var dmCheck = driver.FindElement(By.CssSelector("#main > div > div.col.large-9 > div > div.woof_products_top_panel > ul > li:nth-child(2) > ul > li:nth-child(2) > a > span")).Text;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Asics", dmCheck, "Lỗi tìm danh mục sai!!!");
            IWebElement btnXoaTuyChon = driver.FindElement(By.XPath("//*[@id=\"woof_widget-2\"]/div/div/div/div[1]/button"));
            btnXoaTuyChon.Click();
            Thread.Sleep(5000);
            driver.Quit();
        }
        [Test]
        public void TC25_testSale()
        {
            //Vào trang yoyo1sneaker.com
            driver.Navigate().GoToUrl(Url);
            //Click "Sale Off"
            IWebElement saleOff_li = driver.FindElement(By.XPath("//*[@id=\"menu-item-15013\"]/a"));
            saleOff_li.Click();
            Thread.Sleep(2000);
            IWebElement choicePlus = driver.FindElement(By.XPath("//*[@id=\"woof_widget-2\"]/div/div/div/div[2]/div[2]/div/ul/li[3]/a/span"));
            choicePlus.Click();
            Thread.Sleep(2000);
            //Click checkbox Nike Air
            IWebElement checkbox = driver.FindElement(By.XPath("//*[@id=\"woof_widget-2\"]/div/div/div/div[2]/div[2]/div/ul/li[3]/ul/li[1]/div/ins"));
            checkbox.Click();
            Thread.Sleep(2000);
            //Kiểm tra đúng danh mục không
            var dmCheck = driver.FindElement(By.CssSelector("#main > div > div.col.large-9 > div > div.woof_products_top_panel > ul > li:nth-child(2) > ul > li:nth-child(2) > a > span")).Text;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Nike Air Force 1", dmCheck, "Lỗi tìm danh mục sai!!!");
            Thread.Sleep(5000);
            driver.Quit();
        }
    }
}