using Nethereum.Web3;
using System.Configuration;

namespace RSKKMS.Lib.Services
{
    public interface IRskWalletService
    {
        decimal GetRskBalance(string accountAddress);
    }

    /// <summary>
    /// Reused code https://github.com/jonathansmirnoff/RskMobileWallet
    /// Enhanced code to support for MainNet vs TestNet
    /// </summary>
    public class RskWalletService : IRskWalletService
    {
        private string RskTestnetNodeUrl { get; set; } = ConfigurationManager.AppSettings["RskTestnetNodeUrl"].ToString();
        private string RskMainnetNodeUrl { get; } = ConfigurationManager.AppSettings["RskMainnetNodeUrl"].ToString();
        private Web3 Web3Client { get; set; }

        public RskWalletService(bool isTestNet)
        {
            Web3Client = isTestNet ? new Web3(RskTestnetNodeUrl) 
                : new Web3(RskMainnetNodeUrl);
        }

        public decimal GetRskBalance(string accountAddress)
        {
            var weiBalance = Web3Client.Eth.GetBalance
               .SendRequestAsync(accountAddress)
               .ConfigureAwait(false)
               .GetAwaiter()
               .GetResult();
            return Web3.Convert.FromWei(weiBalance);
        }
    }
}