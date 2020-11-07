using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace RSKKMS.Lib.Services
{
    public interface IRskService
    {
        string SendTransaction(string to,
            decimal amount, decimal gas);
    }

    /// <summary>
    /// Reused and re-factored code from
    /// https://github.com/alebanzas/RskGasPrice/blob/master/GasPrice.Data/Services/RskService.cs
    /// </summary>
    public class RskService : IRskService
    {
        private string privateKey;
        private string nodeUrl;

        public RskService(string nodeUrl,
            string privateKey)
        {
            this.privateKey = privateKey;
            this.nodeUrl = nodeUrl;
            var account = new Account(privateKey);
            Web3Provider = new Web3(account,
                nodeUrl);
        }

        internal Web3 Web3Provider { get; set; }

        public string SendTransaction(string to,
            decimal amount, decimal gas)
        {
            string transactionHash = Web3Provider.Eth
                        .GetEtherTransferService()
                        .TransferEtherAsync(to, amount, gas)
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult();
            return transactionHash;
        }
    }
}
