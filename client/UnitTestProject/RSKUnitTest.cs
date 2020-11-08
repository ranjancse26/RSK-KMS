using RSKKMS.Lib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using Nethereum.Web3.Accounts;

namespace UnitTestProject
{
    [TestClass]
    public class RSKUnitTest : BaseUnitTest
    {
        [TestInitialize]
        public void Init()
        {
            SetUpConfiguration();
        }

        [TestMethod]
        public void RskService_SendTransaction_Complete_Successfully()
        {
            // Arrange
            string testNodeUrl = ConfigurationManager.AppSettings["RskTestnetNodeUrl"];
            string privateKey = ConfigurationManager.AppSettings["PrivateKey"];
            string fromTransferPrivateKey = ConfigurationManager.AppSettings["FromTransferPrivateKey"];
            Account account = new Account(privateKey);

            IRskService rskService = new RskService(testNodeUrl,
                fromTransferPrivateKey);

            // Act
            string transactionHex = rskService.SendTransaction(account.Address, 0.001m, 0.06m);
            
            // Assert
            Assert.IsTrue(transactionHex.Length > 0);
        }
    }
}
