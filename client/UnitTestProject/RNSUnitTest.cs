using RSKKMS.Lib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class RNSUnitTest : BaseUnitTest
    {
        [TestInitialize]
        public void Init()
        {
            SetUpConfiguration();
        }

        [TestMethod]
        public void RskRnsResolverService_GetAddress_Must_Resolve()
        {
            // Arrange
            string rnsAddress = "ranjancse.rsk";
            string hexAddress = "0x19ed7f7b7f0755fc91613c3a6d9b42cab7977b9f";
                       
            IRskRnsResolverService rskRnsResolverService = new RskRnsResolverService(true);

            // Act
            string resolvedAddress = rskRnsResolverService
                .GetAddress(rnsAddress)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.IsTrue(resolvedAddress.ToString() == hexAddress);
        }
    }
}
