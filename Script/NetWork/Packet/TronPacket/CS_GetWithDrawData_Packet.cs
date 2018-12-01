using Nethereum.JsonRpc.UnityClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System.Runtime.InteropServices;

public class CS_GetWithDrawData_Packet : PacketBase
{
    [DllImport("__Internal")]
    private static extern string GetRoundEarings(string address,string roundNum, string StrCallbackObjName, string strCallbackFunc);
    public BigInteger roundIndex
    {
        get;
        set;
    }

    public string playerAddress
    {
        get;
        set;
    }


    public Action<BigInteger> callback;
    public override void _Send(string StrCallbackObjName, string strCallbackFunc)
    {
        GetRoundEarings(playerAddress, roundIndex.ToString(), StrCallbackObjName, strCallbackFunc);
    }

    public override void _Execute(string strData)
    {
        GetAwardPriceDTO data = new GetAwardPriceDTO(strData);
        if (callback != null)
        {
            callback(data.count);
        }
    }


}
