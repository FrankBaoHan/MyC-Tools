using Microsoft.VisualStudio.TestTools.UnitTesting;
using EAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP.Tests
{
    [TestClass()]
    public class ModbusHelperTests
    {
        [TestMethod()]
        public void getReadCmdTest()
        {
            byte[] cmd = ModbusHelper.getReadCmd(2, 3, 513, 10);
            System.Console.WriteLine(cmd);
            Assert.Fail();
        }
    }
}