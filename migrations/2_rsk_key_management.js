const RSKKMSFactory = artifacts.require("RSKKMSFactory");
const RSKKeyManagement = artifacts.require("RSKKeyManagement");

module.exports = function(deployer) {  
	return deployer.deploy(RSKKMSFactory);
};
