const RSKWhiteLabeledKeyManagement = artifacts.require("RSKWhiteLabeledKeyManagement");

// Update the below account based on the deployed network

const ganacheAccount = "0xaa7e2329009d8a906662d12339b8475524194a84";

contract('RSKWhiteLabeledKeyManagement', x => {
  const aesKeyValue = 'A3?KgBNFw7?(*3#]:?kq}78W{Zbp"UnJyjM9<D';
  const ipAddress = '122.167.238.115';
  
  beforeEach(async function () {
    InstanceRSKWhiteLabeledKeyManagement = await RSKWhiteLabeledKeyManagement.new();
  });

  it('1# - set the KMS key vaule', async function () {
    const response = await InstanceRSKWhiteLabeledKeyManagement.setItem('AESKey', aesKeyValue);
    assert.notEqual(response, "", 
      "WhiteLabled KMS setItem response shouldn't be empty");
  });

  it('2# - get the KMS key value', async function () {
    var response = await InstanceRSKWhiteLabeledKeyManagement.setItem('AESKey', aesKeyValue);
    response = await InstanceRSKWhiteLabeledKeyManagement.addWhiteLabel(ipAddress);
    response = await InstanceRSKWhiteLabeledKeyManagement.getItem('AESKey', ipAddress);
    assert.equal(response, aesKeyValue, 
      "WhiteLabled KMS AESKey value should be same");
  });

  it('3# - remove the KMS key vaule', async function () {
    var response = await InstanceRSKWhiteLabeledKeyManagement.setItem('AESKey', aesKeyValue);
    response = await InstanceRSKWhiteLabeledKeyManagement.removeItem('AESKey');
    assert.notEqual(response, "", 
      "WhiteLabled KMS removeItem response shouldn't be empty");
  });
});