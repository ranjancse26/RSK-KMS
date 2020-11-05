using System;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Configuration;
using Nethereum.ENS.ENSRegistry.ContractDefinition;
using Nethereum.ENS;

namespace RSKKMS.Lib.Services
{
    public interface IRskRnsResolverService
    {
        Task<string> GetAddress(string accountDomain, bool isTestNet);
    }

    /// <summary>
    /// Reused code https://github.com/jonathansmirnoff/RskMobileWallet
    /// Extended for resolving the MainNet vs TestNet Registry
    /// </summary>
    public class RskRnsResolverService : IRskRnsResolverService
    {
        private static string RskMainnetNodeUrl { get; } = ConfigurationManager.AppSettings["RskMainnetNodeUrl"].ToString();
        private string RnsMainNetRegistry { get; } = ConfigurationManager.AppSettings["RnsMainNetRegistry"].ToString();

        private static string RskTestnetNodeUrl { get; } = ConfigurationManager.AppSettings["RskTestnetNodeUrl"].ToString();
        private string RnsTestNetRegistry { get; } = ConfigurationManager.AppSettings["RnsTestNetRegistry"].ToString();
      
        private Web3 Web3Client { get; }

        public RskRnsResolverService(bool isTestNet)
        {
            Web3Client = isTestNet ? 
                new Web3(RskTestnetNodeUrl) : 
                new Web3(RskMainnetNodeUrl);
        }

        public RskRnsResolverService(Web3 web3Client)
        {
            Web3Client = web3Client;
        }

        public async Task<string> GetAddress(string accountDomain, bool isTestNet)
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
                return null;
            }
        }

        private static bool IsValidDomain(string accountDomain)
        {
            //TODO: implement
            return true;
        }
    }
}