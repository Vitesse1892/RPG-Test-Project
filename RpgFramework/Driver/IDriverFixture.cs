using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgFramework.Driver
{
    public interface IDriverFixture
    {
        IWebDriver Driver { get; }
    }
}
