pragma solidity ^0.6.0;

import "./Ownable.sol";

/*
    The RSK WhiteLabled Key Management Smart Contract Service Deals with the
    Persistence and management of sensitive keys

    *** The white labeled version of the RSK Key Management ***

    Inspired by the KMS - The Key management Service provided by Azure, AWS and 
    Other big vendors.

    Uses the IterableMapping for the key/value pair management.

    Reused and Enhanced Code - Iterable Mapping  
    https://solidity-by-example.org/0.6/app/iterable-mapping/
*/

contract RSKWhiteLabeledKeyManagement is Ownable {
    
    string private tenant;

    struct WhiteLabeledProvider {
        string IpAddress;
        bool exists;
    }

    mapping(string => WhiteLabeledProvider) WhiteLabeledProviders;

    struct Map {
        string[] keys;
        mapping(string => string) values;
        mapping(string => uint) indexOf;
        mapping(string => bool) inserted;
    }

    Map private map;

    event SetKeyEvent(string key, string description);
    event RemoveKeyEvent(string key, string description);
 
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
    
    function getItem(string memory key, string memory IpAddress) 
        public view returns(string memory) 
    {
         if(WhiteLabeledProviders[IpAddress].exists)
            return map.values[key];
         
         return "";
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

    function addWhiteLabel(string memory IpAddress) public onlyOwner() {
        if(WhiteLabeledProviders[IpAddress].exists == false)
            WhiteLabeledProviders[IpAddress] = WhiteLabeledProvider(IpAddress, true);
    }

    function removeWhiteLabel(string memory IpAddress) public onlyOwner() {
        if(WhiteLabeledProviders[IpAddress].exists)
            delete WhiteLabeledProviders[IpAddress];
    }

    function getWhiteLabel(string memory IpAddress) public view onlyOwner() returns(string memory){
        if(WhiteLabeledProviders[IpAddress].exists == true)
        {
            return WhiteLabeledProviders[IpAddress].IpAddress;
        }
        return "";
    }

    function kill() public onlyOwner() {
        selfdestruct(msg.sender);
    }
}