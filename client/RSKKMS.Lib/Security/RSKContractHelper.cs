using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.Eth.Services;
using Nethereum.Web3;
using RSKKMS.Lib.KeyManagement;
using System.Threading.Tasks;

namespace RSKKMS.Lib.Security
{
    public class RSKContractHelper
    {
        /// <summary>
        /// https://github.com/Nethereum/Nethereum/blob/master/src/Nethereum.StandardTokenEIP20.IntegrationTests/Erc20TokenTester.cs
        /// </summary>
        /// <param name="transactionService"></param>
        /// <param name="transactionHash"></param>
        /// <returns></returns>
        public static async Task<TransactionReceipt> GetTransactionReceiptAsync(
          EthApiTransactionsService transactionService, string transactionHash)
        {
            TransactionReceipt receipt = null;
            //wait for the contract to be mined to the address
            while (receipt == null)
            {
                await Task.Delay(1000);
                receipt = await transactionService.GetTransactionReceipt.SendRequestAsync(transactionHash);
            }
            return receipt;
        }

        public static ContractHandler DeployRSKKeyManagmentContract(Web3 web3,
            TransactionReceipt transactionReceiptDeployment,
            out string contractAddress)
        {
            var rSKKeyManagementDeployment = new RSKKeyManagementDeployment();

            transactionReceiptDeployment = web3.Eth.GetContractDeploymentHandler<RSKKeyManagementDeployment>()
                .SendRequestAndWaitForReceiptAsync(rSKKeyManagementDeployment)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            contractAddress = transactionReceiptDeployment.ContractAddress;
            var contractHandler = web3.Eth.GetContractHandler(contractAddress);
            return contractHandler;
        }

        public static void DeployStringUtilContract(Web3 web3, out TransactionReceipt transactionReceiptDeployment, out string contractAddress, out ContractHandler contractHandler)
        {
            var stringUtilsDeployment = new StringUtilsDeployment();
            transactionReceiptDeployment = web3.Eth.GetContractDeploymentHandler<StringUtilsDeployment>()
                .SendRequestAndWaitForReceiptAsync(stringUtilsDeployment)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            contractAddress = transactionReceiptDeployment.ContractAddress;
            contractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public static void DeployIterableMappingContract(Web3 web3, out TransactionReceipt transactionReceiptDeployment, out string contractAddress, out ContractHandler contractHandler)
        {
            var iterableMappingDeployment = new IterableMappingDeployment();

            transactionReceiptDeployment = web3.Eth.GetContractDeploymentHandler<IterableMappingDeployment>()
                .SendRequestAndWaitForReceiptAsync(iterableMappingDeployment)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            contractAddress = transactionReceiptDeployment.ContractAddress;
            contractHandler = web3.Eth.GetContractHandler(contractAddress);
        }
    }
}
