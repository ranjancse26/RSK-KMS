pragma solidity ^0.6.0;

/*
    The RSKKMSFactory follows the factory pattern for creating the 
    Key Management Services by the specified Tenant.

    A Tenant based key management isolates the sensitive keys by "Tenant"
*/
contract RSKKMSFactory 
{
    address[] public kmsAddresses;
    event KMSCreated(RSKKeyManagement kms);

    function createKMSFor(string memory tenant) public returns (address) {
        RSKKeyManagement kms = new RSKKeyManagement();
        kms.init(tenant);
        kmsAddresses.push(address(kms));
        return address(kms);
    }

    function getKMSContractCount() public view 
        returns(uint count)
    {
        return kmsAddresses.length;
    }

    function getAll() public view returns (address[] memory) {
        return kmsAddresses;
    }
}

abstract contract AbstractRSKKeyManagement
{
    function setItem(string memory key, string memory value) public virtual;
    function getItem(string memory key) public virtual view returns(string memory);
    function removeItem(string memory key) public virtual;

    event SetKeyEvent(string key, string description);
    event RemoveKeyEvent(string key, string description);
}

/*
    The RSK Key Management Smart Contract Service Deals with the
    Persistence and management of sensitive keys

    Inspired by the KMS - The Key management Service provided by Azure, AWS and 
    Other big vendors.

    Uses the IterableMapping for the key/value pair management.

    Reused and Enhanced Code - Iterable Mapping  
    https://solidity-by-example.org/0.6/app/iterable-mapping/
*/

contract RSKKeyManagement is AbstractRSKKeyManagement  {
    
    address private owner; 
    string private tenant;

    struct Map {
        string[] keys;
        mapping(string => string) values;
        mapping(string => uint) indexOf;
        mapping(string => bool) inserted;
    }

    Map private map;

    function init(string memory _tenant) public {
        owner = msg.sender;
        tenant = _tenant;
    }

    modifier isOwner {
       require(
            msg.sender == owner,
            "Only owner can call this function."
        );
      _;
    } 

    function setItem(string memory key, string memory value) public override {
        if (map.inserted[key]) {
            map.values[key] = value;
        } else {
            map.inserted[key] = true;
            map.values[key] = value;
            map.indexOf[key] = map.keys.length;
            map.keys.push(key);
        }
        emit SetKeyEvent(key, 'Within the setItem Operation');
    }
    
    function getItem(string memory key) public override view returns(string memory) {
        return map.values[key];
    }

    function removeItem(string memory key) public override {
        if (!map.inserted[key]) {
            return;
        }

        delete map.inserted[key];
        delete map.values[key];

        uint index = map.indexOf[key];
        uint lastIndex = map.keys.length - 1;
        string memory lastKey = map.keys[lastIndex];

        map.indexOf[lastKey] = index;
        delete map.indexOf[key];

        map.keys[index] = lastKey;
        map.keys.pop();

        emit RemoveKeyEvent(key, 'Within the removeItem Operation');
    }

    function kill() public isOwner{
        selfdestruct(msg.sender);
    }
}