const RSKSimpleKeyManagement = artifacts.require("RSKSimpleKeyManagement");

// Update the below account based on the deployed network

const ganacheAccount = "0xaa7e2329009d8a906662d12339b8475524194a84";

contract('RSKSimpleKeyManagement', x => {
  const aesKeyValue = 'A3?KgBNFw7?(*3#]:?kq}78W{Zbp"UnJyjM9<D';

  beforeEach(async function () {
    InstanceRSKSimpleKeyManagement = await RSKSimpleKeyManagement.new('RSK');
  });

  it('1# - set the KMS key vaule', async function () {
    const response = await InstanceRSKSimpleKeyManagement.setItem('AESKey', aesKeyValue);
    assert.notEqual(response, "", 
      "KMS setItem response shouldn't be empty");
  });

  it('2# - get the KMS key value', async function () {
    var response = await InstanceRSKSimpleKeyManagement.setItem('AESKey', aesKeyValue);
    response = await InstanceRSKSimpleKeyManagement.getItem('AESKey');
    assert.equal(response, aesKeyValue, 
      "KMS AESKey value should be same");
  });

  it('3# - remove the KMS key vaule', async function () {
    var response = await InstanceRSKSimpleKeyManagement.setItem('AESKey', aesKeyValue);
    response = await InstanceRSKSimpleKeyManagement.removeItem('AESKey');
    assert.notEqual(response, "", 
      "KMS removeItem response shouldn't be empty");
  });
});