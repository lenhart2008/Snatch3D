using Nethereum.JsonRpc.UnityClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CS_GetFinalAwardData_Packet : PacketBase {

    [DllImport("__Internal")]
    private static extern string GetFinalAwardData(string StrCallbackObjName, string strCallbackFunc);

    public Action<GetFinalAwardDataDTO> callback;
    public override void _Send(string StrCallbackObjName, string strCallbackFunc)
    {
        GetFinalAwardData(StrCallbackObjName, strCallbackFunc);
    }

    public override void _Execute(string strData)
    {
        GetFinalAwardDataDTO data = new GetFinalAwardDataDTO(strData);
        if (callback != null)
        {
            callback(data);
        }
    }
}
