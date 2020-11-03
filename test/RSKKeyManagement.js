const RSKKMSFactory = artifacts.require("RSKKMSFactory");
const RSKKeyManagement = artifacts.require("RSKKeyManagement");
const RSKKeyManagementJson = require('../build/contracts/RSKKeyManagement.json');

// Update the below account based on the deployed network

const ganacheAccount = "0xaa7e2329009d8a906662d12339b8475524194a84";

contract('RSKKMSFactory', x => {
  const aesKeyValue = 'A3?KgBNFw7?(*3#]:?kq}78W{Zbp"UnJyjM9<D';
  
  beforeEach(async function () {
    InstanceRSKKMSFactory = await RSKKMSFactory.new();
  });

  it('1# - should make a call to the RSKKMSFactory createKMSFor', async function () {
    const response = await InstanceRSKKMSFactory.createKMSFor('RSK');
    assert.notEqual(response, "", 
      "KMS createKMSFor response shouldn't be empty");
  });

  it('2# - should make a call to the RSKKMSFactory createKMSFor and get an instance', async function () {
    let response = await InstanceRSKKMSFactory.createKMSFor('RSK');
    response = await InstanceRSKKMSFactory.getAll();

    assert.notEqual(response, "", 
      "RSKKMSFactory getAll response shouldn't be empty");
  });

  it('3# - should make a call to the RSKKMSFactory instance setItem', async function () {
    let response = await InstanceRSKKMSFactory.createKMSFor('RSK');
    var rskManagementInstances = await InstanceRSKKMSFactory.getAll();

    // console.log('Contract Address: '+ rskManagementInstances[0]);

    const contract = new web3.eth.Contract(RSKKeyManagementJson.abi,
      rskManagementInstances[0]);

    // Set an item
    response = await contract.methods.setItem('AESKey', aesKeyValue)
    .send({ 
         from: ganacheAccount, 
         gas: 6721975, 
         gasPrice: '30000000' 
    });
  });

  it('4# - should make a call to the RSKKMSFactory instance getItem', async function () {
    let response = await InstanceRSKKMSFactory.createKMSFor('RSK');
    var rskManagementInstances = await InstanceRSKKMSFactory.getAll();

    // console.log('Contract Address: '+ rskManagementInstances[0]);

    const contract = new web3.eth.Contract(RSKKeyManagementJson.abi,
      rskManagementInstances[0]);

    // Set an item
    response = await contract.methods.setItem('AESKey', aesKeyValue)
    .send({ 
         from: ganacheAccount, 
         gas: 6721975, 
         gasPrice: '30000000' 
    });

    // Get an item
    await contract.methods.getItem('AESKey')
      .call().then(function(res){
         // console.log('AES Key value: '+ res);
         assert.equal(res, aesKeyValue, 
          "KMS AESKey value should be same");
      });
  });

  it('5# - should make a call to the RSKKMSFactory instance removeItem', async function () {
    let response = await InstanceRSKKMSFactory.createKMSFor('RSK');
    var rskManagementInstances = await InstanceRSKKMSFactory.getAll();

    // console.log('Contract Address: '+ rskManagementInstances[0]);

    const contract = new web3.eth.Contract(RSKKeyManagementJson.abi,
      rskManagementInstances[0]);

    // Set an item
    response = await contract.methods.setItem('AESKey', aesKeyValue)
    .send({ 
         from: ganacheAccount, 
         gas: 6721975, 
         gasPrice: '30000000' 
    });

     // Get an item
    await contract.methods.getItem('AESKey')
      .call().then(function(res){
         // console.log('AES Key value: '+ res);
         assert.equal(res, aesKeyValue, 
          "KMS AESKey value should be same");
      });

    // Remove an item
    await contract.methods.removeItem('AESKey')
      .send({ 
         from: ganacheAccount, 
         gas: 6721975, 
         gasPrice: '30000000' 
    });
      
  });
});