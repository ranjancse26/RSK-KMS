using RSKKMS.Lib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class RSKWalletUnitTest : BaseUnitTest
    {
        [TestInitialize]
        public void Init()
        {
            SetUpConfiguration();
        }

        [TestMethod]
        public void RskWalletService_GetBalance_For_Address()
        {
            // Arrange
            IRskWalletService rskWalletService = new RskWalletService(true);
            
            // Act
            decimal balance = rskWalletService.GetRskBalance("0x182bc5E65C957e05ECe67bfd76465F2dbc0eF36E");
            
            // Assert
            Assert.IsTrue(balance > 0.0m);
        }
    }
}
