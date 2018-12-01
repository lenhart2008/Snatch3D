using Nethereum.JsonRpc.UnityClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CS_GetCurRoundIndex_Packet : PacketBase
{

    [DllImport("__Internal")]
    private static extern string GetRoundIndex(string StrCallbackObjName, string strCallbackFunc);

    public Action<GetRoundIndexDataDTO> callback;
    public override void _Send(string StrCallbackObjName, string strCallbackFunc)
    {
        GetRoundIndex(StrCallbackObjName, strCallbackFunc);
    }

    public override void _Execute(string strData)
    {
        GetRoundIndexDataDTO data = new GetRoundIndexDataDTO(strData);
        if (callback != null)
        {
            callback(data);
        }
    }
}
