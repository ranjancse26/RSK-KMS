using System;
using Nethereum.Hex.HexTypes;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Web3;
using Nethereum.Web3.Accounts; 

namespace RSKKMS.Lib.Security
{
    public class RSKAccount
    {
        public string Address { get; set; }
        public string PrivateKey { get; set; }
    }

    public class AccountHelper
    {
        public static HexBigInteger GetBalance(Web3 web3, Account account)
        {
            var weiBalance = web3.Eth.GetBalance
                .SendRequestAsync(account.Address)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            return weiBalance;
        }

        public static RSKAccount CreateNewAccount()
        {
            try
            {
                var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
                var privateKey = ecKey.GetPrivateKeyAsBytes().ToHex();
                var account = new Nethereum.Web3.Accounts.Account(privateKey);

                return new RSKAccount
                {
                    Address = account.Address.ToLower(),
                    PrivateKey = account.PrivateKey
                };
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
            }
            return null;
        }
    }
}
