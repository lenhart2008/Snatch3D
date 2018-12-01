using Nethereum.JsonRpc.UnityClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System.Runtime.InteropServices;

public class CS_GetPlayerData_Packet : PacketBase
{
    [DllImport("__Internal")]
    private static extern string GetPlayerDataByAddress(string address, string StrCallbackObjName, string strCallbackFunc);
    public Action<GetPlayerDateDTO> callback;
    public string PlayerAddress;

    public override void _Send(string StrCallbackObjName, string strCallbackFunc)
    {
        GetPlayerDataByAddress(PlayerAddress, StrCallbackObjName, strCallbackFunc);

    }

    public override void _Execute(string strData)
    {
        GetPlayerDateDTO data = new GetPlayerDateDTO(strData);
        if (callback != null)
        {
            callback(data);
        }
    }

 

}
