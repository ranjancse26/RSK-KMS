const RSKWhiteLabledKeyManagement = artifacts.require("RSKWhiteLabledKeyManagement");

module.exports = function(deployer) {  
	return deployer.deploy(RSKWhiteLabledKeyManagement);
};
