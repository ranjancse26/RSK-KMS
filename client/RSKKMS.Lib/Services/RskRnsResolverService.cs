using System;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Configuration;
using Nethereum.ENS;
using Nethereum.ENS.ENSRegistry.ContractDefinition;

namespace RSKKMS.Lib.Services
{
    public interface IRskRnsResolverService
    {
        Task<string> GetAddress(string accountDomain);
    }

    /// <summary>
    /// Reused code https://github.com/jonathansmirnoff/RskMobileWallet
    /// Extended for resolving the MainNet vs TestNet Registry
    /// </summary>
    public class RskRnsResolverService : IRskRnsResolverService
    {
        private bool isTestNet;
        private static string RskMainnetNodeUrl { get; } = ConfigurationManager.AppSettings["RskMainnetNodeUrl"].ToString();
        private string RnsMainNetRegistry { get; } = ConfigurationManager.AppSettings["RnsMainNetRegistry"].ToString();

        private static string RskTestnetNodeUrl { get; } = ConfigurationManager.AppSettings["RskTestnetNodeUrl"].ToString();
        private string RnsTestNetRegistry { get; } = ConfigurationManager.AppSettings["RnsTestNetRegistry"].ToString();
      
        private Web3 Web3Client { get; }

        public RskRnsResolverService(bool isTestNet)
        {
            this.isTestNet = isTestNet;
            Web3Client = isTestNet ? 
                new Web3(RskTestnetNodeUrl) : 
                new Web3(RskMainnetNodeUrl);
        }

        public RskRnsResolverService(Web3 web3Client)
        {
            Web3Client = web3Client;
        }

        public async Task<string> GetAddress(string accountDomain)
        {
            if (!IsValidDomain(accountDomain)) throw new ArgumentException("Invalid name.", nameof(accountDomain));
            
            try
            {
                ENSRegistryService ensRegistryService = isTestNet ? 
                    new ENSRegistryService(Web3Client, RnsTestNetRegistry) : new ENSRegistryService(Web3Client, RnsMainNetRegistry);
                var fullNameNode = new EnsUtil().GetNameHash(accountDomain);

                var resolverAddress = await ensRegistryService.ResolverQueryAsync(
                    new ResolverFunction() { Node = fullNameNode.HexToByteArray() }).ConfigureAwait(false);

                var resolverService = new PublicResolverService(Web3Client, resolverAddress);
                var theAddress =
                    await resolverService.AddrQueryAsync(fullNameNode.HexToByteArray()).ConfigureAwait(false);

                return theAddress;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private static bool IsValidDomain(string accountDomain)
        {
            //TODO: implement
            return true;
        }
    }
}