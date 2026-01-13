using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace RpgUiTests.Extensions
{
    public static class WebElementExtensions
    {
        public static void SelectDropdownByText(this IWebElement element, string text)
        {
            SelectElement select = new SelectElement(element);
            select.SelectByText(text);
        }

        public static void SelectDropdownByValue(this IWebElement element, string value)
        {
            SelectElement select = new SelectElement(element);
            select.SelectByValue(value);
        }

        public static void ClearAndEnterText(this IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }
    }
}
