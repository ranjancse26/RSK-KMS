using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using System.Numerics;

namespace RSKKMS.Lib.Security
{
    public class ERC20Helper
    {
        /// <summary>
        /// The ERC30 contract deployment function
        /// </summary>
        /// <param name="web3">Web3</param>
        /// <param name="abi">ABI</param>
        /// <param name="bytecode">Bytecode</param>
        /// <param name="address">Address</param>
        /// <param name="gas">Gas - Ex: 900000</param>
        /// <param name="totalSupply">Total Supply</param>
        /// <returns>ContractAddress</returns>
        public static string DeployERC20Contract(Web3 web3, string abi,
            string bytecode, string address,
            HexBigInteger gas, BigInteger totalSupply)
        {
            var receipt = web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(abi, bytecode, address,
                    gas, null, totalSupply)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            return receipt.ContractAddress;
        }
    }
}
