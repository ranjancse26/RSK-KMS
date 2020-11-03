const RSKWhiteLabeledKeyManagement = artifacts.require("RSKWhiteLabeledKeyManagement");

module.exports = function(deployer) {  
	return deployer.deploy(RSKWhiteLabeledKeyManagement);
};
