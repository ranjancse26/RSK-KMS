// See <http://truffleframework.com/docs/advanced/configuration>
// to customize your Truffle configuration!
const fs = require('fs');
const HDWalletProvider = require('@truffle/hdwallet-provider');

const mnemonic = fs.readFileSync(".secret").toString().trim();
  if (!mnemonic || mnemonic.split(' ').length !== 12) {
    console.log('unable to retrieve mnemonic from .secret');
}

// adjust these as fit to get faster transaction speeds
// minimum value is 1.0, adjust higher to incentivise miners
const TESTNET_GAS_MULT = 1.1;
const MAINNET_GAS_MULT = 1.0;

//Update gas price Testnet
/* Run this first, to use the result in truffle-config:
  curl https://public-node.testnet.rsk.co/ -X POST -H "Content-Type: application/json" \
    --data '{"jsonrpc":"2.0","method":"eth_getBlockByNumber","params":["latest",false],"id":1}' \
    > .minimum-gas-price-testnet.json
*/
const gasPriceTestnetRaw = fs.readFileSync(".minimum-gas-price-testnet.json").toString().trim();
const minimumGasPriceTestnet = parseInt(JSON.parse(gasPriceTestnetRaw).result.minimumGasPrice, 16);
if (typeof minimumGasPriceTestnet !== 'number' || isNaN(minimumGasPriceTestnet)) {
  throw new Error('unable to retrieve network gas price from .gas-price-testnet.json');
}
console.log("Minimum gas price Testnet: " + minimumGasPriceTestnet);

//Update gas price Mainnet
/* Run this first, to use the result in truffle-config:
  curl https://public-node.rsk.co/ -X POST -H "Content-Type: application/json" \
    --data '{"jsonrpc":"2.0","method":"eth_getBlockByNumber","params":["latest",false],"id":1}' \
    > .minimum-gas-price-mainnet.json
*/
const gasPriceMainnetRaw = fs.readFileSync(".minimum-gas-price-mainnet.json").toString().trim();
const minimumGasPriceMainnet = parseInt(JSON.parse(gasPriceMainnetRaw).result.minimumGasPrice, 16);
if (typeof minimumGasPriceMainnet !== 'number' || isNaN(minimumGasPriceMainnet)) {
  throw new Error('unable to retrieve network gas price from .gas-price-mainnet.json');
}
console.log("Minimum gas price Mainnet: " + minimumGasPriceMainnet);

const testnetSeedPhrase = fs
  .readFileSync('.testnet.seed-phrase')
  .toString()
  .trim();
if (!testnetSeedPhrase || testnetSeedPhrase.split(' ').length !== 12) {
  throw new Error('unable to retrieve testnet seed phrase from .testnet.seed-phrase');
}

module.exports = {
  /**
   * Networks define how you connect to your ethereum client and let you set the
   * defaults web3 uses to send transactions. If you don't specify one truffle
   * will spin up a development blockchain for you on port 9545 when you
   * run `develop` or `test`. You can ask a truffle command to use a specific
   * network from the command line, e.g
   *
   * $ truffle test --network <network-name>
   */

  networks: {
    develop: {
      port: 7545
    },   
    rskTestnet: {
       provider: () => new HDWalletProvider(mnemonic, 
         'https://public-node.testnet.rsk.co'),
       network_id: 31,
       gasPrice: 60000000
    },
    regtest: {
      host: '127.0.0.1',
      port: 4444,
      network_id: 31,
      networkCheckTimeout: 1e3
    }, 
    localtestnet: {
      provider: () => new HDWalletProvider(
        mnemonic,
        'http://localhost:7777/',
      ),
      network_id: 31,
      gasPrice: Math.floor(minimumGasPriceTestnet * TESTNET_GAS_MULT),
      networkCheckTimeout: 10e3,
    },
    mainnet: {
      provider: () => new HDWalletProvider(mnemonic, 'https://public-node.rsk.co', 0, 1, true, "m/44'/137'/0'/0/"),
      network_id: 30,
      gasPrice: Math.floor(minimumGasPriceMainnet * 1.02),
      networkCheckTimeout: 1e9
    },
    testnet: {
      provider: () => new HDWalletProvider(
        testnetSeedPhrase,
        'https://public-node.testnet.rsk.co/2.0.1/',
      ),
      network_id: 31,
      gasPrice: Math.floor(minimumGasPriceTestnet * 1.1),
      networkCheckTimeout: 1e9
    }
  },

  // Set default mocha options here, use special reporters etc.
  mocha: {  },

  // Configure your compilers
  compilers: {  
    solc: {
      version: "0.6.2"
    }
  }
}
