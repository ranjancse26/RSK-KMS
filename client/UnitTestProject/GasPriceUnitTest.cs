using RSKKMS.Lib.Services;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class GasPriceUnitTest : BaseUnitTest
    {
        [TestInitialize]
        public void Init()
        {
            SetUpConfiguration();
        }

        [TestMethod]
        public void GasPriceService_GetRskMinGasPrice()
        {
            // Arrange
            IGasPriceService gasPriceService = new GasPriceService(ConfigurationManager.AppSettings["RskTestnetNodeUrl"]);
            
            // Act
            int gasPrice = gasPriceService.GetRskMinGasPrice();

            // Assert
            Assert.IsTrue(gasPrice > 0);
        }
    }
}
