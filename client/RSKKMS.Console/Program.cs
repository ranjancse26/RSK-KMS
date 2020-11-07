using System.Numerics;
using System.Diagnostics;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.ContractHandlers;

using RSKKMS.Lib.Security;
using RSKKMS.Lib.Services;
using RSKKMS.Lib.KeyManagement;

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

            string rnsResolvedAddress = GetRnsResolvedAddress("ranjancse.rsk", true);

            string nodeUrl = ConfigurationManager.AppSettings["RskTestnetNodeUrl"];
            var privateKey = ConfigurationManager.AppSettings["PrivateKey"];
            var fromTransferPrivateKey = ConfigurationManager.AppSettings["FromTransferPrivateKey"]; 
            var account = new Nethereum.Web3.Accounts.Account(privateKey);

            IGasPriceService gasPriceService = new GasPriceService(nodeUrl);
            int gasPrice = gasPriceService.GetRskMinGasPrice();

            // Load some RBTC
            LoadSomeRBTC(nodeUrl, fromTransferPrivateKey,
                account.Address, 0.001m, 0.06m);

            Stopwatch stopwatch = new Stopwatch();

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
            Web3 web3 = new Web3(account, url);

            // Get the balance
            stopwatch.Start();
            var weiBalance = AccountHelper.GetBalance(web3, account);
            var etherAmount = Web3.Convert.FromWei(weiBalance.Value);
            stopwatch.Stop();

            System.Console.WriteLine($"Account Balance: {etherAmount}");
            System.Console.WriteLine($"Time take to fetch the balance:" +
                $" {stopwatch.Elapsed.Seconds} seconds");

            // Gas estimated, in wei
            System.Console.WriteLine($"Estimated Gas Price: {gasPrice}");

            System.Console.WriteLine("Deploying the Iterable Mapping Library");
            stopwatch.Start();

            // Deploy Iterable Mapping Library
            TransactionReceipt transactionReceiptDeployment;
            string contractAddress;
            ContractHandler contractHandler;

            RSKContractHelper.DeployIterableMappingContract(web3,
                out transactionReceiptDeployment,
                out contractAddress,
                out contractHandler);
            stopwatch.Stop();

            System.Console.WriteLine($"Iterable Mapping Contarct Address: " +
                $"{contractAddress}");
            System.Console.WriteLine($"Time taken to deploy the Iterable mapping:" +
                $" {stopwatch.Elapsed.Seconds} seconds");

            System.Console.WriteLine("Deploying the RSK KMS Contract");

            // Deploy the RSK Contract
            stopwatch.Start();
            contractHandler = RSKContractHelper.DeployRSKKeyManagmentContract(web3,
                transactionReceiptDeployment,
                out contractAddress);
            stopwatch.Stop();
            System.Console.WriteLine($"Time taken to deploy the RSK Contract: " +
                $"{stopwatch.Elapsed.Seconds} seconds");

            System.Console.WriteLine("Trying to set a value in RSK KMS Contract");

            /** Function: setItem**/
            var setItemRequest = new SetItemFunction
            {
                Key = key,
                Value = encryptedText,
                FromAddress = account.Address
            };

            setItemRequest.GasPrice = new BigInteger(gasPrice);

            stopwatch.Start();
            var setItemFunctionTxnReceipt = contractHandler
                .SendRequestAndWaitForReceiptAsync(setItemRequest)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            stopwatch.Stop();
            System.Console.WriteLine($"Time taken to set the KMS Key Item: " +
                $"{stopwatch.Elapsed.Seconds} seconds");

            System.Console.WriteLine("Trying to get a value from the RSK KMS Contract");

            /** Function: getItem**/
            var getItemRequest = new GetItemFunction
            {
                Key = key,
                FromAddress = account.Address
            };

            stopwatch.Start();
            var getItemResponse = contractHandler
                .QueryAsync<GetItemFunction, string>(getItemRequest)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            stopwatch.Stop();
            System.Console.WriteLine($"Time taken to get the KMS Key Item: " +
                $"{stopwatch.Elapsed.Seconds} seconds");

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


        /// <summary>
        /// Load some RBTC
        /// </summary>
        /// <param name="nodeUrl">Node Url</param>
        /// <param name="privateKey">Private Key</param>
        /// <param name="address">Address</param>
        /// <param name="amount">Amount</param>
        /// <param name="gas">Gas</param>
        private static void LoadSomeRBTC(string nodeUrl,
            string privateKey, string address,
            decimal amount,
            decimal gas)
        {
            IRskService rskService = new RskService(nodeUrl, privateKey);
            rskService.SendTransaction(address, amount, gas);
        }

        /// <summary>
        /// Get the Rns Resolved Address
        /// </summary>
        /// <param name="rnsAddress">RNS Address</param>
        /// <returns>Hex Address</returns>
        private static string GetRnsResolvedAddress(string rnsAddress,
            bool isTestNet)
        {
            IRskRnsResolverService rskRnsResolverService = new RskRnsResolverService(isTestNet);
            string resolvedAddress = rskRnsResolverService
                .GetAddress(rnsAddress)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            return resolvedAddress;
        }
    }
}
