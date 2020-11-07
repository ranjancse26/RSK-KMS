using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RSKKMS.Lib.Services
{
    public interface IGasPriceService
    {
        int GetRskMinGasPrice();
    }

    public class RskJsonRpcResultModel
    {
        public string number { get; set; }
        public string hash { get; set; }
        public string parentHash { get; set; }
        public string sha3Uncles { get; set; }
        public string logsBloom { get; set; }
        public string transactionsRoot { get; set; }
        public string stateRoot { get; set; }
        public string receiptsRoot { get; set; }
        public string miner { get; set; }
        public string difficulty { get; set; }
        public string totalDifficulty { get; set; }
        public string extraData { get; set; }
        public string size { get; set; }
        public string gasLimit { get; set; }
        public string gasUsed { get; set; }
        public string timestamp { get; set; }
        public List<string> transactions { get; set; }
        public List<string> uncles { get; set; }
        public string minimumGasPrice { get; set; }
    }

    public class RskJsonRpcModel
    {
        public string jsonrpc { get; set; }
        public int id { get; set; }
        public RskJsonRpcResultModel result { get; set; }
    }

    public class GasPriceService : IGasPriceService
    {
        private string nodeUrl;

        public GasPriceService(string nodeUrl)
        {
            this.nodeUrl = nodeUrl;
        }

        /// <summary>
        /// Refactored code - Based on 
        /// https://github.com/alebanzas/RskGasPrice/blob/master/GasPrice.Data/Services/GasPriceService.cs
        /// </summary>
        /// <returns>Gas Price</returns>
        public int GetRskMinGasPrice()
        {
            /*
             curl https://public-node.rsk.co 
                -X POST 
                -H "Content-Type: application/json" 
                --data '{"jsonrpc":"2.0","method":"eth_blockNumber","params":[],"id":1}'
            */

            var jsonData = "{\"jsonrpc\":\"2.0\",\"method\":\"eth_getBlockByNumber\",\"params\":[\"latest\",false],\"id\":1}";
            string response;

            using (var client = new WebClient())
            {
                client.Headers.Add("content-type", "application/json");
                response = client.UploadString(nodeUrl, jsonData);
            }

            var rskJsonRpcModel = JsonConvert.DeserializeObject<RskJsonRpcModel>(response);
            var mgp = Convert.ToInt32(rskJsonRpcModel.result.minimumGasPrice, 16);

            return mgp;
        }
    }
}
