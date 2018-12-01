using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.Encoders;
using Nethereum.Signer;
using Nethereum.Hex.HexConvertors.Extensions;
using UnityEngine;
using Nethereum.JsonRpc.UnityClient;

public class ContractService
{
    // We define a new contract (Netherum.Contracts)
    private Contract contract;

    public ContractService()
    {
        this.contract = new Contract(null, ContractParams.ABI, ContractParams.contractAddress);
    }

    public Function GetContractFunction(string strFuncName)
    {
        return contract.GetFunction(strFuncName);
    }

    public TransactionInput CreateTransactionContractInput(
        string funcName,
        string addressFrom,
        string privateKey,
        BigInteger pingValue,
        HexBigInteger gas = null,
        HexBigInteger gasPrice = null,
        HexBigInteger valueAmount = null)
    {

        var function = GetContractFunction(funcName);
         return function.CreateTransactionInput(addressFrom, gas, gasPrice, valueAmount, pingValue);
    }

    public CallInput CreateCallContractInput(string strFuncName)
    {
        var function = GetContractFunction(strFuncName);
        return function.CreateCallInput();
    }

    
}