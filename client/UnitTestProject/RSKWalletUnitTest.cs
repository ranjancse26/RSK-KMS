using RSKKMS.Lib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nethereum.Web3.Accounts;
using System.Configuration;

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
            string privateKey = ConfigurationManager.AppSettings["PrivateKey"];
            Account account = new Account(privateKey);

            // Act
            decimal balance = rskWalletService.GetRskBalance(account.Address);
            
            // Assert
            Assert.IsTrue(balance > 0.0m);
        }
    }
}
