using System.Configuration;

namespace UnitTestProject
{
    public class BaseUnitTest
    {
        /// <summary>
        /// Set up the initial configurations required for unit testing
        /// </summary>
        public void SetUpConfiguration()
        {
            ConfigurationManager.AppSettings["ContractDeploymentUrl"] = "https://public-node.testnet.rsk.co/2.0.1/";
            ConfigurationManager.AppSettings["FromTransferPrivateKey"] = "0xd35c0b08802b06da57a6f14175620280cd437b4dfc2a94926e226a369eb8c111";
            ConfigurationManager.AppSettings["PrivateKey"] = "0xd8c5adcac8d465c5a2d0772b86788e014ddec516";
            ConfigurationManager.AppSettings["Thumbprint"] = "3d54e2bb9850bcd9e20edac0f5135dc939a05a69";

            ConfigurationManager.AppSettings["RnsMainNetRegistry"] = "0xcb868aeabd31e2b66f74e9a55cf064abb31a4ad5";
            ConfigurationManager.AppSettings["RnsTestNetRegistry"] = "0x7d284aaac6e925aad802a53c0c69efe3764597b8";
            ConfigurationManager.AppSettings["RskMainnetNodeUrl"] = "https://public-node.rsk.co";
            ConfigurationManager.AppSettings["RskTestnetNodeUrl"] = "https://public-node.testnet.rsk.co";
        }
    }
}
