using System.Configuration;
using RSKKMS.Lib.KeyManagement;
using System.Security.Cryptography.X509Certificates;

using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using RSKKMS.Lib.Security;
using Nethereum.Contracts.ContractHandlers;

namespace RSKKMS.Console
{
    class Program
    {
        /// <summary>
        /// The AES Key/Value with the Private Key for Contract is for demonstration purpose only
        /// Feel free to use it.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            string key = "aesKey";
            string value = "testing";

            System.Console.WriteLine("Trying to pull the RSA certificate from the local store using the Thumbprint");

            // Get the certificate by Thumbprint
            string thumbPrint = ConfigurationManager.AppSettings["Thumbprint"].ToUpper();
            X509Certificate2 filteredCert = X509CertificateHelper.GetRSKCertificate(thumbPrint,
                StoreLocation.LocalMachine);

            if (filteredCert == null)
            {
                System.Console.WriteLine($"Unable to find the RSK certificate by Thumbprint: " +
                    $"{thumbPrint}");
                System.Console.ReadLine();
                return;
            }

            // Encrypt Text/Data
            var encryptedText = RSAEncryptionHelper.Encrypt(value, filteredCert);

            var url = ConfigurationManager.AppSettings["ContractDeploymentUrl"];
            var privateKey = ConfigurationManager.AppSettings["PrivateKey"]; ;
            var account = new Nethereum.Web3.Accounts.Account(privateKey);
            Web3 web3 = new Web3(account, url);

            // Get the balance
            var weiBalance = AccountHelper.GetBalance(web3, account);
            var etherAmount = Web3.Convert.FromWei(weiBalance.Value);
            System.Console.WriteLine($"Account Balance: {etherAmount}");

            System.Console.WriteLine("Deploying the Iterable Mapping Library");

            // Deploy Iterable Mapping Library
            TransactionReceipt transactionReceiptDeployment;
            string contractAddress;
            ContractHandler contractHandler;

            RSKContractHelper.DeployIterableMappingContract(web3,
                out transactionReceiptDeployment,
                out contractAddress,
                out contractHandler);

            System.Console.WriteLine($"Iterable Mapping Contarct Address: {contractAddress}");

            System.Console.WriteLine("Deploying the RSK KMS Contract");

            // Deploy the RSK Contract
            contractHandler = RSKContractHelper.DeployRSKKeyManagmentContract(web3,
                transactionReceiptDeployment,
                out contractAddress);

            System.Console.WriteLine("Trying to set a value in RSK KMS Contract");

            /** Function: setItem**/
            var setItemRequest = new SetItemFunction
            {
                Key = key,
                Value = encryptedText,
                FromAddress = account.Address
            };

            var estimate = contractHandler
                .EstimateGasAsync(setItemRequest)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            setItemRequest.Gas = estimate.Value;

            var setItemFunctionTxnReceipt = contractHandler
                .SendRequestAndWaitForReceiptAsync(setItemRequest)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            System.Console.WriteLine("Trying to get a value from the RSK KMS Contract");

            /** Function: getItem**/
            var getItemRequest = new GetItemFunction
            {
                Key = key,
                FromAddress = account.Address
            };

            var getItemResponse = contractHandler
                .QueryAsync<GetItemFunction, string>(getItemRequest)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            if (!string.IsNullOrEmpty(getItemResponse))
            {
                var decryptedText = RSAEncryptionHelper.Decrypt(getItemResponse, filteredCert);
                System.Console.WriteLine($"Decrypted Text: {decryptedText}");
            }
            else
            {
                System.Console.WriteLine("The KMS Response as empty");
            }

            System.Console.WriteLine("Press any key to exit");
            System.Console.ReadLine();
        }
    }
}
