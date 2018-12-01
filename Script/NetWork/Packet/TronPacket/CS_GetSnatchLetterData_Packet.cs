using Nethereum.JsonRpc.UnityClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System.Runtime.InteropServices;

public class CS_GetSnatchLetterData_Packet : PacketBase
{

    [DllImport("__Internal")]
    private static extern string GetStealedInfo(string address, string StrCallbackObjName, string strCallbackFunc);
    public Action<GetSnatchLetterDataDTO> callback;
    public string PlayerAddress;

    public override void _Send(string StrCallbackObjName, string strCallbackFunc)
    {
        GetStealedInfo(PlayerAddress, StrCallbackObjName, strCallbackFunc);

    }

    public override void _Execute(string strData)
    {
        GetSnatchLetterDataDTO data = new GetSnatchLetterDataDTO(strData);
        if (callback != null)
        {
            callback(data);
        }
    }
}
