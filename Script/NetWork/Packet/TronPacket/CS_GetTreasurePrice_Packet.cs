using Nethereum.JsonRpc.UnityClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System.Runtime.InteropServices;

public class CS_GetTreasurePrice_Packet : PacketBase {
    [DllImport("__Internal")]
    private static extern string GetKeyPrice(string num, string StrCallbackObjName, string strCallbackFunc);
    public BigInteger TreasureNum
    {
        get;
        set;
    }


    public Action<BigInteger> callback;
    public override void _Send(string StrCallbackObjName, string strCallbackFunc)
    {
        GetKeyPrice(TreasureNum.ToString(), StrCallbackObjName, strCallbackFunc);
    }

    public override void _Execute(string strData)
    {
        GetTreasurePriceDTO data = new GetTreasurePriceDTO(strData);
        if (callback != null)
        {
            callback(data.treasurePrice);
        }
    }

    
}
