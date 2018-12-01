using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.UnityClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PacketBase : MonoBehaviour{

    public HexBigInteger gas = new HexBigInteger(2100000);
    public HexBigInteger gasPrice = new HexBigInteger(21000000000);
    public HexBigInteger valueAmount = new HexBigInteger(0);
    string callbackFuncName = "Execute";
    
    public IEnumerator Send()
    {
        _Send(this.gameObject.name, callbackFuncName);
        yield return 0;
    }

    public void Execute(string strData)
    {
        _Execute(strData);
    }


    abstract public void _Send(string StrCallbackObjName, string strCallbackFunc);

    abstract public void _Execute(string strData);
}
