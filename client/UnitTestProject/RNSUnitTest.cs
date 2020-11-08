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
            SetConfiguration();
        }

        [TestMethod]
        public void RskRnsResolverService_GetAddress_Must_Resolve()
        {
            string rnsAddress = "ranjancse.rsk";
            string hexAddress = "0x19ed7f7b7f0755fc91613c3a6d9b42cab7977b9f";
                       
            IRskRnsResolverService rskRnsResolverService = new RskRnsResolverService(true);
            string resolvedAddress = rskRnsResolverService
                .GetAddress(rnsAddress)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            Assert.IsTrue(resolvedAddress.ToString() == hexAddress);
        }
    }
}
