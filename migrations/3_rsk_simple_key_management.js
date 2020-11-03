const RSKSimpleKeyManagement = artifacts.require("RSKSimpleKeyManagement");

module.exports = function(deployer) {  
	return deployer.deploy(RSKSimpleKeyManagement, 'RSK');
};
