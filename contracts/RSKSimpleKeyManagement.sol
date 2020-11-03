pragma solidity ^0.6.0;

/*
    The RSK Simple Key Management Smart Contract Service Deals with the
    Persistence and management of sensitive keys

    *** The simplest version of the RSK Key Management ***

    Inspired by the KMS - The Key management Service provided by Azure, AWS and 
    Other big vendors.

    Uses the IterableMapping for the key/value pair management.

    Reused and Enhanced Code - Iterable Mapping  
    https://solidity-by-example.org/0.6/app/iterable-mapping/
*/

contract RSKSimpleKeyManagement {
    
    string private tenant;
    address private owner; 

    struct Map {
        string[] keys;
        mapping(string => string) values;
        mapping(string => uint) indexOf;
        mapping(string => bool) inserted;
    }

    Map private map;

    event SetKeyEvent(string key, string description);
    event RemoveKeyEvent(string key, string description);

    constructor (string memory _tenant) public {
        tenant = _tenant;
        owner = msg.sender;
    }

    modifier isOwner {
       require(
            msg.sender == owner,
            "Only owner can call this function."
        );
      _;
    } 

    function setItem(string memory key, string memory value) public {
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
    
    function getItem(string memory key) public view returns(string memory) {
        return map.values[key];
    }

    function removeItem(string memory key) public {
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

    function kill() public isOwner {
        selfdestruct(msg.sender);
    }
}